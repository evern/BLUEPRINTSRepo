﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using BluePrints.Common.DataModel;
using BluePrints.Common.ViewModel;
using BluePrints.BluePrintsEntitiesDataModel;
using BluePrints.Data;
using DevExpress.Xpf.LayoutControl;
using System.Drawing;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using BluePrints.Common.ViewModel.Filtering;
using System.Windows.Threading;
using BluePrints.Data.Helpers;
using BluePrints.Common.Projections;

namespace BluePrints.ViewModels
{
    /// <summary>
    /// Represents the root POCO view model for the BluePrintsEntities data model.
    /// </summary>
    public partial class BluePrintsEntitiesViewModel : DocumentsViewModel<BluePrintsEntitiesModuleDescription, IBluePrintsEntitiesUnitOfWork>
    {

        const string TablesGroup = "Tables";

        const string ViewsGroup = "Views";

        INavigationService NavigationService { get { return this.GetService<INavigationService>(); } }
        /// <summary>
        /// Creates a new instance of BluePrintsEntitiesViewModel as a POCO view model.
        /// </summary>
        public static BluePrintsEntitiesViewModel Create()
        {
            return ViewModelSource.Create(() => new BluePrintsEntitiesViewModel());
        }

        /// <summary>
        /// Initializes a new instance of the BluePrintsEntitiesViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the BluePrintsEntitiesViewModel type without the POCO proxy factory.
        /// </summary>
        protected BluePrintsEntitiesViewModel()
            : base(BluePrintsEntitiesUnitOfWorkSource.GetUnitOfWorkFactory())
        {

        }

