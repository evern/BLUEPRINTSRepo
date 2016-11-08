﻿using BluePrints.BluePrintsEntitiesDataModel;
using BluePrints.Common;
using BluePrints.Common.DataModel;
using BluePrints.Common.Projections;
using BluePrints.Common.ViewModel;
using BluePrints.Common.ViewModel.Utils;
using BluePrints.Data;
using BluePrints.Data.Helpers;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Grid;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluePrints.ViewModels
{
    public class PROJECTVARIATIONDetailsCollectionViewModel : DetailsFilterableSingleObjectViewModel<PROJECT, VARIATION, Guid, IBluePrintsEntitiesUnitOfWork>
    {
        /// <summary>
        /// Initializes a new instance of the PROJECTViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the PROJECTViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        protected PROJECTVARIATIONDetailsCollectionViewModel(IUnitOfWorkFactory<IBluePrintsEntitiesUnitOfWork> unitOfWorkFactory = null)
            : base(unitOfWorkFactory ?? BluePrintsEntitiesUnitOfWorkSource.GetUnitOfWorkFactory(), x => x.PROJECTS, x => x.NUMBER)
        {
        }

        public ISupportCustomDocumentVARIATIONCollectionViewModel collectionViewModel { get; set; }
        /// <summary>
        /// The view model for the PROJECTVARIATIONES detail collection.
        /// </summary>
        public ISupportCustomDocumentVARIATIONCollectionViewModel PROJECTVARIATIONSDetails
        {
            get
            {
                if (collectionViewModel == null && this.PrimaryKey != Guid.Empty)
                {
                    collectionViewModel = ISupportCustomDocumentVARIATIONCollectionViewModel.Create(this.Entity, query => query.Where(x => x.GUID_PROJECT == this.Entity.GUID));
                    collectionViewModel.OnBeforeEntitySavedCallBack = this.OnBeforeEntitySaved;
                    collectionViewModel.SetParentViewModel(this);
                }

                return collectionViewModel;
            }
        }

        BASELINECollectionViewModel LiveBASELINECollection { get; set; }
        PROGRESSCollectionViewModel LivePROGRESSCollection { get; set; }
        CollectionViewModel<BASELINE_ITEM, Guid, IBluePrintsEntitiesUnitOfWork> VariationAdditionBASELINEITEMSCollection { get; set; }


        public IEnumerable<BASELINE> BASELINECollection
        {
            get { return LiveBASELINECollection.Entities; }
        }

        protected override void OnParameterChanged(object parameter)
        {
            base.OnParameterChanged(parameter);
            LiveBASELINECollection = BASELINECollectionViewModel.Create(this.UnitOfWorkFactory, query => query.Where(x => x.GUID_PROJECT == this.PrimaryKey && x.STATUS == BaselineStatus.Live));
            LiveBASELINECollection.Entities.ToList();
            LivePROGRESSCollection = PROGRESSCollectionViewModel.Create(this.UnitOfWorkFactory, query => query.Where(x => x.GUID_PROJECT == this.PrimaryKey && x.STATUS == ProgressStatus.Live));
            LivePROGRESSCollection.Entities.ToList();
            this.RaisePropertyChanged(x => x.PROJECTVARIATIONSDetails);
        }

        public void OnBeforeEntitySaved(VARIATION entity)
        {
            entity.GUID_PROJECT = this.Entity.GUID;
            if (entity.APPROVED != null)
                entity.GUID_ORIBASELINE = entity.GUID_ORIBASELINE ?? LiveBASELINECollection.Entities.First().GUID;
            else
                entity.GUID_ORIBASELINE = null;
        }

        protected override void OnDestroy()
        {
            LiveBASELINECollection.OnDestroy();
            LivePROGRESSCollection.OnDestroy();
            VariationAdditionBASELINEITEMSCollection.OnDestroy();
            base.OnDestroy();
        }
        #region Commands
        /// <summary>
        /// Determines whether an entities can be approved
        /// Since CollectionViewModelBase is a POCO view model, this method will be used as a CanExecute callback for ApproveCommand.
        /// </summary>
        /// <param name="projectionEntity">Entities to approve.</param>
        public bool CanApprove(VARIATION entity)
        {
            if (LiveBASELINECollection.Entities == null || LiveBASELINECollection.Entities.Count == 0 || LivePROGRESSCollection == null)
                return false;
            else if (entity != null && entity.APPROVED != null)
                return false;

            return true;
        }

        /// <summary>
        /// Approves an entity.
        /// Since CollectionViewModelBase is a POCO view model, an the instance of this class will also expose the ApproveCommand property that can be used as a binding source in views.
        /// </summary>
        /// <param name="projectionEntity">An entity to approve.</param>
        public void Approve(VARIATION entity)
        {
            string errorMessage = string.Empty;
            if (entity == null)
                errorMessage = "Nothing within variation to approve";
            else if (LiveBASELINECollection == null || LiveBASELINECollection.Entities.Count() == 0)
                errorMessage = "Live baseline doesn't exists";
            else if (LivePROGRESSCollection == null || LivePROGRESSCollection.Entities.Count() == 0)
                errorMessage = "Live progress doesn't exists";

            if (errorMessage != string.Empty)
            {
                MessageBoxService.ShowMessage(errorMessage);
                return;
            }

            BASELINE LiveBASELINE = LiveBASELINECollection.Entities.First();
            IEnumerable<VARIATION_ITEM> editVARIATION_ITEMS = BluePrintsEntitiesUnitOfWorkSource.GetUnitOfWorkFactory().CreateUnitOfWork().VARIATION_ITEMS.Where(x => x.GUID_VARIATION == entity.GUID).ToArray().AsEnumerable();
            IEnumerable<BASELINE_ITEM> addBASELINE_ITEMS = BluePrintsEntitiesUnitOfWorkSource.GetUnitOfWorkFactory().CreateUnitOfWork().BASELINE_ITEMS.Where(x => x.GUID_VARIATION == entity.GUID && x.GUID_BASELINE == null).ToArray().AsEnumerable();
            IQueryable<BASELINE_ITEM> editBASELINE_ITEMS = LiveBASELINE.BASELINE_ITEMS.AsQueryable();
            IQueryable<PROGRESS_ITEM> livePROGRESS_ITEMS = LivePROGRESSCollection.Entities.First().PROGRESS_ITEMS.AsQueryable();

            BASELINE newBASELINE = new BASELINE();
            DataUtils.ShallowCopy(newBASELINE, LiveBASELINE);
            newBASELINE.GUID = Guid.Empty;
            newBASELINE.REVISION = ((char)(LiveBASELINE.REVISION.Last() + 1)).ToString();
            //not saving new baseline as live yet because editBASELINE_ITEMS still depends on the current live baseline for copying BASELINE_ITEMS
            newBASELINE.STATUS = BaselineStatus.Superseded;
            LiveBASELINECollection.Save(newBASELINE);

            entity.APPROVED = DateTime.Now;
            entity.GUID_ORIBASELINE = LiveBASELINE.GUID;
            entity.GUID_BASELINE = newBASELINE.GUID;
            PROJECTVARIATIONSDetails.Save(entity);

            ObservableCollection<BASELINE_ITEM> newBASELINE_ITEMS = new ObservableCollection<BASELINE_ITEM>();
            foreach (BASELINE_ITEM editBASELINE_ITEM in editBASELINE_ITEMS)
            {
                BASELINE_ITEM copyBASELINE_ITEM = new BASELINE_ITEM();
                DataUtils.ShallowCopy(copyBASELINE_ITEM, editBASELINE_ITEM);

                VARIATION_ITEM editVARIATION_ITEM = editVARIATION_ITEMS.FirstOrDefault(x => x.GUID_ORIBASEITEM == editBASELINE_ITEM.GUID_ORIGINAL);
                if (editVARIATION_ITEM != null)
                {
                    if (editVARIATION_ITEM.ACTION == VariationAction.Cancel)
                    {
                        decimal progressItemEARNED_UNITS = livePROGRESS_ITEMS.Where(x => x.GUID_ORIBASEITEM == editBASELINE_ITEM.GUID_ORIGINAL).Sum(y => y.EARNED_UNITS);
                        if (progressItemEARNED_UNITS == 0)
                            copyBASELINE_ITEM.DC_HOURS = -1 * copyBASELINE_ITEM.ESTIMATED_HOURS;
                        else
                            copyBASELINE_ITEM.DC_HOURS = -1 * (copyBASELINE_ITEM.TOTAL_HOURS - progressItemEARNED_UNITS);
                    }
                    else if(editVARIATION_ITEM.ACTION == VariationAction.Append)
                        copyBASELINE_ITEM.DC_HOURS += editVARIATION_ITEM.VARIATION_UNITS;

                    if (editVARIATION_ITEM.ACTION != VariationAction.NoAction)
                        copyBASELINE_ITEM.GUID_VARIATION = entity.GUID;
                }

                copyBASELINE_ITEM.GUID = Guid.Empty;
                copyBASELINE_ITEM.GUID_BASELINE = newBASELINE.GUID;
                newBASELINE_ITEMS.Add(copyBASELINE_ITEM);
            }

            foreach (BASELINE_ITEM addBASELINE_ITEM in addBASELINE_ITEMS)
            {
                BASELINE_ITEM newBASELINE_ITEM = new BASELINE_ITEM();
                DataUtils.ShallowCopy(newBASELINE_ITEM, addBASELINE_ITEM);
                newBASELINE_ITEM.GUID = Guid.Empty;
                newBASELINE_ITEM.GUID_BASELINE = newBASELINE.GUID;
                newBASELINE_ITEM.INTERNAL_NUM = BluePrintDataUtils.BASELINEITEM_Generate_InternalNumber(this.Entity, newBASELINE_ITEM, newBASELINE_ITEMS, addBASELINE_ITEM.AREA, addBASELINE_ITEM.DISCIPLINE, addBASELINE_ITEM.DOCTYPE);
                VARIATION_ITEM editVARIATION_ITEM = editVARIATION_ITEMS.First(x => x.GUID_ORIBASEITEM == newBASELINE_ITEM.GUID_ORIGINAL);
                newBASELINE_ITEM.DC_HOURS = editVARIATION_ITEM.VARIATION_UNITS;
                newBASELINE_ITEM.GUID_VARIATION = entity.GUID;

                newBASELINE_ITEMS.Add(newBASELINE_ITEM);
            }

            foreach(BASELINE_ITEM newBASELINE_ITEM in newBASELINE_ITEMS)
            {
                VariationAdditionBASELINEITEMSCollection.Save(newBASELINE_ITEM);
            }

            LiveBASELINE.STATUS = BaselineStatus.Superseded;
            LiveBASELINECollection.Save(LiveBASELINE);
            newBASELINE.STATUS = BaselineStatus.Live;
            LiveBASELINECollection.Save(newBASELINE);
            LiveBASELINECollection.Refresh();
        }
        #endregion
    }

    public class ISupportCustomDocumentVARIATIONCollectionViewModel : CollectionViewModel<VARIATION, Guid, IBluePrintsEntitiesUnitOfWork>, ISupportCustomDocumentTypeNameAndParameter
    {
        /// <summary>
        /// Creates a new instance of VARIATIONCollectionViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static ISupportCustomDocumentVARIATIONCollectionViewModel Create(PROJECT project, Func<IRepositoryQuery<VARIATION>, IQueryable<VARIATION>> projection = null)
        {
            return ViewModelSource.Create(() => new ISupportCustomDocumentVARIATIONCollectionViewModel(project, projection));
        }

        PROJECT thisPROJECT;
        /// <summary>
        /// Initializes a new instance of the VARIATIONCollectionViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the VARIATIONCollectionViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        protected ISupportCustomDocumentVARIATIONCollectionViewModel(PROJECT project, Func<IRepositoryQuery<VARIATION>, IQueryable<VARIATION>> projection = null)
            : base(BluePrintsEntitiesUnitOfWorkSource.GetUnitOfWorkFactory(), x => x.VARIATIONS, projection)
        {
            thisPROJECT = project;
        }

        #region ISupportCustomDocumentTypeNameAndParameter
        public string GetCustomDocumentTypeName()
        {
            return "VARIATION_ITEMCollectionView";
        }

        public object GetCustomDocumentParameter()
        {
            return new OptionalEntitiesParameter<PROJECT, VARIATION>(this.thisPROJECT, SelectedEntity);
        }

        public string GetCustomDocumentTitle()
        {
            return "[" + thisPROJECT.NUMBER + "] VARIATION";
        }

        public bool IsCustomModeEnabled()
        {
            return true;
        }
        #endregion
    }
}
