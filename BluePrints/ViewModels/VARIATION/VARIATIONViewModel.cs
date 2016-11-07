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
using BluePrints.Common.Projections;
using System.Windows.Threading;
using System.Windows;

namespace BluePrints.ViewModels
{
    /// <summary>
    /// Represents the single VARIATION object view model.
    /// </summary>
    public partial class VARIATIONViewModelWrapper
    {
        ///// <summary>
        ///// Creates a new instance of VARIATIONViewModel as a POCO view model.
        ///// </summary>
        ///// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        //public static VARIATIONViewModelWrapper Create(IUnitOfWorkFactory<IBluePrintsEntitiesUnitOfWork> unitOfWorkFactory = null)
        //{
        //    return ViewModelSource.Create(() => new VARIATIONViewModelWrapper(unitOfWorkFactory));
        //}

        ///// <summary>
        ///// Initializes a new instance of the VARIATIONViewModel class.
        ///// This constructor is declared protected to avoid undesired instantiation of the VARIATIONViewModel type without the POCO proxy factory.
        ///// </summary>
        ///// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        //protected VARIATIONViewModelWrapper(IUnitOfWorkFactory<IBluePrintsEntitiesUnitOfWork> unitOfWorkFactory = null)
        //{
        //}

        //#region View Dependencies and Operations
        //public string WORKPACKDisplayMember
        //{
        //    get
        //    {
        //        if (loadPROJECT == null || loadPROJECT.USELEGACYWORKPACK)
        //            return BindableBase.GetPropertyName(() => new WORKPACK().INTERNAL_NAME1);
        //        else
        //            return BindableBase.GetPropertyName(() => new WORKPACK().INTERNAL_NAME2);
        //    }
        //}

        ///// <summary>
        ///// The view model for the VARIATIONVARIATION_ITEMS detail collection.
        ///// </summary>
        //public BASELINE_ITEMSJoinRATESJoinPROGRESS_ITEMSJoinVARIATION_ITEMCollectionViewModel BASELINE_ITEMSJoinRATESJoinPROGRESS_ITEMSJoinVARIATION_ITEMSCollectionViewModel
        //{
        //    get
        //    {

        //        if (mainViewModel == null && IsAllSubEntitiesLoaded)
        //        {
        //            mainViewModel = (BASELINE_ITEMSJoinRATESJoinPROGRESS_ITEMSJoinVARIATION_ITEMCollectionViewModel)mainEntityLoader.GetCollectionViewModel();
        //            mainViewModel.ApplyProjectionPropertiesToEntityCallBack = this.ApplyProjectionPropertiesToEntityCallBack;
        //            mainViewModel.ExistingProjectionEditCallBack = this.ExistingProjectionEditCallBack;
        //            mainViewModel.NewProjectionInitializeCallBack = this.NewProjectionInitializeCallBack;
        //            mainViewModel.EntityBeforeDeletionCallBack = this.EntityBeforeDeletionCallBack;
        //            mainViewModel.CanBulkDeleteCallBack = this.CanBulkDeleteCallBack;
        //            mainViewModel.CanFillDownCallBack = this.CanFillDownCallBack;
        //            mainViewModel.ValidateFillDownCallBack = this.ValidateFillDownCallBack;
        //            mainViewModel.AllowEdit = false;
        //        }

        //        return mainViewModel;
        //    }
        //}
        //#endregion

        //#region Database Operations
        //PROJECT loadPROJECT;
        //PROGRESS livePROGRESS;
        //BASELINE liveBASELINE;
        //VARIATION loadVARIATION;
        //Dispatcher mainThreadDispatcher = Application.Current.Dispatcher;

        //EntitiesLoader<BASELINE> BASELINELoader;
        //EntitiesLoader<PROGRESS> PROGRESSLoader;
        //EntitiesLoader<VARIATION> VARIATIONLoader;

        //EntitiesLoader<PROGRESS_ITEM> PROGRESS_ITEMSLoader;
        //EntitiesLoader<VARIATION_ITEM> VARIATION_ITEMSLoader;

