﻿using System;
using System.Linq;
using DevExpress.Mvvm.POCO;
using BluePrints.Common.Utils;
using BluePrints.BluePrintsEntitiesDataModel;
using BluePrints.Common.DataModel;
using BluePrints.Data;
using BluePrints.Common.ViewModel;
using BluePrints.Data.Helpers;
using System.Resources;
using BluePrints.Common;
using System.Globalization;
using System.Collections.Generic;
using DevExpress.Xpf.Grid;
using System.Windows.Threading;
using DevExpress.Mvvm;
using System.ComponentModel;

namespace BluePrints.ViewModels
{
    public class ROLECollectionViewModelWrapper : IDocumentContent
    {
        public ROLECollectionViewModel ROLECollection { get; set; }
        public ROLE_PERMISSIONSProjectionCollectionViewModel ROLE_PERMISSIONCollection { get; set; }
        public ROLECollectionViewModelWrapper Create()
        {
            return ViewModelSource.Create(() => new ROLECollectionViewModelWrapper());
        }

        Guid GetROLE_KEYFunc()
        {
            if (ROLECollection.SelectedEntity == null)
                return Guid.Empty;

            return ROLECollection.SelectedEntity.GUID;
        }

        DispatcherTimer selectFirstRoleDispatcher;
        protected ROLECollectionViewModelWrapper()
        {
            ROLECollection = ROLECollectionViewModel.Create();
            ROLECollection.SetParentViewModel(this);
            ROLECollection.OnSelectedEntityChangedCallBack = this.OnSelectedEntityChangedCallBack;
            ROLECollection.OnEntitiesLoadedCallBack = this.OnEntitiesLoaded;

            ROLE_PERMISSIONCollection = ROLE_PERMISSIONSProjectionCollectionViewModel.Create(null, query => ROLE_PERMISSIONProjectionQueries.GetAssignedROLE_PERMISSIONByROLE(query, GetROLE_KEYFunc, GetSystemPermissions()));
            ROLE_PERMISSIONCollection.ApplyProjectionPropertiesToEntityCallBack = this.ApplyProjectionPropertiesToEntityCallBack;
            ROLE_PERMISSIONCollection.OnEntitiesLoadedCallBack = this.OnEntitiesLoaded;
            ROLE_PERMISSIONCollection.IsPersistentView = true;
            ROLE_PERMISSIONCollection.SetParentViewModel(this);
            selectFirstRoleDispatcher = new DispatcherTimer();
            selectFirstRoleDispatcher.Interval = new TimeSpan(0, 0, 0, 0, 1);
            selectFirstRoleDispatcher.Tick += selectFirstRoleDispatcher_Tick;
        }

        public void OnEntitiesLoaded(IEnumerable<object> entities)
        {
            detailedEntitiesLoaded += 1;
            if (detailedEntitiesLoaded == 2)
                selectFirstRoleDispatcher.Start();
        }

        void selectFirstRoleDispatcher_Tick(object sender, EventArgs e)
        {
            selectFirstRoleDispatcher.Stop();
            if (ROLECollection.Entities.Count > 0)
            {
                ROLECollection.SelectedEntity = ROLECollection.Entities.Where(x => x.PARENTGUID == Guid.Empty).First();
                ROLE_PERMISSIONCollection.Refresh();
            }
        }

        int detailedEntitiesLoaded = 0;
        public void dragDropManager_Dropped(object sender, DevExpress.Xpf.Grid.DragDrop.TreeListDroppedEventArgs e)
        {
            if (e.TargetNode != null)
                foreach (TreeListNode obj in e.DraggedRows)
                {
                    ROLECollection.Save((ROLE)obj.Content);
                }
        }

        public void AssignPermission(CellValueChangedEventArgs e)
        {
            ROLE_PERMISSIONInfo projection = (ROLE_PERMISSIONInfo)e.Row;
            bool isChecked = (bool)e.Value;
            projection.GUID_ROLE = ROLECollection.SelectedEntity.GUID;
            projection.ASSIGNED = isChecked;
            ROLE_PERMISSIONSave(projection);
        }

        /// <summary>
        /// Keeping consistency between actual and projection entity during save
        /// </summary>
        /// <param name="projection">projection entity</param>
        public void ROLE_PERMISSIONSave(ROLE_PERMISSIONInfo projection)
        {
            var actualEntity = ROLE_PERMISSIONCollection.Entities.FirstOrDefault(x => x.PERMISSION == projection.PERMISSION);
            if (actualEntity != null)
                DataUtils.ShallowCopy(actualEntity, projection);
            else
                actualEntity = projection;

            ROLE_PERMISSIONCollection.Save(actualEntity);
            DataUtils.ShallowCopy(projection, actualEntity); //copy the generated key into projection
        }

