using BluePrints.Data;
using BluePrints.ViewModels;
using DevExpress.Xpf.Core;
using DevExpress.XtraScheduler;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for PROJECTWORKPACKDetailsWorkpackAssignment.xaml
    /// </summary>
    public partial class PROJECTWORKPACKDetailsWorkpackAssignment : DXWindow, IDisposable
    {
        public PROJECTWORKPACKDetailsWorkpackAssignment(IEnumerable<TASK_AppointmentInfo> ALLTASK_Appointments, IEnumerable<WORKPACK_DashboardInfo> WORKPACKS, WORKPACK_ASSIGNMENTSCollectionViewModel WORKPACK_ASSIGNMENTSViewModel, bool IsModified, Appointment SelectedTASK_Appointment = null, WORKPACK_DashboardInfo SelectedWORKPACK = null)
        {
            InitializeComponent();
            this.DataContext = PROJECTWORKPACKAssignmentViewModel.Create(ALLTASK_Appointments, WORKPACKS, WORKPACK_ASSIGNMENTSViewModel, IsModified, SelectedTASK_Appointment, SelectedWORKPACK);
            ((PROJECTWORKPACKAssignmentViewModel)this.DataContext).RefreshWORKPACK_ASSIGNMENTCallBack = this.RefreshWORKPACK_ASSIGNMENTCallBack;
        }

        public void RefreshWORKPACK_ASSIGNMENTCallBack()
        {
            gridControlWORKPACK_ASSIGNMENTS.RefreshData();
        }

        public void Dispose()
        {
            ((PROJECTWORKPACKAssignmentViewModel)this.DataContext).Dispose();
        }
    }
}