        //EntitiesLoader<WORKPACK> WORKPACKSLoader;
        //EntitiesLoader<PHASE> PHASESLoader;
        //EntitiesLoader<AREA> AREASLoader;
        //EntitiesLoader<DEPARTMENT> DEPARTMENTSLoader;
        //EntitiesLoader<DISCIPLINE> DISCIPLINESLoader;
        //EntitiesLoader<DOCTYPE> DOCTYPESLoader;
        //EntitiesLoader<RATE> RATESLoader;

        //protected override void OnParameterChanged(object parameter)
        //{
        //    //both parameters is required because when entity is first initialized the associating entity (PROJECT) is not loaded
        //    OptionalEntitiesParameter<PROJECT, VARIATION> receiveParameter = (OptionalEntitiesParameter<PROJECT, VARIATION>)parameter;
        //    this.loadPROJECT = receiveParameter.GetFirstEntity();
        //    this.loadVARIATION = receiveParameter.GetSecondEntity();

        //    StartLoading();
        //}


        //public void StartLoading()
        //{
        //    auxiliaryEntitiesLoaders.Clear();
        //    if (loadPROJECT == null)
        //        return;

        //    BASELINELoader = new EntitiesLoader<BASELINE>(() => BASELINECollectionViewModel.Create(null, query => query.Where(x => x.GUID_PROJECT == loadPROJECT.GUID && x.STATUS == BaselineStatus.Live)), OnBASELINEEntitiesLoaded, OnPROGRESSorBASELINEChanged);
        //    auxiliaryEntitiesLoaders.Add(BASELINELoader);
        //}

        //private void OnBASELINEEntitiesLoaded(IEnumerable<BASELINE> entities)
        //{
        //    BASELINELoader.RemoveOnEntitiesFirstLoadedCallBack(null);
        //    if (entities.Count() == 0)
        //    {
        //        mainThreadDispatcher.BeginInvoke(new Action(() => MessageBoxService.ShowMessage(CommonResources.Missing_LiveBASELINE)));
        //        return;
        //    }

        //    liveBASELINE = entities.First();
        //    mainViewModel = null;
        //    mainEntityLoader = null;
        //    subEntitiesLoaders.Clear();
        //    isSubEntitiesAdded = false;

        //    liveBASELINE = entities.First();
        //    mainThreadDispatcher.BeginInvoke(new Action(() => LoadPROGRESSEntities()));
        //}

        //private void LoadPROGRESSEntities()
        //{
        //    if (loadPROJECT == null)
        //        return;

        //    PROGRESSLoader = new EntitiesLoader<PROGRESS>(() => PROGRESSCollectionViewModel.Create(null, query => query.Where(x => x.GUID_PROJECT == loadPROJECT.GUID && x.STATUS == ProgressStatus.Live)), OnPROGRESSEntitiesLoaded, OnPROGRESSorBASELINEChanged);
        //    auxiliaryEntitiesLoaders.Add(PROGRESSLoader);
        //}

        //private void OnPROGRESSEntitiesLoaded(IEnumerable<PROGRESS> entities)
        //{
        //    PROGRESSLoader.RemoveOnEntitiesFirstLoadedCallBack(null);
        //    if(entities.Count() == 0)
        //    {
        //        mainThreadDispatcher.BeginInvoke(new Action(() => MessageBoxService.ShowMessage(CommonResources.Missing_LivePROGRESS)));
        //        return;
        //    }

        //    livePROGRESS = entities.First();
        //    mainThreadDispatcher.BeginInvoke(new Action(() => LoadVARIATIONEntities()));
        //}

        //private void LoadVARIATIONEntities()
        //{
        //    if (loadVARIATION == null)
        //        return;

        //    VARIATIONLoader = new EntitiesLoader<VARIATION>(() => VARIATIONCollectionViewModel.Create(null, query => query.Where(x => x.GUID == loadVARIATION.GUID), OnVARIATIONEntitiesLoaded, OnPROGRESSorBASELINEChanged);
        //    auxiliaryEntitiesLoaders.Add(VARIATIONLoader);
        //}

