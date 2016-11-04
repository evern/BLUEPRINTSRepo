using BluePrints.BluePrintsEntitiesDataModel;
using BluePrints.Common.DataModel;
using BluePrints.Common.ViewModel;
using BluePrints.Data;
using DevExpress.Mvvm.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluePrints.Common.Projections
{
    public class BASELINE_ITEMJoinRATE
    {
        public BASELINE_ITEMJoinRATE()
        {
            BASELINE_ITEM = new BASELINE_ITEM();
        }

        public Guid GUID { get; set; }
        public BASELINE_ITEM BASELINE_ITEM { get; set; }
        public RATE RATE { get; set; }
        public decimal ITEMRATE
        {
            get
            {
                if (RATE == null || RATE.RATE1 == null)
                    return 0;

                return (decimal)RATE.RATE1;
            }
        }

        public decimal TOTAL_HOURS 
        { 
            get
            {
                if (BASELINE_ITEM == null)
                    return 0;

                return BASELINE_ITEM.ESTIMATED_HOURS + BASELINE_ITEM.DC_HOURS; 
            } 
        }

        public decimal ESTIMATED_COSTS
        {
            get
            {
                if (BASELINE_ITEM == null)
                    return 0;

                if (RATE == null || RATE.RATE1 == null)
                    return 0;

                return BASELINE_ITEM.ESTIMATED_HOURS * (decimal)RATE.RATE1;
            }
        }

        public decimal TOTAL_COSTS { get { return TOTAL_HOURS * ITEMRATE; } }
    }

    /// <summary>
    /// Represents the BASELINE_ITEMS collection view model.
    /// </summary>
    public partial class BASELINE_ITEMSJoinRATESCollectionViewModel : CollectionViewModel<BASELINE_ITEM, BASELINE_ITEMJoinRATE, Guid, IBluePrintsEntitiesUnitOfWork>
    {
        /// <summary>
        /// Creates a new instance of BASELINE_ITEMSCollectionViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static BASELINE_ITEMSJoinRATESCollectionViewModel Create(Func<IRepositoryQuery<BASELINE_ITEM>, IQueryable<BASELINE_ITEMJoinRATE>> projection, IUnitOfWorkFactory<IBluePrintsEntitiesUnitOfWork> unitOfWorkFactory = null)
        {
            return ViewModelSource.Create(() => new BASELINE_ITEMSJoinRATESCollectionViewModel(projection, unitOfWorkFactory));
        }

        /// <summary>
        /// Initializes a new instance of the BASELINE_ITEMSCollectionViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the BASELINE_ITEMSCollectionViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public BASELINE_ITEMSJoinRATESCollectionViewModel(Func<IRepositoryQuery<BASELINE_ITEM>, IQueryable<BASELINE_ITEMJoinRATE>> projection, IUnitOfWorkFactory<IBluePrintsEntitiesUnitOfWork> unitOfWorkFactory = null)
            : base(unitOfWorkFactory ?? BluePrintsEntitiesUnitOfWorkSource.GetUnitOfWorkFactory(), x => x.BASELINE_ITEMS, projection)
        {
        }
    }

    public static class BASELINE_ITEMSJoinRATESQueries
    {
        public static IQueryable<BASELINE_ITEMJoinRATE> JoinRATESOnBASELINE_ITEMS(IQueryable<BASELINE_ITEM> BASELINE_ITEMS, Func<BASELINE> getBASELINEFunc, Func<IQueryable<RATE>> getRATES_ByProjectFunc = null)
        {
            BASELINE BASELINE = getBASELINEFunc();
            IQueryable<BASELINE_ITEM> contextBASELINE_ITEMS;
            if (BASELINE == null)
                contextBASELINE_ITEMS = BASELINE_ITEMS.Where(x => x.GUID == Guid.Empty);
            else
                contextBASELINE_ITEMS = BASELINE_ITEMS.Where(x => x.GUID_BASELINE == BASELINE.GUID);

            IQueryable<RATE> RATES = getRATES_ByProjectFunc();
            return contextBASELINE_ITEMS.ToArray().AsQueryable().Select(x => new BASELINE_ITEMJoinRATE() { GUID = x.GUID, BASELINE_ITEM = x, RATE = RATES.FirstOrDefault(y => y.GUID_DEPARTMENT == x.GUID_DEPARTMENT && y.GUID_DISCIPLINE == x.GUID_DISCIPLINE) });
        }
    }
}
