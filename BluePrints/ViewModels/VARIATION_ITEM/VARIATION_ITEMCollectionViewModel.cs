using System;
using System.Linq;
using DevExpress.Mvvm.POCO;
using BluePrints.Common.Utils;
using BluePrints.BluePrintsEntitiesDataModel;
using BluePrints.Common.DataModel;
using BluePrints.Data;
using BluePrints.Common.ViewModel;

namespace BluePrints.ViewModels
{
    /// <summary>
    /// Represents the VARIATION_ITEMS collection view model.
    /// </summary>
    public partial class VARIATION_ITEMSCollectionViewModel : CollectionViewModel<VARIATION_ITEM, Guid, IBluePrintsEntitiesUnitOfWork>
    {

        /// <summary>
        /// Creates a new instance of VARIATION_ITEMSCollectionViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static VARIATION_ITEMSCollectionViewModel Create(IUnitOfWorkFactory<IBluePrintsEntitiesUnitOfWork> unitOfWorkFactory = null)
        {
            return ViewModelSource.Create(() => new VARIATION_ITEMSCollectionViewModel(unitOfWorkFactory, null));
        }

        /// <summary>
        /// Creates a new instance of VARIATION_ITEMSCollectionViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static VARIATION_ITEMSCollectionViewModel Create(IUnitOfWorkFactory<IBluePrintsEntitiesUnitOfWork> unitOfWorkFactory = null, Func<IRepositoryQuery<VARIATION_ITEM>, IQueryable<VARIATION_ITEM>> projection = null)
        {
            return ViewModelSource.Create(() => new VARIATION_ITEMSCollectionViewModel(unitOfWorkFactory, projection));
        }

        /// <summary>
        /// Initializes a new instance of the VARIATION_ITEMSCollectionViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the VARIATION_ITEMSCollectionViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        protected VARIATION_ITEMSCollectionViewModel(IUnitOfWorkFactory<IBluePrintsEntitiesUnitOfWork> unitOfWorkFactory = null, Func<IRepositoryQuery<VARIATION_ITEM>, IQueryable<VARIATION_ITEM>> projection = null)
            : base(unitOfWorkFactory ?? BluePrintsEntitiesUnitOfWorkSource.GetUnitOfWorkFactory(), x => x.VARIATION_ITEMS, projection)
        {
        }
    }
}