        //private void OnVARIATIONEntitiesLoaded(IEnumerable<VARIATION> entities)
        //{
        //    VARIATIONLoader.RemoveOnEntitiesFirstLoadedCallBack(null);
        //    if (entities.Count() == 0)
        //    {
        //        mainThreadDispatcher.BeginInvoke(new Action(() => MessageBoxService.ShowMessage(CommonResources.View_Removed)));
        //        return;
        //    }

        //    loadVARIATION = entities.First();
        //    mainThreadDispatcher.BeginInvoke(new Action(() => AfterPROGRESSAndBASELINEAndVARIATIONEntitiesLoaded()));
        //}

        //private void AfterPROGRESSAndBASELINEAndVARIATIONEntitiesLoaded()
        //{
        //    //Auxiliary Entities
        //    PHASESLoader = new EntitiesLoader<PHASE>(() => PHASECollectionViewModel.Create(null, query => query.Where(x => x.GUID_PROJECT == loadPROJECT.GUID)));
        //    auxiliaryEntitiesLoaders.Add(PHASESLoader);
        //    AREASLoader = new EntitiesLoader<AREA>(() => AREACollectionViewModel.Create(null, query => query.Where(x => x.GUID_PROJECT == loadPROJECT.GUID)));
        //    auxiliaryEntitiesLoaders.Add(AREASLoader);
        //    WORKPACKSLoader = new EntitiesLoader<WORKPACK>(() => WORKPACKCollectionViewModel.Create(null, query => query.Where(x => x.GUID_PROJECT == loadPROJECT.GUID)));
        //    auxiliaryEntitiesLoaders.Add(WORKPACKSLoader);
        //    DEPARTMENTSLoader = new EntitiesLoader<DEPARTMENT>(() => DEPARTMENTCollectionViewModel.Create(null));
        //    auxiliaryEntitiesLoaders.Add(DEPARTMENTSLoader);
        //    DISCIPLINESLoader = new EntitiesLoader<DISCIPLINE>(() => DISCIPLINECollectionViewModel.Create(null));
        //    auxiliaryEntitiesLoaders.Add(DISCIPLINESLoader);
        //    DOCTYPESLoader = new EntitiesLoader<DOCTYPE>(() => DOCTYPECollectionViewModel.Create(null));
        //    auxiliaryEntitiesLoaders.Add(DOCTYPESLoader);

        //    //Sub Entities
        //    PROGRESS_ITEMSLoader = new EntitiesLoader<PROGRESS_ITEM>(() => PROGRESS_ITEMSCollectionViewModel.Create(null, query => query.Where(x => x.GUID_PROGRESS == livePROGRESS.GUID)), OnSubEntitiesLoaded);
        //    subEntitiesLoaders.Add(PROGRESS_ITEMSLoader);
        //    VARIATION_ITEMSLoader = new EntitiesLoader<VARIATION_ITEM>(() => VARIATION_ITEMSCollectionViewModel.Create(null, query => query.Where(x => x.GUID_VARIATION == loadVARIATION.GUID)), OnSubEntitiesLoaded);
        //    subEntitiesLoaders.Add(VARIATION_ITEMSLoader);
        //    RATESLoader = new EntitiesLoader<RATE>(() => RATECollectionViewModel.Create(null, query => query.Where(x => x.GUID_PROJECT == loadPROJECT.GUID)), OnSubEntitiesLoaded);
        //    subEntitiesLoaders.Add(RATESLoader);
        //    PROGRESS_ITEMSLoader = new EntitiesLoader<PROGRESS_ITEM>(() => PROGRESS_ITEMSCollectionViewModel.Create(null, query => query.Where(x => x.GUID_PROGRESS == livePROGRESS.GUID)), OnSubEntitiesLoaded);
        //    subEntitiesLoaders.Add(PROGRESS_ITEMSLoader);
        //    isSubEntitiesAdded = true;
        //}

