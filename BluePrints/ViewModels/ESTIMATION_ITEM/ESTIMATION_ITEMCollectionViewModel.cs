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
    /// Represents the ESTIMATION_ITEMS collection view model.
    /// </summary>
    public partial class ESTIMATION_ITEMSCollectionViewModel : CollectionViewModel<ESTIMATION_ITEM, Guid, IBluePrintsEntitiesUnitOfWork>
    {

        /// <summary>
        /// Creates a new instance of ESTIMATION_ITEMSCollectionViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static ESTIMATION_ITEMSCollectionViewModel Create(IUnitOfWorkFactory<IBluePrintsEntitiesUnitOfWork> unitOfWorkFactory = null)
        {
            return ViewModelSource.Create(() => new ESTIMATION_ITEMSCollectionViewModel(unitOfWorkFactory));
        }

        /// <summary>
        /// Initializes a new instance of the ESTIMATION_ITEMSCollectionViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the ESTIMATION_ITEMSCollectionViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        protected ESTIMATION_ITEMSCollectionViewModel(IUnitOfWorkFactory<IBluePrintsEntitiesUnitOfWork> unitOfWorkFactory = null)
            : base(unitOfWorkFactory ?? BluePrintsEntitiesUnitOfWorkSource.GetUnitOfWorkFactory(), x => x.ESTIMATION_ITEMS)
        {
        }
    }
}