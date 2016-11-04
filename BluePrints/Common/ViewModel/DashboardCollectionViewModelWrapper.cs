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

namespace BluePrints.Common.ViewModel
{
    public abstract class DashboardCollectionViewModelWrapper<TEntity, TDashboardEntity> : ISupportLogicalLayout
        where TEntity : class
        where TDashboardEntity : class, ISupportSummaryCalculation, new()
    {
        protected IUnitOfWorkFactory<IBluePrintsEntitiesUnitOfWork> UnitOfWorkFactory;
        protected virtual CollectionViewModel<TEntity, TDashboardEntity, Guid, IBluePrintsEntitiesUnitOfWork> dashboardCollectionViewModel { get; set; }
        public abstract CollectionViewModel<TEntity, TDashboardEntity, Guid, IBluePrintsEntitiesUnitOfWork> DashboardCollection { get; }

        public void InitializeDashboardCollectionViewModel(CollectionViewModel<TEntity, TDashboardEntity, Guid, IBluePrintsEntitiesUnitOfWork> dashboardCollectionViewModel)
        {
            dashboardCollectionViewModel.OnSelectedEntitiesChangedCallBack = this.OnSelectedEntityChanged;
        }

        public ISupportSummaryCalculation EntitiesSummary { get; set; }
        public void OnSelectedEntityChanged(IEnumerable<TDashboardEntity> entities)
        {
            if (entities.Count() == 0)
                return;

            if (entities.Count() == 1)
                EntitiesSummary = entities.First();
            else
            {
                TDashboardEntity newDashboardInfo = new TDashboardEntity();
                DateTime earliestDataDate = entities.Min(x => x.SummarizablePrincipal.ReportingDataDate);
                var earliestLiveProgress = entities.First(x => x.SummarizablePrincipal.LivePROGRESS.DATA_DATE == earliestDataDate).SummarizablePrincipal.LivePROGRESS;
                newDashboardInfo.SummarizablePrincipal = DefaultSummaryCalculation.Create();
                newDashboardInfo.SummarizablePrincipal.LivePROGRESS = earliestLiveProgress;
                newDashboardInfo.SummarizablePrincipal.IntervalPeriod = ISupportProgressReportingExtensions.ConvertProgressIntervalToPeriod(earliestLiveProgress);
                newDashboardInfo.SummarizablePrincipal.ReportableObjects = entities.SelectMany(x => x.SummarizablePrincipal.ReportableObjects);
                newDashboardInfo.SummarizablePrincipal.FirstAlignedDataDate = entities.Min(x => x.SummarizablePrincipal.FirstAlignedDataDate);
                newDashboardInfo.SummarizablePrincipal.ReportingDataDate = earliestLiveProgress.DATA_DATE;
                newDashboardInfo.SummarizablePrincipal.NonCumulative_VariationAdjustments = new ObservableCollection<VariationAdjustment>(entities.SelectMany(x => x.SummarizablePrincipal.NonCumulative_VariationAdjustments));
                newDashboardInfo.SummarizablePrincipal.NonCumulative_ActualDataPoints = new ObservableCollection<ProgressInfo>(entities.SelectMany(x => x.SummarizablePrincipal.NonCumulative_ActualDataPoints));
                newDashboardInfo.SummarizablePrincipal.NonCumulative_BurnedDataPoints = new ObservableCollection<ProgressInfo>(entities.SelectMany(x => x.SummarizablePrincipal.NonCumulative_BurnedDataPoints));
                newDashboardInfo.SummarizablePrincipal.NonCumulative_EarnedDataPoints = new ObservableCollection<ProgressInfo>(entities.SelectMany(x => x.SummarizablePrincipal.NonCumulative_EarnedDataPoints));
                newDashboardInfo.SummarizablePrincipal.NonCumulative_OriginalDataPoints = new ObservableCollection<ProgressInfo>(entities.SelectMany(x => x.SummarizablePrincipal.NonCumulative_OriginalDataPoints));
                newDashboardInfo.SummarizablePrincipal.NonCumulative_PlannedDataPoints = new ObservableCollection<ProgressInfo>(entities.SelectMany(x => x.SummarizablePrincipal.NonCumulative_PlannedDataPoints));
                newDashboardInfo.SummarizablePrincipal.NonCumulative_RemainingPlannedDataPoints = new ObservableCollection<ProgressInfo>(entities.SelectMany(x => x.SummarizablePrincipal.NonCumulative_RemainingPlannedDataPoints));
                ISupportProgressReportingExtensions.GenerateCumulativeSummaryDataPoints(newDashboardInfo.SummarizablePrincipal);
                EntitiesSummary = newDashboardInfo;
            }

            this.RaisePropertyChanged(x => x.EntitiesSummary);
        }

        public virtual void OnLoaded()
        {
            DashboardCollection.OnLoaded();
        }

        public virtual void OnUnloaded()
        {
            DashboardCollection.OnUnloaded();
            DashboardCollection.Destroy();
        }

        public virtual bool CanChangeStatsType(object checkButton)
        {
            return (DashboardCollection != null && !DashboardCollection.IsLoading);
        }

        public Action<DashboardViewType> ChangeViewMemberFieldNames { get; set; }
        public virtual void ChangeStatsType(object checkButton)
        {
            BarCheckItem button = (BarCheckItem)checkButton;
            DashboardViewType calculationType = button.Name.ToUpper().Contains("COSTS") ? DashboardViewType.Costs : DashboardViewType.Units;
            if (ChangeViewMemberFieldNames != null)
                ChangeViewMemberFieldNames(calculationType);

            foreach (var PROJECT_DashboardInfo in this.DashboardCollection.Entities)
            {
                PROJECT_DashboardInfo.SummarizablePrincipal.RecalculateStats(calculationType == DashboardViewType.Costs);
            }
        }

        #region ISupportLogicalLayout
        bool ISupportLogicalLayout.CanSerialize
        {
            get { return true; }
        }

        public IDocumentManagerService ReturnDocumentManagerService()
        {
            return DocumentManagerService;
        }

        protected IDocumentManagerService DocumentManagerService { get { return this.GetService<IDocumentManagerService>(); } }
        IDocumentManagerService ISupportLogicalLayout.DocumentManagerService
        {
            get { return DocumentManagerService; }
        }

        IEnumerable<object> ISupportLogicalLayout.LookupViewModels
        {
            get { return null; }
        }
        #endregion
    }
}
