namespace BluePrints.PrimeroData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class WEEK_DATES
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int WEEK_NO { get; set; }

        public DateTime START_DATE { get; set; }

        public DateTime END_DATE { get; set; }

        [Required]
        [StringLength(1)]
        public string CLOSED { get; set; }
    }
}
