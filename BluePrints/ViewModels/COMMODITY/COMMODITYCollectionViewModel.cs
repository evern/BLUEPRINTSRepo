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
    /// Represents the COMMODITIES collection view model.
    /// </summary>
    public partial class COMMODITYCollectionViewModel : CollectionViewModel<COMMODITY, Guid, IBluePrintsEntitiesUnitOfWork>
    {

        /// <summary>
        /// Creates a new instance of COMMODITYCollectionViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static COMMODITYCollectionViewModel Create(IUnitOfWorkFactory<IBluePrintsEntitiesUnitOfWork> unitOfWorkFactory = null)
        {
            return ViewModelSource.Create(() => new COMMODITYCollectionViewModel(unitOfWorkFactory));
        }

        /// <summary>
        /// Initializes a new instance of the COMMODITYCollectionViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the COMMODITYCollectionViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        protected COMMODITYCollectionViewModel(IUnitOfWorkFactory<IBluePrintsEntitiesUnitOfWork> unitOfWorkFactory = null)
            : base(unitOfWorkFactory ?? BluePrintsEntitiesUnitOfWorkSource.GetUnitOfWorkFactory(), x => x.COMMODITIES)
        {
        }
    }
}