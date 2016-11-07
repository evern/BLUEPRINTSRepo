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
using DevExpress.Xpf.Grid;
using BluePrints.Common;
using BluePrints.Common.Helpers;
using BluePrints.Common.ViewModel.Reporting;
using System.Windows.Threading;
using BluePrints.Views;
using BluePrints.Reports;
using System.IO;
using DevExpress.Xpf.Printing;
using System.Windows;
using System.Threading.Tasks;
using BluePrints.Common.Projections;
using System.ComponentModel;

namespace BluePrints.ViewModels
{
    /// <summary>
    /// Represents the single PROGRESS object view model.
    /// </summary>
    public partial class PROGRESS_ITEMSViewModelWrapper : CollectionViewModelsWrapper<BASELINE_ITEM, BASELINE_ITEMJoinRATEJoinPROGRESS_ITEM, Guid, IBluePrintsEntitiesUnitOfWork, CollectionViewModel<BASELINE_ITEM, BASELINE_ITEMJoinRATEJoinPROGRESS_ITEM, Guid, IBluePrintsEntitiesUnitOfWork>>
    {
        /// <summary>
        /// Creates a new instance of PROGRESSViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static PROGRESS_ITEMSViewModelWrapper Create()
        {
            return ViewModelSource.Create(() => new PROGRESS_ITEMSViewModelWrapper());
        }

        ///// <summary>
        ///// Initializes a new instance of the PROGRESSViewModel class.
        ///// This constructor is declared protected to avoid undesired instantiation of the PROGRESSViewModel type without the POCO proxy factory.
        ///// </summary>
        ///// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        //protected PROGRESSViewModelWrapper()
        //{
        //    ViewName = "PROGRESSViewModelWrapper";
        //}

        //#region View Dependencies and Operations
        ///// <summary>
        ///// The view model for the PROGRESSPROGRESS_ITEMS detail collection.
        ///// </summary>
        //public BASELINE_ITEMSJoinRATESJoinPROGRESS_ITEMSCollectionViewModel BASELINE_ITEMSJoinRATESJoinPROGRESS_ITEMSCollectionViewModel
        //{
        //    get
        //    {
        //        if (mainViewModel == null && IsAllSubEntitiesLoaded)
        //        {
        //            mainViewModel = (BASELINE_ITEMSJoinRATESJoinPROGRESS_ITEMSCollectionViewModel)mainEntityLoader.GetCollectionViewModel();
        //            mainViewModel.ValidateFillDownCallBack = this.ValidateFillDownCallBack;
        //            mainViewModel.OnEntitiesLoadedCallBack = this.OnMainEntitiesFirstLoaded;
        //            mainViewModel.HijackSaveOperation = this.HijackSave;
        //            mainViewModel.HijackBulkSaveOperation = this.HijackBulkSaveOperation;
        //            mainViewModel.OnEntitiesChangedCallBack = this.OnPROGRESSorBASELINEChanged;
        //        }

        //        return mainViewModel;
        //    }
        //}

        //public Action ShowNewWorkpack;
        //public string DataDate
        //{
        //    get
        //    {
        //        if (loadPROGRESS == null || loadPROGRESS.DATA_DATE == null)
        //            return string.Empty;

        //        return loadPROGRESS.DATA_DATE.ToString("g");
        //    }
        //}

        //public bool CanDateBackward()
        //{
        //    if (BASELINE_ITEMSJoinRATESJoinPROGRESS_ITEMSCollectionViewModel == null || BASELINE_ITEMSJoinRATESJoinPROGRESS_ITEMSCollectionViewModel.IsLoading)
        //        return false;

        //    if (loadPROGRESS.DATA_DATE > loadPROGRESS.PROGRESS_START)
        //        return true;

        //    return false;
        //}

        //public bool CanDateForward()
        //{
        //    if (BASELINE_ITEMSJoinRATESJoinPROGRESS_ITEMSCollectionViewModel == null || BASELINE_ITEMSJoinRATESJoinPROGRESS_ITEMSCollectionViewModel.IsLoading)
        //        return false;

        //    return true;
        //}

