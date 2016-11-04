using BluePrints.BluePrintsEntitiesDataModel;
using BluePrints.Common;
using BluePrints.Common.DataModel;
using BluePrints.Common.Projections;
using BluePrints.Common.ViewModel;
using BluePrints.Common.ViewModel.Reporting;
using BluePrints.P6Data;
using BluePrints.P6EntitiesDataModel;
using DevExpress.Mvvm.POCO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluePrints.Data.Helpers
{
    public static class QueriesHelper
    {
        //public static IQueryable<PROGRESS_ITEMInfo> MorphPROGRESS_ITEMInfo(IQueryable<PROGRESS_ITEM> PROGRESS_ITEMS, Func<BASELINE> getBASELINE_byLiveIncludeBASELINE_ITEMSFunc, Func<IQueryable<RATE>> getRATES_ByProjectFunc, DateTime? PROGRESS_DataDate)
        //{
        //    BASELINE BASELINE_byLive = getBASELINE_byLiveIncludeBASELINE_ITEMSFunc();
        //    IQueryable<BASELINE_ITEMInfo> BASELINE_ITEMSInfos_ByLive;
        //    var finalizedPROGRESS_ITEMS = PROGRESS_ITEMS.ToArray().AsQueryable();

        //    if (BASELINE_byLive == null)
        //        BASELINE_ITEMSInfos_ByLive = new List<BASELINE_ITEMInfo>().AsQueryable();
        //    else
        //        BASELINE_ITEMSInfos_ByLive = MorphBASELINE_ITEMInfo(BASELINE_byLive.BASELINE_ITEMS.AsQueryable(), getRATES_ByProjectFunc).ToArray().AsQueryable();

        //    return BASELINE_ITEMSInfos_ByLive.Select(x => new PROGRESS_ITEMInfo(x, finalizedPROGRESS_ITEMS, PROGRESS_DataDate));
        //}

        //public static IQueryable<PROGRESS_ITEMInfo> MorphPROGRESS_ITEMInfoByBaseline(IQueryable<PROGRESS_ITEM> PROGRESS_ITEMS, Func<IQueryable<BASELINE_ITEM>> getBASELINE_ITEMFunc, Func<IQueryable<RATE>> getRATES_ByProjectFunc, DateTime? PROGRESS_DataDate, Guid? reloadGUID)
        //{
        //    var BASELINE_ITEMS = getBASELINE_ITEMFunc();
        //    var finalizedPROGRESS_ITEMS = PROGRESS_ITEMS.ToArray().AsQueryable();
        //    IQueryable<BASELINE_ITEMInfo> BASELINE_ITEMSInfos_ByLive;
        //    if (reloadGUID != null)
        //        BASELINE_ITEMSInfos_ByLive = MorphBASELINE_ITEMInfo(BASELINE_ITEMS, getRATES_ByProjectFunc).Where(x => x.GUID == reloadGUID).AsQueryable();
        //    else
        //        BASELINE_ITEMSInfos_ByLive = MorphBASELINE_ITEMInfo(BASELINE_ITEMS, getRATES_ByProjectFunc).ToArray().AsQueryable();

        //    return BASELINE_ITEMSInfos_ByLive.Select(x => new PROGRESS_ITEMInfo(x, finalizedPROGRESS_ITEMS, PROGRESS_DataDate));
        //}

        //public static void UpdatePROGRESS_ITEMInfoRATE(IEnumerable<PROGRESS_ITEMInfo> entities, IEnumerable<RATE> projectLookUpRATES)
        //{
        //    foreach (var item in entities)
        //    {
        //        item.BASELINE_ITEM.SetRate(projectLookUpRATES.FirstOrDefault(x => x.GUID_DISCIPLINE == item.BASELINE_ITEM.GUID_DISCIPLINE && x.GUID_DEPARTMENT == item.BASELINE_ITEM.GUID_DEPARTMENT));
        //    }
        //}

        public static IQueryable<VARIATION_ITEMInfo> MorphVARIATION_ITEMInfo(IQueryable<VARIATION_ITEM> VARIATION_ITEMS, Func<BASELINE> getBASELINE_byLiveIncludeBASELINE_ITEMSFunc, Func<IQueryable<BASELINE_ITEM>> getBASELINE_ITEM_byVariationFunc, Func<IQueryable<RATE>> getRATES_ByProjectFunc, bool isLocked)
        {
            BASELINE BASELINE_byLive = getBASELINE_byLiveIncludeBASELINE_ITEMSFunc();
            IQueryable<BASELINE_ITEMInfo> fromBaseline_BASELINE_ITEMSInfo;
            if (BASELINE_byLive == null)
                fromBaseline_BASELINE_ITEMSInfo = new List<BASELINE_ITEMInfo>().AsQueryable();
            else
                fromBaseline_BASELINE_ITEMSInfo = MorphBASELINE_ITEMInfo(BASELINE_byLive.BASELINE_ITEMS.AsQueryable(), getRATES_ByProjectFunc).ToArray().AsQueryable();

            IQueryable<BASELINE_ITEMInfo> fromVariation_BASELINE_ITEMSInfo = MorphBASELINE_ITEMInfo(getBASELINE_ITEM_byVariationFunc(), getRATES_ByProjectFunc).ToArray().AsQueryable();
            return fromBaseline_BASELINE_ITEMSInfo.Concat(fromVariation_BASELINE_ITEMSInfo).Select(x => new VARIATION_ITEMInfo(ref x, VARIATION_ITEMS, isLocked));
        }

        public static IQueryable<PROJECT_DashboardInfo> MorphPROJECT_DashboardInfo(IQueryable<PROJECT> PROJECTS, Func<IQueryable<BASELINE>> getBASELINES_byLiveIncludeBASELINE_ITEMSFunc, Func<IQueryable<PROGRESS>> getPROGRESSES_byLiveFunc, Func<IQueryable<RATE>> getRATESFunc, IBluePrintsEntitiesUnitOfWork BluePrintsUnitOfWork, IP6EntitiesUnitOfWork P6UnitOfWork)
        {
            var readPROJECTS = PROJECTS.Where(x => x.STATUS == ProjectStatus.Active).ToArray().AsQueryable(); //need to read here so that linq to sql won't append to query
            return readPROJECTS.Select(x => new PROJECT_DashboardInfo(x, getBASELINES_byLiveIncludeBASELINE_ITEMSFunc(), getPROGRESSES_byLiveFunc(), getRATESFunc(), BluePrintsUnitOfWork, P6UnitOfWork));
        }

        public static IQueryable<BASELINE_ITEMInfo> MorphBASELINE_ITEMInfo(IQueryable<BASELINE_ITEM> BASELINE_ITEMS, Func<IQueryable<RATE>> getRATES_ByProjectFunc)
        {
            var finalizedBASELINE_ITEMS = BASELINE_ITEMS.ToArray().AsQueryable();
            return finalizedBASELINE_ITEMS.Select(x => new BASELINE_ITEMInfo(x, getRATES_ByProjectFunc().FirstOrDefault(y=> y.GUID_DEPARTMENT == x.GUID_DEPARTMENT && y.GUID_DISCIPLINE == x.GUID_DISCIPLINE)));
        }

        public static IQueryable<BASELINE_ITEMInfo> MorphBASELINE_ITEMInfo(IQueryable<BASELINE_ITEM> BASELINE_ITEMS)
        {
            var finalizedBASELINE_ITEMS = BASELINE_ITEMS.ToArray().AsQueryable();
            return finalizedBASELINE_ITEMS.Select(x => new BASELINE_ITEMInfo(x));
        }

        public static void UpdateBASELINE_ITEMInfoRATE(IEnumerable<BASELINE_ITEMInfo> entities, IEnumerable<RATE> projectLookUpRATES)
        {
            foreach(var item in entities)
            {
                item.SetRate(projectLookUpRATES.FirstOrDefault(x => x.GUID_DISCIPLINE == item.GUID_DISCIPLINE && x.GUID_DEPARTMENT == item.GUID_DEPARTMENT));
            }
        }

        public static IQueryable<WORKPACK_DashboardInfo> MorphWORKPACK_DashboardInfo(IQueryable<WORKPACK> WORKPACKS, DefaultSummaryCalculation SummaryObject)
        {
            var finalizedWORKPACKS = WORKPACKS.Where(x => x.GUID_PROJECT == SummaryObject.LivePROGRESS.GUID_PROJECT).ToArray().AsQueryable();
            return finalizedWORKPACKS.Select(x => new WORKPACK_DashboardInfo(x, SummaryObject));
        }

        //public static IQueryable<WORKPACK_DashboardInfo> MorphWORKPACK_DashboardInfo(IQueryable<WORKPACK> WORKPACKS, Func<PROGRESS> getPROGRESS_byLiveIncludePROGRESS_ITEMSFunc, Func<BASELINE> getBASELINE_byLiveIncludeBASELINE_ITEMSFunc, Func<IQueryable<RATE>> getRATES_ByProjectFunc, Guid projectGUID, bool IsGetModifiedWORKPACK_ASSIGNMENTS)
        //{

        //    PROGRESS PROGRESS_byLive = getPROGRESS_byLiveIncludePROGRESS_ITEMSFunc();
        //    IQueryable<PROGRESS_ITEM> PROGRESS_ITEM_byLivePROGRESS;

        //    if (PROGRESS_byLive == null)
        //        PROGRESS_ITEM_byLivePROGRESS = new List<PROGRESS_ITEM>().AsQueryable();
        //    else
        //        PROGRESS_ITEM_byLivePROGRESS = PROGRESS_byLive.PROGRESS_ITEMS.AsQueryable();

        //    IQueryable<PROGRESS_ITEMInfo> PROGRESS_ITEMInfo_byLiveBASELINE = MorphPROGRESS_ITEMInfo(PROGRESS_ITEM_byLivePROGRESS, getBASELINE_byLiveIncludeBASELINE_ITEMSFunc, getRATES_ByProjectFunc, PROGRESS_byLive == null ? (DateTime?)null : PROGRESS_byLive.DATA_DATE);
        //    var finalizedWORKPACKS = WORKPACKS.Where(x => x.GUID_PROJECT == projectGUID && x.BASELINE_ITEMS.Count > 0).ToArray().AsQueryable();
        //    return finalizedWORKPACKS.Select(x => new WORKPACK_DashboardInfo(x, PROGRESS_ITEMInfo_byLiveBASELINE, IsGetModifiedWORKPACK_ASSIGNMENTS));
        //}

        public static IQueryable<TASK_AppointmentInfo> MorphTASK_AppointmentInfo(IQueryable<TASK> TASKS, int Proj_Id)
        {
            var finalizedTASKS = TASKS.Where(x => x.proj_id == Proj_Id && x.delete_date == null).ToArray().AsQueryable();
            return finalizedTASKS.Select(x => new TASK_AppointmentInfo(x));
        }

        public static IQueryable<ROLE_PERMISSIONInfo> MorphROLE_PERMISSIONInfo(IQueryable<ROLE_PERMISSION> ROLE_PERMISSION, Func<Guid> GetROLEKeyFunc, IQueryable<ROLE_PERMISSION> SYSTEM_PERMISSIONS)
        {
            var finalizedROLE_PERMISSION = ROLE_PERMISSION.ToArray().AsQueryable();
            Guid roleKey = GetROLEKeyFunc();
            var currentAssignedROLE_PERMISSIONS = finalizedROLE_PERMISSION.Where(x => x.GUID_ROLE == roleKey && x.ASSIGNED == true).ToArray().AsEnumerable();
            return SYSTEM_PERMISSIONS.Select(x => new ROLE_PERMISSIONInfo(x, currentAssignedROLE_PERMISSIONS));
        }


    }
}