        protected override BluePrintsEntitiesModuleDescription[] CreateModules()
        {
            ImageSource TreeViewImage = new BitmapImage(new Uri("pack://application:,,,/DevExpress.Images.v16.1;component/Images/Actions/Open_16x16.png"));
            ImageSource CategoryViewImage = new BitmapImage(new Uri("pack://application:,,,/DevExpress.Images.v16.1;component/Images/Data/ManageDataSource_16x16.png"));
            TreeViewProperty PROJECTDASHBOARDCollectionModuleTreeProperty = new TreeViewProperty() { Id = "PROJECTDASHBOARDCollectionView", ParentId = 0, Image = TreeViewImage };
            TreeViewProperty PROJECTCollectionModuleTreeProperty = new TreeViewProperty() { Id = "PROJECTCollectionView", ParentId = 0, Image = TreeViewImage, IsExpanded = true };
            TreeViewProperty DATACategoryTreeProperty = new TreeViewProperty() { Id = "DATACategoryView", ParentId = 0, Image = CategoryViewImage, IsExpanded = true };
            TreeViewProperty DEPARTMENTCollectionModuleTreeProperty = new TreeViewProperty() { Id = "DEPARTMENTCollectionView", ParentId = DATACategoryTreeProperty.Id, Image = TreeViewImage };
            TreeViewProperty DISCIPLINECollectionModuleTreeProperty = new TreeViewProperty() { Id = "DISCIPLINECollectionView", ParentId = DATACategoryTreeProperty.Id, Image = TreeViewImage };
            TreeViewProperty DOCTYPECollectionModuleTreeProperty = new TreeViewProperty() { Id = "DOCTYPECollectionView", ParentId = DATACategoryTreeProperty.Id, Image = TreeViewImage };
            TreeViewProperty UOMCollectionModuleTreeProperty = new TreeViewProperty() { Id = "UOMCollectionView", ParentId = DATACategoryTreeProperty.Id, Image = TreeViewImage };
            TreeViewProperty USERCollectionModuleTreeProperty = new TreeViewProperty() { Id = "USERCollectionView", ParentId = DATACategoryTreeProperty.Id, Image = TreeViewImage };
            TreeViewProperty ROLECollectionModuleTreeProperty = new TreeViewProperty() { Id = "ROLECollectionView", ParentId = DATACategoryTreeProperty.Id, Image = TreeViewImage };
            TreeViewProperty COMMODITY_CODECollectionModuleTreeProperty = new TreeViewProperty() { Id = "COMMODITY_CODECollectionView", ParentId = DATACategoryTreeProperty.Id, Image = TreeViewImage };

            List<BluePrintsEntitiesModuleDescription> BluePrintsEntitiesModuleDescriptions = new List<BluePrintsEntitiesModuleDescription>();
            BluePrintsEntitiesModuleDescriptions.Add(BluePrintsEntitiesModuleDescription.Create("DASHBOARDS", "PROJECTDashboardCollectionView", TablesGroup, null, null, PROJECTDASHBOARDCollectionModuleTreeProperty));

            BluePrintsEntitiesModuleDescriptions.Add(BluePrintsEntitiesModuleDescription.Create("DATA", "DATACategoryView", TablesGroup, null, null, DATACategoryTreeProperty));
            BluePrintsEntitiesModuleDescriptions.Add(BluePrintsEntitiesModuleDescription.Create("DEPARTMENTS", "DEPARTMENTCollectionView", TablesGroup, null, null, DEPARTMENTCollectionModuleTreeProperty));
            BluePrintsEntitiesModuleDescriptions.Add(BluePrintsEntitiesModuleDescription.Create("DISCIPLINES", "DISCIPLINECollectionView", TablesGroup, null, null, DISCIPLINECollectionModuleTreeProperty));
            BluePrintsEntitiesModuleDescriptions.Add(BluePrintsEntitiesModuleDescription.Create("DOCTYPES", "DOCTYPECollectionView", TablesGroup, null, null, DOCTYPECollectionModuleTreeProperty));
            BluePrintsEntitiesModuleDescriptions.Add(BluePrintsEntitiesModuleDescription.Create("UOM", "UOMCollectionView", TablesGroup, null, null, UOMCollectionModuleTreeProperty));
            BluePrintsEntitiesModuleDescriptions.Add(BluePrintsEntitiesModuleDescription.Create("USER", "USERCollectionView", TablesGroup, null, null, USERCollectionModuleTreeProperty));
            BluePrintsEntitiesModuleDescriptions.Add(BluePrintsEntitiesModuleDescription.Create("ROLE", "ROLECollectionView", TablesGroup, null, null, ROLECollectionModuleTreeProperty));
            BluePrintsEntitiesModuleDescriptions.Add(BluePrintsEntitiesModuleDescription.Create("COMMODITY_CODE", "COMMODITY_CODECollectionView", TablesGroup, null, null, COMMODITY_CODECollectionModuleTreeProperty));

            BluePrintsEntitiesModuleDescriptions.Add(BluePrintsEntitiesModuleDescription.Create("PROJECTS", "PROJECTCollectionView", TablesGroup, null, null, PROJECTCollectionModuleTreeProperty));

            var Projects = this.CreateUnitOfWork().PROJECTS.OrderBy(x => x.NUMBER).AsQueryable();
            foreach (var Project in Projects)
            {
                //TreeViewProperty PROJECTModuleTreeProperty = new TreeViewProperty() { Id = "PROJECTView" + Project.NUMBER, ParentId = PROJECTCollectionModuleTreeProperty.Id, Image = TreeViewImage };
                //BluePrintsEntitiesModuleDescriptions.Add(BluePrintsEntitiesModuleDescription.Create(Project.NUMBER, "PROJECTView", TablesGroup, null, Project.GUID, PROJECTModuleTreeProperty));
                BluePrintsEntitiesModuleDescriptions = BluePrintsEntitiesModuleDescriptions.Concat(createPROJECTTree(Project)).ToList();
            }

            return BluePrintsEntitiesModuleDescriptions.ToArray();
        }

