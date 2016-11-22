namespace BluePrints.Data
{
    using BluePrints.Common;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PROGRESS")]
    public partial class PROGRESS
    {
        public PROGRESS()
        {
            PROGRESS_ITEM = new HashSet<PROGRESS_ITEM>();
            PROGRESS_START = DateTime.Now;
            DATA_DATE = DateTime.Now;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid GUID { get; set; }

        public Guid GUID_PROJECT { get; set; }

        [Required]
        [StringLength(100)]
        public string NAME { get; set; }

        public DateTime PROGRESS_START { get; set; }

        public DateTime DATA_DATE { get; set; }

        public int INTERVAL_COUNT { get; set; }

        public ProgressIntervalType INTERVAL_TYPE { get; set; }

        public ProgressStatus STATUS { get; set; }

        [StringLength(100)]
        public string COMMENTS { get; set; }

        [StringLength(20)]
        public string P6PROGRESS_NAME { get; set; }

        public DateTime CREATED { get; set; }

        public Guid CREATEDBY { get; set; }

        public DateTime? UPDATED { get; set; }

        public Guid? UPDATEDBY { get; set; }

        public DateTime? DELETED { get; set; }

        public Guid? DELETEDBY { get; set; }

        public virtual ICollection<PROGRESS_ITEM> PROGRESS_ITEM { get; set; }

        public virtual PROJECT PROJECT { get; set; }
    }
}
