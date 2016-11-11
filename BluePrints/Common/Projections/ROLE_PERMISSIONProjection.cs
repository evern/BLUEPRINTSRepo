using BluePrints.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluePrints.Common.Projections
{
    public class ROLE_PERMISSIONProjection
    {
        public Guid GUID { get; set; }
        ROLE_PERMISSION role_permission { get; set; }
        public ROLE_PERMISSION ROLE_PERMISSION 
        {
            get { return role_permission; }
            set 
            { 
                if (value != null) 
                    GUID = value.GUID;

                role_permission = value;
            } 
        }
        public KeyValuePair<string, string> SYSTEM_PERMISSION { get; set; }
        public bool IS_ASSIGNED
        {
            get
            {
                return ROLE_PERMISSION != null && ROLE_PERMISSION.GUID != Guid.Empty;
            }
        }
    }

    public static class ROLE_PERMISSIONProjectionQueries
    {
        public static IQueryable<ROLE_PERMISSIONProjection> JoinSYSTEMPERMISSIONOnROLE_PERMISSIONS(IQueryable<ROLE_PERMISSION> ROLE_PERMISSIONS, Dictionary<string, string> SYSTEM_PERMISSIONS)
        {
            List<ROLE_PERMISSIONProjection> returnROLE_PERMISSIONS = new List<ROLE_PERMISSIONProjection>();
            foreach(KeyValuePair<string, string> SYSTEM_PERMISSION in SYSTEM_PERMISSIONS)
            {
                returnROLE_PERMISSIONS.Add(new ROLE_PERMISSIONProjection() { SYSTEM_PERMISSION = SYSTEM_PERMISSION, ROLE_PERMISSION = ROLE_PERMISSIONS.FirstOrDefault(x => x.PERMISSION == SYSTEM_PERMISSION.Key) });
            }

            return returnROLE_PERMISSIONS.AsQueryable();
        }
    }
}