        //protected override void OnSubEntitiesLoaded(IEnumerable<object> entities)
        //{
        //    if (IsMainEntityLoaded)
        //        mainViewModel.RefreshWithoutClearingUndoManager();
        //    else if (IsAllSubEntitiesLoaded)
        //        mainThreadDispatcher.BeginInvoke(new Action(() => AfterSubEntitiesLoaded()));
        //}

        //protected override void AfterSubEntitiesLoaded()
        //{
        //    mainEntityLoader = new EntitiesLoader<BASELINE_ITEMJoinRATEJoinPROGRESS_ITEMJoinVARIATION_ITEM>(() => BASELINE_ITEMSJoinRATESJoinPROGRESS_ITEMSJoinVARIATION_ITEMCollectionViewModel.Create(query => BASELINE_ITEMSJoinRATESJoinPROGRESS_ITEMSJoinVARIATION_ITEMSQueries.JoinRATESAndPROGRESS_ITEMSAndVARIATION_ITEMSOnBASELINE_ITEMS(query, PROGRESSLoader.GetSingleObjectFunc, BASELINELoader.GetSingleObjectFunc, VARIATIONLoader.GetSingleObjectFunc, PROGRESS_ITEMSLoader.GetCollectionFunc, VARIATION_ITEMSLoader.GetCollectionFunc, RATESLoader.GetCollectionFunc)), OnMainEntitiesFirstLoaded);
        //}

        //protected override void OnMainEntitiesFirstLoaded(IEnumerable<BASELINE_ITEMJoinRATEJoinPROGRESS_ITEMJoinVARIATION_ITEM> entities)
        //{
        //    if (BASELINELoader.GetSingleObjectFunc() != null)
        //    {
        //        VARIATION_ITEMSLoader.RemoveOnEntitiesFirstLoadedCallBack(this.OnVARIATION_ITEMSRefresh); //route the refresh so that it can be mapped to baseline

        //        if (liveBASELINE == null || livePROGRESS == null)
        //            return;

        //        base.OnMainEntitiesFirstLoaded(entities);
        //        mainThreadDispatcher.BeginInvoke(new Action(() => NotifyMainEntityLoaded()));
        //    }
        //}

        ///// <summary>
        ///// Translates VARIATION_ITEM notification into BASELINE_ITEM notification
        ///// </summary>
        //private void OnVARIATION_ITEMSRefresh(IEnumerable<VARIATION_ITEM> entities)
        //{
        //    if (entities.Count() > 0)
        //    {
        //        BASELINE_ITEMJoinRATEJoinPROGRESS_ITEMJoinVARIATION_ITEM findEntity = mainViewModel.Entities.FirstOrDefault(x => x.VARIATION_ITEM != null && x.VARIATION_ITEM.GUID == entities.First().GUID);
        //        if (findEntity != null)
        //            mainThreadDispatcher.BeginInvoke(new Action(() => Messenger.Default.Send(new EntityMessage<BASELINE_ITEM, Guid>(findEntity.GUID, EntityMessageType.Changed, this))));
        //    }
        //}

        //private void NotifyMainEntityLoaded()
        //{
        //    this.RaisePropertyChanged(x => x.BASELINE_ITEMSJoinRATESJoinPROGRESS_ITEMSJoinVARIATION_ITEMSCollectionViewModel);
        //}
        //#endregion

        //#region View Interactions
        //public void CellValueChanging(CellValueChangedEventArgs e)
        //{
        //    if (e.RowHandle != GridControl.NewItemRowHandle)
        //        return;

        //    BASELINE_ITEMJoinRATEJoinPROGRESS_ITEMJoinVARIATION_ITEM activeEntity = (BASELINE_ITEMJoinRATEJoinPROGRESS_ITEMJoinVARIATION_ITEM)e.Row;

