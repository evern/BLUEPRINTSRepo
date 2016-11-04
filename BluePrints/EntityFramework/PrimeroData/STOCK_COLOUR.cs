namespace BluePrints.PrimeroData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class STOCK_COLOUR
    {
        [Key]
        public int COLOURID { get; set; }

        [Required]
        [StringLength(5)]
        public string COLOURCODE { get; set; }

        [StringLength(30)]
        public string COLOURNAME { get; set; }

        public int? SWATCHID { get; set; }

        [StringLength(1)]
        public string ISACTIVE { get; set; }

        public int? SORTORDER { get; set; }
    }
}
