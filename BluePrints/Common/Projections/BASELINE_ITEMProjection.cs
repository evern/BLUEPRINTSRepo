﻿using BluePrints.BluePrintsEntitiesDataModel;
using BluePrints.Common.DataModel;
using BluePrints.Common.ViewModel;
using BluePrints.Data;
using DevExpress.Mvvm.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluePrints.Common.Projections
{
    public class BASELINE_ITEMProjection
    {
        public BASELINE_ITEMProjection()
        {
            BASELINE_ITEM = new BASELINE_ITEM();
        }

        public Guid GUID { get; set; }
        public BASELINE_ITEM BASELINE_ITEM { get; set; }
        public RATE RATE { get; set; }
        public decimal ITEMRATE
        {
            get
            {
                if (RATE == null || RATE.RATE1 == null)
                    return 0;

                return (decimal)RATE.RATE1;
            }
        }

        public decimal ESTIMATED_COSTS
        {
            get
            {
                if (BASELINE_ITEM == null)
                    return 0;

                if (RATE == null || RATE.RATE1 == null)
                    return 0;

                return BASELINE_ITEM.ESTIMATED_HOURS * (decimal)RATE.RATE1;
            }
        }

        public decimal TOTAL_COSTS { get { return BASELINE_ITEM.TOTAL_HOURS * ITEMRATE; } }
    }

    public static class BASELINE_ITEMProjectionQueries
    {
        public static IQueryable<BASELINE_ITEMProjection> JoinRATESOnBASELINE_ITEMS(IQueryable<BASELINE_ITEM> BASELINE_ITEMS, Func<BASELINE> getBASELINEFunc, Func<IQueryable<RATE>> getRATES_ByProjectFunc = null, bool isBASELINEQueryProcessed = false)
        {
            BASELINE BASELINE = getBASELINEFunc();
            IQueryable<BASELINE_ITEM> contextBASELINE_ITEMS;
            if (BASELINE == null)
                contextBASELINE_ITEMS = BASELINE_ITEMS.Where(x => x.GUID == Guid.Empty);
            else
            {
                if(isBASELINEQueryProcessed)
                    contextBASELINE_ITEMS = BASELINE_ITEMS;
                else
                    contextBASELINE_ITEMS = BASELINE_ITEMS.Where(x => x.GUID_BASELINE == BASELINE.GUID);
            }


            IQueryable<RATE> RATES = getRATES_ByProjectFunc();
            return contextBASELINE_ITEMS.ToArray().AsQueryable().Select(x => new BASELINE_ITEMProjection() { GUID = x.GUID, BASELINE_ITEM = x, RATE = RATES.FirstOrDefault(y => y.GUID_DEPARTMENT == x.GUID_DEPARTMENT && y.GUID_DISCIPLINE == x.GUID_DISCIPLINE) });
        }
    }
}
