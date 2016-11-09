using BluePrints.BluePrintsEntitiesDataModel;
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
    public class PROJECT_Dashboard : PROJECTSummary
    {
        public PROJECT_Dashboard()
        {

        }

        public Guid GUID { get; set; }
        public PROJECT PROJECT { get; set; }
        public PROJECTSummaryBuilder SummaryBuilder { get; private set; }
        public void InitializeBuilder(IEnumerable<ReportableObject> reportableObjects, PROGRESS livePROGRESS, BASELINE liveBASELINE, IBluePrintsEntitiesUnitOfWork bluePrintsUnitOfWork, IP6EntitiesUnitOfWork p6UnitOfWork)
        {
            this.ReportableObjects = reportableObjects;
            this.LiveBASELINE = liveBASELINE;
            this.LivePROGRESS = livePROGRESS;

            SummaryBuilder = new PROJECTSummaryBuilder(this, bluePrintsUnitOfWork, p6UnitOfWork);
        }
    }

    public static class PROJECT_DashboardQueries
    {
        public static IQueryable<PROJECT_Dashboard> SummarizePROJECTDashboard(IQueryable<PROJECT> PROJECTS, Func<IQueryable<PROGRESS>> getLivePROGRESSESFunc, Func<IQueryable<BASELINE>> getLiveBASELINESFunc, Func<IQueryable<RATE>> getRATESFunc, Func<IQueryable<VARIATION>> getApprovedVARIATIONFunc)
        {
            IEnumerable<BASELINE> LiveBASELINES = getLiveBASELINESFunc().ToArray().AsEnumerable();
            IEnumerable<PROGRESS> LivePROGRESSES = getLivePROGRESSESFunc().ToArray().AsEnumerable();
            IEnumerable<VARIATION> ApprovedVARIATIONS = getApprovedVARIATIONFunc().ToArray().AsEnumerable();
            IEnumerable<RATE> AllRATES = getRATESFunc();
            IEnumerable<PROJECT> localPROJECTS = PROJECTS.Where(x => x.STATUS == ProjectStatus.Active).ToArray().AsEnumerable(); //process only active PROJECTS
            List<PROJECT_Dashboard> returnPROJECT_Dashboard = new List<PROJECT_Dashboard>();

            IBluePrintsEntitiesUnitOfWork bluePrintsUnitOfWork = BluePrintsEntitiesUnitOfWorkSource.GetUnitOfWorkFactory().CreateUnitOfWork();
            IP6EntitiesUnitOfWork p6UnitOfWork = P6EntitiesUnitOfWorkSource.GetUnitOfWorkFactory().CreateUnitOfWork();

            foreach(PROJECT localPROJECT in localPROJECTS)
            {
                BASELINE currentPROJECTLiveBASELINE = LiveBASELINES.FirstOrDefault(x => x.GUID_PROJECT == localPROJECT.GUID);
                if (currentPROJECTLiveBASELINE == null)
                    continue;

                PROGRESS currentPROJECTLivePROGRESS = LivePROGRESSES.FirstOrDefault(x => x.GUID_PROJECT == localPROJECT.GUID);
                if (currentPROJECTLivePROGRESS == null)
                    continue;

                IQueryable<PROGRESS_ITEM> LivePROGRESS_ITEMS = currentPROJECTLivePROGRESS.PROGRESS_ITEMS.AsQueryable();
                IQueryable<BASELINE_ITEM> LiveBASELINE_ITEMS = currentPROJECTLiveBASELINE.BASELINE_ITEMS.AsQueryable();
                IQueryable<RATE> RATESByProject = AllRATES.Where(x => x.GUID_PROJECT == localPROJECT.GUID).AsQueryable();
                IEnumerable<VARIATION> ApprovedVARIATIONSByProject = ApprovedVARIATIONS.Where(x => x.GUID_PROJECT == localPROJECT.GUID).AsEnumerable();

                IEnumerable<ReportableObject> PROJECTInfos = BASELINE_ITEMSJoinRATESJoinPROGRESS_ITEMSQueries.JoinRATESAndPROGRESS_ITEMSOnBASELINE_ITEMS(
                    LiveBASELINE_ITEMS, () => currentPROJECTLivePROGRESS, () => currentPROJECTLiveBASELINE, () => LivePROGRESS_ITEMS, () => RATESByProject).ToArray().AsEnumerable();

                PROJECT_Dashboard currentPROJECT_Dashboard = new PROJECT_Dashboard() { GUID = localPROJECT.GUID, PROJECT = localPROJECT, VARIATIONS = ApprovedVARIATIONSByProject };
                currentPROJECT_Dashboard.InitializeBuilder(PROJECTInfos, currentPROJECTLivePROGRESS, currentPROJECTLiveBASELINE, bluePrintsUnitOfWork, p6UnitOfWork);
                returnPROJECT_Dashboard.Add(currentPROJECT_Dashboard);
            }

            foreach (PROJECT_Dashboard project in returnPROJECT_Dashboard)
            {
                SummaryManufacturer summaryManufacturer = new SummaryManufacturer();
                summaryManufacturer.Manufacture(project.SummaryBuilder);
            }

            return returnPROJECT_Dashboard.AsQueryable();
        }
    }
}
