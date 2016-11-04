namespace BluePrints.Data
{
    using BluePrints.Data.Attributes;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AREA")]
    [ConstraintAttributes("INTERNAL_NUM")]
    public partial class AREA
    {
        public AREA()
        {
            BASELINE_ITEMS = new HashSet<BASELINE_ITEM>();
            ESTIMATION_ITEMS = new HashSet<ESTIMATION_ITEM>();
            WORKPACKS = new HashSet<WORKPACK>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid GUID { get; set; }

        [FilterValueAttribute]
        public Guid GUID_PROJECT { get; set; }

        [Required]
        [StringLength(100)]
        public string INTERNAL_NUM { get; set; }

        [StringLength(100)]
        public string CLIENT_NUM { get; set; }

        [Required]
        [StringLength(200)]
        public string TITLE { get; set; }

        public DateTime CREATED { get; set; }

        public Guid CREATEDBY { get; set; }

        public DateTime? UPDATED { get; set; }

        public Guid? UPDATEDBY { get; set; }

        public DateTime? DELETED { get; set; }

        public Guid? DELETEDBY { get; set; }

        [FilterNameAttribute]
        public virtual PROJECT PROJECT { get; set; }

        public virtual ICollection<BASELINE_ITEM> BASELINE_ITEMS { get; set; }

        public virtual ICollection<ESTIMATION_ITEM> ESTIMATION_ITEMS { get; set; }

        public virtual ICollection<WORKPACK> WORKPACKS { get; set; }
    }
}
