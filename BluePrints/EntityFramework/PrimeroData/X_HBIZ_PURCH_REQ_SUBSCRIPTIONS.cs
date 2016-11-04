namespace BluePrints.PrimeroData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class X_HBIZ_PURCH_REQ_SUBSCRIPTIONS
    {
        [Key]
        public int SEQNO { get; set; }

        public int PURCH_REQ_SEQNO { get; set; }

        public int STAFFNO { get; set; }

        public bool? SUB_APPROVALS { get; set; }

        public bool? SUB_DISCUSSION { get; set; }

        public bool? SUB_PURCHASE { get; set; }
    }
}
