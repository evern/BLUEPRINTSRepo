using System;
using System.Linq;
using DevExpress.Mvvm.POCO;
using BluePrints.Common.Utils;
using BluePrints.BluePrintsEntitiesDataModel;
using BluePrints.Common.DataModel;
using BluePrints.Data;
using BluePrints.Common.ViewModel;
using BluePrints.Common.Projections;
using System.Collections.Generic;
using System.Resources;
using System.Globalization;
using BluePrints.Common;
using BluePrints.Data.Helpers;
using DevExpress.Xpf.Grid;

namespace BluePrints.ViewModels
{
    /// <summary>
    /// Represents the ROLE_PERMISSIONS collection view model.
    /// </summary>
    public partial class ROLE_PERMISSIONSCollectionViewModelWrapper : CollectionViewModelsWrapper<ROLE_PERMISSION, ROLE_PERMISSIONProjection, Guid, IBluePrintsEntitiesUnitOfWork, CollectionViewModel<ROLE_PERMISSION, ROLE_PERMISSIONProjection, Guid, IBluePrintsEntitiesUnitOfWork>>
    {
        /// <summary>
        /// Creates a new instance of PROGRESS_ITEMSViewModelWrapper as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static ROLE_PERMISSIONSCollectionViewModelWrapper Create()
        {
            return ViewModelSource.Create(() => new ROLE_PERMISSIONSCollectionViewModelWrapper());
        }

        #region Database Operation
        protected override void InitializeParameters(object parameter)
        {

        }

        IUnitOfWorkFactory<IBluePrintsEntitiesUnitOfWork> bluePrintsUnitOfWorkFactory = BluePrintsEntitiesUnitOfWorkSource.GetUnitOfWorkFactory();
        public override void InitializeAndLoadEntitiesLoaderDescription()
        {
            MainViewModel = null;
            loaderCollection = new EntitiesLoaderDescriptionCollection(this);
            loaderCollection.AddEntitiesLoader<ROLE, ROLE, Guid, IBluePrintsEntitiesUnitOfWork>(0, bluePrintsUnitOfWorkFactory, x => x.ROLES);
            
            InvokeEntitiesLoaderDescriptionLoading();
        }

        protected override void OnAllEntitiesCollectionLoaded()
        {
            CreateMainViewModel(this.bluePrintsUnitOfWorkFactory, x => x.ROLE_PERMISSIONS);
            mainThreadDispatcher.BeginInvoke(new Action(() => mainEntityLoader.CreateCollectionViewModel()));
        }

        protected override Func<IRepositoryQuery<ROLE_PERMISSION>, IQueryable<ROLE_PERMISSIONProjection>> ConstructMainViewModelProjection()
        {
            return query => ROLE_PERMISSIONProjectionQueries.JoinSYSTEMPERMISSIONOnROLE_PERMISSIONS(query.Where(x => x.GUID_ROLE == SelectedROLEGuid), SYSTEM_PERMISSIONS);
        }

        protected override void AssignCallBacksAndRaisePropertyChange(IEnumerable<ROLE_PERMISSIONProjection> entities)
        {
            ROLECollectionViewModel.OnSelectedEntitiesChangedCallBack = this.OnSelectedEntityChangedCallBack;
            MainViewModel.ApplyProjectionPropertiesToEntityCallBack = this.ApplyProjectionPropertiesToEntityCallBack;
            MainViewModel.OnEntitySavedCallBack = this.OnEntitiesSavedCallBack;
            MainViewModel.IsPersistentView = true;
            mainThreadDispatcher.BeginInvoke(new Action(() => this.RaisePropertiesChanged()));
        }

        protected override void OnEntitiesChanged(object key, Type changedType, EntityMessageType messageType, object sender)
        {
            if (sender == MainViewModel || sender == ROLECollectionViewModel)
                return;

            if (changedType == typeof(ROLE_PERMISSION))
                MainViewModel.Refresh();
            else
                ROLECollectionViewModel.Refresh();
        }

