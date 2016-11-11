﻿using BluePrints.BluePrintsEntitiesDataModel;
using BluePrints.Common.Projections;
using BluePrints.Common.ViewModel;
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
    /// Interaction logic for PROJECTWORKPACKDetailsActivityAssignment.xaml
    /// </summary>
    public partial class PROJECTWORKPACKDetailsActivityAssignment : DXWindow, IDisposable
    {
        public PROJECTWORKPACKDetailsActivityAssignment(IEnumerable<TASK_AppointmentInfo> AllTASK_Appointments, IEnumerable<WORKPACK_Dashboard> WORKPACKS, 
            CollectionViewModel<WORKPACK_ASSIGNMENT, WORKPACK_ASSIGNMENT, Guid, IBluePrintsEntitiesUnitOfWork> WORKPACK_ASSIGNMENTSViewModel, bool IsModified, Appointment SelectedTASK_Appointment)
        {
            InitializeComponent();
            this.DataContext = PROJECTWORKPACKAssignmentViewModel.Create(AllTASK_Appointments, WORKPACKS, WORKPACK_ASSIGNMENTSViewModel, IsModified, SelectedTASK_Appointment);
            ((PROJECTWORKPACKAssignmentViewModel)this.DataContext).RefreshWORKPACK_ASSIGNMENTCallBack = this.RefreshWORKPACK_ASSIGNMENTCallBack;
        }

        public void RefreshWORKPACK_ASSIGNMENTCallBack()
        {
            gridControlTASK_ASSIGNMENTS.RefreshData();
        }

        public void Dispose()
        {
            ((PROJECTWORKPACKAssignmentViewModel)this.DataContext).Dispose();
        }
    }
}
