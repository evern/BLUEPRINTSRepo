namespace BluePrints.P6Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DOCSTAT")]
    public partial class DOCSTAT
    {
        public DOCSTAT()
        {
            DOCUMENT = new HashSet<DOCUMENT>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int doc_status_id { get; set; }

        public int seq_num { get; set; }

        [Required]
        [StringLength(36)]
        public string doc_status_code { get; set; }

        public DateTime? update_date { get; set; }

        [StringLength(255)]
        public string update_user { get; set; }

        public DateTime? create_date { get; set; }

        [StringLength(255)]
        public string create_user { get; set; }

        public int? delete_session_id { get; set; }

        public DateTime? delete_date { get; set; }

        public virtual ICollection<DOCUMENT> DOCUMENT { get; set; }
    }
}