        public virtual BluePrintsEntitiesModuleDescription SelectedItem { get; set; }
        BluePrintsEntitiesModuleDescription[] createPROJECTTree(PROJECT entity, bool createTree = false)
        {
            List<BluePrintsEntitiesModuleDescription> moduleDescriptions = new List<BluePrintsEntitiesModuleDescription>();

            ImageSource TreeViewImage = new BitmapImage(new Uri("pack://application:,,,/DevExpress.Images.v16.1;component/Images/Actions/Open_16x16.png"));
            TreeViewProperty PROJECTCollectionModuleTreeProperty = new TreeViewProperty() { Id = "PROJECTCollectionView", ParentId = 0, Image = TreeViewImage, IsExpanded = true }; 
            TreeViewProperty PROJECTModuleTreeProperty = new TreeViewProperty() { Id = "PROJECTView" + entity.NUMBER, ParentId = PROJECTCollectionModuleTreeProperty.Id, Image = TreeViewImage };

            string moduleTitle;
            moduleTitle = entity.NUMBER;
            moduleDescriptions.Add(BluePrintsEntitiesModuleDescription.Create(entity.NUMBER, "PROJECTView", TablesGroup, null, entity.GUID, PROJECTModuleTreeProperty));

            TreeViewProperty PROJECTPHASEModuleTreeProperty = new TreeViewProperty() { Id = "PHASEDetailsCollectionView" + entity.NUMBER, ParentId = PROJECTModuleTreeProperty.Id, Image = TreeViewImage };
            moduleTitle = "[" + entity.NUMBER + "] PHASES";
            moduleDescriptions.Add(BluePrintsEntitiesModuleDescription.Create(moduleTitle, "PROJECTPHASEDetailsCollectionView", TablesGroup, null, entity.GUID, PROJECTPHASEModuleTreeProperty));

            TreeViewProperty PROJECTAREAModuleTreeProperty = new TreeViewProperty() { Id = "AREADetailsCollectionView" + entity.NUMBER, ParentId = PROJECTModuleTreeProperty.Id, Image = TreeViewImage };
            moduleTitle = "[" + entity.NUMBER + "] AREAS";
            moduleDescriptions.Add(BluePrintsEntitiesModuleDescription.Create(moduleTitle, "PROJECTAREADetailsCollectionView", TablesGroup, null, entity.GUID, PROJECTAREAModuleTreeProperty));

            TreeViewProperty PROJECTRATEModuleTreeProperty = new TreeViewProperty() { Id = "RATEDetailsCollectionView" + entity.NUMBER, ParentId = PROJECTModuleTreeProperty.Id, Image = TreeViewImage };
            moduleTitle = "[" + entity.NUMBER + "] RATES";
            moduleDescriptions.Add(BluePrintsEntitiesModuleDescription.Create(moduleTitle, "PROJECTRATEDetailsCollectionView", TablesGroup, null, entity.GUID, PROJECTRATEModuleTreeProperty));

            TreeViewProperty PROJECTBASELINEModuleTreeProperty = new TreeViewProperty() { Id = "BASELINEDetailsCollectionView" + entity.NUMBER, ParentId = PROJECTModuleTreeProperty.Id, Image = TreeViewImage };
            moduleTitle = "[" + entity.NUMBER + "] BASELINES";
            moduleDescriptions.Add(BluePrintsEntitiesModuleDescription.Create(moduleTitle, "PROJECTBASELINEDetailsCollectionView", TablesGroup, null, entity.GUID, PROJECTBASELINEModuleTreeProperty));

            TreeViewProperty PROJECTLIVEBASELINEModuleTreeProperty = new TreeViewProperty() { Id = "LiveBASELINEView" + entity.NUMBER, ParentId = PROJECTModuleTreeProperty.Id, Image = TreeViewImage };
            moduleTitle = "[" + entity.NUMBER + "] LIVE BASELINE";
            moduleDescriptions.Add(BluePrintsEntitiesModuleDescription.Create(moduleTitle, "BASELINE_ITEMCollectionView", TablesGroup, null, new OptionalEntitiesParameter<PROJECT, BASELINE>(entity, null), PROJECTLIVEBASELINEModuleTreeProperty));

            TreeViewProperty PROJECTWORKPACKModuleTreeProperty = new TreeViewProperty() { Id = "WORKPACKDetailsCollectionView" + entity.NUMBER, ParentId = PROJECTModuleTreeProperty.Id, Image = TreeViewImage };
            moduleTitle = "[" + entity.NUMBER + "] WORKPACKS";
            moduleDescriptions.Add(BluePrintsEntitiesModuleDescription.Create(moduleTitle, "PROJECTWORKPACKDetailsCollectionView", TablesGroup, null, entity.GUID, PROJECTWORKPACKModuleTreeProperty));

            TreeViewProperty PROJECTALLPROGRESSModuleTreeProperty = new TreeViewProperty() { Id = "PROGRESSDetailsCollectionView" + entity.NUMBER, ParentId = PROJECTModuleTreeProperty.Id, Image = TreeViewImage };
            moduleTitle = "[" + entity.NUMBER + "] ALL PROGRESSES";
            moduleDescriptions.Add(BluePrintsEntitiesModuleDescription.Create(moduleTitle, "PROJECTPROGRESSDetailsCollectionView", TablesGroup, null, entity.GUID, PROJECTALLPROGRESSModuleTreeProperty));

            TreeViewProperty PROJECTLIVEPROGRESSModuleTreeProperty = new TreeViewProperty() { Id = "LivePROGRESSView" + entity.NUMBER, ParentId = PROJECTModuleTreeProperty.Id, Image = TreeViewImage };
            moduleTitle = "[" + entity.NUMBER + "] LIVE PROGRESS";
            moduleDescriptions.Add(BluePrintsEntitiesModuleDescription.Create(moduleTitle, "PROGRESS_ITEMCollectionView", TablesGroup, null, new OptionalEntitiesParameter<PROJECT, PROGRESS>(entity, null), PROJECTLIVEPROGRESSModuleTreeProperty));

            TreeViewProperty PROJECTVARIATIONModuleTreeProperty = new TreeViewProperty() { Id = "VARIATIONDetailsCollectionView" + entity.NUMBER, ParentId = PROJECTModuleTreeProperty.Id, Image = TreeViewImage };
            moduleTitle = "[" + entity.NUMBER + "] VARIATIONS";
            moduleDescriptions.Add(BluePrintsEntitiesModuleDescription.Create(moduleTitle, "VARIATIONCollectionView", TablesGroup, null, entity.GUID, PROJECTVARIATIONModuleTreeProperty));

            return moduleDescriptions.ToArray();
        }

