using BluePrints.Common.DataModel;
using BluePrints.Common.ViewModel;
using BluePrints.P6Data;
using BluePrints.P6EntitiesDataModel;
using DevExpress.Mvvm.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluePrints.ViewModels
{
    /// <summary>
    /// Represents the TASKES collection view model.
    /// </summary>
    public partial class P6PROJWBSCollectionViewModel : CollectionViewModel<PROJWBS, int, IP6EntitiesUnitOfWork>
    {
        /// <summary>
        /// Creates a new instance of P6PROJWBSCollectionViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static P6PROJWBSCollectionViewModel Create(IUnitOfWorkFactory<IP6EntitiesUnitOfWork> unitOfWorkFactory = null)
        {
            return ViewModelSource.Create(() => new P6PROJWBSCollectionViewModel(unitOfWorkFactory, null));
        }

        /// <summary>
        /// Creates a new instance of P6PROJWBSCollectionViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static P6PROJWBSCollectionViewModel Create(IUnitOfWorkFactory<IP6EntitiesUnitOfWork> unitOfWorkFactory = null, Func<IRepositoryQuery<PROJWBS>, IQueryable<PROJWBS>> projection = null)
        {
            return ViewModelSource.Create(() => new P6PROJWBSCollectionViewModel(unitOfWorkFactory, projection));
        }

        /// <summary>
        /// Initializes a new instance of the P6PROJWBSCollectionViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the P6PROJWBSCollectionViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        protected P6PROJWBSCollectionViewModel(IUnitOfWorkFactory<IP6EntitiesUnitOfWork> unitOfWorkFactory = null, Func<IRepositoryQuery<PROJWBS>, IQueryable<PROJWBS>> projection = null)
            : base(unitOfWorkFactory ?? P6EntitiesUnitOfWorkSource.GetUnitOfWorkFactory(), x => x.PROJWBS, projection)
        {
        }
    }
}
