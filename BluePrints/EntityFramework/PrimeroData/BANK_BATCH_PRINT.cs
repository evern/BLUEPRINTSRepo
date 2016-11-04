namespace BluePrints.PrimeroData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class BANK_BATCH_PRINT
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AccNo { get; set; }

        public int? NoInvs { get; set; }

        public int? Released { get; set; }

        public double? Total { get; set; }

        public int? DOPRINT { get; set; }
    }
}
