﻿using BluePrints.BluePrintsEntitiesDataModel;
using BluePrints.Common.DataModel;
using BluePrints.Common.ViewModel;
using BluePrints.Common.ViewModel.Reporting;
using BluePrints.Data;
using BluePrints.Data.Attributes;
using DevExpress.Mvvm.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluePrints.Common.Projections
{
    public class PROGRESS_ITEMProjection : ReportableObject
    {
        public PROGRESS_ITEMProjection()
        {

        }

        public Guid GUID { get; set; }
    }

    public static class PROGRESS_ITEMProjectionQueries
    {
        public static IQueryable<PROGRESS_ITEMProjection> JoinRATESAndPROGRESS_ITEMSOnBASELINE_ITEMS(IQueryable<BASELINE_ITEM> BASELINE_ITEMS, Func<PROGRESS> getPROGRESSFunc, Func<BASELINE> getBASELINEFunc, Func<IQueryable<PROGRESS_ITEM>> getPROGRESS_ITEMSFunc, Func<IQueryable<RATE>> getRATESFunc, bool isBASELINEQueryProcessed = false)
        {
            BASELINE BASELINE = getBASELINEFunc();
            PROGRESS PROGRESS = getPROGRESSFunc();

            IQueryable<RATE> RATES = getRATESFunc();

            IQueryable<PROGRESS_ITEM> LoadPROGRESS_ITEMS;
            if(PROGRESS == null)
                LoadPROGRESS_ITEMS = getPROGRESS_ITEMSFunc().Where(x => x.GUID_PROGRESS == Guid.Empty).ToArray().AsQueryable();
            else
                LoadPROGRESS_ITEMS = getPROGRESS_ITEMSFunc().ToArray().AsQueryable();

            IQueryable<BASELINE_ITEMProjection> BASELINE_ITEMJoinRATES;
            if (PROGRESS == null)
                BASELINE_ITEMJoinRATES = BASELINE_ITEMProjectionQueries.JoinRATESOnBASELINE_ITEMS(BASELINE_ITEMS.Where(x => x.GUID == Guid.Empty), getBASELINEFunc, getRATESFunc, true);
            else
                BASELINE_ITEMJoinRATES = BASELINE_ITEMProjectionQueries.JoinRATESOnBASELINE_ITEMS(BASELINE_ITEMS, getBASELINEFunc, getRATESFunc, isBASELINEQueryProcessed);

            DateTime reportingDate = PROGRESS == null ? new DateTime() : PROGRESS.DATA_DATE;
            return BASELINE_ITEMJoinRATES.ToArray().AsQueryable().Select(x => new PROGRESS_ITEMProjection() { GUID = x.GUID, BASELINE_ITEMJoinRATE = x, ReportingDataDate = reportingDate, PROGRESS_ITEMS = LoadPROGRESS_ITEMS.Where(y => y.GUID_ORIBASEITEM == x.BASELINE_ITEM.GUID_ORIGINAL).ToArray().AsEnumerable() });
        }
    }
}
