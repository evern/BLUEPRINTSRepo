namespace BluePrints.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ESTIMATION")]
    public partial class ESTIMATION
    {
        public ESTIMATION()
        {
            ESTIMATION_ITEMS = new HashSet<ESTIMATION_ITEM>();
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

        public int STATUS { get; set; }

        [StringLength(20)]
        public string P6ESTIMATION_NAME { get; set; }

        [StringLength(20)]
        public string P6MODESTIMATION_NAME { get; set; }

        public DateTime CREATED { get; set; }

        public Guid CREATEDBY { get; set; }

        public DateTime? UPDATED { get; set; }

        public Guid? UPDATEDBY { get; set; }

        public DateTime? DELETED { get; set; }

        public Guid? DELETEDBY { get; set; }

        public virtual ICollection<ESTIMATION_ITEM> ESTIMATION_ITEMS { get; set; }

        public virtual PROJECT PROJECT { get; set; }
    }
}
