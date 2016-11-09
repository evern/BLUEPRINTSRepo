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
    public class WORKPACK_Dashboard : PROJECTSummary
    {
        public Guid GUID { get; set; }
        public WORKPACK WORKPACK { get; set; }
        public UnpackPROJECTSummary SummaryUnpacker { get; private set; }
        public void InitializeUnpacker(PROJECT_Dashboard projectDashboard)
        {
            SummaryUnpacker = new UnpackPROJECTSummary(this, projectDashboard);
        }
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
    }
}
