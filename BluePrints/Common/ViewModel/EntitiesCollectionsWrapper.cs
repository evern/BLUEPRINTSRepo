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
    public abstract class EntitiesCollectionsWrapper<TMainEntity, TMainViewModel> : IDocumentContent, ISupportParameter
        where TMainEntity : class
        where TMainViewModel : IEntitiesViewModel<TMainEntity>
    {
        protected bool isSubEntitiesAdded;
        protected List<IEntitiesLoader> subEntitiesLoader = new List<IEntitiesLoader>();
        protected List<IEntitiesLoader> auxiliaryEntitiesLoader = new List<IEntitiesLoader>();
        protected EntitiesLoader<TMainEntity> mainEntityLoader;
        protected TMainViewModel mainViewModel;

        protected virtual void OnParameterChanged(object parameter) { }

        protected virtual void OnSubEntitiesLoaded(IEnumerable<object> entities) { }

        protected virtual void AfterSubEntitiesLoaded() { }

        protected virtual void OnMainEntitiesFirstLoaded(IEnumerable<TMainEntity> entities)
        {
            if(mainEntityLoader != null)
                mainEntityLoader.SetOnEntitiesFirstLoadedCallBack(null);
        }

        protected virtual bool IsAllSubEntitiesLoaded
        {
            get { return isSubEntitiesAdded && subEntitiesLoader.All(x => x != null && x.isLoaded); }
        }

        protected virtual bool IsMainEntityLoaded
        {
            get { return mainEntityLoader != null && mainEntityLoader.isLoaded; }
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
        protected string ViewName { get; set; }
        protected virtual void OnClose(CancelEventArgs e) { }
        void IDocumentContent.OnClose(CancelEventArgs e)
        {
            OnClose(e);
        }

        void IDocumentContent.OnDestroy()
        {
            this.SaveLayout();
            foreach (IEntitiesLoader entityLoader in subEntitiesLoader)
            {
                if (entityLoader != null)
                    entityLoader.OnDestroy();
            }

            foreach (IEntitiesLoader entityLoader in auxiliaryEntitiesLoader)
            {
                if (entityLoader != null)
                    entityLoader.OnDestroy();
            }

            if ((IEntitiesLoader)mainEntityLoader != null)
                ((IEntitiesLoader)mainEntityLoader).OnDestroy();

            if(mainViewModel != null)
                mainViewModel.OnDestroy();
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
}
