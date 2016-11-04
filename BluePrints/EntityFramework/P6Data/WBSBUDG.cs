namespace BluePrints.P6Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("WBSBUDG")]
    public partial class WBSBUDG
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int wbs_budg_id { get; set; }

        public int proj_id { get; set; }

        public int wbs_id { get; set; }

        public DateTime start_date { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? spend_cost { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? benefit_cost { get; set; }

        public DateTime? update_date { get; set; }

        [StringLength(255)]
        public string update_user { get; set; }

        public DateTime? create_date { get; set; }

        [StringLength(255)]
        public string create_user { get; set; }

        public int? delete_session_id { get; set; }

        public DateTime? delete_date { get; set; }

        public virtual PROJECT PROJECT { get; set; }

        public virtual PROJWBS PROJWBS { get; set; }
    }
}
