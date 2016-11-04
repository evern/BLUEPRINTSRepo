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
using BluePrints.Data.Helpers;
using BluePrints.Common;
using DevExpress.Xpf.Grid;
using BluePrints.Common.ViewModel.Utils;
using BluePrints.Common.ViewModel.UndoRedo;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.Settings;
using BluePrints.Common.Helpers;

namespace BluePrints.ViewModels
{
    /// <summary>
    /// Represents the single VARIATION object view model.
    /// </summary>
    public partial class VARIATIONViewModel : SingleObjectViewModel<VARIATION, Guid, IBluePrintsEntitiesUnitOfWork>
    {
        /// <summary>
        /// Creates a new instance of VARIATIONViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static VARIATIONViewModel Create(IUnitOfWorkFactory<IBluePrintsEntitiesUnitOfWork> unitOfWorkFactory = null)
        {
            return ViewModelSource.Create(() => new VARIATIONViewModel(unitOfWorkFactory));
        }

        /// <summary>
        /// Initializes a new instance of the VARIATIONViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the VARIATIONViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        protected VARIATIONViewModel(IUnitOfWorkFactory<IBluePrintsEntitiesUnitOfWork> unitOfWorkFactory = null)
            : base(unitOfWorkFactory ?? BluePrintsEntitiesUnitOfWorkSource.GetUnitOfWorkFactory(), x => x.VARIATIONS, x => x.NAME)
        {
        }

        #region Main View
        CollectionViewModel<VARIATION_ITEM, VARIATION_ITEMInfo, Guid, IBluePrintsEntitiesUnitOfWork> VARIATION_ITEMInfosCollectionViewModel;
        /// <summary>
        /// The view model for the VARIATIONVARIATION_ITEMS detail collection.
        /// </summary>
        public CollectionViewModel<VARIATION_ITEM, VARIATION_ITEMInfo, Guid, IBluePrintsEntitiesUnitOfWork> VARIATIONVARIATION_ITEMSDetails
        {
            get
            {

                if (VARIATION_ITEMInfosCollectionViewModel == null && loadedViewModelCount == 3)
                {
                    VARIATION_ITEMInfosCollectionViewModel = GetDetailProjectionsCollectionViewModel((VARIATIONViewModel x) => x.VARIATIONVARIATION_ITEMSDetails, x => x.VARIATION_ITEMS, x => x.GUID_VARIATION, (x, key) => { x.GUID_VARIATION = key; }, query => QueriesHelper.MorphVARIATION_ITEMInfo(query, BASELINE_byLiveLoader.GetSingleObjectFunc, BASELINE_ITEM_byVARIATIONLoader.GetCollectionFunc, RATES_byProjectLoader.GetCollectionFunc, this.Entity.SUBMITTED != null));
                    VARIATION_ITEMInfosCollectionViewModel.ApplyProjectionPropertiesToEntityCallBack = this.ApplyProjectionPropertiesToEntityCallBack;
                    VARIATION_ITEMInfosCollectionViewModel.ExistingProjectionEditCallBack = this.ExistingProjectionEditCallBack;
                    VARIATION_ITEMInfosCollectionViewModel.NewProjectionInitializeCallBack = this.NewProjectionInitializeCallBack;
                    VARIATION_ITEMInfosCollectionViewModel.EntityBeforeDeletionCallBack = this.EntityBeforeDeletionCallBack;
                    VARIATION_ITEMInfosCollectionViewModel.CanBulkDeleteCallBack = this.CanBulkDeleteCallBack;
                    VARIATION_ITEMInfosCollectionViewModel.CanFillDownCallBack = this.CanFillDownCallBack;
                    VARIATION_ITEMInfosCollectionViewModel.ValidateFillDownCallBack = this.ValidateFillDownCallBack;
                    VARIATION_ITEMInfosCollectionViewModel.AllowEdit = false;
                }

                return VARIATION_ITEMInfosCollectionViewModel;
            }
        }
        #endregion

