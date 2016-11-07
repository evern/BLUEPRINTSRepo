using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using BluePrints.Common.Utils;
using BluePrints.BluePrintsEntitiesDataModel;
using BluePrints.Common.DataModel;
using BluePrints.Data;
using BluePrints.Common.ViewModel;
using DevExpress.Xpf.Grid;
using System.Threading;
using BluePrints.Common.ViewModel.Utils;
using BluePrints.Data.Helpers;
using BluePrints.Common;
using BluePrints.Common.Helpers;
using BluePrints.Common.Projections;
using System.Windows.Threading;
using System.Windows;

namespace BluePrints.ViewModels
{
    /// <summary>
    /// Represents the single BASELINE object view model.
    /// </summary>
    public partial class BASELINE_ITEMSViewModelWrapper : CollectionViewModelsWrapper<BASELINE_ITEM, BASELINE_ITEMJoinRATE, Guid, IBluePrintsEntitiesUnitOfWork, CollectionViewModel<BASELINE_ITEM, BASELINE_ITEMJoinRATE, Guid, IBluePrintsEntitiesUnitOfWork>>
    {
        /// <summary>
        /// Creates a new instance of BASELINEViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static BASELINE_ITEMSViewModelWrapper Create(IUnitOfWorkFactory<IBluePrintsEntitiesUnitOfWork> unitOfWorkFactory = null)
        {
            return ViewModelSource.Create(() => new BASELINE_ITEMSViewModelWrapper(unitOfWorkFactory));
        }

        /// <summary>
        /// Initializes a new instance of the BASELINEViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the BASELINEViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        protected BASELINE_ITEMSViewModelWrapper(IUnitOfWorkFactory<IBluePrintsEntitiesUnitOfWork> unitOfWorkFactory = null)
        {
        }

        #region Database Operations
        PROJECT loadPROJECT;
        BASELINE loadBASELINE;
        bool isQueryForLiveStatus;
        IUnitOfWorkFactory<IBluePrintsEntitiesUnitOfWork> bluePrintsUnitOfWorkFactory = BluePrintsEntitiesUnitOfWorkSource.GetUnitOfWorkFactory();
        protected override void InitializeParameters(object parameter)
        {
            OptionalEntitiesParameter<PROJECT, BASELINE> receiveParameter = (OptionalEntitiesParameter<PROJECT, BASELINE>)parameter;
            this.loadPROJECT = receiveParameter.GetFirstEntity();
            this.loadBASELINE = receiveParameter.GetSecondEntity();

            if (this.loadPROJECT != null)
                isQueryForLiveStatus = true;
        }

        public override void InitializeAndLoadEntitiesLoaderDescription()
        {
            MainViewModel = null;
            loaderCollection = new EntitiesLoaderDescriptionCollection(this);
            loaderCollection.AddEntitiesLoader<PROJECT, PROJECT, Guid, IBluePrintsEntitiesUnitOfWork>(0, bluePrintsUnitOfWorkFactory, x => x.PROJECTS, PROJECTProjectionFunc, null, isContinueLoadingPROJECT, OnEntitiesChanged);
            loaderCollection.AddEntitiesLoader<BASELINE, BASELINE, Guid, IBluePrintsEntitiesUnitOfWork>(1, bluePrintsUnitOfWorkFactory, x => x.BASELINES, BASELINEProjectionFunc, typeof(PROJECT), isContinueLoadingBASELINE, OnEntitiesChanged);
            loaderCollection.AddEntitiesLoader<WORKPACK, WORKPACK, Guid, IBluePrintsEntitiesUnitOfWork>(2, bluePrintsUnitOfWorkFactory, x => x.WORKPACKS, WORKPACKProjectionFunc, typeof(BASELINE));
            loaderCollection.AddEntitiesLoader<PHASE, PHASE, Guid, IBluePrintsEntitiesUnitOfWork>(3, bluePrintsUnitOfWorkFactory, x => x.PHASES, PHASEProjectionFunc, typeof(BASELINE));
            loaderCollection.AddEntitiesLoader<AREA, AREA, Guid, IBluePrintsEntitiesUnitOfWork>(4, bluePrintsUnitOfWorkFactory, x => x.AREAS, AREAProjectionFunc, typeof(BASELINE));
            loaderCollection.AddEntitiesLoader<DEPARTMENT, DEPARTMENT, Guid, IBluePrintsEntitiesUnitOfWork>(5, bluePrintsUnitOfWorkFactory, x => x.DEPARTMENTS);
            loaderCollection.AddEntitiesLoader<DISCIPLINE, DISCIPLINE, Guid, IBluePrintsEntitiesUnitOfWork>(6, bluePrintsUnitOfWorkFactory, x => x.DISCIPLINES);
            loaderCollection.AddEntitiesLoader<DOCTYPE, DOCTYPE, Guid, IBluePrintsEntitiesUnitOfWork>(7, bluePrintsUnitOfWorkFactory, x => x.DOCTYPES);
            loaderCollection.AddEntitiesLoader<RATE, RATE, Guid, IBluePrintsEntitiesUnitOfWork>(8, bluePrintsUnitOfWorkFactory, x => x.RATES, RATEProjectionFunc);
            InvokeEntitiesLoaderDescriptionLoading();
        }

