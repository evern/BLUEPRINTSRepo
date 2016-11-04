namespace BluePrints.Data
{
    using BluePrints.Data.Attributes;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PHASE")]
    [ConstraintAttributes("INTERNAL_NUM")] //GUID_PROJECT isn't included because phasees will always be retrieved in the context of a project
    public partial class PHASE
    {
        public PHASE()
        {
            BASELINE_ITEMS = new HashSet<BASELINE_ITEM>();
            WORKPACKS = new HashSet<WORKPACK>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid GUID { get; set; }

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

        public virtual ICollection<BASELINE_ITEM> BASELINE_ITEMS { get; set; }

        public virtual ICollection<WORKPACK> WORKPACKS { get; set; }

        public virtual PROJECT PROJECT { get; set; }
    }
}
