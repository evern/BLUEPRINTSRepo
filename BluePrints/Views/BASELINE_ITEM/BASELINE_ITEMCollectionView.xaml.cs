using System;
using System.Linq;
using System.Windows.Controls;
using System.Collections.Generic;
using BluePrints.ViewModels;

namespace BluePrints.Views
{
    public partial class BASELINE_ITEMCollectionView : UserControl
    {
        public BASELINE_ITEMCollectionView()
        {
            InitializeComponent();
            ((BASELINE_ITEMSViewModelWrapper)this.DataContext).ShowWORKPACKInternalName1 = this.ShowWorkpackInternalName1;
            ((BASELINE_ITEMSViewModelWrapper)this.DataContext).ShowWORKPACKInternalName2 = this.ShowWorkpackInternalName2;
        }

        public void ShowWorkpackInternalName1()
        {
            colWORKPACKInternalName1.Visible = true;
        }

        public void ShowWorkpackInternalName2()
        {
            colWORKPACKInternalName2.Visible = true;
        }
    }
}
