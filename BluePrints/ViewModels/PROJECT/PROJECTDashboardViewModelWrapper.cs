﻿using System;
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
using BluePrints.Common.Projections;

namespace BluePrints.ViewModels
{
    /// <summary>
    /// Represents the PROJECTS collection view model.
    /// </summary>
    public class PROJECTDashboardViewModelWrapper : DashboardViewModelWrapper<PROJECT, PROJECT_Dashboard, Guid, IBluePrintsEntitiesUnitOfWork>, ISupportCustomDocumentTypeNameAndParameter
    {
        /// <summary>
        /// Creates a new instance of PROJECT_ITEMSViewModelWrapper as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static PROJECTDashboardViewModelWrapper Create()
        {
            return ViewModelSource.Create(() => new PROJECTDashboardViewModelWrapper());
        }

        /// <summary>
        /// Initializes a new instance of the PROJECTViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the PROJECTViewModel type without the POCO proxy factory.
        /// </summary>
        protected PROJECTDashboardViewModelWrapper()
        {
        }

        #region Database Operation
        IUnitOfWorkFactory<IBluePrintsEntitiesUnitOfWork> bluePrintsUnitOfWorkFactory = BluePrintsEntitiesUnitOfWorkSource.GetUnitOfWorkFactory();
        protected override void InitializeParameters(object parameter)
        {

        }

        public override void InitializeAndLoadEntitiesLoaderDescription()
        {
            MainViewModel = null;
            loaderCollection = new EntitiesLoaderDescriptionCollection(this);
            loaderCollection.AddEntitiesLoader<BASELINE, BASELINE, Guid, IBluePrintsEntitiesUnitOfWork>(1, bluePrintsUnitOfWorkFactory, x => x.BASELINES, BASELINEProjectionFunc, null, null, OnEntitiesChanged);
            loaderCollection.AddEntitiesLoader<PROGRESS, PROGRESS, Guid, IBluePrintsEntitiesUnitOfWork>(2, bluePrintsUnitOfWorkFactory, x => x.PROGRESSES, PROGRESSProjectionFunc, null, null, OnEntitiesChanged);
            loaderCollection.AddEntitiesLoader<RATE, RATE, Guid, IBluePrintsEntitiesUnitOfWork>(3, bluePrintsUnitOfWorkFactory, x => x.RATES, null, null, null, OnEntitiesChanged);
            loaderCollection.AddEntitiesLoader<VARIATION, VARIATION, Guid, IBluePrintsEntitiesUnitOfWork>(4, bluePrintsUnitOfWorkFactory, x => x.VARIATIONS, VARIATIONProjectionFunc, null, null, OnEntitiesChanged);
            InvokeEntitiesLoaderDescriptionLoading();
        }

        Func<IRepositoryQuery<BASELINE>, IQueryable<BASELINE>> BASELINEProjectionFunc()
        {
            return query => query.Where(x => x.STATUS == BaselineStatus.Live).Include(x => x.BASELINE_ITEMS);
        }

        Func<IRepositoryQuery<PROGRESS>, IQueryable<PROGRESS>> PROGRESSProjectionFunc()
        {
            return query => query.Where(x => x.STATUS == ProgressStatus.Live).Include(x => x.PROGRESS_ITEMS);
        }

        Func<IRepositoryQuery<VARIATION>, IQueryable<VARIATION>> VARIATIONProjectionFunc()
        {
            return query => query.Where(x => x.APPROVED != null);
        }

        protected override void OnAllEntitiesCollectionLoaded()
        {
            CreateMainViewModel(this.bluePrintsUnitOfWorkFactory, x => x.PROJECTS);
            mainThreadDispatcher.BeginInvoke(new Action(() => mainEntityLoader.CreateCollectionViewModel()));
        }

        protected override Func<IRepositoryQuery<PROJECT>, IQueryable<PROJECT_Dashboard>> ConstructMainViewModelProjection()
        {
            Func<IQueryable<BASELINE>> getBASELINESFunc = loaderCollection.GetCollectionFunc<BASELINE>();
            Func<IQueryable<PROGRESS>> getPROGRESSESFunc = loaderCollection.GetCollectionFunc<PROGRESS>();
            Func<IQueryable<RATE>> getRATESFunc = loaderCollection.GetCollectionFunc<RATE>();
            Func<IQueryable<VARIATION>> getVARIATIONSFunc = loaderCollection.GetCollectionFunc<VARIATION>();

            return query => PROJECT_DashboardQueries.SummarizePROJECTDashboard(query, getPROGRESSESFunc, getBASELINESFunc, getRATESFunc, getVARIATIONSFunc);
        }

        protected override bool OnMainViewModelLoaded(IEnumerable<PROJECT_Dashboard> entities)
        {
            MainViewModel = (CollectionViewModel<PROJECT, PROJECT_Dashboard, Guid, IBluePrintsEntitiesUnitOfWork>)mainEntityLoader.GetViewModel();
            mainThreadDispatcher.BeginInvoke(new Action(() => this.RaisePropertiesChanged()));
            base.OnMainViewModelLoaded(entities);
            return true;
        }

        protected override void OnEntitiesChanged(object key, Type changedType, EntityMessageType messageType, object sender)
        {

        }
        #endregion

        #region View Properties
        protected IDocumentManagerService DocumentManagerService { get { return this.GetService<IDocumentManagerService>(); } }
        public bool CanEdit(PROJECT_Dashboard entity)
        {
            if (MainViewModel == null || MainViewModel.SelectedEntity == null)
                return false;

            return true;
        }

        public void Edit(PROJECT_Dashboard entity)
        {
            if (entity == null)
                return;

            DocumentManagerService.ShowExistingEntityDocument<WORKPACK_Dashboard, Guid>(this, entity.GUID, string.Empty);
        }

        /// <summary>
        /// The view name to be used when saving layout for IDocumentContent
        /// </summary>
        protected override string ViewName
        {
            get
            {
                return "PROJECTDashboardViewModelWrapper";
            }
        }
        #endregion

        #region ISupportCustomDocumentTypeNameAndParameter
        public string GetCustomDocumentTypeName()
        {
            return "PROJECTWORKPACKDashboardView";
        }

        public object GetCustomDocumentParameter()
        {
            return this.MainViewModel.SelectedEntity;
        }

        public string GetCustomDocumentTitle()
        {
            return this.MainViewModel.SelectedEntity.PROJECT.NUMBER + " - WORKPACKS";
        }

        public bool IsCustomModeEnabled()
        {
            return true;
        }
        #endregion
    }
}