        NavigationTreeViewModel CreateNavigationTree(PROJECT entity, string moduleTitle, bool createTree)
        {
            if (!createTree)
                return null;
            else
                return NavigationTreeViewModel.Create(createPROJECTTree(entity), moduleTitle, this);
        }

        //EntitiesLoader<PROJECT> PROJECTLoader;
        //PROJECTCollectionViewModel projectCollection;
        //PROJECTCollectionViewModel PROJECTCollection
        //{
        //    get
        //    {
        //        if (projectCollection == null)
        //        {
        //            projectCollection = PROJECTCollectionViewModel.Create(null, query => query.OrderBy(x => x.NUMBER));
        //            projectCollection.OnEntitiesChangedCallBack = this.onProjectCollectionChanged;
        //        }

        //        return projectCollection;
        //    }
        //}

        //public void onProjectCollectionChanged(IEnumerable<PROJECT> entities, object sender)
        //{
        //    List<BluePrintsEntitiesModuleDescription> moduleDescriptions = new List<BluePrintsEntitiesModuleDescription>();
        //    foreach(var entity in entities)
        //    {
        //        moduleDescriptions = moduleDescriptions.Concat(createPROJECTTree(entity)).ToList();
        //    }

        //    base.AddModules(moduleDescriptions);
        //    this.RaisePropertyChanged(x => x.Modules);
        //}

        protected override void OnSelectedModuleChanged(BluePrintsEntitiesModuleDescription oldModule)
        {
            //Not to execute base.OnSelectedModuleChanged because navigation is invoked on double click instead
        }

