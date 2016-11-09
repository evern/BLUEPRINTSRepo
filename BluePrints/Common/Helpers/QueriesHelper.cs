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


        public static IQueryable<ROLE_PERMISSIONInfo> MorphROLE_PERMISSIONInfo(IQueryable<ROLE_PERMISSION> ROLE_PERMISSION, Func<Guid> GetROLEKeyFunc, IQueryable<ROLE_PERMISSION> SYSTEM_PERMISSIONS)
        {
            var finalizedROLE_PERMISSION = ROLE_PERMISSION.ToArray().AsQueryable();
            Guid roleKey = GetROLEKeyFunc();
            var currentAssignedROLE_PERMISSIONS = finalizedROLE_PERMISSION.Where(x => x.GUID_ROLE == roleKey && x.ASSIGNED == true).ToArray().AsEnumerable();
            return SYSTEM_PERMISSIONS.Select(x => new ROLE_PERMISSIONInfo(x, currentAssignedROLE_PERMISSIONS));
        }


    }
}

