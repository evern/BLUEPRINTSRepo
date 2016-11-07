using System;
using System.Linq;
using DevExpress.Mvvm.POCO;
using BluePrints.Common.Utils;
using BluePrints.BluePrintsEntitiesDataModel;
using BluePrints.Common.DataModel;
using BluePrints.Data;
using BluePrints.Common.ViewModel;
using DevExpress.Xpf.Grid;
using BluePrints.Common.ViewModel.Filtering;
using DevExpress.Mvvm;
using System.Linq.Expressions;
using BluePrints.Common;
using BluePrints.Data.Helpers;
using BluePrints.P6EntitiesDataModel;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using BluePrints.Common.ViewModel.Reporting;
using System.Windows.Threading;
using DevExpress.Xpf.Bars;
using System.ComponentModel;

namespace BluePrints.ViewModels
{
    /// <summary>
    /// Represents the PROJECTS collection view model.
    /// </summary>
    public partial class PROJECTDashboardCollectionViewModelWrapper
    {
    //    /// <summary>
    //    /// Creates a new instance of PROJECTCollectionViewModel as a POCO view model.
    //    /// </summary>
    //    /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
    //    public static PROJECTDashboardCollectionViewModelWrapper Create(IUnitOfWorkFactory<IBluePrintsEntitiesUnitOfWork> unitOfWorkFactory = null)
    //    {
    //        return ViewModelSource.Create(() => new PROJECTDashboardCollectionViewModelWrapper(unitOfWorkFactory));
    //    }

    //    EntitiesLoader<RATE> RATESLoader;
    //    EntitiesLoader<BASELINE> BASELINES_ByLiveProjectIncludeBASELINE_ITEMSLoader;
    //    EntitiesLoader<PROGRESS> PROGRESSES_ByLiveProjectLoader;
    //    /// <summary>
    //    /// Initializes a new instance of the PROJECTCollectionViewModel class.
    //    /// This constructor is declared protected to avoid undesired instantiation of the PROJECTCollectionViewModel type without the POCO proxy factory.
    //    /// </summary>
    //    /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
    //    protected PROJECTDashboardCollectionViewModelWrapper(IUnitOfWorkFactory<IBluePrintsEntitiesUnitOfWork> unitOfWorkFactory = null)
    //    {
    //        this.UnitOfWorkFactory = unitOfWorkFactory ?? BluePrintsEntitiesUnitOfWorkSource.GetUnitOfWorkFactory();
    //        RATESLoader = new EntitiesLoader<RATE>(() => RATECollectionViewModel.Create(null), this.OnDetailedEntitiesLoaded);
    //        BASELINES_ByLiveProjectIncludeBASELINE_ITEMSLoader = new EntitiesLoader<BASELINE>(() => BASELINECollectionViewModel.Create(null, query => query.Where(x => x.STATUS == BaselineStatus.Live).Include(x => x.BASELINE_ITEMS)), this.OnDetailedEntitiesLoaded);
    //        PROGRESSES_ByLiveProjectLoader = new EntitiesLoader<PROGRESS>(() => PROGRESSCollectionViewModel.Create(null, query => query.Where(x => x.STATUS == ProgressStatus.Live)), this.OnDetailedEntitiesLoaded);
    //    }

    //    int loadedViewModelCount = 0;
    //    private void OnDetailedEntitiesLoaded(IEnumerable<object> entities)
    //    {
    //        loadedViewModelCount += 1;
    //        if (loadedViewModelCount == 3)
    //            this.RaisePropertyChanged(x => x.DashboardCollection);
    //    }