        //public void DateForward()
        //{
        //    DateChange(true);
        //}

        //public void DateBackward()
        //{
        //    DateChange(false);
        //}

        //private void DateChange(bool isForward)
        //{
        //    var interval = ISupportProgressReportingExtensions.ConvertProgressIntervalToPeriod(loadPROGRESS);
        //    int multiplier = isForward == true ? 1 : -1;
        //    loadPROGRESS.DATA_DATE = loadPROGRESS.DATA_DATE.AddDays(multiplier * interval.Days);
        //    ((PROGRESSCollectionViewModel)PROGRESSLoader.GetCollectionViewModel()).Save(loadPROGRESS);
        //    BASELINE_ITEMSJoinRATESJoinPROGRESS_ITEMSCollectionViewModel.Refresh();
        //    this.RaisePropertyChanged(x => x.DataDate);
        //}

        //public bool ValidateFillDownCallBack(BASELINE_ITEMJoinRATEJoinPROGRESS_ITEM fillDownEntity, string fieldName, object fillValue)
        //{
        //    if (fieldName != BindableBase.GetPropertyName(() => new BASELINE_ITEMJoinRATEJoinPROGRESS_ITEM().TOTAL_EARNED_PERCENTAGE))
        //        return false;

        //    decimal newPercentage = (decimal)fillValue;
        //    if (newPercentage > fillDownEntity.MaxPercentage)
        //        return false;
        //    else if (newPercentage < fillDownEntity.MinPercentage)
        //        return false;

        //    return true;
        //}
        //#endregion


        #region Database Operation
        PROJECT loadPROJECT;
        PROGRESS loadPROGRESS;
        BASELINE loadBASELINE;
        bool isQueryForLiveStatus;
        IUnitOfWorkFactory<IBluePrintsEntitiesUnitOfWork> bluePrintsUnitOfWorkFactory = BluePrintsEntitiesUnitOfWorkSource.GetUnitOfWorkFactory();

        protected override void InitializeParameters(object parameter)
        {
            OptionalEntitiesParameter<PROJECT, PROGRESS> receiveParameter = (OptionalEntitiesParameter<PROJECT, PROGRESS>)parameter;
            this.loadPROJECT = receiveParameter.GetFirstEntity();
            this.loadPROGRESS = receiveParameter.GetSecondEntity();

            if (this.loadPROJECT != null)
                isQueryForLiveStatus = true;
        }

        public override void InitializeAndLoadEntitiesLoaderDescription()
        {
            MainViewModel = null;
            loaderCollection = new EntitiesLoaderDescriptionCollection(this);
            loaderCollection.AddEntitiesLoader<PROJECT, PROJECT, Guid, IBluePrintsEntitiesUnitOfWork>(0, bluePrintsUnitOfWorkFactory, x => x.PROJECTS, PROJECTProjectionFunc, null, isContinueLoadingPROJECT, OnEntitiesChanged);
            loaderCollection.AddEntitiesLoader<BASELINE, BASELINE, Guid, IBluePrintsEntitiesUnitOfWork>(1, bluePrintsUnitOfWorkFactory, x => x.BASELINES, BASELINEProjectionFunc, typeof(PROJECT), isContinueLoadingBASELINE, OnEntitiesChanged);
            loaderCollection.AddEntitiesLoader<PROGRESS, PROGRESS, Guid, IBluePrintsEntitiesUnitOfWork>(2, bluePrintsUnitOfWorkFactory, x => x.PROGRESSES, PROGRESSProjectionFunc, typeof(BASELINE), isContinueLoadingPROGRESS, OnEntitiesChanged);
            loaderCollection.AddEntitiesLoader<WORKPACK, WORKPACK, Guid, IBluePrintsEntitiesUnitOfWork>(3, bluePrintsUnitOfWorkFactory, x => x.WORKPACKS, WORKPACKProjectionFunc, typeof(BASELINE));
            loaderCollection.AddEntitiesLoader<PROGRESS_ITEM, PROGRESS_ITEM, Guid, IBluePrintsEntitiesUnitOfWork>(4, bluePrintsUnitOfWorkFactory, x => x.PROGRESS_ITEMS, PROGRESS_ITEMProjectionFunc, typeof(PROGRESS));
            loaderCollection.AddEntitiesLoader<PROJECT_REPORT, PROJECT_REPORT, Guid, IBluePrintsEntitiesUnitOfWork>(5, bluePrintsUnitOfWorkFactory, x => x.PROJECT_REPORTS, PROJECT_REPORTProjectionFunc, typeof(PROJECT));
            loaderCollection.AddEntitiesLoader<DEPARTMENT, DEPARTMENT, Guid, IBluePrintsEntitiesUnitOfWork>(5, bluePrintsUnitOfWorkFactory, x => x.DEPARTMENTS);
            loaderCollection.AddEntitiesLoader<DISCIPLINE, DISCIPLINE, Guid, IBluePrintsEntitiesUnitOfWork>(6, bluePrintsUnitOfWorkFactory, x => x.DISCIPLINES);
            loaderCollection.AddEntitiesLoader<DOCTYPE, DOCTYPE, Guid, IBluePrintsEntitiesUnitOfWork>(7, bluePrintsUnitOfWorkFactory, x => x.DOCTYPES);
            loaderCollection.AddEntitiesLoader<RATE, RATE, Guid, IBluePrintsEntitiesUnitOfWork>(8, bluePrintsUnitOfWorkFactory, x => x.RATES, RATEProjectionFunc);
            InvokeEntitiesLoaderDescriptionLoading();
        }

