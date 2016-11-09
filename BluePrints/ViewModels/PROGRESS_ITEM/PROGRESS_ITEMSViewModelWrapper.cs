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
        /// Creates a new instance of PROGRESS_ITEMSViewModelWrapper as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static PROGRESS_ITEMSViewModelWrapper Create()
        {
            return ViewModelSource.Create(() => new PROGRESS_ITEMSViewModelWrapper());
        }

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
            loaderCollection.AddEntitiesLoader<PROJECT, PROJECT, Guid, IBluePrintsEntitiesUnitOfWork>(0, bluePrintsUnitOfWorkFactory, x => x.PROJECTS, PROJECTProjectionFunc, null, isContinueLoadingAfterPROJECT, OnEntitiesChanged);
            loaderCollection.AddEntitiesLoader<BASELINE, BASELINE, Guid, IBluePrintsEntitiesUnitOfWork>(1, bluePrintsUnitOfWorkFactory, x => x.BASELINES, BASELINEProjectionFunc, typeof(PROJECT), isContinueLoadingAfterBASELINE, OnEntitiesChanged);
            loaderCollection.AddEntitiesLoader<PROGRESS, PROGRESS, Guid, IBluePrintsEntitiesUnitOfWork>(2, bluePrintsUnitOfWorkFactory, x => x.PROGRESSES, PROGRESSProjectionFunc, typeof(BASELINE), isContinueLoadingAfterPROGRESS, OnEntitiesChanged);
            loaderCollection.AddEntitiesLoader<WORKPACK, WORKPACK, Guid, IBluePrintsEntitiesUnitOfWork>(3, bluePrintsUnitOfWorkFactory, x => x.WORKPACKS, WORKPACKProjectionFunc, typeof(BASELINE));
            loaderCollection.AddEntitiesLoader<PROGRESS_ITEM, PROGRESS_ITEM, Guid, IBluePrintsEntitiesUnitOfWork>(4, bluePrintsUnitOfWorkFactory, x => x.PROGRESS_ITEMS, PROGRESS_ITEMProjectionFunc, typeof(PROGRESS), null, OnEntitiesChanged);
            loaderCollection.AddEntitiesLoader<PROJECT_REPORT, PROJECT_REPORT, Guid, IBluePrintsEntitiesUnitOfWork>(5, bluePrintsUnitOfWorkFactory, x => x.PROJECT_REPORTS, PROJECT_REPORTProjectionFunc, typeof(PROJECT));
            loaderCollection.AddEntitiesLoader<DEPARTMENT, DEPARTMENT, Guid, IBluePrintsEntitiesUnitOfWork>(6, bluePrintsUnitOfWorkFactory, x => x.DEPARTMENTS);
            loaderCollection.AddEntitiesLoader<DISCIPLINE, DISCIPLINE, Guid, IBluePrintsEntitiesUnitOfWork>(7, bluePrintsUnitOfWorkFactory, x => x.DISCIPLINES);
            loaderCollection.AddEntitiesLoader<DOCTYPE, DOCTYPE, Guid, IBluePrintsEntitiesUnitOfWork>(8, bluePrintsUnitOfWorkFactory, x => x.DOCTYPES);
            loaderCollection.AddEntitiesLoader<RATE, RATE, Guid, IBluePrintsEntitiesUnitOfWork>(9, bluePrintsUnitOfWorkFactory, x => x.RATES, RATEProjectionFunc, typeof(PROJECT), null, OnEntitiesChanged);
            InvokeEntitiesLoaderDescriptionLoading();
        }

        bool isContinueLoadingAfterPROJECT(IEnumerable<PROJECT> entities)
        {
            if (entities.Count() == 0)
            {
                mainThreadDispatcher.BeginInvoke(new Action(() => MessageBoxService.ShowMessage(string.Format(CommonResources.Notify_View_Removed, "PROJECT"))));
                return false;
            }

            this.loadPROJECT = entities.First();
            return true;
        }

        bool isContinueLoadingAfterBASELINE(IEnumerable<BASELINE> entities)
        {
            if (entities.Count() == 0)
            {
                mainThreadDispatcher.BeginInvoke(new Action(() => MessageBoxService.ShowMessage(string.Format(CommonResources.Notify_View_Removed, "BASELINE"))));
                return false;
            }

            this.loadBASELINE = entities.First();
            return true;
        }

        bool isContinueLoadingAfterPROGRESS(IEnumerable<PROGRESS> entities)
        {
            if (entities.Count() == 0)
            {
                mainThreadDispatcher.BeginInvoke(new Action(() => MessageBoxService.ShowMessage(string.Format(CommonResources.Notify_View_Removed, "PROGRESS"))));
                return false;
            }

            this.loadPROGRESS = entities.First();
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
            return query => query.Where(x => x.GUID_PROJECT == loadPROJECT.GUID && x.REPORT_TYPE == ReportType.Progress_Report.ToString());
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
            Func<BASELINE> getBASELINEFunc = loaderCollection.GetObjectFunc<BASELINE>();
            Func<PROGRESS> getPROGRESSFunc = loaderCollection.GetObjectFunc<PROGRESS>();
            Func<IQueryable<PROGRESS_ITEM>> getPROGRESS_ITEMSFunc = loaderCollection.GetCollectionFunc<PROGRESS_ITEM>();
            Func<IQueryable<RATE>> getRATESFunc = loaderCollection.GetCollectionFunc<RATE>();

            return query => BASELINE_ITEMSJoinRATESJoinPROGRESS_ITEMSQueries.JoinRATESAndPROGRESS_ITEMSOnBASELINE_ITEMS(query, getPROGRESSFunc, getBASELINEFunc, getPROGRESS_ITEMSFunc, getRATESFunc);
        }

        PROJECTSummaryBuilder projectSummaryBuilder; //used for report
        protected override void AssignCallBacksAndRaisePropertyChange(CollectionViewModel<BASELINE_ITEM, BASELINE_ITEMJoinRATEJoinPROGRESS_ITEM, Guid, IBluePrintsEntitiesUnitOfWork> mainViewModel)
        {
            mainViewModel.PreSave = this.MainEntityPreSave;
            mainViewModel.BulkPreSave = this.MainEntityBulkPreSave;
            mainViewModel.ValidateFillDownCallBack = this.ValidateFillDownCallBack;
            mainThreadDispatcher.BeginInvoke(new Action(() => this.CalculatePROGRESS_ITEMSStats()));
        }

        void CalculatePROGRESS_ITEMSStats()
        {
            PROJECTSummary currentPROJECTSummary = PROJECTSummary.Create();
            currentPROJECTSummary.LiveBASELINE = loaderCollection.GetObject<BASELINE>();
            currentPROJECTSummary.LivePROGRESS = loaderCollection.GetObject<PROGRESS>();
            currentPROJECTSummary.ReportableObjects = MainViewModel.Entities;

            projectSummaryBuilder = new PROJECTSummaryBuilder(currentPROJECTSummary);
            PROGRESS_ITEMSummaryManufacturer summaryRollUp = new PROGRESS_ITEMSummaryManufacturer();
            summaryRollUp.Manufacture(projectSummaryBuilder);
            mainThreadDispatcher.BeginInvoke(new Action(() => this.RaisePropertiesChanged()));
        }

        protected override void OnEntitiesChanged(object key, Type changedType, EntityMessageType messageType, object sender)
        {
            //Map the changes from PROGRESS_ITEM to BASELINE_ITEM so undo/redo operation is valid
            if (changedType == typeof(PROGRESS_ITEM))
            {
                BASELINE_ITEMJoinRATEJoinPROGRESS_ITEM mappedEntity = MainViewModel.Entities.FirstOrDefault(x => x.PROGRESS_ITEMCurrent != null && x.PROGRESS_ITEMCurrent.GUID.ToString() == key.ToString());
                mainThreadDispatcher.BeginInvoke(new Action(() => Messenger.Default.Send(new EntityMessage<BASELINE_ITEM, Guid>(mappedEntity.GUID, EntityMessageType.Changed, this))));
                return;
            }

            if (sender == this)
                return;

            if (loadPROGRESS != null && changedType == typeof(PROGRESS) && loadPROGRESS.GUID.ToString() == key.ToString() ||
                loadBASELINE != null && changedType == typeof(BASELINE) && loadBASELINE.GUID.ToString() == key.ToString() ||
                loadPROJECT != null && changedType == typeof(PROJECT) && loadPROJECT.GUID.ToString() == key.ToString())
            {
                if (messageType == EntityMessageType.Added)
                    MessageBoxService.ShowMessage(string.Format(CommonResources.Notify_View_Restored, StringFormatUtils.GetEntityNameByType(changedType)));
                else if (messageType == EntityMessageType.Deleted)
                    MessageBoxService.ShowMessage(string.Format(CommonResources.Notify_View_Removed, StringFormatUtils.GetEntityNameByType(changedType)));
            }

            if (loadPROJECT != null || loadBASELINE != null || loadPROGRESS != null)
            {
                if (MainViewModel != null)
                    mainThreadDispatcher.BeginInvoke(new Action(() => MainViewModel.Refresh()));
                else if (loadPROJECT != null || loadBASELINE != null)
                    mainThreadDispatcher.BeginInvoke(new Action(() => InitializeAndLoadEntitiesLoaderDescription()));
            }
        }

        #region Collection Call Backs
        private bool MainEntityBulkPreSave(IEnumerable<BASELINE_ITEMJoinRATEJoinPROGRESS_ITEM> entities)
        {
            foreach (var entity in entities)
            {
                MainEntityPreSave(entity);
            }

            return false;
        }

        bool MainEntityPreSave(BASELINE_ITEMJoinRATEJoinPROGRESS_ITEM projectionEntity)
        {
            CollectionViewModel<PROGRESS_ITEM, PROGRESS_ITEM, Guid, IBluePrintsEntitiesUnitOfWork> PROGRESS_ITEMSCollectionViewModel = (CollectionViewModel<PROGRESS_ITEM, PROGRESS_ITEM, Guid, IBluePrintsEntitiesUnitOfWork>)loaderCollection.GetViewModel<PROGRESS_ITEM>();
            PROGRESS_ITEM savePROGRESS_ITEM = projectionEntity.PROGRESS_ITEMCurrent;
            savePROGRESS_ITEM.EARNED_DATE = loadPROGRESS.DATA_DATE;
            savePROGRESS_ITEM.GUID_PROGRESS = loadPROGRESS.GUID;
            savePROGRESS_ITEM.GUID_ORIBASEITEM = projectionEntity.BASELINE_ITEMJoinRATE.BASELINE_ITEM.GUID_ORIGINAL;
            //workaround for created because Save() only sets the projection primary key, this is used for property redo where the interceptor only tampers with UPDATED and CREATED is left as null
            if (savePROGRESS_ITEM.CREATED.Date.Year == 1)
                savePROGRESS_ITEM.CREATED = DateTime.Now;

            savePROGRESS_ITEM = projectionEntity.PROGRESS_ITEMCurrent;
            PROGRESS_ITEMSCollectionViewModel.Save(savePROGRESS_ITEM);
            return false;
        }

        public bool ValidateFillDownCallBack(BASELINE_ITEMJoinRATEJoinPROGRESS_ITEM fillDownEntity, string fieldName, object fillValue)
        {
            if (fieldName != BindableBase.GetPropertyName(() => new BASELINE_ITEMJoinRATEJoinPROGRESS_ITEM().TOTAL_EARNED_PERCENTAGE))
                return false;

            decimal newPercentage = (decimal)fillValue;
            if (newPercentage > fillDownEntity.MaxPercentage)
                return false;
            else if (newPercentage < fillDownEntity.MinPercentage)
                return false;

            return true;
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

        public IEnumerable<WORKPACK> WORKPACKCollection
        {
            get
            {
                return GetEntities<WORKPACK>();
            }
        }

        public string DataDate
        {
            get
            {
                if (loadPROGRESS == null || loadPROGRESS.DATA_DATE == null)
                    return string.Empty;

                return loadPROGRESS.DATA_DATE.ToString("g");
            }
        }

        public bool CanDateBackward()
        {
            if (MainViewModel == null || MainViewModel.IsLoading)
                return false;

            if (loadPROGRESS.DATA_DATE > loadPROGRESS.PROGRESS_START)
                return true;

            return false;
        }

        public bool CanDateForward()
        {
            if (MainViewModel == null || MainViewModel.IsLoading)
                return false;

            return true;
        }

        public void DateForward()
        {
            DateChange(true);
        }

        public void DateBackward()
        {
            DateChange(false);
        }

        private void DateChange(bool isForward)
        {
            var interval = ISupportProgressReportingExtensions.ConvertProgressIntervalToPeriod(loadPROGRESS);
            int multiplier = isForward == true ? 1 : -1;
            loadPROGRESS.DATA_DATE = loadPROGRESS.DATA_DATE.AddDays(multiplier * interval.Days);
            CollectionViewModel<PROGRESS, PROGRESS, Guid, IBluePrintsEntitiesUnitOfWork> PROGRESSCollectionViewModel = (CollectionViewModel<PROGRESS, PROGRESS, Guid, IBluePrintsEntitiesUnitOfWork>)loaderCollection.GetViewModel<PROGRESS>();
            PROGRESSCollectionViewModel.Save(loadPROGRESS);
            this.RaisePropertyChanged(x => x.DataDate);
        }
        #endregion

        #region Reporting
        public void EditReport()
        {
            REPORTDesigner reportDesigner = new REPORTDesigner((CollectionViewModel<PROJECT_REPORT, PROJECT_REPORT, Guid, IBluePrintsEntitiesUnitOfWork>)loaderCollection.GetViewModel<PROJECT_REPORT>(), ReportType.Progress_Report);
            if (reportDesigner.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                reportDesigner.Dispose();
            else
                reportDesigner.Dispose();
        }

        public void ViewReport()
        {
            SummaryManufacturer summaryManufacturer = new SummaryManufacturer();
            summaryManufacturer.Build(projectSummaryBuilder);
            var report = projectSummaryBuilder.SummaryObject;
            XtraReportPROGRESS_ITEMS progressReport = new XtraReportPROGRESS_ITEMS();
            PROJECT_REPORT projectReport = loaderCollection.GetObject<PROJECT_REPORT>();
            if (projectReport != null)
            {
                string reportString = projectReport.REPORT.ToString();
                using (StreamWriter sw = new StreamWriter(new MemoryStream()))
                {
                    sw.Write(reportString);
                    sw.Flush();
                    progressReport.LoadLayout(sw.BaseStream);
                }

                progressReport.AssignProperties(report, loadPROGRESS.PROJECT.NAME);
                DocumentPreviewWindow previewWindow = new DocumentPreviewWindow();
                previewWindow.PreviewControl.DocumentSource = progressReport;
                previewWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                previewWindow.WindowState = WindowState.Maximized;
                progressReport.RequestParameters = false;
                progressReport.CreateDocument(true);
                previewWindow.ShowDialog();
            }
        }
        #endregion
    }
}
