   M S B A M L   `   `   `  ���� E  ABluePrints, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:BluePrints.ViewModelsBluePrints.ViewModels  ]3clr-namespace:BluePrints.Common.ViewModel.Filtering%BluePrints.Common.ViewModel.Filtering  'clr-namespace:BluePrints
BluePrints  .    (BluePrints.Views.NavigationTreeViewPanel   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35a ]DevExpress.Xpf.Core.v16.1, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aH 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     ;dx2http://schemas.devexpress.com/winfx/2008/xaml/core  f bDevExpress.Xpf.Grid.v16.1.Core, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49al hDevExpress.Xpf.Grid.v16.1.Extensions, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aa	 ]DevExpress.Xpf.Grid.v16.1, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a@dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid   	 Hmc;http://schemas.openxmlformats.org/markup-compatibility/2006   	 >d2http://schemas.microsoft.com/expression/blend/2008   	 7	viewmodel#clr-namespace:BluePrints.ViewModels   	 G	filtering3clr-namespace:BluePrints.Common.ViewModel.Filtering   	 ]
 YDevExpress.Mvvm.v16.1, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aAdxmvvm2http://schemas.devexpress.com/winfx/2008/xaml/mvvm  
 &localclr-namespace:BluePrints  
   � 	Resources     groupItemStyle �� 
TargetType �� Property �� Value  filterItemStyle �� Mode �� RelativeSource �� AncestorType �� Triggers P� Property	 P� Value��%  &	        &	 (     �� " Q�.���  � ��$ Stretch=��� " Q�.���  �@Q�
IsSelected �� *
IsSelected+$ TwoWay=���  � �� �� *
Foreground+ �� $ TreeViewL���  � ��$ Stretch=� P�  �@Q�
IsSelected	 . . ���  � ��$
 Bold"�.��) 	 #DevExpress.Xpf.Grid.TreeListControl  $
��2000��$
��2000��4  .DevExpress.Xpf.Core.Serialization.DXSerializer
   SerializationID$
 NavigationTree��$��0q�
������ $��Disabled=�)  #DevExpress.Xpf.Grid.DataControlBase   ItemsSource �� *	Modules+   SelectedItem �� *SelectedModule+$ TwoWay=����� *Parent.Foreground+ �� *Self+   Columns ( 	 "DevExpress.Xpf.Grid.TreeListColumn  $  DevExpress.Xpf.Grid.BaseColumn   Header)  #DevExpress.Xpf.Core.ObjectConverter$ Title $  DevExpress.Xpf.Grid.ColumnBase   	FieldName$ ModuleTitle��   VisibleIndex$ 0��   Width2  ,DevExpress.Xpf.Grid.GridColumnWidthConverter$ *    View &	 	  DevExpress.Xpf.Grid.TreeListView	  -    	 Name$ TreeListView�� 	  ShowNodeImages .  	  TreeLineStyle$
 None=� 	  ExpandStateFieldName$ 
IsExpanded�� 	  ImageFieldName$ TreeViewImage��&
   DevExpress.Xpf.Grid.DataViewBase 
  AllowEditing .   
  AllowSorting .   	  ParentFieldName$ TreeViewParentId�� 	  KeyFieldName$ 
TreeViewId�� 	  AutoExpandAllNodes . 2  ,DevExpress.Mvvm.UI.Interactivity.Interaction   	Behaviors '  !DevExpress.Mvvm.UI.EventToCommand  �  �DevExpress.Mvvm.UI.Interactivity.EventTriggerBase`1[[System.Windows.DependencyObject, WindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35]]   	EventName$ MouseDoubleClick��+  %DevExpress.Mvvm.UI.EventToCommandBase   Command �� *NavigateCommand+