        bool isContinueLoadingPROJECT(IEnumerable<PROJECT> entities)
        {
            if (entities.Count() == 0)
            {
                mainThreadDispatcher.BeginInvoke(new Action(() => MessageBoxService.ShowMessage(string.Format(CommonResources.Notify_View_Removed, "PROJECT"))));
                return false;
            }

            this.loadPROJECT = entities.First();
            return true;
        }

        bool isContinueLoadingPROGRESS(IEnumerable<PROGRESS> entities)
        {
            if (entities.Count() == 0)
            {
                mainThreadDispatcher.BeginInvoke(new Action(() => MessageBoxService.ShowMessage(string.Format(CommonResources.Notify_View_Removed, "PROGRESS"))));
                return false;
            }

            this.loadPROGRESS = entities.First();
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
                return query => query.Where(x => x.GUID == this.loadPROGRESS.GUID_PROJECT);
        }

        Func<IRepositoryQuery<PROGRESS>, IQueryable<PROGRESS>> PROGRESSProjectionFunc()
        {
            if (isQueryForLiveStatus)
                return query => query.Where(x => x.GUID_PROJECT == this.loadPROJECT.GUID && x.STATUS == ProgressStatus.Live);
            else
                return query => query.Where(x => x.GUID == this.loadPROGRESS.GUID);
        }

        Func<IRepositoryQuery<BASELINE>, IQueryable<BASELINE>> BASELINEProjectionFunc()
        {
            return query => query.Where(x => x.GUID_PROJECT == this.loadPROJECT.GUID && x.STATUS == BaselineStatus.Live);
        }

        Func<IRepositoryQuery<WORKPACK>, IQueryable<WORKPACK>> WORKPACKProjectionFunc()
        {
            return query => query.Where(x => x.GUID_PROJECT == loadPROJECT.GUID);
        }

        Func<IRepositoryQuery<PROGRESS_ITEM>, IQueryable<PROGRESS_ITEM>> PROGRESS_ITEMProjectionFunc()
        {
            return query => query.Where(x => x.GUID_PROGRESS == loadPROGRESS.GUID);
        }

        Func<IRepositoryQuery<PROJECT_REPORT>, IQueryable<PROJECT_REPORT>> PROJECT_REPORTProjectionFunc()
        {
            return query => query.Where(x => x.GUID_PROJECT == loadPROJECT.GUID);
        }

        Func<IRepositoryQuery<RATE>, IQueryable<RATE>> RATEProjectionFunc()
        {
            return query => query.Where(x => x.GUID_PROJECT == loadPROJECT.GUID);
        }