        //    if (e.Column.FieldName == BindableBase.GetPropertyName(() => new BASELINE_ITEMJoinRATEJoinPROGRESS_ITEMJoinVARIATION_ITEM().BASELINE_ITEMJoinRATE) 
        //        + "."
        //        + BindableBase.GetPropertyName(() => new BASELINE_ITEMJoinRATE().BASELINE_ITEM)
        //        + "."
        //        + BindableBase.GetPropertyName(() => new BASELINE_ITEM().GUID_WORKPACK))
        //    {
        //        WORKPACK chosenWORKPACK = WORKPACKSLoader.Collection.FirstOrDefault(entity => entity.GUID == (Guid)e.Value);
        //        if (chosenWORKPACK != null)
        //        {
        //            activeEntity.BASELINE_ITEMJoinRATE.BASELINE_ITEM.GUID_AREA = chosenWORKPACK.GUID_DAREA;
        //            activeEntity.BASELINE_ITEMJoinRATE.BASELINE_ITEM.GUID_DOCTYPE = chosenWORKPACK.GUID_DDOCTYPE;
        //            activeEntity.BASELINE_ITEMJoinRATE.BASELINE_ITEM.GUID_DEPARTMENT = chosenWORKPACK.GUID_DDEPARTMENT;
        //            activeEntity.BASELINE_ITEMJoinRATE.BASELINE_ITEM.GUID_DISCIPLINE = chosenWORKPACK.GUID_DDISCIPLINE;
        //            activeEntity.BASELINE_ITEMJoinRATE.BASELINE_ITEM.GUID_PHASE = chosenWORKPACK.PHASE != null ? chosenWORKPACK.GUID_DPHASE : null;

        //            IEnumerable<BASELINE_ITEM> otherBASELINE_ITEMS = mainViewModel.Entities.Select(x => x.BASELINE_ITEMJoinRATE.BASELINE_ITEM).AsEnumerable();
        //            activeEntity.BASELINE_ITEMJoinRATE.BASELINE_ITEM.INTERNAL_NUM = BluePrintDataUtils.BASELINEITEM_Generate_InternalNumber(loadPROJECT, activeEntity.BASELINE_ITEMJoinRATE.BASELINE_ITEM, otherBASELINE_ITEMS, chosenWORKPACK.AREA, chosenWORKPACK.DISCIPLINE, chosenWORKPACK.DOCTYPE);
        //            BASELINE_ITEMSJoinRATESJoinPROGRESS_ITEMSJoinVARIATION_ITEMSCollectionViewModel.UpdateSelectedEntity();
        //        }
        //    }
        //    else if (e.Column.FieldName == BindableBase.GetPropertyName(() => new BASELINE_ITEMJoinRATEJoinPROGRESS_ITEMJoinVARIATION_ITEM().BASELINE_ITEMJoinRATE
        //        + "."
        //        + BindableBase.GetPropertyName(() => new VARIATION_ITEMInfo().BASELINE_ITEM) 
        //        + "." 
        //        + BindableBase.GetPropertyName(() => new BASELINE_ITEM().GUID_DOCTYPE)))
        //    {
        //        DOCTYPE chosenDOCTYPE = DOCTYPESLoader.Collection.FirstOrDefault(entity => entity.GUID == (Guid)e.Value);
        //        if (chosenDOCTYPE != null && chosenDOCTYPE.GUID_DDEPARTMENT != null)
        //        {
        //            activeEntity.BASELINE_ITEMJoinRATE.BASELINE_ITEM.GUID_DEPARTMENT = chosenDOCTYPE.DEPARTMENT.GUID;
        //            BASELINE_ITEMSJoinRATESJoinPROGRESS_ITEMSJoinVARIATION_ITEMSCollectionViewModel.UpdateSelectedEntity();
        //        }
        //    }
        //}

        //public void CancelBASELINE_ITEM(BASELINE_ITEMJoinRATEJoinPROGRESS_ITEMJoinVARIATION_ITEM projectionEntity)
        //{
        //    if (projectionEntity.VARIATION_ITEM.ACTION == VariationAction.Add)
        //        return;

