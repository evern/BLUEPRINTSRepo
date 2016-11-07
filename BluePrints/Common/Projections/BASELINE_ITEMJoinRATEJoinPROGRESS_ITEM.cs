using BluePrints.BluePrintsEntitiesDataModel;
using BluePrints.Common.DataModel;
using BluePrints.Common.ViewModel;
using BluePrints.Common.ViewModel.Reporting;
using BluePrints.Data;
using BluePrints.Data.Attributes;
using DevExpress.Mvvm.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluePrints.Common.Projections
{
    public class BASELINE_ITEMJoinRATEJoinPROGRESS_ITEM : ReportableObject
    {
        public BASELINE_ITEMJoinRATEJoinPROGRESS_ITEM()
        {

        }

        public Guid GUID { get; set; }
    }

    /// <summary>
    /// Represents the BASELINE_ITEMS collection view model.
    /// </summary>
    public partial class BASELINE_ITEMSJoinRATESJoinPROGRESS_ITEMSCollectionViewModel : CollectionViewModel<BASELINE_ITEM, BASELINE_ITEMJoinRATEJoinPROGRESS_ITEM, Guid, IBluePrintsEntitiesUnitOfWork>
    {
        /// <summary>
        /// Creates a new instance of BASELINE_ITEMSCollectionViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static BASELINE_ITEMSJoinRATESJoinPROGRESS_ITEMSCollectionViewModel Create(Func<IRepositoryQuery<BASELINE_ITEM>, IQueryable<BASELINE_ITEMJoinRATEJoinPROGRESS_ITEM>> projection, IUnitOfWorkFactory<IBluePrintsEntitiesUnitOfWork> unitOfWorkFactory = null)
        {
            return ViewModelSource.Create(() => new BASELINE_ITEMSJoinRATESJoinPROGRESS_ITEMSCollectionViewModel(projection, unitOfWorkFactory));
        }

        /// <summary>
        /// Initializes a new instance of the BASELINE_ITEMSCollectionViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the BASELINE_ITEMSCollectionViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public BASELINE_ITEMSJoinRATESJoinPROGRESS_ITEMSCollectionViewModel(Func<IRepositoryQuery<BASELINE_ITEM>, IQueryable<BASELINE_ITEMJoinRATEJoinPROGRESS_ITEM>> projection, IUnitOfWorkFactory<IBluePrintsEntitiesUnitOfWork> unitOfWorkFactory = null)
            : base(unitOfWorkFactory ?? BluePrintsEntitiesUnitOfWorkSource.GetUnitOfWorkFactory(), x => x.BASELINE_ITEMS, projection)
        {
        }
    }

    public static class BASELINE_ITEMSJoinRATESJoinPROGRESS_ITEMSQueries
    {
        public static IQueryable<BASELINE_ITEMJoinRATEJoinPROGRESS_ITEM> JoinRATESAndPROGRESS_ITEMSOnBASELINE_ITEMS(IQueryable<BASELINE_ITEM> BASELINE_ITEMS, Func<PROGRESS> getPROGRESSFunc, Func<BASELINE> getBASELINEFunc, Func<IQueryable<PROGRESS_ITEM>> getPROGRESS_ITEMSFunc, Func<IQueryable<RATE>> getRATESFunc)
        {
            BASELINE BASELINE = getBASELINEFunc();
            PROGRESS PROGRESS = getPROGRESSFunc();

            IQueryable<RATE> RATES = getRATESFunc();

            IQueryable<PROGRESS_ITEM> LoadPROGRESS_ITEMS;
            if(PROGRESS == null)
                LoadPROGRESS_ITEMS = getPROGRESS_ITEMSFunc().Where(x => x.GUID_PROGRESS == Guid.Empty).ToArray().AsQueryable();
            else
                LoadPROGRESS_ITEMS = getPROGRESS_ITEMSFunc().ToArray().AsQueryable();

            IQueryable<BASELINE_ITEMJoinRATE> BASELINE_ITEMJoinRATES;
            if (PROGRESS == null)
                BASELINE_ITEMJoinRATES = BASELINE_ITEMSJoinRATESQueries.JoinRATESOnBASELINE_ITEMS(BASELINE_ITEMS.Where(x => x.GUID == Guid.Empty), getBASELINEFunc, getRATESFunc);
            else
                BASELINE_ITEMJoinRATES = BASELINE_ITEMSJoinRATESQueries.JoinRATESOnBASELINE_ITEMS(BASELINE_ITEMS, getBASELINEFunc, getRATESFunc);

            DateTime reportingDate = PROGRESS == null ? new DateTime() : PROGRESS.DATA_DATE;
            return BASELINE_ITEMJoinRATES.ToArray().AsQueryable().Select(x => new BASELINE_ITEMJoinRATEJoinPROGRESS_ITEM() { GUID = x.GUID, BASELINE_ITEMJoinRATE = x, ReportingDataDate = reportingDate, PROGRESS_ITEMS = LoadPROGRESS_ITEMS.Where(y => y.GUID_ORIBASEITEM == x.BASELINE_ITEM.GUID_ORIGINAL).ToArray().AsEnumerable() });
        }
    }
}
