using System;
using System.Linq;
using DevExpress.Mvvm.POCO;
using BluePrints.Common.Utils;
using BluePrints.BluePrintsEntitiesDataModel;
using BluePrints.Common.DataModel;
using BluePrints.Data;
using BluePrints.Common.ViewModel;
using BluePrints.Common.Projections;

namespace BluePrints.ViewModels
{
    /// <summary>
    /// Represents the BASELINE_ITEMS collection view model.
    /// </summary>
    public partial class BASELINE_ITEMSCollectionViewModel : CollectionViewModel<BASELINE_ITEM, Guid, IBluePrintsEntitiesUnitOfWork>
    {

        /// <summary>
        /// Creates a new instance of BASELINE_ITEMSCollectionViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static BASELINE_ITEMSCollectionViewModel Create(IUnitOfWorkFactory<IBluePrintsEntitiesUnitOfWork> unitOfWorkFactory = null)
        {
            return ViewModelSource.Create(() => new BASELINE_ITEMSCollectionViewModel(unitOfWorkFactory, null));
        }

        /// <summary>
        /// Creates a new instance of BASELINE_ITEMSCollectionViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static BASELINE_ITEMSCollectionViewModel Create(IUnitOfWorkFactory<IBluePrintsEntitiesUnitOfWork> unitOfWorkFactory = null, Func<IRepositoryQuery<BASELINE_ITEM>, IQueryable<BASELINE_ITEM>> projection = null)
        {
            return ViewModelSource.Create(() => new BASELINE_ITEMSCollectionViewModel(unitOfWorkFactory, projection));
        }

        /// <summary>
        /// Initializes a new instance of the BASELINE_ITEMSCollectionViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the BASELINE_ITEMSCollectionViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public BASELINE_ITEMSCollectionViewModel(IUnitOfWorkFactory<IBluePrintsEntitiesUnitOfWork> unitOfWorkFactory = null, Func<IRepositoryQuery<BASELINE_ITEM>, IQueryable<BASELINE_ITEM>> projection = null)
            : base(unitOfWorkFactory ?? BluePrintsEntitiesUnitOfWorkSource.GetUnitOfWorkFactory(), x => x.BASELINE_ITEMS, projection)
        {
        }
    }
}