        //    var oldValue = projectionEntity.VARIATION_ITEM.ACTION;
        //    if (projectionEntity.VARIATION_ITEM.ACTION == VariationAction.Cancel)
        //        projectionEntity.VARIATION_ITEM.ACTION = VariationAction.NoAction;
        //    else
        //    {
        //        projectionEntity.VARIATION_ITEM.VARIATION_UNITS = 0;
        //        projectionEntity.VARIATION_ITEM.ACTION = VariationAction.Cancel;
        //    }

        //    BASELINE_ITEMSJoinRATESJoinPROGRESS_ITEMSJoinVARIATION_ITEMSCollectionViewModel.EntitiesUndoRedoManager.AddUndo(projectionEntity, BindableBase.GetPropertyName(() => new VARIATION_ITEMInfo().ACTION), oldValue, projectionEntity.VARIATION_ITEM.ACTION, EntityMessageType.Changed);
        //    BASELINE_ITEMSJoinRATESJoinPROGRESS_ITEMSJoinVARIATION_ITEMSCollectionViewModel.Save(BASELINE_ITEMSJoinRATESJoinPROGRESS_ITEMSJoinVARIATION_ITEMSCollectionViewModel.SelectedEntity);
        //}
        //#endregion

        //#region CallBacks
        //public bool CanFillDownCallBack(IEnumerable<BASELINE_ITEMJoinRATEJoinPROGRESS_ITEMJoinVARIATION_ITEM> selectedEntities, GridMenuInfo info)
        //{
        //    if (loadVARIATION.SUBMITTED != null || !selectedEntities.Any(x => x.VARIATION_ITEM.ACTION == VariationAction.Add))
        //        return false;

        //    return true;
        //}

        //public bool ValidateFillDownCallBack(BASELINE_ITEMJoinRATEJoinPROGRESS_ITEMJoinVARIATION_ITEM fillDownEntity, string fieldName, object fillValue)
        //{
        //    if (fillDownEntity.VARIATION_ITEM.ACTION != VariationAction.Add)
        //        return false;

        //    if (fieldName == BindableBase.GetPropertyName(() => new BASELINE_ITEMJoinRATEJoinPROGRESS_ITEMJoinVARIATION_ITEM().BASELINE_ITEMJoinRATE) 
        //        + "."
        //        + BindableBase.GetPropertyName(() => new BASELINE_ITEMJoinRATE().BASELINE_ITEM)
        //        + "."
        //        + BindableBase.GetPropertyName(() => new BASELINE_ITEM().INTERNAL_NUM))
        //    {
        //        string errorMessage = string.Empty;
        //        BASELINE_ITEMSJoinRATESJoinPROGRESS_ITEMSJoinVARIATION_ITEMSCollectionViewModel.IsValidEntityCellValue(fillDownEntity, fieldName, fillValue, ref errorMessage);
        //        if (errorMessage != string.Empty)
        //            return false;
        //    }

        //    return true;
        //}

        //public bool CanBulkDeleteCallBack(IEnumerable<BASELINE_ITEMJoinRATEJoinPROGRESS_ITEMJoinVARIATION_ITEM> selectedEntities)
        //{
        //    return this.loadVARIATION.SUBMITTED == null && (selectedEntities != null && selectedEntities.All(x => x.VARIATION_ITEM.ACTION == VariationAction.Add));
        //}

        //public void NewProjectionInitializeCallBack(VARIATION_ITEMInfo projectionEntity)
        //{
        //    projectionEntity.ACTION = VariationAction.Add;
        //}

        //public void ExistingProjectionEditCallBack(BASELINE_ITEMJoinRATEJoinPROGRESS_ITEMJoinVARIATION_ITEM projectionEntity, CellValueChangedEventArgs e)
        //{
        //    if (e.Column.FieldName != BindableBase.GetPropertyName(() => new BASELINE_ITEMJoinRATEJoinPROGRESS_ITEMJoinVARIATION_ITEM().VARIATION_ITEM + "." + BindableBase.GetPropertyName(() => new VARIATION_ITEM().VARIATION_UNITS)))
        //        return;

