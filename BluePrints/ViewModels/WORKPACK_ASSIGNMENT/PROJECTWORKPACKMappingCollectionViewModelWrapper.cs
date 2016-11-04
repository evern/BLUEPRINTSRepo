using BluePrints.BluePrintsEntitiesDataModel;
using BluePrints.Common.DataModel;
using BluePrints.Common.ViewModel;
using BluePrints.Common.ViewModel.Utils;
using BluePrints.Data;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Scheduler;
using DevExpress.XtraScheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DevExpress.Mvvm;
using BluePrints.Common;
using DevExpress.Mvvm.POCO;
using BluePrints.P6EntitiesDataModel;
using BluePrints.Data.Helpers;
using BluePrints.P6Data;
using System.Collections.ObjectModel;
using System.Windows.Forms.Integration;
using BluePrints.Views;
using System.Windows.Threading;
using System.ComponentModel;

namespace BluePrints.ViewModels
{
    public class PROJECTWORKPACKMappingCollectionViewModelWrapper : ISupportParameter, IDocumentContent
    {
        IUnitOfWorkFactory<IBluePrintsEntitiesUnitOfWork> UnitOfWorkFactory;
        /// <summary>
        /// Creates a new instance of PROJECTCollectionViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static PROJECTWORKPACKMappingCollectionViewModelWrapper Create(IUnitOfWorkFactory<IBluePrintsEntitiesUnitOfWork> unitOfWorkFactory = null)
        {
            return ViewModelSource.Create(() => new PROJECTWORKPACKMappingCollectionViewModelWrapper(unitOfWorkFactory));
        }

        //Because this View Model won't be binded to a WPF view, RaisePropertyChanged will be replaced by dispatcher timers
        DispatcherTimer projectDependentViewModelDispatcher;
        DispatcherTimer workpackCollectionViewModelDispatcher;
        DispatcherTimer winFormViewDispatcher;
        /// <summary>
        /// Initializes a new instance of the PROJECTViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the PROJECTViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        protected PROJECTWORKPACKMappingCollectionViewModelWrapper(IUnitOfWorkFactory<IBluePrintsEntitiesUnitOfWork> unitOfWorkFactory = null)
        {
            this.UnitOfWorkFactory = unitOfWorkFactory ?? BluePrintsEntitiesUnitOfWorkSource.GetUnitOfWorkFactory();
            this.projectDependentViewModelDispatcher = new DispatcherTimer();
            this.projectDependentViewModelDispatcher.Interval = new TimeSpan(0, 0, 0, 0, 1);
            this.workpackCollectionViewModelDispatcher = new DispatcherTimer();
            this.workpackCollectionViewModelDispatcher.Interval = new TimeSpan(0, 0, 0, 0, 1);
            this.winFormViewDispatcher = new DispatcherTimer();
            this.winFormViewDispatcher.Interval = new TimeSpan(0, 0, 0, 0, 1);
            this.projectDependentViewModelDispatcher.Tick += projectDependentViewModelDispatcher_Tick;
            this.workpackCollectionViewModelDispatcher.Tick += workpackCollectionViewModelDispatcher_Tick;
            this.winFormViewDispatcher.Tick += winFormViewDispatcher_Tick;
        }

        PROJECTWORKPACKMappingCollectionViewModel workpackCollectionViewModel { get; set; }
        public PROJECTWORKPACKMappingCollectionViewModel WorkpackCollectionViewModel
        {
            get
            {
                if (workpackCollectionViewModel == null)
                {
                    //workpackCollectionViewModel = PROJECTWORKPACKMappingCollectionViewModel.Create(this.UnitOfWorkFactory, query => QueriesHelper.MorphWORKPACK_DashboardInfo(query, PROGRESS_byLiveProjectIncludePROGRESS_ITEMSLoader.GetSingleObjectFunc, BASELINE_byContextBASELINEIncludeBASELINE_ITEMSLoader.GetSingleObjectFunc, RATES_byPROJECTMethodsLoader.GetCollectionFunc, contextBASELINE.GUID_PROJECT, this.ISMODIFIEDBASELINE));
                    //workpackCollectionViewModel.SetParentViewModel(this);
                    //workpackCollectionViewModel.OnEntitiesLoadedCallBack = this.OnMainCollectionEntitiesLoaded;
                }

                return workpackCollectionViewModel;
            }
        }

        int loadedViewModelCount = 0;
        private void OnAllDetailedEntitiesLoaded(IEnumerable<object> entities)
        {
            loadedViewModelCount += 1;
            if (loadedViewModelCount == 6)
                workpackCollectionViewModelDispatcher.Start();
        }

