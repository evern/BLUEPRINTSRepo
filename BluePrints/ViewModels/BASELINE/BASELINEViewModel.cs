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
    public partial class BASELINEViewModelWrapper : EntitiesCollectionsWrapper<BASELINE_ITEMJoinRATE, BASELINE_ITEMSJoinRATESCollectionViewModel>
    {
        /// <summary>
        /// Creates a new instance of BASELINEViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static BASELINEViewModelWrapper Create(IUnitOfWorkFactory<IBluePrintsEntitiesUnitOfWork> unitOfWorkFactory = null)
        {
            return ViewModelSource.Create(() => new BASELINEViewModelWrapper(unitOfWorkFactory));
        }

        /// <summary>
        /// Initializes a new instance of the BASELINEViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the BASELINEViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        protected BASELINEViewModelWrapper(IUnitOfWorkFactory<IBluePrintsEntitiesUnitOfWork> unitOfWorkFactory = null)
        {
            ViewName = "BASELINEViewModelWrapper";
        }

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
        /// Influence column(s) when changes happens in other column
        /// </summary>
        public void CellValueChanging(CellValueChangedEventArgs e)
        {
            if (e.RowHandle != GridControl.NewItemRowHandle)
                return;

            BASELINE_ITEMJoinRATE activeBASELINE_ITEM = (BASELINE_ITEMJoinRATE)e.Row;
            if (e.Column.FieldName == BindableBase.GetPropertyName(() => new BASELINE_ITEMJoinRATE().BASELINE_ITEM) + "." + BindableBase.GetPropertyName(() => new BASELINE_ITEM().GUID_WORKPACK))
            {
                WORKPACK chosenWORKPACK = WORKPACKSLoader.GetCollectionFunc().FirstOrDefault(entity => entity.GUID == (Guid)e.Value);
                if(chosenWORKPACK != null)
                {
                    activeBASELINE_ITEM.BASELINE_ITEM.GUID_AREA = chosenWORKPACK.GUID_DAREA;
                    activeBASELINE_ITEM.BASELINE_ITEM.GUID_DOCTYPE = chosenWORKPACK.GUID_DDOCTYPE;
                    activeBASELINE_ITEM.BASELINE_ITEM.GUID_DEPARTMENT = chosenWORKPACK.GUID_DDEPARTMENT;
                    activeBASELINE_ITEM.BASELINE_ITEM.GUID_DISCIPLINE = chosenWORKPACK.GUID_DDISCIPLINE;
                    activeBASELINE_ITEM.BASELINE_ITEM.GUID_PHASE = chosenWORKPACK.PHASE != null ? chosenWORKPACK.GUID_DPHASE : null;
                    var SelectedAREA = AREASLoader.GetCollectionFunc().FirstOrDefault(x => x.GUID == chosenWORKPACK.GUID_DAREA);
                    var SelectedDOCTYPE = DOCTYPESLoader.GetCollectionFunc().FirstOrDefault(x => x.GUID == chosenWORKPACK.GUID_DDOCTYPE);
                    var SelectedDISCIPLINE = DISCIPLINESLoader.GetCollectionFunc().FirstOrDefault(x => x.GUID == chosenWORKPACK.GUID_DDISCIPLINE);

                    activeBASELINE_ITEM.BASELINE_ITEM.INTERNAL_NUM = BluePrintDataUtils.BASELINEITEM_Generate_InternalNumber(loadBASELINE.PROJECT, mainEntityLoader.GetCollectionFunc(), SelectedAREA, SelectedDISCIPLINE, SelectedDOCTYPE);
                    BASELINE_ITEMSJoinRATESCollectionViewModel.UpdateSelectedEntity();
                }
            }
            else if (e.Column.FieldName == BindableBase.GetPropertyName(() => new BASELINE_ITEMJoinRATE().BASELINE_ITEM) + "." + BindableBase.GetPropertyName(() => new BASELINE_ITEM().GUID_DOCTYPE))
            {
                DOCTYPE chosenDOCTYPE = DOCTYPESLoader.GetCollectionFunc().FirstOrDefault(entity => entity.GUID == (Guid)e.Value);
                if (chosenDOCTYPE != null && chosenDOCTYPE.GUID_DDEPARTMENT != null)
                {
                    activeBASELINE_ITEM.BASELINE_ITEM.GUID_DEPARTMENT = chosenDOCTYPE.DEPARTMENT.GUID;
                    BASELINE_ITEMSJoinRATESCollectionViewModel.UpdateSelectedEntity();
                }
            }
        }

        /// <summary>
        /// The view model for the BASELINEBASELINE_ITEMS detail collection.
        /// </summary>
        public BASELINE_ITEMSJoinRATESCollectionViewModel BASELINE_ITEMSJoinRATESCollectionViewModel
        {
            get 
            {
                if (mainViewModel == null && IsAllSubEntitiesLoaded)
                {
                    mainViewModel = (BASELINE_ITEMSJoinRATESCollectionViewModel)mainEntityLoader.GetCollectionViewModel();
                    mainViewModel.ApplyProjectionPropertiesToEntityCallBack = this.ApplyProjectionPropertiesToEntity;
                    mainViewModel.OnEntitySavedCallBack = this.OnEntitiesSavedCallBack;
                    mainViewModel.OnEntitiesLoadedCallBack = this.OnMainEntitiesFirstLoaded;
                    mainViewModel.OnEntitiesChangedCallBack = this.OnEntityChanged;
                }

                return mainViewModel;
            }
        }

        private void OnEntityChanged(object key, Type changedType, EntityMessageType messageType, object sender)
        {
            if (sender == BASELINE_ITEMSJoinRATESCollectionViewModel)
                return;

            if (loadBASELINE != null && changedType == typeof(BASELINE) && loadBASELINE.GUID.ToString() == key.ToString())
            {
                if (messageType == EntityMessageType.Added)
                    MessageBoxService.ShowMessage(StringFormatUtils.GetEntityNameByType(changedType) + CommonResources.View_Restored);
                else if(messageType == EntityMessageType.Deleted)
                    MessageBoxService.ShowMessage(StringFormatUtils.GetEntityNameByType(changedType) + CommonResources.View_Removed);
            }

            if (loadPROJECT != null || loadBASELINE != null)
            {
                if (BASELINE_ITEMSJoinRATESCollectionViewModel != null)
                    mainThreadDispatcher.BeginInvoke(new Action(() => BASELINE_ITEMSJoinRATESCollectionViewModel.Refresh()));
                else if (loadPROJECT != null || loadBASELINE != null)
                    mainThreadDispatcher.BeginInvoke(new Action(() => StartLoading()));
            }
        }

        #region Database Operations
        PROJECT loadPROJECT;
        BASELINE loadBASELINE;
        Dispatcher mainThreadDispatcher = Application.Current.Dispatcher;
        EntitiesLoader<BASELINE> BASELINELoader;
        EntitiesLoader<WORKPACK> WORKPACKSLoader;
        EntitiesLoader<PHASE> PHASESLoader;
        EntitiesLoader<AREA> AREASLoader;
        EntitiesLoader<DEPARTMENT> DEPARTMENTSLoader;
        EntitiesLoader<DISCIPLINE> DISCIPLINESLoader;
        EntitiesLoader<DOCTYPE> DOCTYPESLoader;
        EntitiesLoader<RATE> RATESLoader;
        protected override void OnParameterChanged(object parameter)
        {
            OptionalEntitiesParameter<PROJECT, BASELINE> receiveParameter = (OptionalEntitiesParameter<PROJECT, BASELINE>)parameter;
            this.loadPROJECT = receiveParameter.GetFirstEntity();
            this.loadBASELINE = receiveParameter.GetSecondEntity();
            StartLoading();
        }

        public void StartLoading()
        {
            auxiliaryEntitiesLoader.Clear();
            if (loadPROJECT != null)
                BASELINELoader = new EntitiesLoader<BASELINE>(() => BASELINECollectionViewModel.Create(null, query => query.Where(x => x.GUID_PROJECT == loadPROJECT.GUID && x.STATUS == BaselineStatus.Live)), OnBASELINEEntitiesLoaded, OnEntityChanged);
            else if (loadBASELINE != null)
                BASELINELoader = new EntitiesLoader<BASELINE>(() => BASELINECollectionViewModel.Create(null, query => query.Where(x => x.GUID == loadBASELINE.GUID)), OnBASELINEEntitiesLoaded, OnEntityChanged);

            if(BASELINELoader != null)
                auxiliaryEntitiesLoader.Add(BASELINELoader);
        }

        private void OnBASELINEEntitiesLoaded(IEnumerable<BASELINE> entities)
        {
            mainViewModel = null;
            mainEntityLoader = null;
            isSubEntitiesAdded = false;
            subEntitiesLoader.Clear();

            if (entities.Count() == 0)
                return;

            loadBASELINE = entities.First();
            mainThreadDispatcher.BeginInvoke(new Action(() => AfterBASELINEEntitiesLoaded()));
        }

        private void AfterBASELINEEntitiesLoaded()
        {
            this.RaisePropertyChanged(x => x.WORKPACKDisplayMember);

            PHASESLoader = new EntitiesLoader<PHASE>(() => PHASECollectionViewModel.Create(null, query => query.Where(x => x.GUID_PROJECT == loadBASELINE.GUID_PROJECT)));
            AREASLoader = new EntitiesLoader<AREA>(() => AREACollectionViewModel.Create(null, query => query.Where(x => x.GUID_PROJECT == loadBASELINE.GUID_PROJECT)));
            DEPARTMENTSLoader = new EntitiesLoader<DEPARTMENT>(() => DEPARTMENTCollectionViewModel.Create(null));
            DISCIPLINESLoader = new EntitiesLoader<DISCIPLINE>(() => DISCIPLINECollectionViewModel.Create(null));
            DOCTYPESLoader = new EntitiesLoader<DOCTYPE>(() => DOCTYPECollectionViewModel.Create(null));
            WORKPACKSLoader = new EntitiesLoader<WORKPACK>(() => WORKPACKCollectionViewModel.Create(null, query => query.Where(x => x.GUID_PROJECT == loadBASELINE.GUID_PROJECT)));
            RATESLoader = new EntitiesLoader<RATE>(() => RATECollectionViewModel.Create(null, query => query.Where(x => x.GUID_PROJECT == loadBASELINE.GUID_PROJECT)), OnSubEntitiesLoaded);
            subEntitiesLoader.Add(RATESLoader);
            auxiliaryEntitiesLoader.Add(PHASESLoader);
            auxiliaryEntitiesLoader.Add(AREASLoader);
            auxiliaryEntitiesLoader.Add(DEPARTMENTSLoader);
            auxiliaryEntitiesLoader.Add(DISCIPLINESLoader);
            auxiliaryEntitiesLoader.Add(DOCTYPESLoader);
            auxiliaryEntitiesLoader.Add(WORKPACKSLoader);

            isSubEntitiesAdded = true;
        }

        protected override void OnSubEntitiesLoaded(IEnumerable<object> entities)
        {
            if (IsMainEntityLoaded)
                BASELINE_ITEMSJoinRATESCollectionViewModel.RefreshWithoutClearingUndoManager();
            else if (IsAllSubEntitiesLoaded)
                mainThreadDispatcher.BeginInvoke(new Action(() => AfterSubEntitiesLoaded()));
        }

        protected override void AfterSubEntitiesLoaded()
        {
            mainEntityLoader = new EntitiesLoader<BASELINE_ITEMJoinRATE>(() => BASELINE_ITEMSJoinRATESCollectionViewModel.Create(query => BASELINE_ITEMSJoinRATESQueries.JoinRATESOnBASELINE_ITEMS(query.Where(x => x.GUID_BASELINE == loadBASELINE.GUID), BASELINELoader.GetSingleObjectFunc, RATESLoader.GetCollectionFunc)), OnMainEntitiesFirstLoaded);
        }

        protected override void OnMainEntitiesFirstLoaded(IEnumerable<BASELINE_ITEMJoinRATE> entities)
        {
            base.OnMainEntitiesFirstLoaded(entities);
            mainThreadDispatcher.BeginInvoke(new Action(() => this.RaisePropertiesChanged()));
        }

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

        #region Lookup Collection
        public IEnumerable<WORKPACK> WORKPACKCollection
        {
            get 
            {
                if (WORKPACKSLoader == null)
                    return null;

                return WORKPACKSLoader.Collection; 
            }
        }

        public IEnumerable<PHASE> PHASECollection
        {
            get
            {
                if (PHASESLoader == null)
                    return null;

                return PHASESLoader.Collection;
            }
        }

        public IEnumerable<AREA> AREACollection
        {
            get
            {
                if (AREASLoader == null)
                    return null;

                return AREASLoader.Collection;
            }
        }

        public IEnumerable<DEPARTMENT> DEPARTMENTCollection
        {
            get
            {
                if (DEPARTMENTSLoader == null)
                    return null;

                return DEPARTMENTSLoader.Collection;
            }
        }

        public IEnumerable<DISCIPLINE> DISCIPLINECollection
        {
            get
            {
                if (DISCIPLINESLoader == null)
                    return null;

                return DISCIPLINESLoader.Collection;
            }
        }

        public IEnumerable<DOCTYPE> DOCTYPECollection
        {
            get
            {
                if (DOCTYPESLoader == null)
                    return null;

                return DOCTYPESLoader.Collection;
            }
        }
        #endregion
    }
}
