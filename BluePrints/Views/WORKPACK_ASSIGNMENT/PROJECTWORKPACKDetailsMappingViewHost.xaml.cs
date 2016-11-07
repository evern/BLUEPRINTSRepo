using BluePrints.Data;
using BluePrints.P6Data;
using BluePrints.P6EntitiesDataModel;
using BluePrints.ViewModels;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Scheduler;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BluePrints.Views
{
    /// <summary>
    /// Interaction logic for PROJECTWORKPACKDetailsMappingView.xaml
    /// </summary>
    public partial class PROJECTWORKPACKDetailsMappingViewHost : UserControl
    {
        public PROJECTWORKPACKDetailsMappingViewHost()
        {
            InitializeComponent();
            //((PROJECTWORKPACKMappingCollectionViewModelWrapper)this.DataContext).windowsFormHostViewInitialization = this.windowsFormHostViewInitialization;
        }

        public void windowsFormHostViewInitialization(Func<IQueryable<TASK>> getTASKsFunc, Func<IQueryable<PROJWBS>> getWBSSFunc, PROJECTWORKPACKMappingCollectionViewModel WORKPACKSMappingCollectionViewModel, WORKPACK_ASSIGNMENTSCollectionViewModel WORKPACKS_ASSIGNMENTSViewModel, bool IsModified)
        {
            winFormHost.Child = new PROJECTWORKPACKDetailsMappingView(getTASKsFunc, getWBSSFunc, WORKPACKSMappingCollectionViewModel, WORKPACKS_ASSIGNMENTSViewModel, IsModified);
        }
    }
}