        #region Query Lookups
        EntitiesLoader<BASELINE_ITEM> BASELINE_ITEM_byVARIATIONLoader;
        EntitiesLoader<BASELINE> BASELINE_byLiveLoader;
        EntitiesLoader<RATE> RATES_byProjectLoader;
        protected override void OnParameterChanged(object parameter)
        {
            base.OnParameterChanged(parameter);
            BASELINE_ITEM_byVARIATIONLoader = new EntitiesLoader<BASELINE_ITEM>(() => GetDetailsCollectionViewModel((VARIATIONViewModel x) => x.BASELINE_ITEMSDetails_ByVariation, x => x.BASELINE_ITEMS, x => x.GUID_VARIATION, (x, key) => { x.GUID_VARIATION = key; }), this.OnDetailedEntitiesLoaded);
            BASELINE_byLiveLoader = new EntitiesLoader<BASELINE>(() => GetLookUpEntitiesViewModel((PROJECTViewModel x) => x.LookUpBASELINES, x => x.BASELINES, query => query.Where(x => (Entity.GUID_ORIBASELINE != null && x.GUID == this.Entity.GUID_ORIBASELINE) || x.STATUS == BaselineStatus.Live && x.GUID_PROJECT == Entity.GUID_PROJECT)), this.OnDetailedEntitiesLoaded);
            RATES_byProjectLoader = new EntitiesLoader<RATE>(() => GetLookUpEntitiesViewModel((PROJECTViewModel x) => x.LookUpRATES, x => x.RATES, query => query.Where(x => x.GUID_PROJECT == Entity.GUID_PROJECT)), this.OnDetailedEntitiesLoaded);
            //stimulate the loading of entities
            //BASELINE_ITEMSDetails_ByVariation.Entities.ToList();
            //LookUpBASELINE_ByLiveStatus.Entities.ToList();
            //LookUpRATES_ByProject.Entities.ToList();
        }

        int loadedViewModelCount = 0;
        private void OnDetailedEntitiesLoaded(IEnumerable<object> entities)
        {
            loadedViewModelCount += 1;
            if (loadedViewModelCount == 3)
                this.RaisePropertyChanged(x => x.VARIATIONVARIATION_ITEMSDetails);
        }

        private void OnEntitiesChangedCallBack(IEnumerable<object> entities, object sender)
        {
            if (VARIATIONVARIATION_ITEMSDetails != null)
            {
                VARIATIONVARIATION_ITEMSDetails.Refresh();
                MessageBoxService.ShowMessage(CommonResources.Refresh_Notify + StringFormatUtils.GetEntityNameByEntitiesType(entities));
            }

            this.RaisePropertyChanged(x => x.VARIATIONVARIATION_ITEMSDetails);
        }

        /// <summary>
        /// The view model for the BASELINEBASELINE_ITEMS detail collection.
        /// </summary>
        private CollectionViewModel<BASELINE_ITEM, Guid, IBluePrintsEntitiesUnitOfWork> BASELINE_ITEMSDetails_ByVariation
        {
            get 
            { 
                return GetDetailsCollectionViewModel((VARIATIONViewModel x) => x.BASELINE_ITEMSDetails_ByVariation, x => x.BASELINE_ITEMS, x => x.GUID_VARIATION, (x, key) => { x.GUID_VARIATION = key; });
            }
        }

        ///// <summary>
        ///// The view model that contains a look-up collection of BASELINES for the corresponding navigation property in the view.
        ///// </summary>
        //private IEntitiesViewModel<BASELINE> LookUpBASELINE_ByLiveStatus
        //{
        //    get
        //    {
        //        var viewModel = GetLookUpEntitiesViewModel((VARIATIONViewModel x) => x.LookUpBASELINE_ByLiveStatus, x => x.BASELINES, query => query.Where(x => (Entity.GUID_ORIBASELINE != null && x.GUID == this.Entity.GUID_ORIBASELINE) || x.STATUS == BaselineStatus.Live && x.GUID_PROJECT == Entity.GUID_PROJECT));
        //        viewModel.OnEntitiesLoadedCallBack = this.OnDetailedEntitiesLoaded;
        //        return viewModel;
        //    }
        //}

        /// <summary>
        /// The view model that contains a look-up collection of RATES for the corresponding navigation property in the view.
        /// </summary>
        private IEntitiesViewModel<RATE> LookUpRATES_ByProject
        {
            get
            {
                var viewModel = GetLookUpEntitiesViewModel((PROJECTViewModel x) => x.LookUpRATES, x => x.RATES, query => query.Where(x => x.GUID_PROJECT == Entity.GUID_PROJECT));
                viewModel.OnEntitiesLoadedCallBack = this.OnDetailedEntitiesLoaded;
                return viewModel;
            }
        }

