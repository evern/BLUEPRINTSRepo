namespace BluePrints.Data
{
    using BluePrints.Common;
    using BluePrints.Data.Attributes;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BASELINE")]
    [ConstraintAttributes("REVISION")]
    public partial class BASELINE
    {
        public BASELINE()
        {
            BASELINE_ITEMS = new HashSet<BASELINE_ITEM>();
            VARIATIONS = new HashSet<VARIATION>();
            VARIATIONS1 = new HashSet<VARIATION>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid GUID { get; set; }

        public Guid GUID_PROJECT { get; set; }

        [Required]
        [StringLength(100)]
        public string NAME { get; set; }

        [Required]
        [StringLength(50)]
        public string REVISION { get; set; }

        [StringLength(100)]
        public string COMMENTS { get; set; }

        public decimal? ACTUAL_UNITS { get; set; }

        public decimal? BUDGETED_UNITS { get; set; }

        public bool ALLOW_EXCEED { get; set; }

        public BaselineStatus STATUS { get; set; }

        [StringLength(20)]
        public string P6BASELINE_NAME { get; set; }

        [StringLength(20)]
        public string P6MODBASELINE_NAME { get; set; }

        public DateTime CREATED { get; set; }

        public Guid CREATEDBY { get; set; }

        public DateTime? UPDATED { get; set; }

        public Guid? UPDATEDBY { get; set; }

        public DateTime? DELETED { get; set; }

        public Guid? DELETEDBY { get; set; }

        public virtual ICollection<BASELINE_ITEM> BASELINE_ITEMS { get; set; }

        public virtual PROJECT PROJECT { get; set; }

        public virtual ICollection<VARIATION> VARIATIONS { get; set; }
        public virtual ICollection<VARIATION> VARIATIONS1 { get; set; }
    }
}
