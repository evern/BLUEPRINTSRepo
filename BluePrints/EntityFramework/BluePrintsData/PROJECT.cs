namespace BluePrints.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using BluePrints.Common;
    using Attributes;
    using System.ComponentModel;

    [Table("PROJECT")]
    [ConstraintAttributes("NUMBER")]
    public partial class PROJECT
    {
        public PROJECT()
        {
            AREAS = new HashSet<AREA>();
            BASELINES = new HashSet<BASELINE>();
            COMMODITIES = new HashSet<COMMODITY>();
            ESTIMATIONS = new HashSet<ESTIMATION>();
            PHASES = new HashSet<PHASE>();
            PROGRESSES = new HashSet<PROGRESS>();
            REGISTERS = new HashSet<REGISTER>();
            PROJECT_REPORTS = new HashSet<PROJECT_REPORT>();
            RATES = new HashSet<RATE>();
            VARIATIONS = new HashSet<VARIATION>();
            WORKPACKS = new HashSet<WORKPACK>();
            CURRENCYCONVERSION = 1;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid GUID { get; set; }

        [Required]
        [StringLength(100)]
        [FilterNameAttribute]
        [FilterValueAttribute]
        public string NUMBER { get; set; }

        [StringLength(100)]
        public string NAME { get; set; }

        [StringLength(100)]
        public string CLIENT { get; set; }

        public ProjectStatus STATUS { get; set; }

        public ContractType CONTRACTTYPE { get; set; }

        public ProjectType TYPE { get; set; }

        public DesignManager DESIGNMANAGER { get; set; }

        public decimal CURRENCYCONVERSION { get; set; }

        public bool USELEGACYWORKPACK { get; set; }

        public decimal REVIEWPERCENTAGE { get; set; }

        public decimal REVIEWPERIOD { get; set; }

        public DateTime CREATED { get; set; }

        public Guid CREATEDBY { get; set; }

        public DateTime? UPDATED { get; set; }

        public Guid? UPDATEDBY { get; set; }

        public DateTime? DELETED { get; set; }

        public Guid? DELETEDBY { get; set; }

        public virtual ICollection<AREA> AREAS { get; set; }

        public virtual ICollection<BASELINE> BASELINES { get; set; }

        public virtual ICollection<COMMODITY> COMMODITIES { get; set; }

        public virtual ICollection<ESTIMATION> ESTIMATIONS { get; set; }

        public virtual ICollection<PHASE> PHASES { get; set; }

        public virtual ICollection<PROGRESS> PROGRESSES { get; set; }

        public virtual ICollection<REGISTER> REGISTERS { get; set; }

        public virtual ICollection<PROJECT_REPORT> PROJECT_REPORTS { get; set; }

        public virtual ICollection<RATE> RATES { get; set; }

        public virtual ICollection<VARIATION> VARIATIONS { get; set; }

        public virtual ICollection<WORKPACK> WORKPACKS { get; set; }
    }
}
