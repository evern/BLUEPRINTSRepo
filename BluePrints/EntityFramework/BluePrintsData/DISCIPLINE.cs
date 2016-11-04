namespace BluePrints.Data
{
    using BluePrints.Data.Attributes;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DISCIPLINE")]
    [ConstraintAttributes("CODE")]
    public partial class DISCIPLINE
    {
        public DISCIPLINE()
        {
            BASELINE_ITEMS = new HashSet<BASELINE_ITEM>();
            COMMODITY_CODES = new HashSet<COMMODITY_CODE>();
            ESTIMATION_ITEMS = new HashSet<ESTIMATION_ITEM>();
            RATES = new HashSet<RATE>();
            WORKPACKS = new HashSet<WORKPACK>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid GUID { get; set; }

        [Required]
        [StringLength(50)]
        [FilterNameAttribute]
        [FilterValueAttribute]
        public string CODE { get; set; }

        [Required]
        [StringLength(100)]
        public string NAME { get; set; }

        public DateTime CREATED { get; set; }

        public Guid CREATEDBY { get; set; }

        public DateTime? UPDATED { get; set; }

        public Guid? UPDATEDBY { get; set; }

        public DateTime? DELETED { get; set; }

        public Guid? DELETEDBY { get; set; }

        public virtual ICollection<BASELINE_ITEM> BASELINE_ITEMS { get; set; }

        public virtual ICollection<COMMODITY_CODE> COMMODITY_CODES { get; set; }

        public virtual ICollection<ESTIMATION_ITEM> ESTIMATION_ITEMS { get; set; }

        public virtual ICollection<RATE> RATES { get; set; }

        public virtual ICollection<WORKPACK> WORKPACKS { get; set; }
    }
}