        bool isContinueLoadingPROJECT(IEnumerable<PROJECT> entities)
        {
            if(entities.Count() == 0)
            {
                mainThreadDispatcher.BeginInvoke(new Action(() => MessageBoxService.ShowMessage(string.Format(CommonResources.Notify_View_Removed, "PROJECT"))));
                return false;
            }

            this.loadPROJECT = entities.First();
            return true;
        }

        bool isContinueLoadingBASELINE(IEnumerable<BASELINE> entities)
        {
            if (entities.Count() == 0)
            {
                mainThreadDispatcher.BeginInvoke(new Action(() => MessageBoxService.ShowMessage(string.Format(CommonResources.Notify_View_Removed, "BASELINE"))));
                return false;
            }

            this.loadBASELINE = entities.First();
            return true;
        }

        Func<IRepositoryQuery<PROJECT>, IQueryable<PROJECT>> PROJECTProjectionFunc()
        {
            if (isQueryForLiveStatus)
                return query => query.Where(x => x.GUID == this.loadPROJECT.GUID);
            else
                return query => query.Where(x => x.GUID == this.loadBASELINE.GUID_PROJECT);
        }

        Func<IRepositoryQuery<BASELINE>, IQueryable<BASELINE>> BASELINEProjectionFunc()
        {
            if (isQueryForLiveStatus)
                return query => query.Where(x => x.GUID_PROJECT == this.loadPROJECT.GUID && x.STATUS == BaselineStatus.Live);
            else
                return query => query.Where(x => x.GUID == this.loadBASELINE.GUID);
        }

        Func<IRepositoryQuery<WORKPACK>, IQueryable<WORKPACK>> WORKPACKProjectionFunc()
        {
            return query => query.Where(x => x.GUID_PROJECT == loadBASELINE.GUID_PROJECT);
        }

        Func<IRepositoryQuery<PHASE>, IQueryable<PHASE>> PHASEProjectionFunc()
        {
            return query => query.Where(x => x.GUID_PROJECT == loadBASELINE.GUID_PROJECT);
        }

        Func<IRepositoryQuery<AREA>, IQueryable<AREA>> AREAProjectionFunc()
        {
            return query => query.Where(x => x.GUID_PROJECT == loadBASELINE.GUID_PROJECT);
        }

        Func<IRepositoryQuery<RATE>, IQueryable<RATE>> RATEProjectionFunc()
        {
            return query => query.Where(x => x.GUID_PROJECT == loadBASELINE.GUID_PROJECT);
        }

        protected override void OnAllEntitiesCollectionLoaded()
        {
            CreateMainViewModel(this.bluePrintsUnitOfWorkFactory, x => x.BASELINE_ITEMS);
            mainThreadDispatcher.BeginInvoke(new Action(() => mainEntityLoader.CreateCollectionViewModel()));
        }

        protected override Func<IRepositoryQuery<BASELINE_ITEM>, IQueryable<BASELINE_ITEMJoinRATE>> ConstructMainViewModelProjection()
        {
            Func<IQueryable<RATE>> getRATESFunc = loaderCollection.GetCollectionFunc<RATE>();
            Func<BASELINE> getBASELINEFunc = loaderCollection.GetObjectFunc<BASELINE>();
            return query => BASELINE_ITEMSJoinRATESQueries.JoinRATESOnBASELINE_ITEMS(query, getBASELINEFunc, getRATESFunc);
        }