        protected override void OnAllEntitiesCollectionLoaded()
        {
            CreateMainViewModel(this.bluePrintsUnitOfWorkFactory, x => x.BASELINE_ITEMS);
            mainThreadDispatcher.BeginInvoke(new Action(() => mainEntityLoader.CreateCollectionViewModel()));
        }

        protected override Func<IRepositoryQuery<BASELINE_ITEM>, IQueryable<BASELINE_ITEMJoinRATEJoinPROGRESS_ITEM>> ConstructMainViewModelProjection()
        {
            Func<IQueryable<RATE>> getRATESFunc = loaderCollection.GetCollectionFunc<RATE>();
            Func<IQueryable<PROGRESS_ITEM>> getPROGRESS_ITEMSFunc = loaderCollection.GetCollectionFunc<PROGRESS_ITEM>();
            Func<BASELINE> getBASELINEFunc = loaderCollection.GetObjectFunc<BASELINE>();
            Func<PROGRESS> getPROGRESSFunc = loaderCollection.GetObjectFunc<PROGRESS>();

            return query => BASELINE_ITEMSJoinRATESJoinPROGRESS_ITEMSQueries.JoinRATESAndPROGRESS_ITEMSOnBASELINE_ITEMS(query, getPROGRESSFunc, getBASELINEFunc, getPROGRESS_ITEMSFunc, getRATESFunc);
        }

        protected override void AssignCallBacksAndRaisePropertyChange(CollectionViewModel<BASELINE_ITEM, BASELINE_ITEMJoinRATEJoinPROGRESS_ITEM, Guid, IBluePrintsEntitiesUnitOfWork> mainViewModel)
        {
            mainViewModel.HijackBulkSaveOperation = this.HijackBulkSaveOperation;
            ProjectSummaryBuilder projectSummaryBuilder = new ProjectSummaryBuilder(DefaultSummaryCalculation.Create(), mainViewModel.Entities, loaderCollection.GetObject<PROGRESS>(), loaderCollection.GetObject<BASELINE>());
            PROGRESS_ITEMSummaryManufacturer summaryRollUp = new PROGRESS_ITEMSummaryManufacturer();
            summaryRollUp.Manufacture(projectSummaryBuilder);

            mainThreadDispatcher.BeginInvoke(new Action(() => this.RaisePropertiesChanged()));
        }

        #region Collection Call Backs
        private void HijackBulkSaveOperation(IEnumerable<BASELINE_ITEMJoinRATEJoinPROGRESS_ITEM> entities)
        {
            foreach (var entity in entities)
            {
                HijackSave(entity);
            }
        }

        public void HijackSave(BASELINE_ITEMJoinRATEJoinPROGRESS_ITEM projectionEntity)
        {
            PROGRESS_ITEMSCollectionViewModel PROGRESS_ITEMSCollectionViewModel = (PROGRESS_ITEMSCollectionViewModel)loaderCollection.GetViewModel<PROGRESS_ITEM>();
            PROGRESS_ITEM savePROGRESS_ITEM = projectionEntity.PROGRESS_ITEMCurrent;
            savePROGRESS_ITEM.EARNED_DATE = loadPROGRESS.DATA_DATE;
            savePROGRESS_ITEM.GUID_PROGRESS = loadPROGRESS.GUID;
            savePROGRESS_ITEM.GUID_ORIBASEITEM = projectionEntity.BASELINE_ITEMJoinRATE.BASELINE_ITEM.GUID_ORIGINAL;
            //workaround for created because Save() only sets the projection primary key, this is used for property redo where the interceptor only tampers with UPDATED and CREATED is left as null
            if (savePROGRESS_ITEM.CREATED.Date.Year == 1)
                savePROGRESS_ITEM.CREATED = DateTime.Now;

            savePROGRESS_ITEM = projectionEntity.PROGRESS_ITEMCurrent;

            PROGRESS_ITEMSCollectionViewModel.Save(savePROGRESS_ITEM);
        }
        #endregion
        #endregion