        public string WORKPACKDisplayMember
        {
            get
            {
                if (Entity == null || Entity.PROJECT == null || Entity.PROJECT.USELEGACYWORKPACK)
                    return BindableBase.GetPropertyName(() => new WORKPACK().INTERNAL_NAME1);
                else
                    return BindableBase.GetPropertyName(() => new WORKPACK().INTERNAL_NAME2);
            }
        }
        #endregion

        #region View Lookups
        /// <summary>
        /// The view model that contains a look-up collection of PHASES for the corresponding navigation property in the view.
        /// </summary>
        public IEntitiesViewModel<PHASE> LookUpPHASES
        {
            get { return GetLookUpEntitiesViewModel((PROJECTViewModel x) => x.LookUpPHASES, x => x.PHASES, query => query.Where(phase => phase.GUID_PROJECT == Entity.GUID_PROJECT)); }
        }

        /// <summary>
        /// The view model that contains a look-up collection of WORKPACKS for the corresponding navigation property in the view.
        /// </summary>
        public IEntitiesViewModel<WORKPACK> LookUpWORKPACKS
        {
            get { return GetLookUpEntitiesViewModel((VARIATIONViewModel x) => x.LookUpWORKPACKS, x => x.WORKPACKS, query => query.Where(workpack => workpack.GUID_PROJECT == Entity.GUID_PROJECT)); }
        }

        /// <summary>
        /// The view model that contains a look-up collection of AREAS for the corresponding navigation property in the view.
        /// </summary>
        public IEntitiesViewModel<AREA> LookUpAREAS
        {
            get { return GetLookUpEntitiesViewModel((BASELINE_ITEMSViewModel x) => x.LookUpAREAS, x => x.AREAS, query => query.Where(area => area.GUID_PROJECT == Entity.GUID_PROJECT)); }
        }

        /// <summary>
        /// The view model that contains a look-up collection of DEPARTMENTS for the corresponding navigation property in the view.
        /// </summary>
        public IEntitiesViewModel<DEPARTMENT> LookUpDEPARTMENTS
        {
            get { return GetLookUpEntitiesViewModel((BASELINE_ITEMSViewModel x) => x.LookUpDEPARTMENTS, x => x.DEPARTMENTS); }
        }
        /// <summary>
        /// The view model that contains a look-up collection of DISCIPLINES for the corresponding navigation property in the view.
        /// </summary>
        public IEntitiesViewModel<DISCIPLINE> LookUpDISCIPLINES
        {
            get { return GetLookUpEntitiesViewModel((BASELINE_ITEMSViewModel x) => x.LookUpDISCIPLINES, x => x.DISCIPLINES); }
        }

        /// <summary>
        /// The view model that contains a look-up collection of DOCTYPES for the corresponding navigation property in the view.
        /// </summary>
        public IEntitiesViewModel<DOCTYPE> LookUpDOCTYPES
        {
            get { return GetLookUpEntitiesViewModel((BASELINE_ITEMSViewModel x) => x.LookUpDOCTYPES, x => x.DOCTYPES); }
        }
        #endregion

