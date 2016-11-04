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
    /// Represents the VARIATIONS collection view model.
    /// </summary>
    public partial class VARIATIONCollectionViewModel : CollectionViewModel<VARIATION, Guid, IBluePrintsEntitiesUnitOfWork>
    {

        /// <summary>
        /// Creates a new instance of VARIATIONCollectionViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static VARIATIONCollectionViewModel Create(IUnitOfWorkFactory<IBluePrintsEntitiesUnitOfWork> unitOfWorkFactory = null)
        {
            return ViewModelSource.Create(() => new VARIATIONCollectionViewModel(unitOfWorkFactory));
        }

        /// <summary>
        /// Initializes a new instance of the VARIATIONCollectionViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the VARIATIONCollectionViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        protected VARIATIONCollectionViewModel(IUnitOfWorkFactory<IBluePrintsEntitiesUnitOfWork> unitOfWorkFactory = null)
            : base(unitOfWorkFactory ?? BluePrintsEntitiesUnitOfWorkSource.GetUnitOfWorkFactory(), x => x.VARIATIONS)
        {
        }
    }
}