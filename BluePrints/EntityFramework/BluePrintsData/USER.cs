namespace BluePrints.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("USER")]
    public partial class USER
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid GUID { get; set; }

        public Guid? GUID_ROLE { get; set; }

        [Required]
        [StringLength(100)]
        public string NAME { get; set; }

        [StringLength(100)]
        public string DEPARTMENT { get; set; }

        [StringLength(100)]
        public string TITLE { get; set; }

        [StringLength(500)]
        public string DESCRIPTION { get; set; }

        public DateTime CREATED { get; set; }

        public Guid CREATEDBY { get; set; }

        public DateTime? UPDATED { get; set; }

        public Guid? UPDATEDBY { get; set; }

        public DateTime? DELETED { get; set; }

        public Guid? DELETEDBY { get; set; }

        public virtual ROLE ROLE { get; set; }
    }
}