        #region View Interactions
        public void CellValueChanging(CellValueChangedEventArgs e)
        {
            if (e.RowHandle != GridControl.NewItemRowHandle)
                return;

            VARIATION_ITEMInfo activeVARIATION_ITEMInfo = (VARIATION_ITEMInfo)e.Row;
            if (e.Column.FieldName == BindableBase.GetPropertyName(() => new VARIATION_ITEMInfo().BASELINE_ITEM) + "." + BindableBase.GetPropertyName(() => new BASELINE_ITEM().GUID_WORKPACK))
            {
                WORKPACK chosenWORKPACK = LookUpWORKPACKS.Entities.FirstOrDefault(entity => entity.GUID == (Guid)e.Value);
                if (chosenWORKPACK != null)
                {
                    activeVARIATION_ITEMInfo.BASELINE_ITEM.GUID_AREA = chosenWORKPACK.GUID_DAREA;
                    activeVARIATION_ITEMInfo.BASELINE_ITEM.GUID_DOCTYPE = chosenWORKPACK.GUID_DDOCTYPE;
                    activeVARIATION_ITEMInfo.BASELINE_ITEM.GUID_DEPARTMENT = chosenWORKPACK.GUID_DDEPARTMENT;
                    activeVARIATION_ITEMInfo.BASELINE_ITEM.GUID_DISCIPLINE = chosenWORKPACK.GUID_DDISCIPLINE;
                    activeVARIATION_ITEMInfo.BASELINE_ITEM.GUID_PHASE = chosenWORKPACK.PHASE != null ? chosenWORKPACK.GUID_DPHASE : null;
                    var SelectedAREA = LookUpAREAS.Entities.FirstOrDefault(x => x.GUID == chosenWORKPACK.GUID_DAREA);
                    var SelectedDOCTYPE = LookUpDOCTYPES.Entities.FirstOrDefault(x => x.GUID == chosenWORKPACK.GUID_DDOCTYPE);
                    var SelectedDISCIPLINE = LookUpDISCIPLINES.Entities.FirstOrDefault(x => x.GUID == chosenWORKPACK.GUID_DDISCIPLINE);

                    IEnumerable<BASELINE_ITEMInfo> enumerateBASELINE_ITEMS = VARIATIONVARIATION_ITEMSDetails.Entities.Select(x => x.BASELINE_ITEM).AsEnumerable();
                    activeVARIATION_ITEMInfo.BASELINE_ITEM.INTERNAL_NUM = BluePrintDataUtils.BASELINEITEM_Generate_InternalNumber(Entity.PROJECT, activeVARIATION_ITEMInfo.BASELINE_ITEM, enumerateBASELINE_ITEMS, SelectedAREA, SelectedDISCIPLINE, SelectedDOCTYPE);
                    VARIATIONVARIATION_ITEMSDetails.UpdateSelectedEntity();
                }
            }
            else if (e.Column.FieldName == BindableBase.GetPropertyName(() => new VARIATION_ITEMInfo().BASELINE_ITEM) + "." + BindableBase.GetPropertyName(() => new BASELINE_ITEM().GUID_DOCTYPE))
            {
                DOCTYPE chosenDOCTYPE = LookUpDOCTYPES.Entities.FirstOrDefault(entity => entity.GUID == (Guid)e.Value);
                if (chosenDOCTYPE != null && chosenDOCTYPE.GUID_DDEPARTMENT != null)
                {
                    activeVARIATION_ITEMInfo.BASELINE_ITEM.GUID_DEPARTMENT = chosenDOCTYPE.DEPARTMENT.GUID;
                    VARIATIONVARIATION_ITEMSDetails.UpdateSelectedEntity();
                }
            }
        }

        public void CancelBASELINE_ITEM(VARIATION_ITEMInfo projectionEntity)
        {
            if (projectionEntity.ACTION == VariationAction.Add)
                return;

            var oldValue = projectionEntity.ACTION;
            if (projectionEntity.ACTION == VariationAction.Cancel)
                projectionEntity.ACTION = VariationAction.NoAction;
            else
            {
                projectionEntity.VARIATION_UNITS = 0;
                projectionEntity.ACTION = VariationAction.Cancel;
            }

            VARIATIONVARIATION_ITEMSDetails.EntitiesUndoRedoManager.AddUndo(projectionEntity, BindableBase.GetPropertyName(() => new VARIATION_ITEMInfo().ACTION), oldValue, projectionEntity.ACTION, EntityMessageType.Changed);
            VARIATIONVARIATION_ITEMSDetails.Save(VARIATIONVARIATION_ITEMSDetails.SelectedEntity);
        }

        public void CustomUnboundColumnData(GridColumnDataEventArgs e)
        {
            if (e.IsGetData)
            {
                if (VARIATIONVARIATION_ITEMSDetails == null)
                    e.Value = null;
                else
                {

                    VARIATION_ITEMInfo item = VARIATIONVARIATION_ITEMSDetails.Entities[e.ListSourceRowIndex];
                    if (item.BASELINE_ITEM.RATE == null)
                        e.Value = 0;
                    else
                        e.Value = (decimal)item.BASELINE_ITEM.RATE.RATE1 * (item.BASELINE_ITEM.ESTIMATED_HOURS + item.VARIATION_UNITS);
                }
            }
        }

        #endregion

        #region CallBacks
        public bool CanFillDownCallBack(IEnumerable<VARIATION_ITEMInfo> selectedEntities, GridMenuInfo info)
        {
            if (this.Entity.SUBMITTED != null || !selectedEntities.Any(x => x.ACTION == VariationAction.Add))
                return false;

            return true;
        }

