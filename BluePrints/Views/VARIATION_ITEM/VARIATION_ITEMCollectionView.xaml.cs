using System;
using System.Linq;
using System.Windows.Controls;
using System.Collections.Generic;
using BluePrints.ViewModels;
using BluePrints.Common;

namespace BluePrints.Views
{
    public partial class VARIATION_ITEMCollectionView : UserControl
    {
        public VARIATION_ITEMCollectionView()
        {
            InitializeComponent();
            ((VARIATION_ITEMSViewModelWrapper)this.DataContext).ShowWORKPACKInternalName1 = this.ShowWorkpackInternalName1;
            ((VARIATION_ITEMSViewModelWrapper)this.DataContext).ShowWORKPACKInternalName2 = this.ShowWorkpackInternalName2;
            ((VARIATION_ITEMSViewModelWrapper)this.DataContext).SetCopyPasteBinding = this.SetCopyPasteBinding;
        }

        public void ShowWorkpackInternalName1()
        {
            colWORKPACKInternalName1.Visible = true;
        }

        public void ShowWorkpackInternalName2()
        {
            colWORKPACKInternalName2.Visible = true;
        }

        public void SetCopyPasteBinding()
        {
            this.copyPasteArea.Items = ((VARIATION_ITEMSViewModelWrapper)this.DataContext).AREACollection;
            this.copyPasteDeliverabletype.Items = Enum.GetValues(typeof(DeliverableType)).Cast<object>().ToList();
            this.copyPasteDepartment.Items = ((VARIATION_ITEMSViewModelWrapper)this.DataContext).DEPARTMENTCollection;
            this.copyPasteDiscipline.Items = ((VARIATION_ITEMSViewModelWrapper)this.DataContext).DISCIPLINECollection;
            this.copyPasteDocType.Items = ((VARIATION_ITEMSViewModelWrapper)this.DataContext).DOCTYPECollection;
            this.copyPasteInternalName1.Items = ((VARIATION_ITEMSViewModelWrapper)this.DataContext).WORKPACKCollection;
            this.copyPasteInternalName2.Items = ((VARIATION_ITEMSViewModelWrapper)this.DataContext).WORKPACKCollection;
            this.copyPastePhase.Items = ((VARIATION_ITEMSViewModelWrapper)this.DataContext).PHASECollection;
        }
    }
}
