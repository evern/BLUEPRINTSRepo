namespace BluePrints.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("VARIATION")]
    public partial class VARIATION
    {
        public VARIATION()
        {
            BASELINE_ITEM = new HashSet<BASELINE_ITEM>();
            VARIATION_ITEM = new HashSet<VARIATION_ITEM>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid GUID { get; set; }

        public Guid GUID_PROJECT { get; set; }

        public Guid? GUID_ORIBASELINE { get; set; }

        public Guid? GUID_BASELINE { get; set; }

        [Required]
        [StringLength(100)]
        public string NAME { get; set; }

        [StringLength(500)]
        public string COMMENTS { get; set; }

        public DateTime? SUBMITTED { get; set; }

        public Guid? SUBMITTEDBY { get; set; }

        public DateTime? APPROVED { get; set; }

        public Guid? APPROVEDBY { get; set; }

        public DateTime CREATED { get; set; }

        public Guid CREATEDBY { get; set; }

        public DateTime? UPDATED { get; set; }

        public Guid? UPDATEDBY { get; set; }

        public DateTime? DELETED { get; set; }

        public Guid? DELETEDBY { get; set; }

        public virtual BASELINE FROMBASELINE { get; set; }

        public virtual BASELINE TOBASELINE { get; set; }

        public virtual ICollection<BASELINE_ITEM> BASELINE_ITEM { get; set; }

        public virtual PROJECT PROJECT { get; set; }

        public virtual ICollection<VARIATION_ITEM> VARIATION_ITEM { get; set; }
    }
}
