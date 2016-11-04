namespace BluePrints.PrimeroData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CL_TAX_RETURN
    {
        [Key]
        public int SEQNO { get; set; }

        public int WIDGETID { get; set; }

        public int? CTX_SEQNO { get; set; }

        public int? TAX_RETURN { get; set; }

        public int? TAX_BY_RATE_TYPE { get; set; }

        public int? REVIEW_TAX_RETURNS { get; set; }

        public int? BAS_TAX_RETURN { get; set; }

        public int? NZ_GST_RETURN { get; set; }
    }
}
