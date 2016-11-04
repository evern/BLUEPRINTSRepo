using BluePrints.Common.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace BluePrints.Data.Helpers
{
    /// <summary>
    /// Initializes and force load the EntitiesViewModel
    /// also contains callBack to notify when Entities are loaded
    /// </summary>
    /// <typeparam name="TReturnEntity">Type of entity to return</typeparam>
    public class EntitiesLoader<TReturnEntity> : IEntitiesLoader
        where TReturnEntity : class
    {
        IEntitiesViewModel<TReturnEntity> collectionViewModel;
        Action<IEnumerable<TReturnEntity>> onEntitiesLoadedCallBack;
        public EntitiesLoader(Func<IEntitiesViewModel<TReturnEntity>> GetLookUpEntitiesViewModelFunc, Action<IEnumerable<TReturnEntity>> OnEntitiesLoadedCallBackFunc = null, Action<object, Type, EntityMessageType, object> OnEntitiesChangedCallBackFunc = null, bool defferedLoading = false)
        {
            this.collectionViewModel = GetLookUpEntitiesViewModelFunc();
            this.onEntitiesLoadedCallBack = OnEntitiesLoadedCallBackFunc;
            this.collectionViewModel.OnEntitiesLoadedCallBack = this.OnEntitiesLoaded;
            this.collectionViewModel.OnEntitiesChangedCallBack = OnEntitiesChangedCallBackFunc;
            //stimulate the loading of entities to avoid cross-thread operation because entities will call LoadCore on another thread
            if (!defferedLoading)
                this.collectionViewModel.Entities.ToList();
        }

        public void OnDestroy()
        {
            if (this.collectionViewModel != null)
            {
                collectionViewModel.OnDestroy();

                this.onEntitiesLoadedCallBack = null;
                this.collectionViewModel.OnEntitiesLoadedCallBack = null;
                this.collectionViewModel.OnEntitiesChangedCallBack = null;
                collectionViewModel = null;
            }
        }

        public void SetOnEntitiesFirstLoadedCallBack(Action<IEnumerable<TReturnEntity>> OnEntitiesLoadedCallBackFunc)
        {
            this.onEntitiesLoadedCallBack = OnEntitiesLoadedCallBackFunc;
        }

        bool isloaded;
        public bool isLoaded
        {
            get { return isloaded; }
            set { isloaded = value; }
        }

        void OnEntitiesLoaded(IEnumerable<TReturnEntity> loadedEntities)
        {
            isLoaded = true;
            if (onEntitiesLoadedCallBack != null)
                onEntitiesLoadedCallBack(loadedEntities);
        }

        /// <summary>
        /// Call this only after entities has been loaded as notified by OnEntitiesLoadedCallBackFunc
        /// </summary>
        public IQueryable<TReturnEntity> GetCollectionFunc()
        {
            //this.collectionViewModel.OnEntitiesLoadedCallBack = null;
            if (this.collectionViewModel == null || this.collectionViewModel.Entities == null)
                return new List<TReturnEntity>().AsQueryable();
            else
                return this.collectionViewModel.Entities.AsQueryable();
        }

        public IEnumerable<TReturnEntity> Collection
        {
            get
            {
                return GetCollectionFunc().AsEnumerable();
            }
        }

        /// <summary>
        /// Call this only after entities has been loaded as notified by OnEntitiesLoadedCallBackFunc
        /// </summary>
        public TReturnEntity GetSingleObjectFunc()
        {
            //this.collectionViewModel.OnEntitiesLoadedCallBack = null;
            if (this.collectionViewModel.Entities == null)
                return null;
            else
            {
                if (this.collectionViewModel.Entities.Count == 0)
                    return null;

                return this.collectionViewModel.Entities.First();
            }
        }

        /// <summary>
        /// Sometimes collectionViewModel retrieval is necessary so that its CRUD methods could be used, 
        /// Note that when CRUD methods are used the entity must be derived from the containing collectionViewModel's repository
        /// </summary>
        /// <returns></returns>
        public IEntitiesViewModel<TReturnEntity> GetCollectionViewModel()
        {
            return this.collectionViewModel;
        }
    }

    public interface IEntitiesLoader
    {
        void OnDestroy();
        bool isLoaded { get; set; }
    }
}