    //    public override CollectionViewModel<PROJECT, PROJECT_DashboardInfo, Guid, IBluePrintsEntitiesUnitOfWork> DashboardCollection
    //    {
    //        get
    //        {
    //            if (dashboardCollectionViewModel == null && loadedViewModelCount == 3)
    //            {
    //                var BluePrintsUnitOfWork = this.UnitOfWorkFactory.CreateUnitOfWork();
    //                var P6UnitOfWork = P6EntitiesUnitOfWorkSource.GetUnitOfWorkFactory().CreateUnitOfWork();
    //                dashboardCollectionViewModel = PROJECTDashboardCollectionViewModel.Create(this.UnitOfWorkFactory, query => QueriesHelper.MorphPROJECT_DashboardInfo(query, BASELINES_ByLiveProjectIncludeBASELINE_ITEMSLoader.GetCollectionFunc, PROGRESSES_ByLiveProjectLoader.GetCollectionFunc, RATESLoader.GetCollectionFunc, BluePrintsUnitOfWork, P6UnitOfWork));
    //                dashboardCollectionViewModel.OnSelectedEntitiesChangedCallBack = this.OnSelectedEntityChanged;
    //                dashboardCollectionViewModel.SetParentViewModel(this);
    //            }

    //            return dashboardCollectionViewModel;
    //        }
    //    }

        //#region IDocumentContent
        //protected IDocumentOwner DocumentOwner { get; private set; }
        //object IDocumentContent.Title { get { return null; } }

        //protected virtual void OnClose(CancelEventArgs e) { }
        //void IDocumentContent.OnClose(CancelEventArgs e)
        //{
        //    OnClose(e);
        //}

        //void IDocumentContent.OnDestroy()
        //{
        //    //RATESLoader.GetCollectionViewModel().OnDestroy();
        //    //BASELINES_ByLiveProjectIncludeBASELINE_ITEMSLoader.GetCollectionViewModel().OnDestroy();
        //    //PROGRESSES_ByLiveProjectLoader.GetCollectionViewModel().OnDestroy();
        //    //DashboardCollection.OnDestroy();
        //}

        //IDocumentOwner IDocumentContent.DocumentOwner
        //{
        //    get { return DocumentOwner; }
        //    set { DocumentOwner = value; }
        //}
        //#endregion
    }

    public class PROJECTDashboardCollectionViewModel : CollectionViewModel<PROJECT, PROJECT_DashboardInfo, Guid, IBluePrintsEntitiesUnitOfWork>, ISupportCustomDocumentTypeNameAndParameter
    {
        /// <summary>
        /// Creates a new instance of PROJECTDashboardCollectionViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static PROJECTDashboardCollectionViewModel Create(IUnitOfWorkFactory<IBluePrintsEntitiesUnitOfWork> unitOfWorkFactory = null, Func<IRepositoryQuery<PROJECT>, IQueryable<PROJECT_DashboardInfo>> projection = null)
        {
            return ViewModelSource.Create(() => new PROJECTDashboardCollectionViewModel(unitOfWorkFactory, projection));
        }

        /// <summary>
        /// Initializes a new instance of the PROJECTDashboardCollectionViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the PROJECTCollectionViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        protected PROJECTDashboardCollectionViewModel(IUnitOfWorkFactory<IBluePrintsEntitiesUnitOfWork> unitOfWorkFactory = null, Func<IRepositoryQuery<PROJECT>, IQueryable<PROJECT_DashboardInfo>> projection = null)
            : base(unitOfWorkFactory ?? BluePrintsEntitiesUnitOfWorkSource.GetUnitOfWorkFactory(), x => x.PROJECTS, projection)
        {
            dispatchTimer = new DispatcherTimer();
            dispatchTimer.Interval = new TimeSpan(0, 0, 0, 0, 10);
        }

        DispatcherTimer dispatchTimer;
        protected override void SelectedEntities_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            dispatchTimer.Tick -= dispatchTimer_Tick;
            dispatchTimer.Tick += dispatchTimer_Tick;
            dispatchTimer.Start();
        }

        void dispatchTimer_Tick(object sender, EventArgs e)
        {
            if (OnSelectedEntitiesChangedCallBack != null)
                OnSelectedEntitiesChangedCallBack(SelectedEntities);
            dispatchTimer.Stop();
        }

        public string GetCustomDocumentTypeName()
        {
            return "PROJECTWORKPACKDashboardCollectionView";
        }

        public object GetCustomDocumentParameter()
        {
            return this.SelectedEntity.SummarizablePrincipal;
        }

        public string GetCustomDocumentTitle()
        {
            return this.SelectedEntity.NUMBER + " - WORKPACKS";
        }

        public bool IsCustomModeEnabled()
        {
            return true;
        }
    }
}