        #region View Properties
        /// <summary>
        /// The view name to be used when saving layout for IDocumentContent
        /// </summary>
        protected override string ViewName
        {
            get
            {
                return "PROGRESS_ITEMSViewModelWrapper";
            }
        }
        #endregion
        //#region Database Operations
        //PROJECT loadPROJECT;
        //PROGRESS loadPROGRESS;
        //BASELINE liveBASELINE;
        //Dispatcher mainThreadDispatcher = Application.Current.Dispatcher;

        //EntitiesLoader<RATE> RATESLoader;
        //EntitiesLoader<BASELINE> BASELINELoader;
        //EntitiesLoader<PROGRESS_ITEM> PROGRESS_ITEMSLoader;
        //EntitiesLoader<PROJECT_REPORT> PROJECT_REPORTSLoader;
        //EntitiesLoader<PROGRESS> PROGRESSLoader;
        //EntitiesLoader<WORKPACK> WORKPACKSLoader;

        //protected override void OnParameterChanged(object parameter)
        //{
        //    OptionalEntitiesParameter<PROJECT, PROGRESS> receiveParameter = (OptionalEntitiesParameter<PROJECT, PROGRESS>)parameter;
        //    this.loadPROJECT = receiveParameter.GetFirstEntity();
        //    this.loadPROGRESS = receiveParameter.GetSecondEntity();

        //    StartLoading();
        //}

        //private void OnPROGRESSorBASELINEChanged(object key, Type changedType, EntityMessageType messageType, object sender)
        //{
        //    if (sender == this)
        //        return;

        //    if ((loadPROGRESS != null && changedType == typeof(PROGRESS) && loadPROGRESS.GUID.ToString() == key.ToString())
        //        || liveBASELINE != null && changedType == typeof(BASELINE) && liveBASELINE.GUID.ToString() == key.ToString())
        //    {
        //        if (messageType == EntityMessageType.Added)
        //            MessageBoxService.ShowMessage(StringFormatUtils.GetEntityNameByType(changedType) + CommonResources.View_Restored);
        //        else if (messageType == EntityMessageType.Deleted)
        //            MessageBoxService.ShowMessage(StringFormatUtils.GetEntityNameByType(changedType) + CommonResources.View_Removed);
        //    }

        //    if (BASELINE_ITEMSJoinRATESJoinPROGRESS_ITEMSCollectionViewModel != null)
        //        mainThreadDispatcher.BeginInvoke(new Action(() => BASELINE_ITEMSJoinRATESJoinPROGRESS_ITEMSCollectionViewModel.Refresh()));
        //    else if (loadPROJECT != null || loadPROGRESS != null)
        //        mainThreadDispatcher.BeginInvoke(new Action(() => StartLoading()));
        //}

        //private void StartLoading()
        //{
        //    auxiliaryEntitiesLoaders.Clear();
        //    if (loadPROJECT != null)
        //        PROGRESSLoader = new EntitiesLoader<PROGRESS>(() => PROGRESSCollectionViewModel.Create(null, query => query.Where(x => x.GUID_PROJECT == loadPROJECT.GUID && x.STATUS == ProgressStatus.Live)), OnPROGRESSEntitiesLoaded, OnPROGRESSorBASELINEChanged);
        //    else if (loadPROGRESS != null)
        //    {
        //        PROGRESSLoader = new EntitiesLoader<PROGRESS>(() => PROGRESSCollectionViewModel.Create(null, query => query.Where(x => x.GUID == loadPROGRESS.GUID)), OnPROGRESSEntitiesLoaded, OnPROGRESSorBASELINEChanged);
        //    }

        //    if (PROGRESSLoader != null)
        //        auxiliaryEntitiesLoaders.Add(PROGRESSLoader);
        //}

        //private void OnPROGRESSEntitiesLoaded(IEnumerable<PROGRESS> entities)
        //{
        //    PROGRESSLoader.RemoveOnEntitiesFirstLoadedCallBack(null);
        //    if (entities.Count() == 0)
        //    {
        //        mainThreadDispatcher.BeginInvoke(new Action(() => MessageBoxService.ShowMessage(CommonResources.Missing_LivePROGRESS)));
        //        return;
        //    }

