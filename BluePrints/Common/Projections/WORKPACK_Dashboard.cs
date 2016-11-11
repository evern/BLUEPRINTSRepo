﻿using BluePrints.BluePrintsEntitiesDataModel;
using BluePrints.Common.ViewModel.Reporting;
using BluePrints.Data;
using BluePrints.P6EntitiesDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluePrints.Common.Projections
{
    public class WORKPACK_Dashboard : PROJECTSummary
    {
        public Guid GUID { get; set; }
        public WORKPACK WORKPACK { get; set; }

        public PROJECTSummaryBuilder SummaryBuilder { get; private set; }
        public UnpackPROJECTSummary SummaryUnpacker { get; private set; }
        public void InitializeUnpacker(PROJECT_Dashboard projectDashboard)
        {
            SummaryUnpacker = new UnpackPROJECTSummary(this, projectDashboard);
        }

        #region WORKPACK Mapping
        public bool IsGetModifiedWORKPACK_ASSIGNMENTS { get; set; }
        public void InitializeBuilder(IEnumerable<ReportableObject> reportableObjects, PROGRESS livePROGRESS, BASELINE liveBASELINE, IBluePrintsEntitiesUnitOfWork bluePrintsUnitOfWork, IP6EntitiesUnitOfWork p6UnitOfWork)
        {
            this.ReportableObjects = reportableObjects;
            this.LiveBASELINE = liveBASELINE;
            this.LivePROGRESS = livePROGRESS;

            SummaryBuilder = new PROJECTSummaryBuilder(this, bluePrintsUnitOfWork, p6UnitOfWork);
        }

        public ICollection<WORKPACK_ASSIGNMENT> ObservableWORKPACK_ASSIGNMENTS
        {
            get
            {
                return WORKPACK.WORKPACK_ASSIGNMENTS.Where(x => x.ISMODIFIEDBASELINE == IsGetModifiedWORKPACK_ASSIGNMENTS).ToList();
            }
        }

        public virtual ICollection<BASELINE_ITEM> BASELINE_ITEMS { get; set; }

        public decimal ASSIGNED_UNITS
        {
            get { return ObservableWORKPACK_ASSIGNMENTS.Sum(x => (x.HIGH_VALUE - x.LOW_VALUE) + 1); }
        } 
        #endregion
    }

    public static class WORKPACK_DashboardQueries
    {
        public static IQueryable<WORKPACK_Dashboard> SummarizeWORKPACKDashboard(IQueryable<WORKPACK> WORKPACKS, PROJECT_Dashboard projectDashboard)
        {
            IEnumerable<WORKPACK_Dashboard> projectWORKPACKDashboards = WORKPACKS.Where(x => x.GUID_PROJECT == projectDashboard.GUID).Select(x => new WORKPACK_Dashboard() { GUID = x.GUID, WORKPACK = x });
            List<WORKPACK_Dashboard> newWORKPACKDashboards = projectWORKPACKDashboards.ToList();
            SummaryRollUp rollUpReportableObjects = new SummaryRollUp();
            foreach (WORKPACK_Dashboard newWORKPACKDashboard in newWORKPACKDashboards)
            {
                newWORKPACKDashboard.InitializeUnpacker(projectDashboard);
                rollUpReportableObjects.Manufacture(newWORKPACKDashboard.SummaryUnpacker);
            }

            return newWORKPACKDashboards.AsQueryable();
        }

        public static IQueryable<WORKPACK_Dashboard> MappingWORKPACKDashboard(IQueryable<WORKPACK> WORKPACKS, Func<PROGRESS> getPROGRESSFunc, Func<BASELINE> getBASELINEFunc, 
            Func<IQueryable<BASELINE_ITEM>> getBASELINE_ITEMFunc, 
            Func<IQueryable<PROGRESS_ITEM>> getPROGRESS_ITEMSFunc, 
            Func<IQueryable<RATE>> getRATESFunc, 
            bool getModifiedWORKPACK_ASSIGNMENT)
        {
            List<WORKPACK_Dashboard> returnWORKPACK_Dashboard = new List<WORKPACK_Dashboard>();
            IEnumerable<WORKPACK> localWORKPACKS = WORKPACKS.ToArray().AsEnumerable();

            PROJECT_Dashboard projectDashboard = PROJECT_DashboardQueries.SummarizeSinglePROJECTDashboard(getBASELINEFunc().PROJECT, getPROGRESSFunc, getPROGRESS_ITEMSFunc, getBASELINE_ITEMFunc, getBASELINEFunc, getRATESFunc);
            return SummarizeWORKPACKDashboard(WORKPACKS, projectDashboard);
        }
    }
}
