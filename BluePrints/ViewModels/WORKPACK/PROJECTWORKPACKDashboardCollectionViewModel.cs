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
    public partial class PROJECTWORKPACKDashboardCollectionViewModelWrapper : DashboardCollectionViewModelWrapper<WORKPACK, WORKPACK_DashboardInfo>, ISupportParameter, IDocumentContent
    {
        /// <summary>
        /// Creates a new instance of PROJECTCollectionViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static PROJECTWORKPACKDashboardCollectionViewModelWrapper Create(IUnitOfWorkFactory<IBluePrintsEntitiesUnitOfWork> unitOfWorkFactory = null)
        {
            return ViewModelSource.Create(() => new PROJECTWORKPACKDashboardCollectionViewModelWrapper(unitOfWorkFactory));
        }

        private DefaultSummaryCalculation ProjectSummary { get; set; }
        /// <summary>
        /// Initializes a new instance of the PROJECTCollectionViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the PROJECTCollectionViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        protected PROJECTWORKPACKDashboardCollectionViewModelWrapper(IUnitOfWorkFactory<IBluePrintsEntitiesUnitOfWork> unitOfWorkFactory = null)
        {
            this.UnitOfWorkFactory = unitOfWorkFactory ?? BluePrintsEntitiesUnitOfWorkSource.GetUnitOfWorkFactory();
        }

        #region ISupportParameter
        object ISupportParameter.Parameter
        {
            get { return null; }
            set 
            { 
                ProjectSummary = (DefaultSummaryCalculation)value;
                this.RaisePropertyChanged(x => x.DashboardCollection);
            }
        }
        #endregion

        public override CollectionViewModel<WORKPACK, WORKPACK_DashboardInfo, Guid, IBluePrintsEntitiesUnitOfWork> DashboardCollection
        {
            get
            {
                if (ProjectSummary == null)
                    return null;

                if (dashboardCollectionViewModel == null)
                {
                    dashboardCollectionViewModel = PROJECTWORKPACKDashboardCollectionViewModel.Create(this.UnitOfWorkFactory, query => QueriesHelper.MorphWORKPACK_DashboardInfo(query, ProjectSummary));
                    dashboardCollectionViewModel.OnSelectedEntitiesChangedCallBack = this.OnSelectedEntityChanged;
                    dashboardCollectionViewModel.SetParentViewModel(this);
                }

                return dashboardCollectionViewModel;
            }
        }


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
            DashboardCollection.OnDestroy();
        }

        IDocumentOwner IDocumentContent.DocumentOwner
        {
            get { return DocumentOwner; }
            set { DocumentOwner = value; }
        }
        #endregion
    }

    public class PROJECTWORKPACKDashboardCollectionViewModel : CollectionViewModel<WORKPACK, WORKPACK_DashboardInfo, Guid, IBluePrintsEntitiesUnitOfWork>, ISupportCustomDocumentTypeNameAndParameter
    {
        /// <summary>
        /// Creates a new instance of PROJECTCollectionViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static PROJECTWORKPACKDashboardCollectionViewModel Create(IUnitOfWorkFactory<IBluePrintsEntitiesUnitOfWork> unitOfWorkFactory = null, Func<IRepositoryQuery<WORKPACK>, IQueryable<WORKPACK_DashboardInfo>> projection = null)
        {
            return ViewModelSource.Create(() => new PROJECTWORKPACKDashboardCollectionViewModel(unitOfWorkFactory, projection));
        }

        /// <summary>
        /// Initializes a new instance of the PROJECTCollectionViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the PROJECTCollectionViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        protected PROJECTWORKPACKDashboardCollectionViewModel(IUnitOfWorkFactory<IBluePrintsEntitiesUnitOfWork> unitOfWorkFactory = null, Func<IRepositoryQuery<WORKPACK>, IQueryable<WORKPACK_DashboardInfo>> projection = null)
            : base(unitOfWorkFactory ?? BluePrintsEntitiesUnitOfWorkSource.GetUnitOfWorkFactory(), x => x.WORKPACKS, projection)
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
            return "PROJECTWORKPACKDetailsCollectionViewModel";
        }
        public object GetCustomDocumentParameter()
        {
            return this.SelectedEntity.SummarizablePrincipal;
        }

        public string GetCustomDocumentTitle()
        {
            return this.SelectedEntity.INTERNAL_NAME1 + " RESOURCES";
        }

        public bool IsCustomModeEnabled()
        {
            return true;
        }
    }
}