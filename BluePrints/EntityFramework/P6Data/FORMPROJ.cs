namespace BluePrints.P6Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FORMPROJ")]
    public partial class FORMPROJ
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int form_tmpl_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int proj_id { get; set; }

        public DateTime? update_date { get; set; }

        [StringLength(255)]
        public string update_user { get; set; }

        public DateTime? create_date { get; set; }

        [StringLength(255)]
        public string create_user { get; set; }

        public int? delete_session_id { get; set; }

        public DateTime? delete_date { get; set; }

        public virtual FORMTMPL FORMTMPL { get; set; }

        public virtual PROJECT PROJECT { get; set; }
    }
}
