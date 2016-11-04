﻿using BluePrints.Common.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluePrints.Common.ViewModel.UndoRedo;
using DevExpress.Xpf.Grid;
using System.Collections.ObjectModel;
using DevExpress.Xpf.Bars;
using System.Windows;
using DevExpress.Mvvm.POCO;
using BluePrints.Data.Attributes;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using DevExpress.Mvvm;
using DevExpress.Xpf.Editors;
using System.Text.RegularExpressions;
using System.Linq.Expressions;
using BluePrints.Common.Utils;
using BluePrints.Data.Helpers;
using BluePrints.Common.ViewModel.Filtering;
using BluePrints.Common.ViewModel.Utils;
using DevExpress.Xpf.Editors.Settings;
using DevExpress.Xpf.Grid.TreeList;

namespace BluePrints.Common.ViewModel
{
    /// <summary>
    /// The base class for a POCO view models exposing a colection of entities of a given type and CRUD operations against these entities.
    /// This is a partial class that provides extension point to add custom properties, commands and override methods without modifying the auto-generated code.
    /// All extensions from DevExpress will be implemented here
    /// </summary>
    /// <typeparam name="TEntity">An entity type.</typeparam>
    /// <typeparam name="TProjection">A projection entity type.</typeparam>
    /// <typeparam name="TPrimaryKey">A primary key value type.</typeparam>
    /// <typeparam name="TUnitOfWork">A unit of work type.</typeparam>
    public partial class CollectionViewModel<TEntity, TProjection, TPrimaryKey, TUnitOfWork> : CollectionViewModelBase<TEntity, TProjection, TPrimaryKey, TUnitOfWork>, ISupportUndoRedo<TProjection>, ISupportFiltering<TEntity>
        where TEntity : class
        where TProjection : class
        where TUnitOfWork : IUnitOfWork
    {

        /// <summary>
        /// Initializes a new instance of the CollectionViewModel class.
        /// This constructor is declared protected to avoid an undesired instantiation of the CollectionViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        /// <param name="getRepositoryFunc">A function that returns a repository representing entities of the given type.</param>
        /// <param name="projection">A LINQ function used to customize a query for entities. The parameter, for example, can be used for sorting data and/or for projecting data to a custom type that does not match the repository entity type.</param>
        /// <param name="newEntityInitializer">An optional parameter that provides a function to initialize a new entity. This parameter is used in the detail collection view models when creating a single object view model for a new entity.</param>
        /// <param name="canCreateNewEntity">A function that is called before an attempt to create a new entity is made. This parameter is used together with the newEntityInitializer parameter.</param>
        /// <param name="ignoreSelectEntityMessage">An optional parameter that used to specify that the selected entity should not be managed by PeekCollectionViewModel.</param>
        protected CollectionViewModel(
            IUnitOfWorkFactory<TUnitOfWork> unitOfWorkFactory,
            Func<TUnitOfWork, IRepository<TEntity, TPrimaryKey>> getRepositoryFunc,
            Func<IRepositoryQuery<TEntity>, IQueryable<TProjection>> projection,
            Action<TEntity> newEntityInitializer = null,
            Func<bool> canCreateNewEntity = null,
            bool ignoreSelectEntityMessage = false
            )
            : base(unitOfWorkFactory, getRepositoryFunc, projection, newEntityInitializer, canCreateNewEntity, ignoreSelectEntityMessage)
        {
            SelectedEntities = new ObservableCollection<TProjection>();
            SelectedEntities.CollectionChanged += SelectedEntities_CollectionChanged;
        }

        public Action<IEnumerable<TProjection>> OnSelectedEntitiesChangedCallBack;
        protected virtual void SelectedEntities_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {

        }

        #region Selected Entities
        /// <summary>
        /// The selected entities.
        /// Since CollectionViewModel is a POCO view model, this property will raise INotifyPropertyChanged.PropertyEvent when modified so it can be used as a binding source in views.
        /// </summary>
        ObservableCollection<TProjection> selectedentities { get; set; }
        public ObservableCollection<TProjection> SelectedEntities
        {
            get { return selectedentities; }
            set { selectedentities = value; }
        }

        IEnumerable<TPrimaryKey> selectedEntitiesKey;
        protected override void StoreSelectedEntitiesKey()
        {
            selectedEntitiesKey = base.RetrieveLocalProjectionsEntitiesKey(this.SelectedEntities.AsEnumerable());
        }

        protected override Action GetSelectedEntityCallback()
        {
            return () => RestoreSelectionEntitiesByKey();
        }

        protected override void RestoreSelectionEntitiesByKey()
        {
            if (selectedEntitiesKey.Count() == 0)
                return;

            SelectedEntities.Clear();
            foreach(TPrimaryKey entityKey in selectedEntitiesKey)
            {
                SelectedEntities.Add(base.FindLocalProjectionByKey(entityKey));
            }
        }
        #endregion

        #region ISupportFiltering
        public void CreateCustomFilter()
        {
            Messenger.Default.Send(new CreateCustomFilterMessage<TEntity>());
        }

        Expression<Func<TEntity, bool>> ISupportFiltering<TEntity>.FilterExpression
        {
            get { return FilterExpression; }
            set { FilterExpression = value; }
        }

        #endregion

        #region ISupportUndoRedo
        /// <summary>
        /// Manages all undo and redo operation
        /// </summary>
        EntitiesUndoRedoManager<TProjection> entitiesundoredomanager { get; set; }
        public EntitiesUndoRedoManager<TProjection> EntitiesUndoRedoManager
        {
            get
            {
                if (entitiesundoredomanager == null)
                    entitiesundoredomanager = new EntitiesUndoRedoManager<TProjection>(PropertyUndo, PropertyRedo);

                return entitiesundoredomanager;
            }
        }

        public Action<TProjection> PropertyRedoAddCallBack;
        /// <summary>
        /// Function to undo the entity changes
        /// Must be used in conjunction of EntitiesUndoManager
        /// </summary>
        /// <param name="entityProperty">Entity passed over from EntitiesUndoRedo</param>
        public virtual void PropertyUndo(UndoRedoEntityInfo<TProjection> entityProperty)
        {
            if (entityProperty.MessageType == EntityMessageType.Added)
                Delete(entityProperty.ChangedEntity);
            else
            {
                if (entityProperty.MessageType == EntityMessageType.Changed)
                    DataUtils.SetNestedValue(entityProperty.PropertyName, entityProperty.ChangedEntity, entityProperty.OldValue);

                Save(entityProperty.ChangedEntity);
            }
        }

        /// <summary>
        /// Function to redo the entity changes
        /// Must be used in conjunction of EntitiesUndoManager
        /// </summary>
        /// <param name="entityProperty">Entity passed over from EntitiesUndoRedo</param>
        public virtual void PropertyRedo(UndoRedoEntityInfo<TProjection> entityProperty)
        {
            if (entityProperty.MessageType == EntityMessageType.Deleted)
                Delete(entityProperty.ChangedEntity);
            else
            {
                if (entityProperty.MessageType == EntityMessageType.Changed)
                    DataUtils.SetNestedValue(entityProperty.PropertyName, entityProperty.ChangedEntity, entityProperty.NewValue);
                //if (entityProperty.MessageType == EntityMessageType.Added && PropertyRedoAddCallBack != null)
                //    PropertyRedoAddCallBack(entityProperty.ChangedEntity);

                Save(entityProperty.ChangedEntity);
            }
        }

        /// <summary>
        /// Specify whether any elements remains in the undo list
        /// Since CollectionViewModelBase is a POCO view model, an the instance of this class will also expose the CanUndoCommand property that can be used as a binding source in views.
        /// </summary>
        public bool CanUndo()
        {
            return EntitiesUndoRedoManager.CanUndo();
        }

        /// <summary>
        /// Specify whether any elements remains in the undo list
        /// Since CollectionViewModelBase is a POCO view model, an the instance of this class will also expose the CanRedoCommand property that can be used as a binding source in views.
        /// </summary>
        public bool CanRedo()
        {
            return EntitiesUndoRedoManager.CanRedo();
        }

        /// <summary>
        /// Undo last operation
        /// Since CollectionViewModelBase is a POCO view model, an the instance of this class will also expose the UndoCommand property that can be used as a binding source in views.
        /// </summary>
        public void Undo()
        {
            EntitiesUndoRedoManager.Undo();
        }

        /// <summary>
        /// Redo last operation
        /// Since CollectionViewModelBase is a POCO view model, an the instance of this class will also expose the RedoCommand property that can be used as a binding source in views.
        /// </summary>
        public void Redo()
        {
            EntitiesUndoRedoManager.Redo();
        } 
        #endregion

        #region Data Operations
        /// <summary>
        /// Determine whether other entities in the collection shares any common combination of unique constraints
        /// </summary>
        /// <param name="entity">The entity to be validated</param>
        /// <param name="errorMessage">Error message to notify the user of conflicting constraints</param>
        /// <returns>Returns true if no other entity contains similar constraint member values</returns>
        public bool IsValidEntity(TProjection entity, ref string errorMessage)
        {
            if (!isRequiredAttributesHasValue(entity, ref errorMessage))
                return false;

            return IsUniqueEntityConstraintValues(entity, ref errorMessage);
        }

        /// <summary>
        /// Determine whether other entities in the collection shares any common combination of unique constraints
        /// And since this is designed to be called from cell value changing the entity would not have been updated with the new value
        /// Hence fieldName and newValue is used for validation
        /// </summary>
        /// <param name="entity">The entity to be validated</param>
        /// <param name="fieldName">Fieldname of the current changing cell</param>
        /// <param name="newValue">New value of the current changing cell</param>
        /// <param name="errorMessage">Error message to notify the user of conflicting constraints</param>
        /// <returns>Returns true if no other entity contains similar constraint member values</returns>
        public bool IsValidEntityCellValue(TProjection entity, string fieldName, object newValue, ref string errorMessage)
        {
            return IsUniqueEntityConstraintValues(entity, ref errorMessage, new KeyValuePair<string,object>(fieldName, newValue));
        }

        /// <summary>
        /// Gets the concatenated string value for constraint field name for an entity
        /// </summary>
        /// <param name="entity">Entity to retrieve the field name</param>
        /// <param name="errorMessage">Error message to be populated with entity member constraint field names</param>
        /// <param name="keyValuePairNewFieldValue">In some instance the new value isn't yet updated on the entity, so this provides other ways pass in the new value</param>
        /// <returns>Concatenated constraint value string</returns>
        private bool IsUniqueEntityConstraintValues(TProjection entity, ref string errorMessage, KeyValuePair<string, object>? keyValuePairNewFieldValue = null)
        {
            string currentEntityConcatenatedConstraints = string.Empty;

            IEnumerable<string> constraintMemberPropertyStrings = DataUtils.GetConstraintPropertyStrings(typeof(TProjection));
            if (constraintMemberPropertyStrings == null)
                return true;
            else if (keyValuePairNewFieldValue != null && !constraintMemberPropertyStrings.Contains(((KeyValuePair<string, object>)keyValuePairNewFieldValue).Key))
                return true;

            foreach (string constraintMemberPropertyString in constraintMemberPropertyStrings)
            {
                object constraintMemberPropertyValue = null;
                if (keyValuePairNewFieldValue == null)
                    constraintMemberPropertyValue = DataUtils.GetNestedValue(constraintMemberPropertyString, entity);
                else
                {
                    KeyValuePair<string, object> keyValuePairForNewFieldValue = (KeyValuePair<string, object>)keyValuePairNewFieldValue;
                    if (constraintMemberPropertyString == keyValuePairForNewFieldValue.Key)
                        constraintMemberPropertyValue = keyValuePairForNewFieldValue.Value;
                }

                if (constraintMemberPropertyValue != null)
                {
                    string immediatePropertyString = constraintMemberPropertyString.Split('.').Last();
                    errorMessage += immediatePropertyString + " and ";
                    currentEntityConcatenatedConstraints += constraintMemberPropertyValue.ToString();
                }
            }

            return IsConstraintExistsInOtherEntities(entity, currentEntityConcatenatedConstraints, constraintMemberPropertyStrings, ref errorMessage);
        }


        /// <summary>
        /// Determine whether current entity constraint exists in other entity
        /// </summary>
        /// <param name="entity">The entity to be validated</param>
        /// <param name="entityConstraint">Constraint string of the current entity</param>
        /// <param name="constraintMemberPropertyInfos">Constraint property infos</param>
        /// <param name="constraintErrorMessage">Error message to notify the user of conflicting constraints</param>
        /// <returns>Returns true if no other entity contains similar constraint member values</returns>
        private bool IsConstraintExistsInOtherEntities(TProjection entity, string entityConstraint, IEnumerable<string> constraintMemberPropertyStrings, ref string constraintErrorMessage)
        {
            PropertyInfo keyPropertyInfo = DataUtils.GetKeyPropertyInfo(typeof(TProjection));
            object exclusionKeyValue = null;
            if (keyPropertyInfo != null)
                exclusionKeyValue = keyPropertyInfo.GetValue(entity);

            foreach (var otherEntity in Entities)
            {
                if (keyPropertyInfo != null)
                {
                    var otherKey = keyPropertyInfo.GetValue(otherEntity);
                    if (otherKey.Equals(exclusionKeyValue))
                        continue;
                }

                string otherEntityConcatenatedConstraints = string.Empty;
                foreach (var constraintMemberPropertyString in constraintMemberPropertyStrings)
                {
                    var constraintMemberPropertyValue = DataUtils.GetNestedValue(constraintMemberPropertyString, otherEntity);
                    if (constraintMemberPropertyValue != null)
                        otherEntityConcatenatedConstraints += constraintMemberPropertyValue.ToString();
                }

                if (otherEntityConcatenatedConstraints == entityConstraint)
                {
                    constraintErrorMessage = constraintErrorMessage.Substring(0, constraintErrorMessage.Length - 5);
                    constraintErrorMessage = constraintErrorMessage.Replace("GUID_", "");
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Check if entity have key value
        /// </summary>
        /// <param name="entity">The entity to be validated</param>
        /// <param name="errorMessage">Error mesasage formatted with key property info</param>
        /// <returns></returns>
        private bool isRequiredAttributesHasValue(TProjection entity, ref string errorMessage)
        {
            IEnumerable<string> requiredPropertyStrings = DataUtils.GetRequiredPropertyStrings(typeof(TProjection));
            string requiredPropertyNames = string.Empty;

            if (requiredPropertyStrings.Count() == 0)
                return true;
            else
            {
                foreach(string requiredPropertyString in requiredPropertyStrings)
                {
                    var requiredPropertyValue = DataUtils.GetNestedValue(requiredPropertyString, entity);
                    if(requiredPropertyValue == null)
                        requiredPropertyNames += requiredPropertyString.Split('.').Last() + ", ";
                }

                if (requiredPropertyNames != string.Empty)
                {
                    errorMessage = string.Format("{0} value missing", requiredPropertyNames.Substring(0, requiredPropertyNames.Length - 2));
                    return false;
                }
                else
                    return true;
            }
        }
        #endregion

        #region LookUpCollection
        readonly Dictionary<string, IDocumentContent> lookUpViewModels = new Dictionary<string, IDocumentContent>();

        protected IEntitiesViewModel<TLookUpEntity> GetLookUpEntitiesViewModel<TViewModel, TLookUpEntity, TLookUpEntityKey>(Expression<Func<TViewModel, IEntitiesViewModel<TLookUpEntity>>> propertyExpression, Func<TUnitOfWork, IRepository<TLookUpEntity, TLookUpEntityKey>> getRepositoryFunc, Func<IRepositoryQuery<TLookUpEntity>, IQueryable<TLookUpEntity>> projection = null) where TLookUpEntity : class
        {
            return GetLookUpProjectionsViewModel(propertyExpression, getRepositoryFunc, projection);
        }

        protected virtual IEntitiesViewModel<TLookUpProjection> GetLookUpProjectionsViewModel<TViewModel, TLookUpEntity, TLookUpProjection, TLookUpEntityKey>(Expression<Func<TViewModel, IEntitiesViewModel<TLookUpProjection>>> propertyExpression, Func<TUnitOfWork, IRepository<TLookUpEntity, TLookUpEntityKey>> getRepositoryFunc, Func<IRepositoryQuery<TLookUpEntity>, IQueryable<TLookUpProjection>> projection)
            where TLookUpEntity : class
            where TLookUpProjection : class
        {
            return GetEntitiesViewModelCore<IEntitiesViewModel<TLookUpProjection>, TLookUpProjection>(propertyExpression, () => LookUpEntitiesViewModel<TLookUpEntity, TLookUpProjection, TLookUpEntityKey, TUnitOfWork>.Create(this.unitOfWorkFactory, getRepositoryFunc, projection));
        }

        TViewModel GetEntitiesViewModelCore<TViewModel, TDetailEntity>(LambdaExpression propertyExpression, Func<TViewModel> createViewModelCallback)
            where TViewModel : IDocumentContent
            where TDetailEntity : class
        {
            IDocumentContent result = null;
            string propertyName = ExpressionHelper.GetPropertyName(propertyExpression);
            if (!lookUpViewModels.TryGetValue(propertyName, out result))
            {
                result = createViewModelCallback();
                lookUpViewModels[propertyName] = result;
            }
            return (TViewModel)result;
        }
        #endregion

        #region Views
        protected virtual void OnSelectedEntitiesChanged() { }

        public Action<TProjection, CellValueChangedEventArgs> ExistingProjectionEditCallBack;
        /// <summary>
        /// Remembers an entity property old value for undoing
        /// Since CollectionViewModelBase is a POCO view model, an the instance of this class will also expose the AddUndoCommand property that can be used as a binding source in views.
        /// </summary>
        public virtual void ExistingRowAddUndoAndSave(CellValueChangedEventArgs e)
        {
            if (e.RowHandle != GridControl.NewItemRowHandle)
            {
                TProjection projection = (TProjection)e.Row;

                EntitiesUndoRedoManager.PauseActionId();
                if (ExistingProjectionEditCallBack != null)
                    ExistingProjectionEditCallBack(projection, e);

                EntitiesUndoRedoManager.AddUndo(projection, e.Column.FieldName, e.OldValue, e.Value, EntityMessageType.Changed);
                EntitiesUndoRedoManager.UnpauseActionId();

                Save(projection);
            }
        }

        /// <summary>
        /// Remembers an entity property old value for undoing
        /// Since CollectionViewModelBase is a POCO view model, an the instance of this class will also expose the AddUndoCommand property that can be used as a binding source in views.
        /// </summary>
        public virtual void TreelistExistingRowAddUndoAndSave(TreeListCellValueChangedEventArgs e)
        {
            TProjection projection = (TProjection)e.Row;

            EntitiesUndoRedoManager.PauseActionId();
            EntitiesUndoRedoManager.AddUndo(projection, e.Column.FieldName, e.OldValue, e.Value, EntityMessageType.Changed);
            EntitiesUndoRedoManager.UnpauseActionId();

            Save(projection);
        }

        public Action<TProjection> NewProjectionInitializeCallBack;
        /// <summary>
        /// Remembers an entity added for undoing
        /// Since CollectionViewModelBase is a POCO view model, an the instance of this class will also expose the AddUndoCommand property that can be used as a binding source in views.
        /// </summary>
        public virtual void NewRowAddUndoAndSave(RowEventArgs e)
        {
            if (e.RowHandle == GridControl.NewItemRowHandle)
            {
                TProjection projection = (TProjection)e.Row;

                if (NewProjectionInitializeCallBack != null)
                    NewProjectionInitializeCallBack(projection);

                //TEntity entity = CreateNewEntity((TProjection)e.Row);
                //InitializeEntity(entity);

                EntitiesUndoRedoManager.AddUndo(projection, null, null, null, EntityMessageType.Added);
                Save(projection);
            }
        }

        protected override void OnBeforeEntitySaved(TEntity entity)
        {
            InitializeEntity(entity);
            base.OnBeforeEntitySaved(entity);
        }

        /// <summary>
        /// Validate any row within the binded datagrid
        /// Since CollectionViewModelBase is a POCO view model, an the instance of this class will also expose the ValidateRowCommand property that can be used as a binding source in views.
        /// </summary>
        /// <param name="e"></param>
        public virtual void ValidateCell(GridCellValidationEventArgs e)
        {
            string constraintName = string.Empty;
            if (!IsValidEntityCellValue((TProjection)e.Row, e.Column.FieldName, e.Value, ref constraintName))
            {
                e.IsValid = false;
                e.ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical;
                e.ErrorContent = constraintName + " is not unique";
            }
        }

        public virtual void ValidateRow(GridRowValidationEventArgs e)
        {
            string errorMessage = string.Empty;
            if (!IsValidEntity((TProjection)e.Row, ref errorMessage))
            {
                e.IsValid = false;
                e.ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical;
                e.ErrorContent = errorMessage ;
            }
        }
        #endregion

        #region Fill Down Convention
        public Func<IEnumerable<TProjection>, GridMenuInfo, bool> CanFillDownCallBack;
        public bool CanFillDown(object button)
        {
            var info = GridPopupMenuBase.GetGridMenuInfo((DependencyObject)button) as GridMenuInfo;
            return Entities != null && Entities.Count > 1 && !IsLoading && (info != null && info.Column != null && !info.Column.ReadOnly) && (CanFillDownCallBack == null || CanFillDownCallBack(SelectedEntities, info));
        }

        public Func<TProjection, string, object, bool> ValidateFillDownCallBack;
        public Action OnFillDownCompletedCallBack;
        /// <summary>
        /// Copy the selected column and row value of first selected entity to the others
        /// Since CollectionViewModelBase is a POCO view model, an the instance of this class will also expose the FillDownCommand property that can be used as a binding source in views.
        /// </summary>
        /// <param name="button">Must be assigned with a button from a GridControl's context menu</param>
        public void FillDown(object button)
        {
            var info = GridPopupMenuBase.GetGridMenuInfo((DependencyObject)button) as GridMenuInfo;

            var ValueToFillDown = DataUtils.GetNestedValue(info.Column.FieldName, SelectedEntities[0]);

            EntitiesUndoRedoManager.PauseActionId();
            foreach (TProjection SelectedEntity in SelectedEntities)
            {
                if (ValidateFillDownCallBack != null && !ValidateFillDownCallBack(SelectedEntity, info.Column.FieldName, ValueToFillDown))
                    continue;

                var OldValue = DataUtils.GetNestedValue(info.Column.FieldName, SelectedEntity);
                EntitiesUndoRedoManager.AddUndo(SelectedEntity, info.Column.FieldName, OldValue, ValueToFillDown, EntityMessageType.Changed);
                DataUtils.SetNestedValue(info.Column.FieldName, SelectedEntity, ValueToFillDown);
            }

            BulkSave(SelectedEntities);
            EntitiesUndoRedoManager.UnpauseActionId();

            if (OnFillDownCompletedCallBack != null)
                OnFillDownCompletedCallBack();
        }

        public bool AllowEdit = true;
        public override void Edit(TProjection projectionEntity)
        {
            if (AllowEdit)
                base.Edit(projectionEntity);
            //Do not allow edit in projection view
        }

        public override void Refresh()
        {
            base.Refresh();
            this.EntitiesUndoRedoManager.Clear();
        }

        public void RefreshWithoutClearingUndoManager()
        {
            base.Refresh();
        }

        public void Destroy()
        {
            base.OnDestroy();
        }

        #endregion

        #region Bulk Operation
        public Func<IEnumerable<TProjection>, bool> CanBulkDeleteCallBack;

        /// <summary>
        /// Determines whether an entities can be deleted
        /// Since CollectionViewModelBase is a POCO view model, this method will be used as a CanExecute callback for BulkDeleteCommand.
        /// </summary>
        /// <param name="projectionEntity">Entities to delete.</param>
        public virtual bool CanBulkDelete(IEnumerable<TProjection> entities)
        {
            return Entities != null && Entities.Count > 0 && !IsLoading && (CanBulkDeleteCallBack == null || CanBulkDeleteCallBack(entities));
        }

        /// <summary>
        /// Determines whether an entities can be saved
        /// Since CollectionViewModelBase is a POCO view model, this method will be used as a CanExecute callback for BulkSaveCommand.
        /// </summary>
        /// <param name="projectionEntity">Entities to save.</param>
        public virtual bool CanBulkSave(IEnumerable<TProjection> entities)
        {
            return Entities != null && Entities.Count > 0 && !IsLoading;
        }

        /// <summary>
        /// Deletes a collection of entities from the repository.
        /// Since CollectionViewModelBase is a POCO view model, an the instance of this class will also expose the DeleteCommand property that can be used as a binding source in views.
        /// </summary>
        /// <param name="projectionEntity">An entity to edit.</param>
        public virtual void BulkDelete(IEnumerable<TProjection> entities)
        {
            EntitiesUndoRedoManager.PauseActionId();
            BaseBulkDelete(entities);
            EntitiesUndoRedoManager.UnpauseActionId();
        }

        /// <summary>
        /// Deletes a collection of entities from the repository.
        /// Since CollectionViewModelBase is a POCO view model, an the instance of this class will also expose the DeleteCommand property that can be used as a binding source in views.
        /// </summary>
        /// <param name="projectionEntity">An entity to edit.</param>
        public virtual void TestDelete(object entities)
        {
            string s = entities.ToString();

        }

        /// <summary>
        /// Deletes a given entity from the repository and saves changes if confirmed by the user.
        /// Since CollectionViewModelBase is a POCO view model, an the instance of this class will also expose the DeleteCommand property that can be used as a binding source in views.
        /// </summary>
        /// <param name="projectionEntity">An entity to edit.</param>
        public virtual void BulkSave(IEnumerable<TProjection> entities)
        {
            EntitiesUndoRedoManager.PauseActionId();
            BaseBulkSave(entities);
            EntitiesUndoRedoManager.UnpauseActionId();
        }
        #endregion

        #region Data Operations
        /// <summary>
        /// Converts clipboard text into entity values and saves to database
        /// </summary>
        /// <param name="e"></param>
        public virtual void PastingFromClipboard(PastingFromClipboardEventArgs e)
        {
            if (CreateNewProjectionFromNewEntityCallBack == null)
                return;

            String PasteString = Clipboard.GetText();
            string[] RowData = PasteString.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            GridControl sourceGridControl = (GridControl)e.Source;
            PropertyInfo[] entityProperties = typeof(TEntity).GetProperties();
            DataViewBase gridView = sourceGridControl.View;

            if (gridView.GetType() == typeof(TableView))
            {
                TableView gridTableView = gridView as TableView;
                foreach (string Row in RowData)
                {
                    TEntity newEntity = CreateEntity();
                    TProjection projection = CreateNewProjectionFromNewEntity(newEntity);

                    string[] ColumnStrings = Row.Split('\t');
                    for (int i = 0; i < ColumnStrings.Count(); i++)
                    {
                        try
                        {
                            GridColumn copyColumn = gridTableView.VisibleColumns[i];

                            if (copyColumn.ReadOnly)
                                continue;

                            string columnName = copyColumn.FieldName;
                            PropertyInfo columnPropertyInfo = projection.GetType().GetProperty(columnName);
                            if (columnPropertyInfo != null)
                            {
                                if (columnPropertyInfo.PropertyType == typeof(Guid))
                                {
                                    ComboBoxEditSettings copyColumnEditSettings = copyColumn.ActualEditSettings as ComboBoxEditSettings;
                                    if (copyColumnEditSettings != null)
                                    {
                                        string copyColumnValueMember = copyColumnEditSettings.ValueMember;
                                        string copyColumnDisplayMember = copyColumnEditSettings.DisplayMember;
                                        var copyColumnItemsSource = copyColumnEditSettings.ItemsSource as IEnumerable<object>;
                                        Guid? itemValue = null;
                                        foreach (var copyColumnItem in copyColumnItemsSource)
                                        {
                                            PropertyInfo itemDisplayMemberPropertyInfo = copyColumnItem.GetType().GetProperty(copyColumnDisplayMember);
                                            PropertyInfo itemValueMemberPropertyInfo = copyColumnItem.GetType().GetProperty(copyColumnValueMember);
                                            if (itemDisplayMemberPropertyInfo.GetValue(copyColumnItem).ToString() == ColumnStrings[i])
                                            {
                                                itemValue = (Guid)itemValueMemberPropertyInfo.GetValue(copyColumnItem);
                                            }
                                        }

                                        if (itemValue != null)
                                            projection.GetType().GetProperty(columnName).SetValue(projection, itemValue);
                                        else
                                            break;
                                    }
                                }
                                else if (columnPropertyInfo.PropertyType == typeof(string))
                                    projection.GetType().GetProperty(columnName).SetValue(projection, ColumnStrings[i]);
                                else if (columnPropertyInfo.PropertyType.BaseType == typeof(Enum))
                                {
                                    var enumValues = Enum.GetValues(columnPropertyInfo.PropertyType);
                                    foreach (var enumValue in enumValues)
                                    {
                                        var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());
                                        if (fieldInfo == null)
                                            return;

                                        var descriptionAttributes = fieldInfo.GetCustomAttributes(typeof(DisplayAttribute), false) as DisplayAttribute[];
                                        if (descriptionAttributes == null || descriptionAttributes.Count() == 0)
                                            return;

                                        var descriptionAttribute = descriptionAttributes.First();
                                        if (ColumnStrings[i] == descriptionAttribute.Name)
                                        {
                                            projection.GetType().GetProperty(columnName).SetValue(projection, enumValue);
                                            break;
                                        }
                                    }
                                }
                                else if (columnPropertyInfo.PropertyType == typeof(decimal) || columnPropertyInfo.PropertyType == typeof(int) || columnPropertyInfo.PropertyType == typeof(double))
                                {
                                    Regex rgx = new Regex("[^0-9a-z\\.]");
                                    string cleanColumnString = rgx.Replace(ColumnStrings[i], "");

                                    if (columnPropertyInfo.PropertyType == typeof(decimal))
                                    {
                                        decimal getDecimal;
                                        if (decimal.TryParse(cleanColumnString, out getDecimal))
                                        {
                                            if (columnName.Contains('%') || columnName.ToUpper().Contains("PERCENT"))
                                                getDecimal /= 100;

                                            projection.GetType().GetProperty(columnName).SetValue(projection, getDecimal);
                                        }
                                        else
                                            return;
                                    }
                                    else if (columnPropertyInfo.PropertyType == typeof(int))
                                    {
                                        int getInt;
                                        if (int.TryParse(cleanColumnString, out getInt))
                                            projection.GetType().GetProperty(columnName).SetValue(projection, getInt);
                                        else
                                            return;
                                    }
                                    else if (columnPropertyInfo.PropertyType == typeof(double))
                                    {
                                        double getDouble;
                                        if (double.TryParse(cleanColumnString, out getDouble))
                                        {
                                            if (columnName.Contains('%') || columnName.ToUpper().Contains("PERCENT"))
                                                getDouble /= 100;

                                            projection.GetType().GetProperty(columnName).SetValue(projection, getDouble);
                                        }
                                        else
                                            return;
                                    }
                                    else
                                        return;
                                }
                                else
                                    return;
                            }
                            else
                                return;
                        }
                        catch
                        {
                            return;
                        }
                    }

                    string errorMessage = "Duplicate exists on constraint field named: ";
                    if (IsValidEntity(projection, ref errorMessage))
                        Save(projection);
                    else
                    {
                        errorMessage += " , paste operation will be terminated";
                        MessageBoxService.ShowMessage(errorMessage, CommonResources.Exception_UpdateErrorCaption, MessageButton.OK);
                        break;
                    }
                }
            }

            e.Handled = true;
        }
        #endregion

        #region Interceptors
        protected override void AddUndoBeforeEntityDeleted(TProjection projection)
        {
            if(!EntitiesUndoRedoManager.IsInUndoRedoOperation())
                EntitiesUndoRedoManager.AddUndo(projection, null, null, null, EntityMessageType.Deleted);
            base.AddUndoBeforeEntityDeleted(projection);
        }
        #endregion
    }


    /// <summary>
    /// The base class for POCO view models exposing a collection of entities of a given type and CRUD operations against these entities.
    /// It is not recommended to inherit directly from this class. Use the CollectionViewModel class instead.
    /// </summary>
    /// <typeparam name="TEntity">A repository entity type.</typeparam>
    /// <typeparam name="TProjection">A projection entity type.</typeparam>
    /// <typeparam name="TPrimaryKey">A primary key value type.</typeparam>
    /// <typeparam name="TUnitOfWork">A unit of work type.</typeparam>
    public abstract class CollectionViewModelBase<TEntity, TProjection, TPrimaryKey, TUnitOfWork> : ReadOnlyCollectionViewModel<TEntity, TProjection, TUnitOfWork>, ISupportLogicalLayout
        where TEntity : class
        where TProjection : class
        where TUnitOfWork : IUnitOfWork
    {

        EntitiesChangeTracker<TPrimaryKey> ChangeTrackerWithKey { get { return (EntitiesChangeTracker<TPrimaryKey>)ChangeTracker; } }
        readonly Action<TEntity> newEntityInitializer;
        readonly Func<bool> canCreateNewEntity;
        IRepository<TEntity, TPrimaryKey> Repository { get { return (IRepository<TEntity, TPrimaryKey>)ReadOnlyRepository; } }

        /// <summary>
        /// Initializes a new instance of the CollectionViewModelBase class.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        /// <param name="getRepositoryFunc">A function that returns a repository representing entities of the given type.</param>
        /// <param name="projection">A LINQ function used to customize a query for entities. The parameter, for example, can be used for sorting data and/or for projecting data to a custom type that does not match the repository entity type.</param>
        /// <param name="newEntityInitializer">A function to initialize a new entity. This parameter is used in the detail collection view models when creating a single object view model for a new entity.</param>
        /// <param name="canCreateNewEntity">A function that is called before an attempt to create a new entity is made. This parameter is used together with the newEntityInitializer parameter.</param>
        /// <param name="ignoreSelectEntityMessage">A parameter used to specify whether the selected entity should be managed by PeekCollectionViewModel.</param>
        protected CollectionViewModelBase(
            IUnitOfWorkFactory<TUnitOfWork> unitOfWorkFactory,
            Func<TUnitOfWork, IRepository<TEntity, TPrimaryKey>> getRepositoryFunc,
            Func<IRepositoryQuery<TEntity>, IQueryable<TProjection>> projection,
            Action<TEntity> newEntityInitializer,
            Func<bool> canCreateNewEntity,
            bool ignoreSelectEntityMessage
            )
            : base(unitOfWorkFactory, getRepositoryFunc, projection)
        {
            RepositoryExtensions.VerifyProjection(CreateRepository(), projection);
            this.newEntityInitializer = newEntityInitializer;
            this.canCreateNewEntity = canCreateNewEntity;
            this.ignoreSelectEntityMessage = ignoreSelectEntityMessage;
            if (!this.IsInDesignMode())
                RegisterSelectEntityMessage();
        }

        /// <summary>
        /// Creates and shows a document that contains a single object view model for new entity.
        /// Since CollectionViewModelBase is a POCO view model, an the instance of this class will also expose the NewCommand property that can be used as a binding source in views.
        /// </summary>
        public virtual void New()
        {
            if (canCreateNewEntity != null && !canCreateNewEntity())
                return;
            DocumentManagerService.ShowNewEntityDocument(this, newEntityInitializer);
        }

        public virtual TEntity InitializeEntity(TEntity entity)
        {
            if (this.newEntityInitializer != null)
                this.newEntityInitializer(entity);

            return entity;
        }

        /// <summary>
        /// Creates and shows a document that contains a single object view model for the existing entity.
        /// Since CollectionViewModelBase is a POCO view model, an the instance of this class will also expose the EditCommand property that can be used as a binding source in views.
        /// </summary>
        /// <param name="projectionEntity">Entity to edit.</param>
        public virtual void Edit(TProjection projectionEntity)
        {
            if (Repository.IsDetached(projectionEntity))
                return;
            TPrimaryKey primaryKey = Repository.GetProjectionPrimaryKey(projectionEntity);
            int index = Entities.IndexOf(projectionEntity);
            projectionEntity = ChangeTrackerWithKey.FindActualProjectionByKey(primaryKey);
            if (index >= 0)
            {
                if (projectionEntity == null)
                    Entities.RemoveAt(index);
                else
                    Entities[index] = projectionEntity;
            }

            //BluePrints Customization Start
            string customTitle = string.Empty;
            var viewModel = this as ISupportCustomDocumentTypeNameAndParameter;
            if (viewModel != null && viewModel.IsCustomModeEnabled())
                customTitle = viewModel.GetCustomDocumentTitle();
            //BluePrints Customization End

            if (projectionEntity == null)
            {
                //BluePrints Customization Start
                //DestroyDocument(DocumentManagerService.FindEntityDocument<TEntity, TPrimaryKey>(primaryKey));
                DestroyDocument(DocumentManagerService.FindEntityDocument<TProjection, TPrimaryKey>(primaryKey, customTitle));
                //BluePrints Customization End
                return;
            }

            DocumentManagerService.ShowExistingEntityDocument<TProjection, TPrimaryKey>(this, primaryKey, customTitle);
            //BluePrints Customization End
        }

        /// <summary>
        /// Determines whether an entity can be edited.
        /// Since CollectionViewModelBase is a POCO view model, this method will be used as a CanExecute callback for EditCommand.
        /// </summary>
        /// <param name="projectionEntity">An entity to edit.</param>
        public virtual bool CanEdit(TProjection projectionEntity)
        {
            return projectionEntity != null && !IsLoading;
        }

        public Action<TProjection> EntityBeforeDeletionCallBack;
        public Action<IEnumerable<TProjection>> EntitiesBeforeDeletionCallBack;
        public Action<IEnumerable<TEntity>> EntitiesAfterDeletionCallBack;
        /// <summary>
        /// Deletes a given entity from the repository and saves changes if confirmed by the user.
        /// Since CollectionViewModelBase is a POCO view model, an the instance of this class will also expose the DeleteCommand property that can be used as a binding source in views.
        /// </summary>
        /// <param name="projectionEntity">An entity to edit.</param>
        public virtual void Delete(TProjection projectionEntity)
        {
            //BluePrints Customization Start
            //if (MessageBoxService.ShowMessage(string.Format(CommonResources.Confirmation_Delete, EntityDisplayName), CommonResources.Confirmation_Caption, MessageButton.YesNo) != MessageResult.Yes)
            //    return;
            //BluePrints Customization End
            try
            {
                //BluePrints Customization Start
                AddUndoBeforeEntityDeleted(projectionEntity);

                if (EntityBeforeDeletionCallBack != null)
                    EntityBeforeDeletionCallBack(projectionEntity);
                //BluePrints Customization End
                Entities.Remove(projectionEntity);

                TPrimaryKey primaryKey = Repository.GetProjectionPrimaryKey(projectionEntity);
                TEntity entity = Repository.Find(primaryKey);
                if (entity != null)
                {
                    OnBeforeEntityDeleted(primaryKey, entity);
                    Repository.Remove(entity);
                    Repository.UnitOfWork.SaveChanges();
                    OnEntityDeleted(primaryKey, entity);
                }
            }
            catch (DbException e)
            {
                Refresh();
                MessageBoxService.ShowMessage(e.ErrorMessage, e.ErrorCaption, MessageButton.OK, MessageIcon.Error);
            }
        }

        public virtual void BaseBulkDelete(IEnumerable<TProjection> projectionEntities)
        {
            List<KeyValuePair<int, TProjection>> projectionEntitiesWithTag = new List<KeyValuePair<int, TProjection>>();
            List<KeyValuePair<int, TEntity>> entitiesWithTag = new List<KeyValuePair<int, TEntity>>();
            List<KeyValuePair<int, TPrimaryKey>> primaryKeysWithTag = new List<KeyValuePair<int, TPrimaryKey>>();

            List<TEntity> findOrAddNewEntities = new List<TEntity>();
            List<TProjection> projectionEntitiesList = projectionEntities.ToList();
            if (EntitiesBeforeDeletionCallBack != null)
                EntitiesBeforeDeletionCallBack(projectionEntities);

            for (int i = 0; i < projectionEntitiesList.Count; i++)
            {
                AddUndoBeforeEntityDeleted(projectionEntitiesList[i]);

                if (EntityBeforeDeletionCallBack != null)
                    EntityBeforeDeletionCallBack(projectionEntitiesList[i]);

                Entities.Remove(projectionEntitiesList[i]);
                projectionEntitiesWithTag.Add(new KeyValuePair<int, TProjection>(i, projectionEntitiesList[i]));
            }

            try
            {
                foreach (var projectionEntityWithTag in projectionEntitiesWithTag)
                {
                    TPrimaryKey primaryKey = Repository.GetProjectionPrimaryKey(projectionEntityWithTag.Value);
                    TEntity entity = Repository.Find(primaryKey);
                    if (entity != null)
                    {
                        entitiesWithTag.Add(new KeyValuePair<int, TEntity>(projectionEntityWithTag.Key, entity));
                        primaryKeysWithTag.Add(new KeyValuePair<int, TPrimaryKey>(projectionEntityWithTag.Key, primaryKey));
                        OnBeforeEntityDeleted(primaryKey, entity);
                        Repository.Remove(entity);
                    }
                }

                Repository.UnitOfWork.SaveChanges();

                foreach (var entityWithTag in entitiesWithTag)
                {
                    TPrimaryKey findPrimaryKey = primaryKeysWithTag.First(x => x.Key == entityWithTag.Key).Value;
                    OnEntityDeleted(findPrimaryKey, entityWithTag.Value);
                }

                List<TEntity> entitiesDeleted = entitiesWithTag.Select(x => x.Value).ToList();
                if (EntitiesAfterDeletionCallBack != null)
                    EntitiesAfterDeletionCallBack(entitiesDeleted);
            }
            catch (DbException e)
            {
                MessageBoxService.ShowMessage(e.ErrorMessage, e.ErrorCaption, MessageButton.OK, MessageIcon.Error);
            }
        }

        /// <summary>
        /// Determines whether an entity can be deleted.
        /// Since CollectionViewModelBase is a POCO view model, this method will be used as a CanExecute callback for DeleteCommand.
        /// </summary>
        /// <param name="projectionEntity">An entity to edit.</param>
        public virtual bool CanDelete(TProjection projectionEntity)
        {
            return projectionEntity != null && !IsLoading;
        }

        /// <summary>
        /// The display name of TEntity to be used when presenting messages to the user.
        /// </summary>
        public virtual string EntityDisplayName { get { return typeof(TEntity).Name; } }

        public Action<TProjection> HijackSaveOperation;
        /// <summary>
        /// Saves the given entity.
        /// Since CollectionViewModelBase is a POCO view model, the instance of this class will also expose the SaveCommand property that can be used as a binding source in views.
        /// </summary>
        /// <param name="projectionEntity">An entity to save.</param>
        [Display(AutoGenerateField = false)]
        public virtual void Save(TProjection projectionEntity)
        {
            if(HijackSaveOperation != null)
            {
                HijackSaveOperation(projectionEntity);
                return;
            }

            bool isNewEntity;
            var entity = Repository.FindExistingOrAddNewEntity(projectionEntity, (p, e) => { ApplyProjectionPropertiesToEntity(p, e); }, out isNewEntity);
            try
            {
                OnBeforeEntitySaved(entity);
                Repository.UnitOfWork.SaveChanges();
                var primaryKey = Repository.GetPrimaryKey(entity);
                Repository.SetProjectionPrimaryKey(projectionEntity, primaryKey);
                OnEntitySaved(primaryKey, projectionEntity, entity, isNewEntity);
            }
            catch (DbException e)
            {
                MessageBoxService.ShowMessage(e.ErrorMessage, e.ErrorCaption, MessageButton.OK, MessageIcon.Error);
            }
        }

        public Action<IEnumerable<TProjection>> HijackBulkSaveOperation;
        public virtual void BaseBulkSave(IEnumerable<TProjection> projectionEntities)
        {
            if(HijackBulkSaveOperation != null)
            {
                HijackBulkSaveOperation(projectionEntities);
                return;
            }

            List<KeyValuePair<int, TProjection>> projectionEntitiesWithTag = new List<KeyValuePair<int, TProjection>>();
            List<KeyValuePair<int, TEntity>> entitiesWithTag = new List<KeyValuePair<int, TEntity>>();
            List<KeyValuePair<int, bool>> isNewEntityWithTag = new List<KeyValuePair<int, bool>>();

            List<TEntity> findOrAddNewEntities = new List<TEntity>();
            List<TProjection> projectionEntitiesList = projectionEntities.ToList();
            for (int i = 0; i < projectionEntitiesList.Count; i++)
            {
                projectionEntitiesWithTag.Add(new KeyValuePair<int, TProjection>(i, projectionEntitiesList[i]));
            }

            foreach (var projectionEntityWithTag in projectionEntitiesWithTag)
            {
                bool isNewEntity;
                var findOrAddNewEntity = Repository.FindExistingOrAddNewEntity(projectionEntityWithTag.Value, (p, e) => { ApplyProjectionPropertiesToEntity(p, e); }, out isNewEntity);
                entitiesWithTag.Add(new KeyValuePair<int, TEntity>(projectionEntityWithTag.Key, findOrAddNewEntity));
                isNewEntityWithTag.Add(new KeyValuePair<int, bool>(projectionEntityWithTag.Key, isNewEntity));
                OnBeforeEntitySaved(findOrAddNewEntity);
            }

            try
            {
                Repository.UnitOfWork.SaveChanges();

                foreach (var entityWithTag in entitiesWithTag)
                {
                    var primaryKey = Repository.GetPrimaryKey(entityWithTag.Value);
                    var projectionEntity = projectionEntitiesWithTag.First(x => x.Key == entityWithTag.Key).Value;
                    bool isNewEntity = isNewEntityWithTag.First(x => x.Key == entityWithTag.Key).Value;
                    Repository.SetProjectionPrimaryKey(projectionEntity, primaryKey);
                    OnEntitySaved(primaryKey, projectionEntity, entityWithTag.Value, isNewEntity);
                }
            }
            catch (DbException e)
            {
                MessageBoxService.ShowMessage(e.ErrorMessage, e.ErrorCaption, MessageButton.OK, MessageIcon.Error);
            }
        }


        /// <summary>
        /// Determines whether entity local changes can be saved.
        /// Since CollectionViewModelBase is a POCO view model, this method will be used as a CanExecute callback for SaveCommand.
        /// </summary>
        /// <param name="projectionEntity">An entity to save.</param>
        public virtual bool CanSave(TProjection projectionEntity)
        {
            return projectionEntity != null && !IsLoading;
        }

        /// <summary>
        /// Notifies that SelectedEntity has been changed by raising the PropertyChanged event.
        /// Since CollectionViewModelBase is a POCO view model, an the instance of this class will also expose the UpdateSelectedEntityCommand property that can be used as a binding source in views.
        /// </summary>
        [Display(AutoGenerateField = false)]
        public virtual void UpdateSelectedEntity()
        {
            this.RaisePropertyChanged(x => x.SelectedEntity);
        }

        /// <summary>
        /// Closes the corresponding view.
        /// Since CollectionViewModelBase is a POCO view model, an the instance of this class will also expose the CloseCommand property that can be used as a binding source in views.
        /// </summary>
        [Display(AutoGenerateField = false)]
        public void Close()
        {
            if (DocumentOwner != null)
                DocumentOwner.Close(this);
        }

        protected override string ViewName { get { return typeof(TEntity).Name + "CollectionView"; } }

        protected IMessageBoxService MessageBoxService { get { return this.GetRequiredService<IMessageBoxService>(); } }

        public Func<IDocumentManagerService> OverrideGetDocumentManagerService { get; set; }
        protected IDocumentManagerService DocumentManagerService { get { return this.GetService<IDocumentManagerService>(); } }

        protected virtual void OnBeforeEntityDeleted(TPrimaryKey primaryKey, TEntity entity) { }

        protected virtual void OnEntityDeleted(TPrimaryKey primaryKey, TEntity entity)
        {
            Messenger.Default.Send(new EntityMessage<TEntity, TPrimaryKey>(primaryKey, EntityMessageType.Deleted, this));
        }

        protected IEnumerable<TPrimaryKey> RetrieveLocalProjectionsEntitiesKey(IEnumerable<TProjection> projectionEntities)
        {
            List<TPrimaryKey> returningEntitiesKey = new List<TPrimaryKey>();
            foreach (TProjection projectionEntity in projectionEntities)
            {
                bool primaryKeyAvailable = projectionEntity != null && Repository.ProjectionHasPrimaryKey(projectionEntity);
                if (primaryKeyAvailable)
                    returningEntitiesKey.Add(Repository.GetProjectionPrimaryKey(projectionEntity));
            }
            return returningEntitiesKey.AsEnumerable();
        }

        protected TProjection FindLocalProjectionByKey(TPrimaryKey projectionKey)
        {
            return ChangeTrackerWithKey.FindLocalProjectionByKey(projectionKey);
        }

        protected virtual void OnBeforeEntitySaved(TEntity entity)
        {
            if (OnBeforeEntitySavedCallBack != null)
                OnBeforeEntitySavedCallBack(entity);
        }

        public Action<TPrimaryKey, TProjection, TEntity, bool> OnEntitySavedCallBack;
        protected virtual void OnEntitySaved(TPrimaryKey primaryKey, TProjection projectionEntity, TEntity entity, bool isNewEntity)
        {
            if (OnEntitySavedCallBack != null)
                OnEntitySavedCallBack(primaryKey, projectionEntity, entity, isNewEntity);

            Messenger.Default.Send(new EntityMessage<TEntity, TPrimaryKey>(primaryKey, isNewEntity ? EntityMessageType.Added : EntityMessageType.Changed, this));
        }

        protected virtual void ApplyProjectionPropertiesToEntity(TProjection projectionEntity, TEntity entity)
        {
            if (ApplyProjectionPropertiesToEntityCallBack != null)
                ApplyProjectionPropertiesToEntityCallBack(projectionEntity, entity);
            else
                throw new NotImplementedException("Override this method in the collection view model class and apply projection properties to the entity so that it can be correctly saved by unit of work.");
        }

        protected override void OnSelectedEntityChanged()
        {
            base.OnSelectedEntityChanged();
            UpdateCommands();
        }

        protected override void RestoreSelectedEntity(TProjection existingProjectionEntity, TProjection newProjectionEntity)
        {
            base.RestoreSelectedEntity(existingProjectionEntity, newProjectionEntity);
            if (ReferenceEquals(SelectedEntity, existingProjectionEntity))
                SelectedEntity = newProjectionEntity;
        }

        protected override void OnIsLoadingChanged()
        {
            base.OnIsLoadingChanged();
            UpdateCommands();
            if (!IsLoading)
                RequestSelectedEntity();
        }

        void UpdateCommands()
        {
            TProjection projectionEntity = null;
            this.RaiseCanExecuteChanged(x => x.Edit(projectionEntity));
            this.RaiseCanExecuteChanged(x => x.Delete(projectionEntity));
            this.RaiseCanExecuteChanged(x => x.Save(projectionEntity));
        }

        protected void DestroyDocument(IDocument document)
        {
            if (document != null)
                document.Close();
        }

        protected IRepository<TEntity, TPrimaryKey> CreateRepository()
        {
            return (IRepository<TEntity, TPrimaryKey>)CreateReadOnlyRepository();
        }

        protected override IEntitiesChangeTracker CreateEntitiesChangeTracker()
        {
            return new EntitiesChangeTracker<TPrimaryKey>(this);
        }

        #region SelectEntityMessage
        protected class SelectEntityMessage
        {
            public SelectEntityMessage(TPrimaryKey primaryKey)
            {
                PrimaryKey = primaryKey;
            }
            public TPrimaryKey PrimaryKey { get; private set; }
        }

        protected class SelectedEntityRequest { }

        readonly bool ignoreSelectEntityMessage;

        void RegisterSelectEntityMessage()
        {
            if (!ignoreSelectEntityMessage)
                Messenger.Default.Register<SelectEntityMessage>(this, x => OnSelectEntityMessage(x));
        }

        void RequestSelectedEntity()
        {
            if (!ignoreSelectEntityMessage)
                Messenger.Default.Send(new SelectedEntityRequest());
        }

        void OnSelectEntityMessage(SelectEntityMessage message)
        {
            if (!IsLoaded)
                return;
            var projectionEntity = ChangeTrackerWithKey.FindActualProjectionByKey(message.PrimaryKey);
            if (projectionEntity == null)
            {
                FilterExpression = null;
                projectionEntity = ChangeTrackerWithKey.FindActualProjectionByKey(message.PrimaryKey);
            }
            SelectedEntity = projectionEntity;
        }
        #endregion

        #region ISupportLogicalLayout
        bool ISupportLogicalLayout.CanSerialize
        {
            get { return true; }
        }

        IDocumentManagerService ISupportLogicalLayout.DocumentManagerService
        {
            get { return DocumentManagerService; }
        }

        IEnumerable<object> ISupportLogicalLayout.LookupViewModels
        {
            get { return null; }
        }
        #endregion

        #region BluePrints Customization
        public Action<TProjection, TEntity> ApplyProjectionPropertiesToEntityCallBack;
        public Func<TEntity, TProjection> CreateNewProjectionFromNewEntityCallBack;
        protected virtual void AddUndoBeforeEntityDeleted(TProjection projectionEntity) { }
        public Action<TEntity> OnBeforeEntitySavedCallBack;
        /// <summary>
        /// Custom method deviating from devexpress scaffolding to expose repository create function.
        /// </summary>
        /// <returns>A new entity</returns>
        protected virtual TEntity CreateEntity()
        {
            var entity = Repository.Create();
            entity = InitializeEntity(entity);

            return entity;
        }

        protected virtual TProjection CreateNewProjectionFromNewEntity(TEntity entity)
        {
            if (CreateNewProjectionFromNewEntityCallBack != null)
                return CreateNewProjectionFromNewEntityCallBack(entity);
            else
                throw new NotImplementedException("Override this method in the collection view model class and apply projection properties to the entity so that it can be correctly saved by unit of work.");
        }

        protected virtual TEntity CreateNewEntity(TProjection projectionEntity)
        {
            var entity = Repository.Create();
            //ApplyProjectionPropertiesToEntity(projectionEntity, entity);
            return entity;
        }
        #endregion
    }
}