        protected override void OnActiveModuleChanged(BluePrintsEntitiesModuleDescription oldModule)
        {
            base.OnActiveModuleChanged(oldModule);
            //if (ActiveModule != null)
            //{
            //    if (ActiveModule.FilterTreeViewModel != null)
            //    {
            //        ActiveModule.FilterTreeViewModel.SetViewModel(DocumentManagerService.ActiveDocument.Content, ActiveModule.SelectedFilterName);
            //    }
            //}
        }
    }

    public partial class BluePrintsEntitiesModuleDescription : ModuleDescription<BluePrintsEntitiesModuleDescription>
    {
        public BluePrintsEntitiesModuleDescription Clone()
        {
            return BluePrintsEntitiesModuleDescription.Create(this.ModuleTitle, this.DocumentType, this.ModuleGroup, this.NavigationTreeViewModel, this.DocumentParameter, this.TreeViewProperty);
        }

        public static BluePrintsEntitiesModuleDescription Create(string title, string documentType, string group, NavigationTreeViewModel navigationTreeViewModel, object documentParameter = null, TreeViewProperty treeViewProperty = null)
        {
            return ViewModelSource.Create(() => new BluePrintsEntitiesModuleDescription(title, documentType, group, navigationTreeViewModel, documentParameter, treeViewProperty));
        }

        protected BluePrintsEntitiesModuleDescription(string title, string documentType, string group, NavigationTreeViewModel navigationTreeViewModel, object documentParameter = null, TreeViewProperty treeViewProperty = null)
            : base(title, documentType, group, null, documentParameter, treeViewProperty)
        {
            NavigationTreeViewModel = navigationTreeViewModel;
        }

        public virtual bool IsSelected { get; set; }
        public NavigationTreeViewModel NavigationTreeViewModel { get; private set; }

        public string DisplayTitle
        {
            get 
            {
                if (ModuleTitle.Length > 8)
                    return ModuleTitle.Substring(8, ModuleTitle.Length - 8);
                return ModuleTitle;
            }
        }
        //public IFilterTreeViewModel FilterTreeViewModel { get; private set; }
    }

    public class NavigationTreeViewModel
    {
        public static NavigationTreeViewModel Create(BluePrintsEntitiesModuleDescription[] modules, string selectedTitle, BluePrintsEntitiesViewModel owner)
        {
            return ViewModelSource.Create(() => new NavigationTreeViewModel(modules, selectedTitle, owner));
        }

        protected NavigationTreeViewModel(BluePrintsEntitiesModuleDescription[] modules, string selectedTitle, BluePrintsEntitiesViewModel owner)
        {
            Owner = owner;
            Modules = modules;
            DefaultItem = Modules.FirstOrDefault(x => x.ModuleTitle == selectedTitle);
            if(DefaultItem != null)
            {
                DefaultItem.IsSelected = true;
                SelectedItem = DefaultItem;
            }

            if (selectedTitle.Length > 7)
                StaticCategoryName = selectedTitle.Substring(0, 7);
            else
                StaticCategoryName = "[" + selectedTitle + "]";

            defaultSelectionTimer = new DispatcherTimer();
            defaultSelectionTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            defaultSelectionTimer.Tick += defaultSelectionTimer_Tick;
        }

        BluePrintsEntitiesViewModel Owner { get; set; }
        public virtual BluePrintsEntitiesModuleDescription[] Modules { get; protected set; }
        BluePrintsEntitiesModuleDescription lastSelectedItem { get; set; }
        public virtual BluePrintsEntitiesModuleDescription SelectedItem { get; set; }
        public virtual BluePrintsEntitiesModuleDescription DefaultItem { get; set; }

        public virtual string StaticCategoryName { get; set; }

        public void ResetToAll()
        {
            SelectedItem = Modules[0];
        }

