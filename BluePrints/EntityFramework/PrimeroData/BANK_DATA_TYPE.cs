namespace BluePrints.PrimeroData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class BANK_DATA_TYPE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DATA_TYPE { get; set; }

        [StringLength(50)]
        public string TYPE_DESC { get; set; }
    }
}
