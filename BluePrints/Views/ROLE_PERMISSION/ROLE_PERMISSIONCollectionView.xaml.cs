using System;
using System.Linq;
using System.Windows.Controls;
using System.Collections.Generic;
using BluePrints.ViewModels;

namespace BluePrints.Views
{
    public partial class ROLE_PERMISSIONCollectionView : UserControl
    {
        public ROLE_PERMISSIONCollectionView()
        {
            InitializeComponent();
        }

        private void dragDropManager_Dropped(object sender, DevExpress.Xpf.Grid.DragDrop.TreeListDroppedEventArgs e)
        {
            ((ROLE_PERMISSIONSCollectionViewModelWrapper)this.DataContext).dragDropManager_Dropped(sender, e);
        }
    }
}
