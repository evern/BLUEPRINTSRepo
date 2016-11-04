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
    public partial class P6TASKCollectionViewModel : CollectionViewModel<TASK, int, IP6EntitiesUnitOfWork>
    {

        /// <summary>
        /// Creates a new instance of P6TASKCollectionViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static P6TASKCollectionViewModel Create(IUnitOfWorkFactory<IP6EntitiesUnitOfWork> unitOfWorkFactory = null)
        {
            return ViewModelSource.Create(() => new P6TASKCollectionViewModel(unitOfWorkFactory, null));
        }

        /// <summary>
        /// Creates a new instance of P6TASKCollectionViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static P6TASKCollectionViewModel Create(IUnitOfWorkFactory<IP6EntitiesUnitOfWork> unitOfWorkFactory = null, Func<IRepositoryQuery<TASK>, IQueryable<TASK>> projection = null)
        {
            return ViewModelSource.Create(() => new P6TASKCollectionViewModel(unitOfWorkFactory, projection));
        }

        /// <summary>
        /// Initializes a new instance of the P6TASKCollectionViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the P6TASKCollectionViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        protected P6TASKCollectionViewModel(IUnitOfWorkFactory<IP6EntitiesUnitOfWork> unitOfWorkFactory = null, Func<IRepositoryQuery<TASK>, IQueryable<TASK>> projection = null)
            : base(unitOfWorkFactory ?? P6EntitiesUnitOfWorkSource.GetUnitOfWorkFactory(), x => x.TASK, projection)
        {
        }
    }
}