        protected override void AssignCallBacksAndRaisePropertyChange(CollectionViewModel<BASELINE_ITEM, BASELINE_ITEMJoinRATE, Guid, IBluePrintsEntitiesUnitOfWork> mainViewModel)
        {
            mainViewModel.ApplyProjectionPropertiesToEntityCallBack = this.ApplyProjectionPropertiesToEntity;
            mainViewModel.OnEntitySavedCallBack = this.OnEntitiesSavedCallBack;
            mainThreadDispatcher.BeginInvoke(new Action(() => this.RaisePropertiesChanged()));
        }

        protected override void OnEntitiesChanged(object key, Type changedType, EntityMessageType messageType, object sender)
        {
            if (MainViewModel == null)
                return;

            if (sender == MainViewModel)
                return;

            if (loadBASELINE != null && changedType == typeof(BASELINE) && loadBASELINE.GUID.ToString() == key.ToString() ||
                loadPROJECT != null && changedType == typeof(PROJECT) && loadPROJECT.GUID.ToString() == key.ToString())
            {
                if (messageType == EntityMessageType.Added)
                    MessageBoxService.ShowMessage(string.Format(CommonResources.Notify_View_Restored, StringFormatUtils.GetEntityNameByType(changedType)));
                else if (messageType == EntityMessageType.Deleted)
                    MessageBoxService.ShowMessage(string.Format(CommonResources.Notify_View_Removed, StringFormatUtils.GetEntityNameByType(changedType)));
            }

            if (loadPROJECT != null || loadBASELINE != null)
            {
                if (BASELINE_ITEMSJoinRATESCollectionViewModel != null)
                    mainThreadDispatcher.BeginInvoke(new Action(() => BASELINE_ITEMSJoinRATESCollectionViewModel.Refresh()));
                else if (loadPROJECT != null || loadBASELINE != null)
                    mainThreadDispatcher.BeginInvoke(new Action(() => InitializeAndLoadEntitiesLoaderDescription()));
            }
        }

        #region Collection Call Backs
        public void ApplyProjectionPropertiesToEntity(BASELINE_ITEMJoinRATE projectionEntity, BASELINE_ITEM entity)
        {
            projectionEntity.BASELINE_ITEM.GUID_BASELINE = loadBASELINE.GUID;
            DataUtils.ShallowCopy(entity, projectionEntity.BASELINE_ITEM);
            //workaround for created because Save() only sets the projection primary key, this is used for property redo where the interceptor only tampers with UPDATED and CREATED is left as null
            if (entity.CREATED.Date.Year == 1)
                projectionEntity.BASELINE_ITEM.CREATED = DateTime.Now;

            entity.CREATED = projectionEntity.BASELINE_ITEM.CREATED;
        }

        public void OnEntitiesSavedCallBack(Guid primaryKey, BASELINE_ITEMJoinRATE projectionEntity, BASELINE_ITEM entity, bool isNewEntity)
        {
            projectionEntity.GUID = entity.GUID;
            projectionEntity.BASELINE_ITEM.GUID = entity.GUID;
            projectionEntity.BASELINE_ITEM.GUID_ORIGINAL = entity.GUID_ORIGINAL;
        }
        #endregion
        #endregion