        int queryP6PROJECTId = 0;
        /// <summary>
        /// Because P6TASKS and P6PROJWBS depends on P6PROJECT, Entities for P6PROJECT must be loaded before P6TASKSCollectionViewModel can be initialized
        /// </summary>
        private void OnAuxiliaryDetailedEntitiesLoaded(IEnumerable<object> entities)
        {
            P6Data.PROJECT P6PROJECT = (P6Data.PROJECT)entities.FirstOrDefault();
            if (P6PROJECT != null)
                queryP6PROJECTId = P6PROJECT.proj_id;

            this.projectDependentViewModelDispatcher.Start();
        }

        private void OnMainCollectionEntitiesLoaded(IEnumerable<object> entities)
        {
            winFormViewDispatcher.Start();
        }

        public Action<Func<IQueryable<TASK>>, Func<IQueryable<PROJWBS>>, PROJECTWORKPACKMappingCollectionViewModel, WORKPACK_ASSIGNMENTSCollectionViewModel, bool> windowsFormHostViewInitialization { get; set; }
        BASELINE contextBASELINE { get; set; }
        bool ISMODIFIEDBASELINE { get; set; } //Specify whether the context is a original or modified P6BASELINE

        EntitiesLoader<BASELINE> BASELINE_byContextBASELINEIncludeBASELINE_ITEMSLoader;
        EntitiesLoader<PROGRESS> PROGRESS_byLiveProjectIncludePROGRESS_ITEMSLoader;
        EntitiesLoader<RATE> RATES_byPROJECTMethodsLoader;
        EntitiesLoader<WORKPACK_ASSIGNMENT> WORKPACK_ASSIGNMENTS_byIsModifiedPROJECTLoader;
        EntitiesLoader<BluePrints.P6Data.PROJECT> P6PROJECT_byNameLoader;
        EntitiesLoader<TASK> P6TASK_byPROJECTLoader;
        EntitiesLoader<PROJWBS> P6PROJWBS_byPROJECTLoader;


        WORKPACK_ASSIGNMENTSCollectionViewModel WORKPACK_ASSIGNMENTSViewModel;
        #region ISupportParameter
        object ISupportParameter.Parameter
        {
            get { return null; }
            set
            {
                object[] parameter = (object[])value;

                contextBASELINE = (BASELINE)parameter[0];
                BaselineMappingSelectionType baselineType = (BaselineMappingSelectionType)parameter[1];
                ISMODIFIEDBASELINE = baselineType == BaselineMappingSelectionType.Modified ? true : false;

                //Counted By loadedViewModelCount
                BASELINE_byContextBASELINEIncludeBASELINE_ITEMSLoader = new EntitiesLoader<BASELINE>(() => BASELINECollectionViewModel.Create(this.UnitOfWorkFactory, query => query.Where(x => x.GUID == contextBASELINE.GUID).Include(x => x.BASELINE_ITEMS)), this.OnAllDetailedEntitiesLoaded);
                PROGRESS_byLiveProjectIncludePROGRESS_ITEMSLoader = new EntitiesLoader<PROGRESS>(() => PROGRESSCollectionViewModel.Create(this.UnitOfWorkFactory, query => query.Where(x => x.STATUS == ProgressStatus.Live && x.GUID_PROJECT == contextBASELINE.GUID_PROJECT).Include(x => x.PROGRESS_ITEMS)), this.OnAllDetailedEntitiesLoaded);
                RATES_byPROJECTMethodsLoader = new EntitiesLoader<RATE>(() => RATECollectionViewModel.Create(this.UnitOfWorkFactory, projection => projection.Where(y => y.GUID_PROJECT == contextBASELINE.GUID_PROJECT)), this.OnAllDetailedEntitiesLoaded);
                WORKPACK_ASSIGNMENTS_byIsModifiedPROJECTLoader = new EntitiesLoader<WORKPACK_ASSIGNMENT>(() => WORKPACK_ASSIGNMENTSCollectionViewModel.Create(null, query => query.Where(x => x.WORKPACK.GUID_PROJECT == contextBASELINE.GUID_PROJECT && x.ISMODIFIEDBASELINE == this.ISMODIFIEDBASELINE)), this.OnAllDetailedEntitiesLoaded);
                //WORKPACK_ASSIGNMENTSViewModel is used to track DbRepository of WORKPACK_ASSIGNMENTS
                WORKPACK_ASSIGNMENTSViewModel = (WORKPACK_ASSIGNMENTSCollectionViewModel)WORKPACK_ASSIGNMENTS_byIsModifiedPROJECTLoader.GetCollectionViewModel();
                //Not counted by loadedViewModelCount
                P6PROJECT_byNameLoader = new EntitiesLoader<P6Data.PROJECT>(() => P6PROJECTCollectionViewModel.Create(null, query => query.Where(x => x.proj_short_name == contextBASELINE.P6BASELINE_NAME)), this.OnAuxiliaryDetailedEntitiesLoaded);
            }
        }

