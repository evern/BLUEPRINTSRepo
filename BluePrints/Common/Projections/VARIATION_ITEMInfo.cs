using BluePrints.Common;
using BluePrints.Data.Attributes;
using BluePrints.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluePrints.Data
{
    [ConstraintAttributes("BASELINE_ITEM.INTERNAL_NUM")]
    public class VARIATION_ITEMInfo
    {
        public VARIATION_ITEMInfo()
        {
            BASELINE_ITEM = new BASELINE_ITEMInfo();
        }

        public VARIATION_ITEMInfo(ref BASELINE_ITEMInfo BASELINE_ITEM, IQueryable<VARIATION_ITEM> VARIATION_ITEMS, bool IsLocked)
        {
            this.BASELINE_ITEM = BASELINE_ITEM;
            this.IsLocked = IsLocked;
            var currentVARIATION_ITEM = VARIATION_ITEMS.FirstOrDefault(x => x.GUID_ORIBASEITEM == this.BASELINE_ITEM.GUID_ORIGINAL);

            if (currentVARIATION_ITEM != null)
            {
                DataUtils.ShallowCopy(this, currentVARIATION_ITEM);
            }
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid GUID { get; set; }

        public Guid GUID_VARIATION { get; set; }

        public Guid GUID_ORIBASEITEM { get; set; }

        public decimal VARIATION_UNITS { get; set; }

        public VariationAction ACTION { get; set; }

        public DateTime CREATED { get; set; }

        public Guid CREATEDBY { get; set; }

        public DateTime? UPDATED { get; set; }

        public Guid? UPDATEDBY { get; set; }

        public DateTime? DELETED { get; set; }

        public Guid? DELETEDBY { get; set; }

        public BASELINE_ITEMInfo BASELINE_ITEM { get; set; }

        public bool IsLocked { get; set; }

        public bool IsReadOnly
        {
            get { return IsLocked || (ACTION != VariationAction.Add && ACTION != VariationAction.Cancel); }
        }

        public bool CanToggleCancellation
        {
            get { return !IsLocked && ACTION != VariationAction.Add; }
        }
    }
}
