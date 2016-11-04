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
    /// Represents the ROLE_PERMISSIONS collection view model.
    /// </summary>
    public partial class ROLE_PERMISSIONSCollectionViewModel : CollectionViewModel<ROLE_PERMISSION, Guid, IBluePrintsEntitiesUnitOfWork>
    {

        /// <summary>
        /// Creates a new instance of ROLE_PERMISSIONSCollectionViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static ROLE_PERMISSIONSCollectionViewModel Create(IUnitOfWorkFactory<IBluePrintsEntitiesUnitOfWork> unitOfWorkFactory = null, Func<IRepositoryQuery<ROLE_PERMISSION>, IQueryable<ROLE_PERMISSION>> projection = null)
        {
            return ViewModelSource.Create(() => new ROLE_PERMISSIONSCollectionViewModel(unitOfWorkFactory, projection));
        }

        /// <summary>
        /// Initializes a new instance of the ROLE_PERMISSIONSCollectionViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the ROLE_PERMISSIONSCollectionViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        protected ROLE_PERMISSIONSCollectionViewModel(IUnitOfWorkFactory<IBluePrintsEntitiesUnitOfWork> unitOfWorkFactory = null, Func<IRepositoryQuery<ROLE_PERMISSION>, IQueryable<ROLE_PERMISSION>> projection = null)
            : base(unitOfWorkFactory ?? BluePrintsEntitiesUnitOfWorkSource.GetUnitOfWorkFactory(), x => x.ROLE_PERMISSIONS, projection)
        {
        }
    }
}