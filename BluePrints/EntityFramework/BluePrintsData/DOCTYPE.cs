namespace BluePrints.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DOCTYPE")]
    public partial class DOCTYPE
    {
        public DOCTYPE()
        {
            BASELINE_ITEM = new HashSet<BASELINE_ITEM>();
            WORKPACK = new HashSet<WORKPACK>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid GUID { get; set; }

        public Guid GUID_DDEPARTMENT { get; set; }

        [Required]
        [StringLength(10)]
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

        public virtual ICollection<BASELINE_ITEM> BASELINE_ITEM { get; set; }

        public virtual DEPARTMENT DEPARTMENT { get; set; }

        public virtual ICollection<WORKPACK> WORKPACK { get; set; }
    }
}
