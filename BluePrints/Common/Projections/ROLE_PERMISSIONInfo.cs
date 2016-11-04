using BluePrints.Data.Attributes;
using BluePrints.Data.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluePrints.Data
{
    public class ROLE_PERMISSIONInfo
    {
        public ROLE_PERMISSIONInfo(ROLE_PERMISSION systemPERMISSION, IEnumerable<ROLE_PERMISSION> currentAssignedROLE_PERMISSIONS)
        {
            var currentROLE_PERMISSION = currentAssignedROLE_PERMISSIONS.FirstOrDefault(x => x.PERMISSION == systemPERMISSION.PERMISSION);
            if (currentROLE_PERMISSION != null)
            {
                DataUtils.ShallowCopy(this, currentROLE_PERMISSION);
                this.ASSIGNED = true;
            }
            else
                this.PERMISSION = systemPERMISSION.PERMISSION;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid GUID { get; set; }

        public Guid GUID_ROLE { get; set; }

        [Required]
        [StringLength(50)]
        public string PERMISSION { get; set; }

        [Required]
        public bool ASSIGNED { get; set; }

        public DateTime CREATED { get; set; }

        public Guid CREATEDBY { get; set; }

        public DateTime? UPDATED { get; set; }

        public Guid? UPDATEDBY { get; set; }

        public DateTime? DELETED { get; set; }

        public Guid? DELETEDBY { get; set; }

        public virtual ROLE ROLE { get; set; }
    }
}
