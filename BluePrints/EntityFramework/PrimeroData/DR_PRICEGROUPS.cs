namespace BluePrints.PrimeroData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DR_PRICEGROUPS
    {
        [Key]
        public int GROUPNO { get; set; }

        [Required]
        [StringLength(30)]
        public string GROUPNAME { get; set; }

        [StringLength(15)]
        public string REPORTCODE { get; set; }
    }
}