        void winFormViewDispatcher_Tick(object sender, EventArgs e)
        {
            winFormViewDispatcher.Stop();
            windowsFormHostViewInitialization(P6TASK_byPROJECTLoader.GetCollectionFunc, P6PROJWBS_byPROJECTLoader.GetCollectionFunc, WorkpackCollectionViewModel, WORKPACK_ASSIGNMENTSViewModel, ISMODIFIEDBASELINE);
        }

        void projectDependentViewModelDispatcher_Tick(object sender, EventArgs e)
        {
            projectDependentViewModelDispatcher.Stop();
            //Counted By loadedViewModelCount
            P6TASK_byPROJECTLoader = new EntitiesLoader<TASK>(() => P6TASKCollectionViewModel.Create(null, query => query.Where(x => x.proj_id == queryP6PROJECTId)), this.OnAllDetailedEntitiesLoaded);
            P6PROJWBS_byPROJECTLoader = new EntitiesLoader<PROJWBS>(() => P6PROJWBSCollectionViewModel.Create(null, query => query.Where(x => x.proj_id == queryP6PROJECTId)), this.OnAllDetailedEntitiesLoaded);
        }

        void workpackCollectionViewModelDispatcher_Tick(object sender, EventArgs e)
        {
            workpackCollectionViewModelDispatcher.Stop();
            WorkpackCollectionViewModel.Entities.ToList();
        }
        #endregion
        #region IDocumentContent
        protected IDocumentOwner DocumentOwner { get; private set; }
        object IDocumentContent.Title { get { return null; } }

        protected virtual void OnClose(CancelEventArgs e) { }
        void IDocumentContent.OnClose(CancelEventArgs e)
        {
            OnClose(e);
        }

        void IDocumentContent.OnDestroy()
        {
            BASELINE_byContextBASELINEIncludeBASELINE_ITEMSLoader.GetCollectionViewModel().OnDestroy();
            PROGRESS_byLiveProjectIncludePROGRESS_ITEMSLoader.GetCollectionViewModel().OnDestroy();
            RATES_byPROJECTMethodsLoader.GetCollectionViewModel().OnDestroy();
            WORKPACK_ASSIGNMENTS_byIsModifiedPROJECTLoader.GetCollectionViewModel().OnDestroy();
            P6PROJECT_byNameLoader.GetCollectionViewModel().OnDestroy();
            P6TASK_byPROJECTLoader.GetCollectionViewModel().OnDestroy();
            P6PROJWBS_byPROJECTLoader.GetCollectionViewModel().OnDestroy();
        }

        IDocumentOwner IDocumentContent.DocumentOwner
        {
            get { return DocumentOwner; }
            set { DocumentOwner = value; }
        }
        #endregion

    }

    public class PROJECTWORKPACKMappingCollectionViewModel : CollectionViewModel<WORKPACK, WORKPACK_DashboardInfo, Guid, IBluePrintsEntitiesUnitOfWork>
    {
        /// <summary>
        /// Creates a new instance of WORKPACKDashboardCollectionViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static PROJECTWORKPACKMappingCollectionViewModel Create(IUnitOfWorkFactory<IBluePrintsEntitiesUnitOfWork> unitOfWorkFactory = null, Func<IRepositoryQuery<WORKPACK>, IQueryable<WORKPACK_DashboardInfo>> projection = null)
        {
            return ViewModelSource.Create(() => new PROJECTWORKPACKMappingCollectionViewModel(unitOfWorkFactory, projection));
        }

        /// <summary>
        /// Initializes a new instance of the WORKPACKDashboardCollectionViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the PROJECTCollectionViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        protected PROJECTWORKPACKMappingCollectionViewModel(IUnitOfWorkFactory<IBluePrintsEntitiesUnitOfWork> unitOfWorkFactory = null, Func<IRepositoryQuery<WORKPACK>, IQueryable<WORKPACK_DashboardInfo>> projection = null)
            : base(unitOfWorkFactory ?? BluePrintsEntitiesUnitOfWorkSource.GetUnitOfWorkFactory(), x => x.WORKPACKS, projection)
        {
        }
    }
}
