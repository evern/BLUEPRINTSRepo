   M S B A M L   `   `   `  ����E  ABluePrints, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:BluePrints
BluePrints  5      =#clr-namespace:BluePrints.ViewModelsBluePrints.ViewModels  5      _4clr-namespace:BluePrints.Common.ViewModel.Converters&BluePrints.Common.ViewModel.Converters  5      5clr-namespace:BluePrints.CommonBluePrints.Common  5      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5      I2clr-namespace:System.Collections;assembly=mscorlibSystem.Collections 5      3clr-namespace:BluePrints.ViewsBluePrints.Views  6\   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35a ]DevExpress.Xpf.Core.v16.1, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aR NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089c _DevExpress.Xpf.Charts.v16.1, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49ac	 _DevExpress.Xpf.Ribbon.v16.1, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49af
 bDevExpress.Xpf.Grid.v16.1.Core, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49al hDevExpress.Xpf.Grid.v16.1.Extensions, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aa ]DevExpress.Xpf.Grid.v16.1, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a] YDevExpress.Mvvm.v16.1, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aH    BBluePrints.Common.ViewModel.Converters.QuantityExpressionConverter   QuantityExpressionConverter8  2DevExpress.Xpf.Core.EmbeddedResourceImageConverter " EmbeddedResourceImageConverterP   JBluePrints.Common.ViewModel.Converters.NumericCompareToVisibilityConverter ' #NumericCompareToVisibilityConverterM   GBluePrints.Common.ViewModel.Converters.NumericCompareToBooleanConverter $  NumericCompareToBooleanConverter1  +DevExpress.Mvvm.UI.BooleanNegationConverter  BooleanNegationConverterL   FBluePrints.Common.ViewModel.Converters.VariationActionToImageConverter # VariationActionToImageConverterM   GBluePrints.Common.ViewModel.Converters.VariationActionToStringConverter $  VariationActionToStringConverter1  +DevExpress.Mvvm.UI.StringToBooleanConverter  StringToBoolConverter ! GridCellVariationCancellation  �� 
TargetType/  )DevExpress.Xpf.Grid.LightweightCellEditor �� BasedOn9	  3DevExpress.Xpf.Grid.Themes.GridRowThemeKeyExtension�
  �DevExpress.Xpf.Utils.Themes.ThemeKeyExtensionInternalBase`1[[DevExpress.Xpf.Grid.Themes.GridRowThemeKeys, DevExpress.Xpf.Grid.v16.1, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a]] 
  ResourceKey �� Property �� Value �� Path 	 Button_BASELINE_ASSIGN �� ElementName �� 	Converter �� ConverterParameter	 �� FallbackValue
 �� Orientation4  .DevExpress.Xpf.Core.DXImageOffice2013Extension   Image;  5DevExpress.Xpf.Core.Native.DXImageOffice2013Converter 
 Button_MODBASELINE_ASSIGN  DefaultLineSeries2DTemplate(  "DevExpress.Xpf.Charts.LineSeries2D&   DevExpress.Xpf.Charts.XYSeries2D"  DevExpress.Xpf.Charts.Series%  DevExpress.Xpf.Charts.LineStyle   	Thickness ( $DefaultBarSideBySideSeries2DTemplate1  +DevExpress.Xpf.Charts.BarSideBySideSeries2D'  !DevExpress.Xpf.Charts.XYDiagram2D  DefaultTableViewTemplate#  DevExpress.Xpf.Grid.TableView2  ,DevExpress.Mvvm.UI.Interactivity.Interaction'  !DevExpress.Mvvm.UI.EventToCommand   PassEventArgsToCommand�  �DevExpress.Mvvm.UI.Interactivity.EventTriggerBase`1[[System.Windows.DependencyObject, WindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35]]   	EventName+  %DevExpress.Mvvm.UI.EventToCommandBase   Command%  DevExpress.Mvvm.UI.KeyToCommand   
KeyGesture   CommandParameter& 
  DevExpress.Xpf.Grid.DataViewBase&   DevExpress.Xpf.Grid.GridViewBase  DefaultGridTemplate%  DevExpress.Xpf.Grid.GridControl) 
 #DevExpress.Xpf.Grid.DataControlBase �� Mode  DefaultTreeTemplate)  #DevExpress.Xpf.Grid.TreeListControl"  System.Collections.ArrayList  PROJECTcolumns$  DevExpress.Xpf.Grid.GridColumn$  
 DevExpress.Xpf.Grid.BaseColumn    Header)!  #DevExpress.Xpf.Core.ObjectConverter$" 
 DevExpress.Xpf.Grid.ColumnBase "  	FieldName "  EditSettings:#  4DevExpress.Xpf.Editors.Settings.ComboBoxEditSettings8$  2DevExpress.Xpf.Editors.Settings.ButtonEditSettings $  IsTextEditable<%  6DevExpress.Xpf.Editors.Settings.LookUpEditSettingsBase %  AutoComplete %  ItemsSource,&  &DevExpress.Xpf.Editors.EnumItemsSource &  EnumType%'   BluePrints.Common.ProjectStatus$(   BluePrints.Common.ContractType#)   BluePrints.Common.ProjectType%*   BluePrints.Common.DesignManager7+  1DevExpress.Xpf.Editors.Settings.CheckEditSettings6,  0DevExpress.Xpf.Editors.Settings.SpinEditSettings , Name ,  MinValue6-  0DevExpress.Xpf.Editors.Settings.TextEditSettings -  MaskType -  Mask -  MaskUseAsDisplayFormat ,  	Increment  ,  MaxValue! "  CellTemplate%.  DevExpress.Xpf.Editors.SpinEdit" . Name%/  DevExpress.Xpf.Editors.TextEdit# /  MaskUseAsDisplayFormat$ /  MaskType% .  	Increment& .  MinValue' .  MaxValue( /  Mask) �� RelativeSource  AREAColumns  DEPARTMENTColumns  DISCIPLINEColumns  DOCTYPEColumns* %  DisplayMember+ %  ValueMember  PHASEColumns  RATEColumns, � DataContext  
UOMColumns  BASELINEColumns&0    BluePrints.Common.BaselineStatus- "  UnboundType] YDevExpress.Data.v16.1, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a81  2DevExpress.Utils.Design.UnboundColumnTypeConverter. "  AllowEditing52  /DevExpress.Utils.Design.DefaultBooleanConverter/ "  DisplayTemplate0 "  EditTemplate  BASELINEITEMColumns'3   !BluePrints.Common.DeliverableType1 "  ReadOnly2 "  AllowSearchPanel ! BASELINEITEMSJoinRATESColumns  WORKPACKColumns64  0DevExpress.Xpf.Editors.Settings.DateEditSettings3 4  MinValue4 4  MaxValue  PROGRESSColumns,5   &BluePrints.Common.ProgressIntervalType&6    BluePrints.Common.ProgressStatus  PROGRESSITEMColumns37  -DevExpress.Xpf.Core.DXFrameworkContentElement5 7  	IsEnabled)8  #DevExpress.Xpf.Editors.ComboBoxEdit'9  !DevExpress.Xpf.Editors.ButtonEdit6 9  IsTextEditable+:  %DevExpress.Xpf.Editors.LookUpEditBase7 :  ValueMember8 :  ItemsSource9 :  DisplayMember: � ColumnDefinitions; � ToolTip< �� Source= `� 	Converter> `� ConverterParameter  VARIATIONColumns  VARIATIONITEMColumns%;  DevExpress.Xpf.Editors.BaseEdit? ;  
IsReadOnly@ 7  	AllowDrop'<   !BluePrints.Common.VariationActionA "  	CellStyle   USERColumns�� 5      H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      :x,http://schemas.microsoft.com/winfx/2006/xaml     5      ;dx2http://schemas.devexpress.com/winfx/2008/xaml/core  5      <dxb2http://schemas.devexpress.com/winfx/2008/xaml/bars  5      >dxc4http://schemas.devexpress.com/winfx/2008/xaml/charts  5      >dxr4http://schemas.devexpress.com/winfx/2008/xaml/ribbon 	 5      @dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid 
   5      Gdxgt<http://schemas.devexpress.com/winfx/2008/xaml/grid/themekeys  5      ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  5	      Adxmvvm2http://schemas.devexpress.com/winfx/2008/xaml/mvvm   5
      "localclr-namespace:BluePrints  5      1	viewmodel#clr-namespace:BluePrints.ViewModels  5      B	converter4clr-namespace:BluePrints.Common.ViewModel.Converters  5      *commonclr-namespace:BluePrints.Common  5      .sys&clr-namespace:System;assembly=mscorlib  5      Bcollections2clr-namespace:System.Collections;assembly=mscorlib  5      (Viewsclr-namespace:BluePrints.Views  6\   %e  &	        &	      &	 &     &	 9     &	 L     &	 _     &	 r     &	 �     &	 �     0�� 5   Y   *	  $ LightweightCellStyle=�+1&		     7  &	
 �    7  &	 n    &	 Q    &	 �    &	 �    &	 �    &	 �    7   &	 �    &	     &	 i    &	 �    &	 �    &	 V    &	 �    &	 3    7 	 7 	 7 
 7 
 &	 �    &	 �(    &	 ;3    &	 �;    &	 d>    7  7  7  7  &	 �L    &	 QO    7  7  7  7  7  7  7  7  7  7  7  &	  �a       5      6,     5      6(     5      64     5      61     5      6&     5      60     5      61     5      6&   �� 5      "   62    6Y   2  .�5   
   ��  � {� 6&   �� $' !RowData.Row.CanToggleCancellation�5      �� 5      .R�5   
   �� ��6   �� *><DataContext.PROJECTBASELINESDetails.P6BASELINE_ASSIGNCommand+$ userControl��{�6{   �� $! RowData.Row.P6BASELINE_NAME�8   $ false��$	 false��.��5      �� $
 
Horizontal=�6   .+�5       �� $��16��6M   $��16��6X   $��
0, 0, 3, 0q�6d   ��6     $ Edit_16x16.png 6d   �� 5!      $
��Edit��6   $��Center=�6(   5"      5#      5$      �� 5%      .R�5&   
   �� ��6   �� *A?DataContext.PROJECTBASELINESDetails.P6MODBASELINE_ASSIGNCommand+$ userControl��{�6~   �� $$ RowData.Row.P6MODBASELINE_NAME�8   $ false��$	 false��.��5'      �� $
 
Horizontal=�6   .+�5(      �� $��16��6M   $��16��6X   $��
0, 0, 3, 0q�6d   ��6     $ Edit_16x16.png 6d   �� 5)      $
��Edit��6   $��Center=�6(   5*      5+      5,      �� 5/      "   60   .�50   
   ��  �@ CrosshairLabelPattern$ {S} - [{V:p2}]��62   �� 51   
    �@ MarkerVisible .  6*   �� 52   
    �@ ArgumentDataMember$ ProgressDate��6/   �� 53   
    �@ ArgumentScaleType$ DateTime=�6.   �� 54   
    �@ ValueDataMember$ UnitsPercentage��6,   �� 55   
    �@ ValueScaleType$ 	Numerical=�6+   �� 56   
    �@ 	LineStyle 57        58      $ 1��6    5:      5;      �� 5=      "   69   .�5>   
   ��  �@ CrosshairLabelPattern$ {S} - [{V:n2}]��62   �� 5?   
    �@ ArgumentDataMember$ ProgressDate��6/   �� 5@   
    �@ ArgumentScaleType$ DateTime=�6.   �� 5A   
    �@ ValueDataMember$ Units��6,   �� 5B   
    �@ ValueScaleType$ 	Numerical=�6+   �� 5C   
    �@ SeriesAxisY 68   �� $ secondaryAxisY��5D      �� 5E      "   6-   .�5F   
   ��  �@ BehaviorsTemplate 5G      �� 5H      .R�5I      �� .@�5J        $
 TrueX�60   $ CellValueChanged��6�    6N   �� *" ExistingRowAddUndoAndSaveCommand+6�     5K      $
 TrueX�60   $ 
RowUpdated��6~    6N   �� *NewRowAddUndoAndSaveCommand+6~     5L      $
 TrueX�60   $ ValidateCell��6v    6N   �� *ValidateCellCommand+6v     5M      $
 TrueX�60   $ ValidateRow��6u    6N   �� *ValidateRowCommand+6u     5N      $
 TrueX�60   $ PastingFromClipboard��6~    6N   �� *PastingFromClipboardCommand+6~     5O      $ Delete��6.    6B   �� *BulkDeleteCommand+ 6h   �� *SelectedEntities+  5P      $ CTRL+Z��6.    6B   �� *UndoCommand+  5Q      $ CTRL+Y��6.    6B   �� *RedoCommand+5R      5S      5U      �� 5V   
    �@ ShowFixedTotalSummary . 62   �� 5W   
    �@ NewItemRowPosition$	 Top=�6/   �� 5X   
    �@ AllowPerPixelScrolling . 63   �� 5Y   
    �@ ShowGroupedColumns . 6/   �� 5Z   
    �@ ShowSearchPanelMode$ Always=�60   �� 5[   
    �@ SearchDelay$ 0��6(   �� 5\   
    �@ AllowScrollAnimation . 61   5]      �� 5^      "   6(   .�5_   
   ��  �@ ItemsSource 6(   �� *
Entities+�� 5`   
    �@ ClipboardCopyMode$ ExcludeHeader=�6.   �� 5a   
    �@ SelectionMode$	 Row=�6*   �� 5b   
    �@ CurrentItem 6(   �� *SelectedEntity+�� 5c   
    �@ SelectedItems 6*   �� *SelectedEntities+$ TwoWay=��� 5d   
    �@ 
ShowBorder .  6'   �� 5e   
    �@ ShowLoadingPanel 6-   �� *	isLoading+5s      �� 5t      "   6(   .�5u   
   ��  �@ ItemsSource 6(   �� *
Entities+�� 5v   
    �@ ClipboardCopyMode$ ExcludeHeader=�6.   �� 5w   
    �@ SelectionMode$	 Row=�6*   �� 5x   
    �@ CurrentItem 6(   �� *SelectedEntity+�� 5y   
    �@ SelectedItems 6*   �� *SelectedEntities+$ TwoWay=��� 5z   
    �@ 
ShowBorder .  6'   �� 5{   
    �@ ShowLoadingPanel 6-   �� *	isLoading+5�        5�        5�   
   $ Number! 6   $ NUMBER��6)     5�   
   $
 Name! 6   $
 NAME��6'     5�   
   $ Client! 6   $ CLIENT��6)     5�   
   $ Status! 6   $ STATUS��6)    5�      #  5�      $
 TrueX�6+    .  6U   &  " ' 5�        5�   
   $ Contract Type! 6   $ CONTRACTTYPE��60    5�      #  5�      $
 TrueX�6+    .  6U   &  " ( 5�        5�   
   $
 Type! 6   $
 TYPE��6'    5�      #  5�      $
 TrueX�6+    .  6U   &  " ) 5�        5�   
   $ Design Manager! 6   $ DESIGNMANAGER��61    5�      #  5�      $
 TrueX�6+    .  6U   &  " * 5�        5�   
   $ Legacy Workpack! 6   $ USELEGACYWORKPACK��62    5�      +  5�      5�        5�   
   $ Currency Conversion! 6   $ CURRENCYCONVERSION��66    5�      ,  5�      -   5        $ PART_Editor��5�   6   $	 0.1X�6'   $ Numeric=�6K   $ n2��6^    . $
 0.01~�6�   5�        5�   
   $ Review %! 6   $ REVIEWPERCENTAGE��6+    5�      ,  5�      $ Numeric=�6'   $ p2��6:   $ 0X�6D   $	 0.1~�6Q   $  1X�6a    . 6n   5�        5�   
   $ Review Period! 6   $ REVIEWPERIOD��60   ! 5�      �� 5�      .R�5�      .  $" PART_Editor��5�      # . $$ Numeric=�5�      $% 1~�5�      $& 0X�5�      $' 99X�5�      ( 5�      �� *	EditValue+$	 Day��8   ) �� *Self+5�      5�      5�        5�        5�   
   $ INTERNAL_NUM��6     5�   
   $ 
CLIENT_NUM��6     5�   
   $ TITLE��6   5�        5�        5�   
   $
 CODE��6     5�   
   $
 NAME��6   5�        5�        5�   
   $
 CODE��6     5�   
   $
 NAME��6   5�        5�        5�   
   $ 
DEPARTMENT! 6   $ GUID_DDEPARTMENT��6-    5�      #  5�      $ FalseX�6^   $
* NAME��6u   $
+ GUID��6�    6+   �� *LookUpDEPARTMENTS.Entities+6�   5�        5�   
   $
 CODE��6     5�   
   $
 NAME��6   5�        5�        5�   
   $ INTERNAL_NUM��6     5�   
   $ 
CLIENT_NUM��6     5�   
   $ TITLE��6   5�        5�        5�   
   $ 
DEPARTMENT! 6   $ GUID_DEPARTMENT��6-   , 6I   �� *DataContext+$ userControl�� 5�      #  5�      $ FalseX�6^   $
* NAME��6u   $
+ GUID��6�    6+   �� *LookUpDEPARTMENTS.Entities+6�   5�        5�   
   $ 
DISCIPLINE! 6   $ GUID_DISCIPLINE��6-   , 6I   �� *DataContext+$ userControl�� 5�      #  5�      $ FalseX�6^   $
* NAME��6u   $
+ GUID��6�    6+   �� *LookUpDISCIPLINES.Entities+6�   5�        5�   
   $ RATE1��6   $
 RATE! 6+    5�      ,  5�      $ 0X�6'   $ Numeric=�64   $ n2��6G    . $ 10~�6o   5�      5�        5�        5�   
   $
 UOM1��6   $	 UOM! 6*   5�        5�        5�   
   $
 NAME��6     5�   
   $ REVISION��6     5�   
   $ COMMENTS��6     5�   
   $ ACTUAL_UNITS��6    5�      ,  5�      $ Numeric=�6'   $ n��6:   $ 0X�6C   $ 10~�6P    . 6_   5�        5�   
   $ BUDGETED_UNITS��6    5�      ,  5�      $ Numeric=�6'   $ n��6:   $ 0X�6C   $ 10~�6P    . 6_   5       5  
   $ ALLOW_EXCEED��6    5     +  5     5       5  
   $ STATUS��6    5     #  5	     $
 TrueX�6+    .  6U   &  " 0 5       5  
   $ P6BASELINE_NAME��6     5  
   $ UNBOUND_ORIGINALBASELINE��6   $ ORIGINAL BASELINE! 6>   $- Boolean1 6Y   $. False2 6o   8/   80  6�     5  
   $ P6MODBASELINE_NAME��6     5  
   $ UNBOUND_MODBASELINE��6   $ MODIFIED BASELINE! 69   $- Boolean1 6T   $. False2 6j   8/  80  6�   5       5       5  
   $! BASELINE_ITEM.GUID_WORKPACK��6   $ WORKPACK! 6A   , 6S   �� *DataContext+$ userControl�� 5     #  5     $ FalseX�6\   $
+ GUID��6�    6+   �� *LookUpWORKPACKS.Entities+* 6s   �� *WORKPACKDisplayMember+$ OneWay=�6�   5       5  
   $ 
GUID_PHASE��6   $ PHASE! 60   , 6?   �� *DataContext+$ userControl�� 5     #  5     $ FalseX�6Y   $* INTERNAL_NUM��6p   $
+ GUID��6�    6+   �� *LookUpPHASES.Entities+6�   5       5  
   $ 	GUID_AREA��6   $
 AREA! 6/   , 6=   �� *DataContext+$ userControl�� 5     #  5     $ FalseX�6X   $* INTERNAL_NUM��6o   $
+ GUID��6�    6+   �� *LookUpAREAS.Entities+6�   5        5!  
   $ GUID_DOCTYPE��6   $ DOCTYPE! 62   , 6C   �� *DataContext+$ userControl�� 5"     #  5#     $ FalseX�6[   $
* NAME��6r   $
+ GUID��6�    6+   �� *LookUpDOCTYPES.Entities+6�   5%       5&  
   $ GUID_DISCIPLINE��6   $ 
DISCIPLINE! 65   , 6I   �� *DataContext+$ userControl�� 5'     #  5(     $ FalseX�6^   $
* NAME��6u   $
+ GUID��6�    6+   �� *LookUpDISCIPLINES.Entities+6�   5*       5+  
   $ GUID_DEPARTMENT��6   $ 
DEPARTMENT! 65   , 6I   �� *DataContext+$ userControl�� 5,     #  5-     $ FalseX�6^   $
* NAME��6u   $
+ GUID��6�    6+   �� *LookUpDEPARTMENTS.Entities+6�   5/       50  
   $ INTERNAL_NUM��6     51  
   $ 
CLIENT_NUM��6     52  
   $ REVISION_NUMBER��6     53  
   $ DELIVERABLE_TYPE��6    54     #  55     $
 TrueX�6+    .  6U   &  " 3 57       58  
   $ PRIMARY_TITLE��6     59  
   $ SECONDARY_TITLE��6     5:  
   $ COMMENTS��6     5;  
   $ ESTIMATED_HOURS��6    5<     ,  5=     $ Numeric=�6'   $ n��6:   $ 0X�6C   $ 1~�6P    . 6^   5?       5@  
   $ DC_HOURS��6   1 . 6.     5A  
   $ TOTAL_COSTS��6   $ COSTS! 61   $- Decimal1 6@   $2 False2 6V   ! 5B     �� 5C     .R�5D     /  $��PART_Editor��6#   # . $$ Numeric=�6T   $( c��6h   5E     5G     5H       5I       5J  
   $! BASELINE_ITEM.GUID_WORKPACK��6   $ WORKPACK! 6A   , 6S   �� *DataContext+$ userControl�� 5K     #  5L     $ FalseX�6\   $
+ GUID��6�    6+   �� *LookUpWORKPACKS.Entities+* 6s   �� *WORKPACKDisplayMember+$ OneWay=�6�   5N       5O  
   $ BASELINE_ITEM.GUID_PHASE��6   $ PHASE! 6>   , 6M   �� *DataContext+$ userControl�� 5P     #  5Q     $ FalseX�6Y   $* INTERNAL_NUM��6p   $
+ GUID��6�    6+   �� *LookUpPHASES.Entities+6�   5S       5T  
   $ BASELINE_ITEM.GUID_AREA��6   $
 AREA! 6=   , 6K   �� *DataContext+$ userControl�� 5U     #  5V     $ FalseX�6X   $* INTERNAL_NUM��6o   $
+ GUID��6�    6+   �� *LookUpAREAS.Entities+6�   5X       5Y  
   $  BASELINE_ITEM.GUID_DOCTYPE��6   $ DOCTYPE! 6@   , 6Q   �� *DataContext+$ userControl�� 5Z     #  5[     $ FalseX�6[   $
* NAME��6r   $
+ GUID��6�    6+   �� *LookUpDOCTYPES.Entities+6�   5]       5^  
   $# BASELINE_ITEM.GUID_DISCIPLINE��6   $ 
DISCIPLINE! 6C   , 6W   �� *DataContext+$ userControl�� 5_     #  5`     $ FalseX�6^   $
* NAME��6u   $
+ GUID��6�    6+   �� *LookUpDISCIPLINES.Entities+6�   5b       5c  
   $# BASELINE_ITEM.GUID_DEPARTMENT��6   $ 
DEPARTMENT! 6C   , 6W   �� *DataContext+$ userControl�� 5d     #  5e     $ FalseX�6^   $
* NAME��6u   $
+ GUID��6�    6+   �� *LookUpDEPARTMENTS.Entities+6�   5g       5h  
   $  BASELINE_ITEM.INTERNAL_NUM��6   $ INTERNAL_NUM! 6@     5i  
   $ BASELINE_ITEM.CLIENT_NUM��6   $ 
CLIENT_NUM! 6>     5j  
   $# BASELINE_ITEM.REVISION_NUMBER��6   $ REVISION_NUMBER! 6C     5k  
   $$ BASELINE_ITEM.DELIVERABLE_TYPE��6   $ DELIVERABLE_TYPE! 6D    5l     #  5m     $
 TrueX�6+    .  6U   &  " 3 5o       5p  
   $! BASELINE_ITEM.PRIMARY_TITLE��6   $ PRIMARY_TITLE! 6A     5q  
   $# BASELINE_ITEM.SECONDARY_TITLE��6   $ SECONDARY_TITLE! 6C     5r  
   $ BASELINE_ITEM.COMMENTS��6   $ COMMENTS! 6<     5s  
   $# BASELINE_ITEM.ESTIMATED_HOURS��6   $ ESTIMATED_HOURS! 6C    5t     ,  5u     $ Numeric=�6'   $ n��6:   $ 0X�6C   $ 1~�6P    . 6^   5w       5x  
   $ BASELINE_ITEM.DC_HOURS��6   1 . $ DC_HOURS! 6L     5y  
   $ TOTAL_COSTS��6   $ COSTS! 61   $- Decimal1 6@   $2 False2 6V   ! 5z     �� 5{     .R�5|     /  $��PART_Editor��6#   # . $$ Numeric=�6T   $( c��6h   5}     5     5�       5�       5�  
   $ PHASE! 6   $ GUID_DPHASE��6(   , 6@   �� *DataContext+$ userControl�� 5�     #  5�     $ FalseX�6Y   $* INTERNAL_NUM��6p   $
+ GUID��6�    6+   �� *LookUpPHASES.Entities+6�   5�       5�  
   $
 AREA! 6   $ 
GUID_DAREA��6'   , 6>   �� *DataContext+$ userControl�� 5�     #  5�     $ FalseX�6X   $* INTERNAL_NUM��6o   $
+ GUID��6�    6+   �� *LookUpAREAS.Entities+6�   5�       5�  
   $ DOCTYPE! 6   $ GUID_DDOCTYPE��6*   , 6D   �� *DataContext+$ userControl�� 5�     #  5�     $ FalseX�6[   $
* NAME��6r   $
+ GUID��6�    6+   �� *LookUpDOCTYPES.Entities+6�   5�       5�  
   $ 
DISCIPLINE! 6   $ GUID_DDISCIPLINE��6-   , 6J   �� *DataContext+$ userControl�� 5�     #  5�     $ FalseX�6^   $
* NAME��6u   $
+ GUID��6�    6+   �� *LookUpDISCIPLINES.Entities+6�   5�       5�  
   $ 
DEPARTMENT! 6   $ GUID_DDEPARTMENT��6-   , 6J   �� *DataContext+$ userControl�� 5�     #  5�     $ FalseX�6^   $
* NAME��6u   $
+ GUID��6�    6+   �� *LookUpDEPARTMENTS.Entities+6�   5�       5�  
   $ INTERNAL_NAME1��6     5�  
   $ INTERNAL_NAME2��6     5�  
   $ 	STARTDATE��6    5�     4  5�     $3 
1753-01-01X�6'   $4 12/31/9999 23:59:59X�6=   5�       5�  
   $ ENDDATE��6    5�     4  5�     $3 
1753-01-01X�6'   $4 12/31/9999 23:59:59X�6=   5�       5�  
   $ REVIEWSTARTDATE��6    5�     4  5�     $3 
1753-01-01X�6'   $4 12/31/9999 23:59:59X�6=   5�       5�  
   $ REVIEWENDDATE��6    5�     4  5�     $3 
1753-01-01X�6'   $4 12/31/9999 23:59:59X�6=   5�       5�  
   $ FORECASTSTARTDATE��6    5�     4  5�     $3 
1753-01-01X�6'   $4 12/31/9999 23:59:59X�6=   5�       5�  
   $ FORECASTENDDATE��6    5�     4  5�     $3 
1753-01-01X�6'   $4 12/31/9999 23:59:59X�6=   5�       5�  
   $ AUTOGENERATED��6   1 . 63   5�       5�       5�  
   $
 NAME��6     5�  
   $ PROGRESS_START��6    5�     4  5�     $3 
1753-01-01X�6'   $4 12/31/9999 23:59:59X�6=   5�       5�  
   $ 	DATA_DATE��6    5�     4  5�     $3 
1753-01-01X�6'   $4 12/31/9999 23:59:59X�6=   5�       5�  
   $ INTERVAL_COUNT��6    5�     ,  5�     $ Numeric=�6'   $ n0��6:   $ 0X�6D   $ 1~�6Q   $  99X�6_    . 6m   5�       5�  
   $ INTERVAL_TYPE��6    5�     #  5�     $
 TrueX�6+    .  6U   &  " 5 5�       5�  
   $ STATUS��6    5�     #  5�     $
 TrueX�6+    .  6U   &  " 6 5�       5�  
   $ COMMENTS��6   5�       5�       5�  
   $! BASELINE_ITEM.GUID_WORKPACK��6   $ WORKPACK! 6A   1 . 5 .  ! 5�     �� 5�     .R�5�     8  $��PART_Editor��6'   $6 FalseX�6�   $
7 GUID��6	  8 6:   �� *&$DataContext.LookUpWORKPACKS.Entities+$ userControl��9 6�   �� *#!DataContext.WORKPACKDisplayMember+$ OneWay=�$ userControl��6	  5�     5�       5�  
   $&  BASELINE_ITEM.PHASE.INTERNAL_NUM��6   $ PHASE! 6F   1 . 5 .  6e     5�  
   $% BASELINE_ITEM.AREA.INTERNAL_NUM��6   $
 AREA! 6E   1 . 5 .  6c     5�  
   $  BASELINE_ITEM.DOCTYPE.NAME��6   $ DOCTYPE! 6@   1 . 5 .  6a     5�  
   $# BASELINE_ITEM.DISCIPLINE.NAME��6   $ 
DISCIPLINE! 6C   1 . 5 .  6g     5�  
   $# BASELINE_ITEM.DEPARTMENT.NAME��6   $ 
DEPARTMENT! 6C   1 . 5 .  6g     5�  
   $  BASELINE_ITEM.INTERNAL_NUM��6   $ INTERNAL NUM! 6@   1 . 5 .  6f     5�  
   $ BASELINE_ITEM.CLIENT_NUM��6   $ 
CLIENT NUM! 6>   1 . 5 .  6b     5�  
   $# BASELINE_ITEM.REVISION_NUMBER��6   $ REVISION NUM! 6C   1 . 5 .  6i     5�  
   $$ BASELINE_ITEM.DELIVERABLE_TYPE��6   $
 TYPE! 6D   1 . 5 .  6b     5�  
   $! BASELINE_ITEM.PRIMARY_TITLE��6   $ PRIMARY TITLE! 6A   1 . 5 .  6h     5�  
   $# BASELINE_ITEM.SECONDARY_TITLE��6   $ SECONDARY TITLE! 6C   1 . 5 .  6l     5�  
   $ BASELINE_ITEM.COMMENTS��6   $ COMMENTS! 6<     5�  
   $ CurrentPercentage��6   $ 	CURRENT %! 67   $
. True2 6J   ! 5�     �� 5�     .R�5�     � : 5�     �� 5�     $��3 �6/   �� 5�     $
��Auto �6/   �� 5�     $��* �6/   .+�5�     �� $; No progress��6�   $��1��6�   ��6    �� $1< +DevExpress.Xpf.Core.Editors.Images.info.png��8   y�5�     `� 5�  "   8=  $	 Hidden��6p   > 5�  &   ��5�  *   	False��6=   .1�5�  &   �� $ PART_Editor��6.   $ MinValue�6H   �� 5�  &   $ PART_Editor��6.   $ 	EditValue�6H   5   #   5     �� 5     $; Max progress��6�   $��1��6�   ��6    �� $1< +DevExpress.Xpf.Core.Editors.Images.info.png��8  y�5     `� 5  "   8=  $	 Hidden��6p   > 5  &   ��5  *   	False��6=   .1�5	  &   �� $ PART_Editor��6.   $ MaxValue�6H   �� 5
  &   $ PART_Editor��6.   $ 	EditValue�6H   5  #   5     .  5     $��PART_Editor��6'   $$ Numeric=�6:   $( p2��6M   # . $
% 0.01~�6u   $��2��6�   ' 6�   �� *RowData.Row.MaxPercentage+& 6�   �� *RowData.Row.MinPercentage+6�   5     5     5       5  
   $# BASELINE_ITEM.ESTIMATED_HOURS��6   $ ESTIMATED HOURS! 6C   1 . 5 .   5     ,  5     $ Numeric=�6'   $ n��6:   $ 0X�6C   $ 1~�6P    . 6^   5       5  
   $ BASELINE_ITEM.DC_HOURS��6   $ DC HOURS! 6<   1 . 5 .   5     ,  5     $ Numeric=�6'   $ n��6:   $ 0X�6C   $ 1~�6P    . 6^   5       5  
   $ BASELINE_ITEM.TOTAL_COSTS��6   $ TOTAL COSTS! 6?   1 . 5 .  ! 5     �� 5     .R�5      /  $��PART_Editor��6#   # . $$ Numeric=�6T   $( c��6g   5!     5#       5$  
   $ CURRENT_EARNED_COSTS��6   $ CURRENT EARNED COSTS! 6:   1 . 5 .  ! 5%     �� 5&     .R�5'     /  $��PART_Editor��6#   # . $$ Numeric=�6T   $( c��6g   5(     5*       5+  
   $ TOTAL_EARNED_COSTS��6   $ TOTAL COSTS! 68   1 . 5 .  ! 5,     �� 5-     .R�5.     /  $��PART_Editor��6#   # . $$ Numeric=�6T   $( c��6g   5/     51     52       53       54  
   $
 NAME��6     55  
   $ COMMENTS��6     56  
   $ 	SUBMITTED��6     57  
   $ APPROVED��6     58  
   $ FROM BASELINE! 6   $ GUID_ORIBASELINE��60   1 . $. False2 6]    59     #  5:     $ FalseX�6�   $* REVISION��6�   $
+ GUID��6�    6+   �� *&$DataContext.LookUpBASELINES.Entities+$ userControl��6�   5<       5=  
   $ TO BASELINE! 6   $ GUID_BASELINE��6.   1 . $. False2 6X    5>     #  5?     $ FalseX�6�   $* REVISION��6�   $
+ GUID��6�    6+   �� *&$DataContext.LookUpBASELINES.Entities+$ userControl��6�   5A     5B       5C       5D  
   $! BASELINE_ITEM.GUID_WORKPACK��6   $ WORKPACK! 6A   ! 5E     �� 5F     .R�5G     8  $��PART_Editor��6'   $6 FalseX�6�   $
7 GUID��6	  8 6:   �� *&$DataContext.LookUpWORKPACKS.Entities+$ userControl��9 6�   �� *#!DataContext.WORKPACKDisplayMember+$ OneWay=�$ userControl��{�6  �� *RowData.Row.IsReadOnly+8   5H     5J       5K  
   $ BASELINE_ITEM.GUID_PHASE��6   $ PHASE! 6>   ! 5L     �� 5M     .R�5N     8  $��PART_Editor��6'   $6 FalseX�6�   $9 INTERNAL_NUM��6�   $
7 GUID��6�   8 6:   �� *#!DataContext.LookUpPHASES.Entities+$ userControl��{�6�   �� *RowData.Row.IsReadOnly+8  5O     5Q       5R  
   $ BASELINE_ITEM.GUID_AREA��6   $
 AREA! 6=   ! 5S     �� 5T     .R�5U     8  $��PART_Editor��6'   $6 FalseX�6�   $9 INTERNAL_NUM��6�   $
7 GUID��6�   8 6:   �� *" DataContext.LookUpAREAS.Entities+$ userControl��{�6�   �� *RowData.Row.IsReadOnly+8  5V     5X       5Y  
   $  BASELINE_ITEM.GUID_DOCTYPE��6   $ DOCTYPE! 6@   ! 5Z     �� 5[     .R�5\     8  $��PART_Editor��6'   $6 FalseX�6�   $
9 NAME��6�   $
7 GUID��6�   8 6:   �� *%#DataContext.LookUpDOCTYPES.Entities+$ userControl��{�6�   �� *RowData.Row.IsReadOnly+8  5]     5_       5`  
   $# BASELINE_ITEM.GUID_DISCIPLINE��6   $ 
DISCIPLINE! 6C   ! 5a     �� 5b     .R�5c     8  $��PART_Editor��6'   $6 FalseX�6�   $
9 NAME��6�   $
7 GUID��6�   8 6:   �� *(&DataContext.LookUpDISCIPLINES.Entities+$ userControl��{�6�   �� *RowData.Row.IsReadOnly+8  5d     5f       5g  
   $# BASELINE_ITEM.GUID_DEPARTMENT��6   $ 
DEPARTMENT! 6C   ! 5h     �� 5i     .R�5j     8  $��PART_Editor��6'   $6 FalseX�6�   $
9 NAME��6�   $
7 GUID��6�   8 6:   �� *(&DataContext.LookUpDEPARTMENTS.Entities+$ userControl��{�6�   �� *RowData.Row.IsReadOnly+8  5k     5m       5n  
   $  BASELINE_ITEM.INTERNAL_NUM��6   $ INTERNAL NUM! 6@   ! 5o     �� 5p     .R�5q     /  $��PART_Editor��6#   ? 66   �� *RowData.Row.IsReadOnly+5r     5t       5u  
   $ BASELINE_ITEM.CLIENT_NUM��6   $ 
CLIENT NUM! 6>   ! 5v     �� 5w     .R�5x     /  $��PART_Editor��6#   ? 66   �� *RowData.Row.IsReadOnly+5y     5{       5|  
   $# BASELINE_ITEM.REVISION_NUMBER��6   $ REVISION NUM! 6C   ! 5}     �� 5~     .R�5     /  $��PART_Editor��6#   ? 66   �� *RowData.Row.IsReadOnly+5�     5�       5�  
   $$ BASELINE_ITEM.DELIVERABLE_TYPE��6   $
 TYPE! 6D    5�     #  5�     $
 TrueX�6+    .  6U   &  " 3 5 6�   �� *RowData.Row.IsReadOnly+8  5�       5�  
   $! BASELINE_ITEM.PRIMARY_TITLE��6   $ PRIMARY TITLE! 6A   ! 5�     �� 5�     .R�5�     /  $��PART_Editor��6#   ? 66   �� *RowData.Row.IsReadOnly+5�     5�       5�  
   $# BASELINE_ITEM.SECONDARY_TITLE��6   $ SECONDARY TITLE! 6C   ! 5�     �� 5�     .R�5�     /  $��PART_Editor��6#   ? 66   �� *RowData.Row.IsReadOnly+5�     5�       5�  
   $ BASELINE_ITEM.COMMENTS��6   $ COMMENTS! 6<   ! 5�     �� 5�     .R�5�     /  $��PART_Editor��6#   ? 66   �� *RowData.Row.IsReadOnly+5�     5�       5�  
   $# BASELINE_ITEM.ESTIMATED_HOURS��6   $ ESTIMATED HOURS! 6C   1 . 5 .   5�     ,  5�     $ Numeric=�6'   $ n��6:   $ 0X�6C   $ 1~�6P    . 6^   5�       5�  
   $ VARIATION_UNITS��6   $ DC HOURS! 65    5�     ,  5�     $ Numeric=�6'   $ n��6:   $ 0X�6C   $ 1~�6P    . 5 6|   �� *RowData.Row.IsReadOnly+8  5�       5�  
   $ UNBOUND_TOTAL_COSTS��6   $ TOTAL COSTS! 69   $- Decimal1 6N   1 . 5 .  ! 5�     �� 5�     .R�5�     /  $��PART_Editor��6#   # . $$ Numeric=�6T   $( c��6g   5�     5�       5�  
   $ ACTION��6   1 . 5 .   5�     #  5�     $
 TrueX�6+    . @ .   6U   &  " < 6�   5�       5�  
   $ UNBOUND_CANCEL��6   $ CANCEL! 64   $- Boolean1 6D   $. False2 6Z   8A  ! 5�     �� 5�     .R�5�     �� $��18��6   ��6)   �� *(&DataContext.CancelBASELINE_ITEMCommand+$ userControl����6}   �� *RowData.Row+.��5�     �� $
 
Horizontal=�6%   .+�5�     �� $��
0, 0, 3, 0q�6�   ��6$   �� $ RowData.Row.ACTION�8 	 6�   �� 5�     $��Center=�6�   ��6(   �� $ RowData.Row.ACTION�8 
 6�   5�     5�     5�     5�     5�       5�       5�  
   $
 NAME��6     5�  
   $ 
DEPARTMENT��6     5�  
   $ TITLE��6     5�  
   $ DESCRIPTION��6     5�  
   $
 ROLE! 6   $ 	GUID_ROLE��6'    5�     #  5�     $ FalseX�6}   $
* NAME��6�   $
+ GUID��6�    6+   �� *" DataContext.LookUpROLES.Entities+$ userControl��6�   5�     5�     5�     