        public void OnSelectedEntityChangedCallBack(ROLE Entity)
        {
            ROLE_PERMISSIONCollection.Refresh();
        }

        public void ApplyProjectionPropertiesToEntityCallBack(ROLE_PERMISSIONInfo ROLEPERMISSIONInfo, ROLE_PERMISSION ROLEPERMISSION)
        {
            DataUtils.ShallowCopy(ROLEPERMISSION, ROLEPERMISSIONInfo, false);
        }

        Dictionary<string, string> permissionLookUp;
        public Dictionary<string, string> PermissionLookUp
        {
            get 
            { 
                if(permissionLookUp == null)
                    permissionLookUp = GetPermissionLookUpInDictionary();

                return permissionLookUp; 
            }
        }
        private IQueryable<ROLE_PERMISSION> GetSystemPermissions()
        {
            ResourceSet resourceSet = PermissionResources.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
            List<ROLE_PERMISSION> permissions = new List<ROLE_PERMISSION>();
            foreach (System.Collections.DictionaryEntry permission in resourceSet)
            {
                permissions.Add(new ROLE_PERMISSION() { PERMISSION = permission.Key.ToString() });
            }

            return permissions.AsQueryable();
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
            ROLECollection.OnDestroy();
            ROLE_PERMISSIONCollection.OnDestroy();
        }

        IDocumentOwner IDocumentContent.DocumentOwner
        {
            get { return DocumentOwner; }
            set { DocumentOwner = value; }
        }
        #endregion
    }

    /// <summary>
    /// Represents the ROLES collection view model.
    /// </summary>
    public partial class ROLECollectionViewModel : CollectionViewModel<ROLE, Guid, IBluePrintsEntitiesUnitOfWork>
    {

        /// <summary>
        /// Creates a new instance of ROLECollectionViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static ROLECollectionViewModel Create(IUnitOfWorkFactory<IBluePrintsEntitiesUnitOfWork> unitOfWorkFactory = null)
        {
            return ViewModelSource.Create(() => new ROLECollectionViewModel(unitOfWorkFactory));
        }

        /// <summary>
        /// Initializes a new instance of the ROLECollectionViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the ROLECollectionViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        protected ROLECollectionViewModel(IUnitOfWorkFactory<IBluePrintsEntitiesUnitOfWork> unitOfWorkFactory = null)
            : base(unitOfWorkFactory ?? BluePrintsEntitiesUnitOfWorkSource.GetUnitOfWorkFactory(), x => x.ROLES)
        {
        }

        public Action<ROLE> OnSelectedEntityChangedCallBack;
        protected override void OnSelectedEntityChanged()
        {
            base.OnSelectedEntityChanged();
            if (OnSelectedEntityChangedCallBack != null)
                OnSelectedEntityChangedCallBack(this.SelectedEntity);
        }
    }

    /// <summary>
    /// Represents the ROLE_PERMISSIONS collection view model.
    /// </summary>
    public partial class ROLE_PERMISSIONSProjectionCollectionViewModel : CollectionViewModel<ROLE_PERMISSION, ROLE_PERMISSIONInfo, Guid, IBluePrintsEntitiesUnitOfWork>
    {

        /// <summary>
        /// Creates a new instance of ROLE_PERMISSIONSCollectionViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static ROLE_PERMISSIONSProjectionCollectionViewModel Create(IUnitOfWorkFactory<IBluePrintsEntitiesUnitOfWork> unitOfWorkFactory = null, Func<IRepositoryQuery<ROLE_PERMISSION>, IQueryable<ROLE_PERMISSIONInfo>> projection = null)
        {
            return ViewModelSource.Create(() => new ROLE_PERMISSIONSProjectionCollectionViewModel(unitOfWorkFactory, projection));
        }

        /// <summary>
        /// Initializes a new instance of the ROLE_PERMISSIONSCollectionViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the ROLE_PERMISSIONSCollectionViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        protected ROLE_PERMISSIONSProjectionCollectionViewModel(IUnitOfWorkFactory<IBluePrintsEntitiesUnitOfWork> unitOfWorkFactory = null, Func<IRepositoryQuery<ROLE_PERMISSION>, IQueryable<ROLE_PERMISSIONInfo>> projection = null)
            : base(unitOfWorkFactory ?? BluePrintsEntitiesUnitOfWorkSource.GetUnitOfWorkFactory(), x => x.ROLE_PERMISSIONS, projection)
        {
        }
    }
}