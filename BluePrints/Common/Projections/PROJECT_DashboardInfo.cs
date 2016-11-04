using BluePrints.BluePrintsEntitiesDataModel;
using BluePrints.Common;
using BluePrints.Common.ViewModel.Reporting;
using BluePrints.Data.Attributes;
using BluePrints.Data.Helpers;
using BluePrints.P6EntitiesDataModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluePrints.Data
{
    public class PROJECT_DashboardInfo : ISupportSummaryCalculation
    {
        public PROJECT_DashboardInfo()
        {

        }

        public PROJECT_DashboardInfo(PROJECT PROJECT, IQueryable<BASELINE> LiveBASELINES, IQueryable<PROGRESS> LivePROGRESSES, IQueryable<RATE> RATES, IBluePrintsEntitiesUnitOfWork BluePrintsUnitOfWork, IP6EntitiesUnitOfWork P6UnitOfWork)
        {
            //DataUtils.ShallowCopy(this, PROJECT);
            //BASELINE LiveBASELINE = LiveBASELINES.FirstOrDefault(x => x.GUID_PROJECT == this.GUID);
            //PROGRESS LivePROGRESS = LivePROGRESSES.FirstOrDefault(x => x.GUID_PROJECT == this.GUID);
            //IEnumerable<RATE> projectRATE = RATES.Where(x => x.GUID_PROJECT == this.GUID).AsEnumerable();
            //SummaryManufacturer summaryManufacturer = new SummaryManufacturer();

            //if(LiveBASELINE != null && LivePROGRESS != null)
            //{
            //    ProjectSummaryBuilder projectSummaryBuilder = new ProjectSummaryBuilder(DefaultSummaryCalculation.Create(), QueriesHelper.MorphPROGRESS_ITEMInfo(LivePROGRESS.PROGRESS_ITEMS.AsQueryable(), () => LiveBASELINE, () => RATES.AsQueryable(), LivePROGRESS.DATA_DATE), LivePROGRESS, LiveBASELINE, BluePrintsUnitOfWork, P6UnitOfWork);
            //    this.SummarizablePrincipal = (DefaultSummaryCalculation)projectSummaryBuilder.SummaryObject;
            //    IEnumerable<PROGRESS_ITEMInfo> entities = this.SummarizablePrincipal.ReportableObjects as IEnumerable<PROGRESS_ITEMInfo>;
            //    if (entities != null)
            //    {
            //        IEnumerable<RATE> PROJECTRATES = RATES.Where(x => x.GUID_PROJECT == LiveBASELINE.GUID_PROJECT);
            //        QueriesHelper.UpdatePROGRESS_ITEMInfoRATE(entities, PROJECTRATES);
            //    }

            //    summaryManufacturer.Manufacture(projectSummaryBuilder);
            //}
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

        public DefaultSummaryCalculation SummarizablePrincipal { get; set; }

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