        //    loadPROGRESS = entities.First();
        //    mainThreadDispatcher.BeginInvoke(new Action(() => LoadBASELINE()));
        //}

        //private void LoadBASELINE()
        //{
        //    BASELINELoader = new EntitiesLoader<BASELINE>(() => BASELINECollectionViewModel.Create(null, query => query.Where(x => x.STATUS == BaselineStatus.Live && x.GUID_PROJECT == loadPROGRESS.GUID_PROJECT).Include(x => x.PROJECT)), OnBASELINEEntitiesLoaded, OnPROGRESSorBASELINEChanged);
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
        //    isSubEntitiesAdded = false;
        //    subEntitiesLoaders.Clear();
        //    mainThreadDispatcher.BeginInvoke(new Action(() => AfterPROGRESSAndBASELINEEntitiesLoaded()));
        //}

        //private void AfterPROGRESSAndBASELINEEntitiesLoaded()
        //{
        //    if (!loadPROGRESS.PROJECT.USELEGACYWORKPACK)
        //    {
        //        if(ShowNewWorkpack != null)
        //            ShowNewWorkpack();
        //    }

        //    WORKPACKSLoader = new EntitiesLoader<WORKPACK>(() => WORKPACKCollectionViewModel.Create(null, query => query.Where(x => x.GUID_PROJECT == loadPROGRESS.GUID_PROJECT)));
        //    PROJECT_REPORTSLoader = new EntitiesLoader<PROJECT_REPORT>(() => PROJECT_REPORTSCollectionViewModel.Create(null, query => query.Where(x => x.GUID_PROJECT == loadPROGRESS.GUID_PROJECT)));
        //    RATESLoader = new EntitiesLoader<RATE>(() => RATECollectionViewModel.Create(null, query => query.Where(x => x.GUID_PROJECT == loadPROGRESS.GUID_PROJECT)), OnSubEntitiesLoaded);
        //    PROGRESS_ITEMSLoader = new EntitiesLoader<PROGRESS_ITEM>(() => PROGRESS_ITEMSCollectionViewModel.Create(null, query => query.Where(x => x.GUID_PROGRESS == loadPROGRESS.GUID)), OnSubEntitiesLoaded);
        //    subEntitiesLoaders.Add(PROGRESS_ITEMSLoader);
        //    subEntitiesLoaders.Add(RATESLoader);
        //    isSubEntitiesAdded = true;
        //    auxiliaryEntitiesLoaders.Add(WORKPACKSLoader);
        //    auxiliaryEntitiesLoaders.Add(PROJECT_REPORTSLoader);
        //}

        //protected override void OnSubEntitiesLoaded(IEnumerable<object> entities)
        //{
        //    if (IsMainEntityLoaded)
        //        BASELINE_ITEMSJoinRATESJoinPROGRESS_ITEMSCollectionViewModel.RefreshWithoutClearingUndoManager();
        //    else if (IsAllSubEntitiesLoaded)
        //        mainThreadDispatcher.BeginInvoke(new Action(() => AfterSubEntitiesLoaded()));
        //}

        ///// <summary>
        ///// Translates PROGRESS_ITEM notification into BASELINE_ITEM notification
        ///// </summary>
        //private void OnPROGRESS_ITEMSRefresh(IEnumerable<PROGRESS_ITEM> entities)
        //{
        //    if(entities.Count() > 0)
        //    {
        //        BASELINE_ITEMJoinRATEJoinPROGRESS_ITEM findEntity = BASELINE_ITEMSJoinRATESJoinPROGRESS_ITEMSCollectionViewModel.Entities.FirstOrDefault(x => x.PROGRESS_ITEMCurrent != null && x.PROGRESS_ITEMCurrent.GUID == entities.First().GUID);
        //        if (findEntity != null)
        //            mainThreadDispatcher.BeginInvoke(new Action(() => Messenger.Default.Send(new EntityMessage<BASELINE_ITEM, Guid>(findEntity.GUID, EntityMessageType.Changed, this))));
        //    }
        //}

