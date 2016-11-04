using System;
using System.Linq;
using System.Windows.Controls;
using System.Collections.Generic;
using BluePrints.ViewModels;

namespace BluePrints.Views
{
    public partial class PROGRESSView : UserControl
    {
        public PROGRESSView()
        {
            InitializeComponent();
            ((PROGRESSViewModelWrapper)this.DataContext).ShowNewWorkpack = this.ShowNewWorkpack;
        }

        void ShowNewWorkpack()
        {
            columnWorkpack1.Visible = false;
            columnWorkpack2.Visible = true;
        }
    }
}
