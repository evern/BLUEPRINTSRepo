using BluePrints.BluePrintsEntitiesDataModel;
using BluePrints.Common.DataModel;
using BluePrints.Common.Helpers;
using BluePrints.Common.Projections;
using BluePrints.Data.Helpers;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace BluePrints.Common.ViewModel
{
    public abstract class CollectionViewModelsWrapper<TMainEntity, TMainProjectionEntity, TMainEntityPrimaryKey, TMainEntityUnitOfWork, TMainViewModel> : ICollectionViewModelsWrapper, IDocumentContent, ISupportParameter
        where TMainEntity : class
        where TMainProjectionEntity : class
        where TMainEntityUnitOfWork : IUnitOfWork
        where TMainViewModel : IEntitiesViewModel<TMainProjectionEntity>
    {
        protected bool isSubEntitiesAdded;
        protected EntitiesLoaderDescriptionCollection entitiesLoaderDescriptionCollection = null;
        protected EntitiesLoaderDescription<TMainEntity, TMainProjectionEntity, TMainEntityPrimaryKey, TMainEntityUnitOfWork> mainEntityLoader;
        protected TMainViewModel mainViewModel;
        protected Dispatcher mainThreadDispatcher = Application.Current.Dispatcher;

        public virtual void InvokeEntitiesLoaderDescriptionLoading()
        {
            if (mainViewModel != null)
                return;
            else if (isAllEntitiesLoaded())
                mainThreadDispatcher.BeginInvoke(new Action(() => OnAllEntitiesCollectionLoaded()));
            else
                mainThreadDispatcher.BeginInvoke(new Action(() => loadEntitiesCollectionOnMainThread()));
        }

        /// <summary>
        /// Begins loading the collection of entities loader
        /// </summary>
        void loadEntitiesCollectionOnMainThread()
        {
            int currentLoadOrder = entitiesLoaderDescriptionCollection.Where(x => !x.isLoaded).Min(x => x.loadOrder);
            IEntitiesLoaderDescription entitiesLoaderDescription = entitiesLoaderDescriptionCollection.First(x => x.loadOrder == currentLoadOrder);

            if (entitiesLoaderDescription.dependencyType != null)
            {
                if (entitiesLoaderDescriptionCollection.IsEntitiesLoaderExists(entitiesLoaderDescription.dependencyType))
                {
                    IEntitiesLoaderDescription dependentEntitiesLoaderDescription = entitiesLoaderDescriptionCollection.GetLoader(entitiesLoaderDescription.dependencyType);
                    if (!dependentEntitiesLoaderDescription.isLoaded)
                        throw new InvalidOperationException("Dependent entities loader is sequenced after the current entities loader.");
                    else
                        entitiesLoaderDescription.CreateCollectionViewModel();
                }
                else
                    throw new InvalidOperationException("Dependent entities loader not added.");
            }
            else
                entitiesLoaderDescription.CreateCollectionViewModel();
        }

        bool isAllEntitiesLoaded()
        {
            if (entitiesLoaderDescriptionCollection == null)
                return false;

            return entitiesLoaderDescriptionCollection.Where(x => !x.isLoaded).Count() == 0 ? true : false;
        }

        protected IEnumerable<TProjection> GetEntities<TProjection>()
            where TProjection : class
        {
            if (entitiesLoaderDescriptionCollection == null)
                return null;

            Func<IEnumerable<TProjection>> getCollectionFunc = entitiesLoaderDescriptionCollection.GetCollectionFunc<TProjection>();
            return getCollectionFunc();
        }

        protected virtual void OnParameterChanged(object parameter)
        {
            InitializeParameters(parameter);
            InitializeAndLoadEntitiesLoaderDescription();
        }

        protected virtual void InitializeParameters(object parameter)
        {
            throw new NotImplementedException("Override this method to initialize primary parameter attributes in inherited member.");
        }

        public virtual void InitializeAndLoadEntitiesLoaderDescription()
        {
            throw new NotImplementedException("Override this method to initialize EntitiesLoaderDescriptionCollection.");
        }

        protected virtual void OnAllEntitiesCollectionLoaded()
        {
            throw new NotImplementedException("Override this method to initialize main entity loader.");
        }

        protected virtual void OnMainViewModelAssigned(IEnumerable<TMainProjectionEntity> entities)
        {
            mainViewModel = (TMainViewModel)mainEntityLoader.GetViewModel();
        }

        protected virtual void OnEntitiesChanged(object key, Type changedType, EntityMessageType messageType, object sender)
        {
            throw new NotImplementedException("Override this method to reload or refresh the main view model.");
        }

        #region ISupportParameter
        object ISupportParameter.Parameter
        {
            get { return null; }
            set { OnParameterChanged(value); }
        }
        #endregion

        #region IDocumentContent
        protected IDocumentOwner DocumentOwner { get; private set; }
        object IDocumentContent.Title { get { return null; } }
        protected virtual string ViewName
        {
            get
            {
                throw new NotImplementedException("Override this method to specify the view name.");
            }
        }

        protected virtual void OnClose(CancelEventArgs e) { }
        void IDocumentContent.OnClose(CancelEventArgs e)
        {
            OnClose(e);
        }

        void IDocumentContent.OnDestroy()
        {
            this.SaveLayout();

            if (mainEntityLoader != null)
                mainEntityLoader.OnDestroy();

            if (entitiesLoaderDescriptionCollection == null)
                return;

            foreach (IEntitiesLoaderDescription entityLoaderDescription in entitiesLoaderDescriptionCollection)
            {
                entityLoaderDescription.OnDestroy();
            }
        }

        IDocumentOwner IDocumentContent.DocumentOwner
        {
            get { return DocumentOwner; }
            set { DocumentOwner = value; }
        }
        #endregion

        protected IMessageBoxService MessageBoxService { get { return this.GetRequiredService<IMessageBoxService>(); } }
        protected ILayoutSerializationService LayoutSerializationService { get { return this.GetService<ILayoutSerializationService>(); } }
        void SaveLayout()
        {
            PersistentLayoutHelper.TrySerializeLayout(LayoutSerializationService, ViewName);
            PersistentLayoutHelper.SaveLayout();
        }
    }

    public interface ICollectionViewModelsWrapper
    {
        void InvokeEntitiesLoaderDescriptionLoading();

        void InitializeAndLoadEntitiesLoaderDescription();
    }
}