        #region View Behavior
        /// <summary>
        /// Influence column(s) when changes happens in other column
        /// </summary>
        public void CellValueChanging(CellValueChangedEventArgs e)
        {
            if (e.RowHandle != GridControl.NewItemRowHandle)
                return;

            BASELINE_ITEMJoinRATE activeBASELINE_ITEM = (BASELINE_ITEMJoinRATE)e.Row;
            if (e.Column.FieldName == BindableBase.GetPropertyName(() => new BASELINE_ITEMJoinRATE().BASELINE_ITEM) + "." + BindableBase.GetPropertyName(() => new BASELINE_ITEM().GUID_WORKPACK))
            {
                WORKPACK chosenWORKPACK = WORKPACKCollection.FirstOrDefault(entity => entity.GUID == (Guid)e.Value);
                if (chosenWORKPACK != null)
                {
                    activeBASELINE_ITEM.BASELINE_ITEM.GUID_AREA = chosenWORKPACK.GUID_DAREA;
                    activeBASELINE_ITEM.BASELINE_ITEM.GUID_DOCTYPE = chosenWORKPACK.GUID_DDOCTYPE;
                    activeBASELINE_ITEM.BASELINE_ITEM.GUID_DEPARTMENT = chosenWORKPACK.GUID_DDEPARTMENT;
                    activeBASELINE_ITEM.BASELINE_ITEM.GUID_DISCIPLINE = chosenWORKPACK.GUID_DDISCIPLINE;
                    activeBASELINE_ITEM.BASELINE_ITEM.GUID_PHASE = chosenWORKPACK.PHASE != null ? chosenWORKPACK.GUID_DPHASE : null;
                    var SelectedAREA = AREACollection.FirstOrDefault(x => x.GUID == chosenWORKPACK.GUID_DAREA);
                    var SelectedDOCTYPE = DOCTYPECollection.FirstOrDefault(x => x.GUID == chosenWORKPACK.GUID_DDOCTYPE);
                    var SelectedDISCIPLINE = DISCIPLINECollection.FirstOrDefault(x => x.GUID == chosenWORKPACK.GUID_DDISCIPLINE);

                    activeBASELINE_ITEM.BASELINE_ITEM.INTERNAL_NUM = BluePrintDataUtils.BASELINEITEM_Generate_InternalNumber(loadBASELINE.PROJECT, MainViewModel.Entities, SelectedAREA, SelectedDISCIPLINE, SelectedDOCTYPE);
                    BASELINE_ITEMSJoinRATESCollectionViewModel.UpdateSelectedEntity();
                }
            }
            else if (e.Column.FieldName == BindableBase.GetPropertyName(() => new BASELINE_ITEMJoinRATE().BASELINE_ITEM) + "." + BindableBase.GetPropertyName(() => new BASELINE_ITEM().GUID_DOCTYPE))
            {
                DOCTYPE chosenDOCTYPE = DOCTYPECollection.FirstOrDefault(entity => entity.GUID == (Guid)e.Value);
                if (chosenDOCTYPE != null && chosenDOCTYPE.GUID_DDEPARTMENT != null)
                {
                    activeBASELINE_ITEM.BASELINE_ITEM.GUID_DEPARTMENT = chosenDOCTYPE.DEPARTMENT.GUID;
                    BASELINE_ITEMSJoinRATESCollectionViewModel.UpdateSelectedEntity();
                }
            }
        }
        #endregion

        #region View Properties
        /// <summary>
        /// The view name to be used when saving layout for IDocumentContent
        /// </summary>
        protected override string ViewName
        {
            get
            {
                return "BASELINE_ITEMSViewModelWrapper";
            }
        }

        /// <summary>
        /// The workpack internal name to be used
        /// </summary>
        public string WORKPACKDisplayMember
        {
            get
            {
                if (loadBASELINE == null || loadBASELINE.PROJECT.USELEGACYWORKPACK)
                    return BindableBase.GetPropertyName(() => new WORKPACK().INTERNAL_NAME1);
                else
                    return BindableBase.GetPropertyName(() => new WORKPACK().INTERNAL_NAME2);
            }
        }

        /// <summary>
        /// The view model for the BASELINEBASELINE_ITEMS detail collection.
        /// </summary>
        public CollectionViewModel<BASELINE_ITEM, BASELINE_ITEMJoinRATE, Guid, IBluePrintsEntitiesUnitOfWork> BASELINE_ITEMSJoinRATESCollectionViewModel
        {
            get
            {
                return MainViewModel;
            }
        }

        public IEnumerable<WORKPACK> WORKPACKCollection
        {
            get
            {
                return GetEntities<WORKPACK>();
            }
        }

        public IEnumerable<PHASE> PHASECollection
        {
            get
            {
                return GetEntities<PHASE>();
            }
        }

        public IEnumerable<AREA> AREACollection
        {
            get
            {
                return GetEntities<AREA>();
            }
        }

        public IEnumerable<DEPARTMENT> DEPARTMENTCollection
        {
            get
            {
                return GetEntities<DEPARTMENT>();
            }
        }

        public IEnumerable<DISCIPLINE> DISCIPLINECollection
        {
            get
            {
                return GetEntities<DISCIPLINE>();
            }
        }

        public IEnumerable<DOCTYPE> DOCTYPECollection
        {
            get
            {
                return GetEntities<DOCTYPE>();
            }
        }
        #endregion


    }
}