        #region Collection Call Backs
        public void ApplyProjectionPropertiesToEntityCallBack(ROLE_PERMISSIONProjection projection, ROLE_PERMISSION entity)
        {
            DataUtils.ShallowCopy(entity, projection.ROLE_PERMISSION);
        }

        public void OnEntitiesSavedCallBack(Guid primaryKey, ROLE_PERMISSIONProjection projectionEntity, ROLE_PERMISSION entity, bool isNewEntity)
        {
            projectionEntity.GUID = entity.GUID;
            projectionEntity.ROLE_PERMISSION.GUID = entity.GUID;
        }

        public void OnSelectedEntityChangedCallBack()
        {
            MainViewModel.Refresh();
        }

        Guid SelectedROLEGuid
        {
            get
            {
                if (ROLECollectionViewModel == null || ROLECollectionViewModel.SelectedEntity == null || ROLECollectionViewModel.Entities.Count == 0)
                    return Guid.Empty;

                if (ROLECollectionViewModel.Entities.Count > 0)
                    return ROLECollectionViewModel.Entities.First().GUID;

                return ROLECollectionViewModel.SelectedEntity.GUID;
            }
        }
        #endregion
        #endregion

        #region View Commands
        public void AssignPermission()
        {
            ROLE_PERMISSIONProjection projection = MainViewModel.SelectedEntity;
            
            if(!projection.IS_ASSIGNED)
            {
                projection.ROLE_PERMISSION = new ROLE_PERMISSION()
                {
                    GUID_ROLE = ROLECollectionViewModel.SelectedEntity.GUID,
                    PERMISSION = projection.SYSTEM_PERMISSION.Key, 
                    CREATED = DateTime.Now 
                };

                MainViewModel.Save(projection);
            }
            else
            {
                if(projection.ROLE_PERMISSION != null)
                    MainViewModel.Delete(projection);
            }
        }

        public void dragDropManager_Dropped(object sender, DevExpress.Xpf.Grid.DragDrop.TreeListDroppedEventArgs e)
        {
            if (e.TargetNode != null)
                foreach (TreeListNode obj in e.DraggedRows)
                {
                    ROLECollectionViewModel.Save((ROLE)obj.Content);
                }
        }
        #endregion

        #region View Properties
        /// <summary>
        /// The view name to be used when saving layout for IDocumentContent
        /// </summary>
        protected override string ViewName
        {
            get
            {
                return "ROLE_PERMISSIONSCollectionViewModelWrapper";
            }
        }

        CollectionViewModel<ROLE, ROLE, Guid, IBluePrintsEntitiesUnitOfWork> roleCollectionViewModel { get; set; }
        public CollectionViewModel<ROLE, ROLE, Guid, IBluePrintsEntitiesUnitOfWork> ROLECollectionViewModel
        {
            get
            {
                if (roleCollectionViewModel == null && MainViewModel != null)
                    roleCollectionViewModel = (CollectionViewModel<ROLE, ROLE, Guid, IBluePrintsEntitiesUnitOfWork>)loaderCollection.GetViewModel<ROLE>();

                return roleCollectionViewModel;
            }
        }

        Dictionary<string, string> systempermissions;
        public Dictionary<string, string> SYSTEM_PERMISSIONS
        {
            get
            {
                if (systempermissions == null)
                    systempermissions = GetPermissionLookUpInDictionary();

                return systempermissions;
            }
        }

        Dictionary<string, string> GetPermissionLookUpInDictionary()
        {
            Dictionary<string, string> returnPermissions = new Dictionary<string, string>();
            ResourceSet resourceSet = PermissionResources.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
            foreach (System.Collections.DictionaryEntry permission in resourceSet)
            {
                returnPermissions.Add(permission.Key.ToString(), permission.Value.ToString());
            }

            return returnPermissions;
        }

        public IEnumerable<ROLE> ROLECollection
        {
            get
            {
                return GetEntities<ROLE>();
            }
        }
        #endregion
    }
}