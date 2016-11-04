namespace BluePrints.PrimeroData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class RELATIONSHIPS_SET
    {
        [Key]
        public int SEQNO { get; set; }

        [StringLength(40)]
        public string SETNAME { get; set; }

        public int SETBRANCH { get; set; }

        public int SETTYPE { get; set; }

        [Required]
        [StringLength(1)]
        public string SETACTIVE { get; set; }

        public int SETDEFAULT { get; set; }

        public int? SETOPTIONS { get; set; }
    }
}