        //    if (projectionEntity.VARIATION_ITEM.ACTION == VariationAction.Add)
        //        return;

        //    VariationAction oldValue = projectionEntity.VARIATION_ITEM.ACTION;

        //    if (projectionEntity.VARIATION_ITEM.VARIATION_UNITS == 0)
        //        projectionEntity.VARIATION_ITEM.ACTION = VariationAction.NoAction;
        //    else if (projectionEntity.VARIATION_ITEM.ACTION == VariationAction.NoAction)
        //        projectionEntity.VARIATION_ITEM.ACTION = VariationAction.Append;

        //    BASELINE_ITEMSJoinRATESJoinPROGRESS_ITEMSJoinVARIATION_ITEMSCollectionViewModel.EntitiesUndoRedoManager.AddUndo(projectionEntity, BindableBase.GetPropertyName(() => new BASELINE_ITEMJoinRATEJoinPROGRESS_ITEMJoinVARIATION_ITEM().VARIATION_ITEM) + "." + BindableBase.GetPropertyName(() => new VARIATION_ITEMInfo().ACTION), oldValue, projectionEntity.VARIATION_ITEM.ACTION, EntityMessageType.Changed);
        //}

        //public void ApplyProjectionPropertiesToEntityCallBack(BASELINE_ITEMJoinRATEJoinPROGRESS_ITEMJoinVARIATION_ITEM projectionEntity, BASELINE_ITEM entity)
        //{
        //    entity.GUID = projectionEntity.GUID;
        //    entity.ACTION = projectionEntity.ACTION;
        //    entity.VARIATION_UNITS = projectionEntity.VARIATION_UNITS;
        //    entity.GUID_VARIATION = this.Entity.GUID;

        //    //workaround for created because Save() only sets the projection primary key, this is used for property redo where the interceptor only tampers with UPDATED and CREATED is left as null
        //    if (entity.CREATED.Date.Year == 1)
        //        projectionEntity.CREATED = DateTime.Now;

        //    entity.CREATED = projectionEntity.CREATED;

        //    if (entity.ACTION == VariationAction.Add)
        //    {
        //        projectionEntity.BASELINE_ITEM.GUID_VARIATION = this.Entity.GUID;

        //        var actualEntity = BASELINE_ITEMSDetails_ByVariation.Entities.FirstOrDefault(x => x.GUID == projectionEntity.BASELINE_ITEM.GUID);
        //        if (actualEntity != null)
        //        {
        //            DataUtils.ShallowCopy(actualEntity, projectionEntity.BASELINE_ITEM);
        //        }
        //        else
        //        {
        //            BASELINE_ITEM newBASELINE_ITEM = new BASELINE_ITEM();
        //            DataUtils.ShallowCopy(newBASELINE_ITEM, projectionEntity.BASELINE_ITEM);
        //            actualEntity = newBASELINE_ITEM;
        //        }

        //        BASELINE_ITEMSDetails_ByVariation.Save(actualEntity);
        //        DataUtils.ShallowCopy(projectionEntity.BASELINE_ITEM, actualEntity); //copy the generated key into projection
        //    }

        //    //use BASELINE_ITEM.GUID_ORIGINAL because ProgressItemInfo might not be initialized
        //    entity.GUID_ORIBASEITEM = projectionEntity.BASELINE_ITEM.GUID_ORIGINAL;
        //}

        //public void EntityBeforeDeletionCallBack(VARIATION_ITEMInfo undoRedoEntity)
        //{
        //    var actualEntity = BASELINE_ITEMSDetails_ByVariation.Entities.FirstOrDefault(x => x.GUID == undoRedoEntity.BASELINE_ITEM.GUID);
        //    if (actualEntity != null)
        //        BASELINE_ITEMSDetails_ByVariation.Delete(actualEntity);
        //}
        //#endregion
    }
}