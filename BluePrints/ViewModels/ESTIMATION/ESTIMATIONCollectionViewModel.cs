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
    /// Represents the ESTIMATIONS collection view model.
    /// </summary>
    public partial class ESTIMATIONCollectionViewModel : CollectionViewModel<ESTIMATION, Guid, IBluePrintsEntitiesUnitOfWork>
    {

        /// <summary>
        /// Creates a new instance of ESTIMATIONCollectionViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static ESTIMATIONCollectionViewModel Create(IUnitOfWorkFactory<IBluePrintsEntitiesUnitOfWork> unitOfWorkFactory = null)
        {
            return ViewModelSource.Create(() => new ESTIMATIONCollectionViewModel(unitOfWorkFactory));
        }

        /// <summary>
        /// Initializes a new instance of the ESTIMATIONCollectionViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the ESTIMATIONCollectionViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        protected ESTIMATIONCollectionViewModel(IUnitOfWorkFactory<IBluePrintsEntitiesUnitOfWork> unitOfWorkFactory = null)
            : base(unitOfWorkFactory ?? BluePrintsEntitiesUnitOfWorkSource.GetUnitOfWorkFactory(), x => x.ESTIMATIONS)
        {
        }
    }
}