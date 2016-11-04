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
    /// Represents the PROJECTES collection view model.
    /// </summary>
    public partial class P6PROJECTCollectionViewModel : CollectionViewModel<PROJECT, int, IP6EntitiesUnitOfWork>
    {

        /// <summary>
        /// Creates a new instance of P6PROJECTCollectionViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static P6PROJECTCollectionViewModel Create(IUnitOfWorkFactory<IP6EntitiesUnitOfWork> unitOfWorkFactory = null)
        {
            return ViewModelSource.Create(() => new P6PROJECTCollectionViewModel(unitOfWorkFactory, null));
        }

        /// <summary>
        /// Creates a new instance of P6PROJECTCollectionViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static P6PROJECTCollectionViewModel Create(IUnitOfWorkFactory<IP6EntitiesUnitOfWork> unitOfWorkFactory = null, Func<IRepositoryQuery<PROJECT>, IQueryable<PROJECT>> projection = null)
        {
            return ViewModelSource.Create(() => new P6PROJECTCollectionViewModel(unitOfWorkFactory, projection));
        }

        /// <summary>
        /// Initializes a new instance of the P6PROJECTCollectionViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the P6PROJECTCollectionViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        protected P6PROJECTCollectionViewModel(IUnitOfWorkFactory<IP6EntitiesUnitOfWork> unitOfWorkFactory = null, Func<IRepositoryQuery<PROJECT>, IQueryable<PROJECT>> projection = null)
            : base(unitOfWorkFactory ?? P6EntitiesUnitOfWorkSource.GetUnitOfWorkFactory(), x => x.PROJECT, projection)
        {
        }
    }
}
