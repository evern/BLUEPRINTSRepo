using BluePrints.BluePrintsEntitiesDataModel;
using BluePrints.Common;
using BluePrints.Common.DataModel;
using BluePrints.Common.Projections;
using BluePrints.Common.ViewModel;
using BluePrints.Common.ViewModel.Utils;
using BluePrints.Data;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluePrints.ViewModels
{
    public class PROJECTBASELINEDetailsCollectionViewModel : DetailsFilterableSingleObjectViewModel<PROJECT, BASELINE, Guid, IBluePrintsEntitiesUnitOfWork>
    {
        /// <summary>
        /// Initializes a new instance of the PROJECTViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the PROJECTViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        protected PROJECTBASELINEDetailsCollectionViewModel(IUnitOfWorkFactory<IBluePrintsEntitiesUnitOfWork> unitOfWorkFactory = null)
            : base(unitOfWorkFactory ?? BluePrintsEntitiesUnitOfWorkSource.GetUnitOfWorkFactory(), x => x.PROJECTS, x => x.NUMBER)
        {
        }

        CustomPROJECTBASELINESDetails customPROJECTBASELINESdetails { get; set; }
        /// <summary>
        /// The view model for the PROJECTBASELINES detail collection.
        /// </summary>
        public CustomPROJECTBASELINESDetails PROJECTBASELINESDetails
        {
            get 
            {
                if (this.PrimaryKey == Guid.Empty)
                    return null;

                if (customPROJECTBASELINESdetails == null)
                {
                    customPROJECTBASELINESdetails = CustomPROJECTBASELINESDetails.Create(this.Entity, this.UnitOfWorkFactory, query => query.Where(x => x.GUID_PROJECT == this.PrimaryKey));
                    customPROJECTBASELINESdetails.OnBeforeEntitySavedCallBack = this.OnBeforeEntitySaved;
                    customPROJECTBASELINESdetails.SetParentViewModel(this);
                }

                return customPROJECTBASELINESdetails;
            }
        }

        /// <summary>
        /// CallBack to apply global convention
        /// </summary>
        public void OnBeforeEntitySaved(BASELINE entity)
        {
            entity.GUID_PROJECT = this.Entity.GUID;
        }

        protected override void OnParameterChanged(object parameter)
        {
            base.OnParameterChanged(parameter);
            this.RaisePropertyChanged(x => x.PROJECTBASELINESDetails);
        }

        protected override void OnDestroy()
        {
            customPROJECTBASELINESdetails.OnBeforeEntitySavedCallBack = null;
            customPROJECTBASELINESdetails.SetParentViewModel(null);
            customPROJECTBASELINESdetails.OnDestroy();
            customPROJECTBASELINESdetails = null;
            base.OnDestroy();
        }
    }

    public class CustomPROJECTBASELINESDetails : CollectionViewModel<BASELINE, Guid, IBluePrintsEntitiesUnitOfWork>, ISupportCustomDocumentTypeNameAndParameter
    {
                /// <summary>
        /// Creates a new instance of PROJECTCollectionViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static CustomPROJECTBASELINESDetails Create(PROJECT project, IUnitOfWorkFactory<IBluePrintsEntitiesUnitOfWork> unitOfWorkFactory = null, Func<IRepositoryQuery<BASELINE>, IQueryable<BASELINE>> projection = null)
        {
            return ViewModelSource.Create(() => new CustomPROJECTBASELINESDetails(project, unitOfWorkFactory, projection));
        }

        PROJECT thisPROJECT;
        /// <summary>
        /// Initializes a new instance of the PROJECTCollectionViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the PROJECTCollectionViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        protected CustomPROJECTBASELINESDetails(PROJECT project, IUnitOfWorkFactory<IBluePrintsEntitiesUnitOfWork> unitOfWorkFactory = null, Func<IRepositoryQuery<BASELINE>, IQueryable<BASELINE>> projection = null)
            : base(unitOfWorkFactory ?? BluePrintsEntitiesUnitOfWorkSource.GetUnitOfWorkFactory(), x => x.BASELINES, projection)
        {
            thisPROJECT = project;
        }


        BaselineMappingSelectionType mappingSelectionType = new BaselineMappingSelectionType();
        public void P6BASELINE_ASSIGN()
        {
            mappingSelectionType = BaselineMappingSelectionType.Original;
            Edit(this.SelectedEntity);
            mappingSelectionType = BaselineMappingSelectionType.None;
        }

        public void P6MODBASELINE_ASSIGN()
        {
            mappingSelectionType = BaselineMappingSelectionType.Modified;
            Edit(this.SelectedEntity);
            mappingSelectionType = BaselineMappingSelectionType.None;
        }

        public string GetCustomDocumentTypeName()
        {
            if (mappingSelectionType == BaselineMappingSelectionType.None)
                return "BASELINE_ITEMCollectionView";

            return "PROJECTWORKPACKDetailsMappingViewHost";
        }

        public object GetCustomDocumentParameter()
        {
            if(mappingSelectionType == BaselineMappingSelectionType.None)
                return new OptionalEntitiesParameter<PROJECT, BASELINE>(null, SelectedEntity);

            return  new object[] { this.SelectedEntity, mappingSelectionType };
        }

        public string GetCustomDocumentTitle()
        {
            if(mappingSelectionType == BaselineMappingSelectionType.Original)
                return this.SelectedEntity.NAME + " - " + this.SelectedEntity.P6BASELINE_NAME + " Mapping";
            else if (mappingSelectionType == BaselineMappingSelectionType.Modified)
                return this.SelectedEntity.NAME + " - " + this.SelectedEntity.P6MODBASELINE_NAME + " Mapping";
            else
                return "[" + thisPROJECT.NUMBER + "] BASELINE";
        }

        public bool IsCustomModeEnabled()
        {
            return true;
        }
    }
}