        DispatcherTimer defaultSelectionTimer;
        protected virtual void OnSelectedItemChanged()
        {
            if (SelectedItem == null)
                SelectedItem = DefaultItem;

            if (SelectedItem != DefaultItem)
            {
                Owner.NavigateCore(SelectedItem);
                defaultSelectionTimer.Start();
            }
            else
                this.RaisePropertyChanged(x => x.SelectedItem);
        }

        //need to be invoked externally because during selecteditem time the routine is still within OnTreeViewSelectedItemChanged in TreeViewSelectedItemBehavior
        void defaultSelectionTimer_Tick(object sender, EventArgs e)
        {
            defaultSelectionTimer.Stop();
            SelectedItem = DefaultItem;
        }
    }
}

namespace BluePrints.Common.ViewModel
{
    public class TileProperty
    {
        public TileProperty()
        {
            TileLayoutFlowBreak = false;
            TileLayoutSize = TileSize.Small;
        }

        /// <summary>
        /// Specify whether the tile should break in tileLayout
        /// </summary>
        public bool TileLayoutFlowBreak { get; set; }
        /// <summary>
        /// Specify whether the size of the tile in tileLayout
        /// </summary>
        public TileSize TileLayoutSize { get; set; }
    }

    public class TreeViewProperty
    {
        public TreeViewProperty()
        {
            Id = 0;
            ParentId = 0;
        }

        public object Id { get; set; }
        public object ParentId { get; set; }
        public ImageSource Image { get; set; }
        public bool IsExpanded { get; set; }
    }


    public abstract partial class ModuleDescription<TModule> where TModule : ModuleDescription<TModule>
    {
        /// <summary>
        /// The navigation parameter for SingleObjectViewModel.
        /// </summary>
        public object DocumentParameter { get; private set; }
        /// <summary>
        /// Specifies the SingleObjectView document id
        /// </summary>
        public object DocumentId { get; private set; }
        /// <summary>
        /// Specifies the parentId for treeview binding, cannot be nested since dxTreeView doesn't support nested for Ids
        /// </summary>
        public object TreeViewParentId { get; private set; }
        /// <summary>
        /// Specifies the Id for treeview binding, cannot be nested since dxTreeView doesn't support nested for Ids
        /// </summary>
        public object TreeViewId { get; private set; }
        /// <summary>
        /// Specify the treeview property when binded to TreeViewControl
        /// </summary>
        public TreeViewProperty TreeViewProperty { get; private set; }
        /// <summary>
        /// Specify the treeview image property when binded to TreeViewControl
        /// </summary>
        public ImageSource TreeViewImage { get; set; }

        public bool IsExpanded { get; set; }

        /// <summary>
        /// Initializes a new instance of the ModuleDescription class.
        /// </summary>
        /// <param name="title">A navigation list entry display text.</param>
        /// <param name="documentType">A string value that specifies the view type of corresponding document.</param>
        /// <param name="group">A navigation list entry group name.</param>
        /// <param name="peekCollectionViewModelFactory">An optional parameter that provides a function used to create a PeekCollectionViewModel that provides quick navigation between collection views.</param>
        /// <param name="documentParameter">A document parameter to specify SingleObjectView to display</param>
        /// <param name="treeViewProperty">A property containing tree view specific properties for view binding</param>
        public ModuleDescription(string title, string documentType, string group, Func<TModule, object> peekCollectionViewModelFactory = null, object documentParameter = null, TreeViewProperty treeViewProperty = null)
        {
            ModuleTitle = title;
            ModuleGroup = group;
            DocumentType = documentType;
            DocumentId = documentParameter == null ? documentType : (documentType + documentParameter.ToString().Replace('-', '_'));
            DocumentParameter = documentParameter;
            TreeViewProperty = treeViewProperty;
            TreeViewParentId = treeViewProperty.ParentId;
            TreeViewId = treeViewProperty.Id;
            TreeViewImage = treeViewProperty.Image;
            IsExpanded = treeViewProperty.IsExpanded;
            this.peekCollectionViewModelFactory = peekCollectionViewModelFactory;
        }
    }
}