        public bool ValidateFillDownCallBack(VARIATION_ITEMInfo fillDownEntity, string fieldName, object fillValue)
        {
            if (fillDownEntity.ACTION != VariationAction.Add)
                return false;

            if (fieldName == BindableBase.GetPropertyName(() => new VARIATION_ITEMInfo().BASELINE_ITEM) + "." + BindableBase.GetPropertyName(() => new BASELINE_ITEM().INTERNAL_NUM))
            {
                string errorMessage = string.Empty;
                VARIATIONVARIATION_ITEMSDetails.IsValidEntityCellValue(fillDownEntity, fieldName, fillValue, ref errorMessage);
                if (errorMessage != string.Empty)
                    return false;
            }

            return true;
        }

        public bool CanBulkDeleteCallBack(IEnumerable<VARIATION_ITEMInfo> selectedEntities)
        {
            return this.Entity.SUBMITTED == null && (selectedEntities != null && selectedEntities.All(x => x.ACTION == VariationAction.Add));
        }

        public void NewProjectionInitializeCallBack(VARIATION_ITEMInfo projectionEntity)
        {
            projectionEntity.ACTION = VariationAction.Add;
        }

        public void ExistingProjectionEditCallBack(VARIATION_ITEMInfo projectionEntity, CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName != BindableBase.GetPropertyName(() => new VARIATION_ITEMInfo().VARIATION_UNITS))
                return;

            if (projectionEntity.ACTION == VariationAction.Add)
                return;

            VariationAction oldValue = projectionEntity.ACTION;

            if (projectionEntity.VARIATION_UNITS == 0)
                projectionEntity.ACTION = VariationAction.NoAction;
            else if (projectionEntity.ACTION == VariationAction.NoAction)
                projectionEntity.ACTION = VariationAction.Append;

            VARIATIONVARIATION_ITEMSDetails.EntitiesUndoRedoManager.AddUndo(projectionEntity, BindableBase.GetPropertyName(() => new VARIATION_ITEMInfo().ACTION), oldValue, projectionEntity.ACTION, EntityMessageType.Changed);
        }

        public void ApplyProjectionPropertiesToEntityCallBack(VARIATION_ITEMInfo projectionEntity, VARIATION_ITEM entity)
        {
            entity.GUID = projectionEntity.GUID;
            entity.ACTION = projectionEntity.ACTION;
            entity.VARIATION_UNITS = projectionEntity.VARIATION_UNITS;
            entity.GUID_VARIATION = this.Entity.GUID;

            //workaround for created because Save() only sets the projection primary key, this is used for property redo where the interceptor only tampers with UPDATED and CREATED is left as null
            if (entity.CREATED.Date.Year == 1)
                projectionEntity.CREATED = DateTime.Now;

            entity.CREATED = projectionEntity.CREATED;

            if (entity.ACTION == VariationAction.Add)
            {
                projectionEntity.BASELINE_ITEM.GUID_VARIATION = this.Entity.GUID;

                var actualEntity = BASELINE_ITEMSDetails_ByVariation.Entities.FirstOrDefault(x => x.GUID == projectionEntity.BASELINE_ITEM.GUID);
                if (actualEntity != null)
                {
                    DataUtils.ShallowCopy(actualEntity, projectionEntity.BASELINE_ITEM);
                }
                else
                {
                    BASELINE_ITEM newBASELINE_ITEM = new BASELINE_ITEM();
                    DataUtils.ShallowCopy(newBASELINE_ITEM, projectionEntity.BASELINE_ITEM);
                    actualEntity = newBASELINE_ITEM;
                }

                BASELINE_ITEMSDetails_ByVariation.Save(actualEntity);
                DataUtils.ShallowCopy(projectionEntity.BASELINE_ITEM, actualEntity); //copy the generated key into projection
            }

            //use BASELINE_ITEM.GUID_ORIGINAL because ProgressItemInfo might not be initialized
            entity.GUID_ORIBASEITEM = projectionEntity.BASELINE_ITEM.GUID_ORIGINAL;
        }

        public void EntityBeforeDeletionCallBack(VARIATION_ITEMInfo undoRedoEntity)
        {
            var actualEntity = BASELINE_ITEMSDetails_ByVariation.Entities.FirstOrDefault(x => x.GUID == undoRedoEntity.BASELINE_ITEM.GUID);
            if (actualEntity != null)
                BASELINE_ITEMSDetails_ByVariation.Delete(actualEntity);
        }
        #endregion

        //#region Entity Data Shaping
        //public void BASELINE_ITEMInfoApplyProjectionPropertiesToEntity(BASELINE_ITEMInfo projectionEntity, BASELINE_ITEMInfo entity)
        //{
        //    DataUtils.ShallowCopy(entity, projectionEntity);
        //}
        //#endregion
    }
}