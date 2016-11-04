namespace BluePrints.PrimeroData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class BATCH_QUANTITIES
    {
        [Key]
        public int SEQNO { get; set; }

        [StringLength(23)]
        public string STOCKCODE { get; set; }

        public int? LOCATION { get; set; }

        [StringLength(20)]
        public string BATCHCODE { get; set; }

        public double? QUANTITY { get; set; }

        public DateTime? EXPIRY_DATE { get; set; }

        [StringLength(30)]
        public string REFERENCE { get; set; }
    }
}