        //protected override void AfterSubEntitiesLoaded()
        //{
        //    mainEntityLoader = new EntitiesLoader<BASELINE_ITEMJoinRATEJoinPROGRESS_ITEM>(() => BASELINE_ITEMSJoinRATESJoinPROGRESS_ITEMSCollectionViewModel.Create(query => BASELINE_ITEMSJoinRATESJoinPROGRESS_ITEMSQueries.JoinRATESAndPROGRESS_ITEMSOnBASELINE_ITEMS(query, PROGRESSLoader.GetSingleObjectFunc, BASELINELoader.GetSingleObjectFunc, PROGRESS_ITEMSLoader.GetCollectionFunc, RATESLoader.GetCollectionFunc)), OnMainEntitiesFirstLoaded);
        //}


        //ProjectSummaryBuilder projectSummaryBuilder;
        //protected override void OnMainEntitiesFirstLoaded(IEnumerable<BASELINE_ITEMJoinRATEJoinPROGRESS_ITEM> entities)
        //{
        //    if (BASELINELoader.GetSingleObjectFunc() != null)
        //    {
        //        this.RaisePropertyChanged(x => x.DataDate);
        //        PROGRESS_ITEMSLoader.RemoveOnEntitiesFirstLoadedCallBack(this.OnPROGRESS_ITEMSRefresh); //route the refresh so that it can be mapped to baseline

        //        if (liveBASELINE == null || loadPROGRESS == null)
        //            return;

        //        projectSummaryBuilder = new ProjectSummaryBuilder(DefaultSummaryCalculation.Create(), entities.AsQueryable(), loadPROGRESS, liveBASELINE);
        //        PROGRESS_ITEMSummaryManufacturer summaryRollUp = new PROGRESS_ITEMSummaryManufacturer();
        //        summaryRollUp.Manufacture(projectSummaryBuilder);

        //        base.OnMainEntitiesFirstLoaded(entities);
        //        mainThreadDispatcher.BeginInvoke(new Action(() => NotifyMainEntityLoaded()));
        //    }
        //}

        //void NotifyMainEntityLoaded()
        //{
        //    this.RaisePropertyChanged(x => x.BASELINE_ITEMSJoinRATESJoinPROGRESS_ITEMSCollectionViewModel);
        //}


        //#endregion

        //#region Reporting
        //public void EditReport()
        //{
        //    REPORTDesigner reportDesigner = new REPORTDesigner(loadPROGRESS.PROJECT, ReportType.Progress_Report);
        //    if (reportDesigner.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //        reportDesigner.Dispose();
        //    else
        //        reportDesigner.Dispose();
        //}

        //public void ViewReport()
        //{
        //    SummaryManufacturer summaryManufacturer = new SummaryManufacturer();
        //    summaryManufacturer.Build(projectSummaryBuilder);
        //    var report = projectSummaryBuilder.SummaryObject;
        //    XtraReportPROGRESS_ITEMS progressReport = new XtraReportPROGRESS_ITEMS();
        //    PROJECT_REPORT projectReport = PROJECT_REPORTSLoader.GetCollectionFunc().FirstOrDefault(x => x.GUID_PROJECT == loadPROGRESS.GUID_PROJECT && x.REPORT_TYPE.ToString() == ReportType.Progress_Report.ToString());
        //    if (projectReport != null)
        //    {
        //        string reportString = projectReport.REPORT.ToString();
        //        using (StreamWriter sw = new StreamWriter(new MemoryStream()))
        //        {
        //            sw.Write(reportString);
        //            sw.Flush();
        //            progressReport.LoadLayout(sw.BaseStream);
        //        }

        //        progressReport.AssignProperties(report, loadPROGRESS.PROJECT.NAME);
        //        DocumentPreviewWindow previewWindow = new DocumentPreviewWindow();
        //        previewWindow.PreviewControl.DocumentSource = progressReport;
        //        previewWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        //        previewWindow.WindowState = WindowState.Maximized;
        //        progressReport.RequestParameters = false;
        //        progressReport.CreateDocument(true);
        //        previewWindow.ShowDialog();
        //    }
        //}
        //#endregion

    }
}
