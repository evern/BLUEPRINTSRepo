   M S B A M L   `   `   `  ���� E  ABluePrints, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:BluePrints
BluePrints  =#clr-namespace:BluePrints.ViewModelsBluePrints.ViewModels  _4clr-namespace:BluePrints.Common.ViewModel.Converters&BluePrints.Common.ViewModel.Converters  5clr-namespace:BluePrints.CommonBluePrints.Common  O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem I2clr-namespace:System.Collections;assembly=mscorlibSystem.Collections 3clr-namespace:BluePrints.ViewsBluePrints.Views  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35a ]DevExpress.Xpf.Core.v16.1, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aR NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089c _DevExpress.Xpf.Charts.v16.1, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49ac	 _DevExpress.Xpf.Ribbon.v16.1, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49af
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
IsReadOnly@ 7  	AllowDrop'<   !BluePrints.Common.VariationActionA "  	CellStyle   USERColumns�� H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      :x,http://schemas.microsoft.com/winfx/2006/xaml     ;dx2http://schemas.devexpress.com/winfx/2008/xaml/core  <dxb2http://schemas.devexpress.com/winfx/2008/xaml/bars  >dxc4http://schemas.devexpress.com/winfx/2008/xaml/charts  >dxr4http://schemas.devexpress.com/winfx/2008/xaml/ribbon 	 @dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid 
   Gdxgt<http://schemas.devexpress.com/winfx/2008/xaml/grid/themekeys  ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  Adxmvvm2http://schemas.devexpress.com/winfx/2008/xaml/mvvm   "localclr-namespace:BluePrints  1	viewmodel#clr-namespace:BluePrints.ViewModels  B	converter4clr-namespace:BluePrints.Common.ViewModel.Converters  *commonclr-namespace:BluePrints.Common  .sys&clr-namespace:System;assembly=mscorlib  Bcollections2clr-namespace:System.Collections;assembly=mscorlib  (Viewsclr-namespace:BluePrints.Views  %C  &	        &	      &	 
     &	      &	      &	      &	      &	 #     &	 (     0�� *	  $ LightweightCellStyle=�+1&		 z     7  &	
 �    7  &	 �    &	 /    &	 a    &	 �    &	 @
    &	 �    7   &	 !    &	 e    &	 �    &	 �    &	 T    &	 �    &	 E    &	 d    7 	 7 	 7 
 7 
 &	 �    &	 d    &	 �!    &	 '    &	 �(    7  7  7  7  &	 A2    &	 �3    7  7  7  7  7  7  7  7  7  7  7  &	  v@                     �� "    2  .���  � {� �� $' !RowData.Row.CanToggleCancellation��� .R��� ���� *><DataContext.PROJECTBASELINESDetails.P6BASELINE_ASSIGNCommand+$ userControl��{��� $! RowData.Row.P6BASELINE_NAME�8   $ false��$	 false��.���� $
 
Horizontal=�.+��� $��16��$��16��$��
0, 0, 3, 0q���  $ Edit_16x16.png �� $
��Edit��$��Center=��� .R��� ���� *A?DataContext.PROJECTBASELINESDetails.P6MODBASELINE_ASSIGNCommand+$ userControl��{��� $$ RowData.Row.P6MODBASELINE_NAME�8   $ false��$	 false��.���� $
 
Horizontal=�.+��� $��16��$��16��$��
0, 0, 3, 0q���  $ Edit_16x16.png �� $
��Edit��$��Center=��� "   .���  �@ CrosshairLabelPattern$ {S} - [{V:p2}]����  �@ MarkerVisible .  ��  �@ ArgumentDataMember$ ProgressDate����  �@ ArgumentScaleType$ DateTime=���  �@ ValueDataMember$ UnitsPercentage����  �@ ValueScaleType$ 	Numerical=���  �@ 	LineStyle   $ 1���� "   .���  �@ CrosshairLabelPattern$ {S} - [{V:n2}]����  �@ ArgumentDataMember$ ProgressDate����  �@ ArgumentScaleType$ DateTime=���  �@ ValueDataMember$ Units����  �@ ValueScaleType$ 	Numerical=���  �@ SeriesAxisY �� $ secondaryAxisY���� "   .���  �@ BehaviorsTemplate �� .R��� .@�  $
 TrueX�$ CellValueChanged�� �� *" ExistingRowAddUndoAndSaveCommand+  $
 TrueX�$ 
RowUpdated�� �� *NewRowAddUndoAndSaveCommand+  $
 TrueX�$ ValidateCell�� �� *ValidateCellCommand+  $
 TrueX�$ ValidateRow�� �� *ValidateRowCommand+  $
 TrueX�$ PastingFromClipboard�� �� *PastingFromClipboardCommand+  $ Delete�� �� *BulkDeleteCommand+ �� *SelectedEntities+  $ CTRL+Z�� �� *UndoCommand+  $ CTRL+Y�� �� *RedoCommand+��  �@ ShowFixedTotalSummary . ��  �@ NewItemRowPosition$	 Top=���  �@ AllowPerPixelScrolling . ��  �@ ShowGroupedColumns . ��  �@ ShowSearchPanelMode$ Always=���  �@ SearchDelay$ 0����  �@ AllowScrollAnimation . �� "   .���  �@ ItemsSource �� *
Entities+��  �@ ClipboardCopyMode$ ExcludeHeader=���  �@ SelectionMode$	 Row=���  �@ CurrentItem �� *SelectedEntity+��  �@ SelectedItems �� *SelectedEntities+$ TwoWay=���  �@ 
ShowBorder .  ��  �@ ShowLoadingPanel �� *	isLoading+�� "   .���  �@ ItemsSource �� *
Entities+��  �@ ClipboardCopyMode$ ExcludeHeader=���  �@ SelectionMode$	 Row=���  �@ CurrentItem �� *SelectedEntity+��  �@ SelectedItems �� *SelectedEntities+$ TwoWay=���  �@ 
ShowBorder .  ��  �@ ShowLoadingPanel �� *	isLoading+    $ Number! $ NUMBER��  $
 Name! $
 NAME��  $ Client! $ CLIENT��  $ Status! $ STATUS�� #  $
 TrueX� .  &  " '   $ Contract Type! $ CONTRACTTYPE�� #  $
 TrueX� .  &  " (   $
 Type! $
 TYPE�� #  $
 TrueX� .  &  " )   $ Design Manager! $ DESIGNMANAGER�� #  $
 TrueX� .  &  " *   $ Legacy Workpack! $ USELEGACYWORKPACK�� +    $ Currency Conversion! $ CURRENCYCONVERSION�� ,  -   $ PART_Editor��$	 0.1X�$ Numeric=�$ n2�� . $
 0.01~�  $ Review %! $ REVIEWPERCENTAGE�� ,  $ Numeric=�$ p2��$ 0X�$	 0.1~�$  1X� .   $ Review Period! $ REVIEWPERIOD��! �� .R�.  $" PART_Editor��# . $$ Numeric=�$% 1~�$& 0X�$' 99X�( �� *	EditValue+$	 Day��8   ) �� *Self+    $ INTERNAL_NUM��  $ 
CLIENT_NUM��  $ TITLE��    $
 CODE��  $
 NAME��    $
 CODE��  $
 NAME��    $ 
DEPARTMENT! $ GUID_DDEPARTMENT�� #  $ FalseX�$
* NAME��$
+ GUID�� �� *LookUpDEPARTMENTS.Entities+  $
 CODE��  $
 NAME��    $ INTERNAL_NUM��  $ 
CLIENT_NUM��  $ TITLE��    $ 
DEPARTMENT! $ GUID_DEPARTMENT��, �� *DataContext+$ userControl�� #  $ FalseX�$
* NAME��$
+ GUID�� �� *LookUpDEPARTMENTS.Entities+  $ 
DISCIPLINE! $ GUID_DISCIPLINE��, �� *DataContext+$ userControl�� #  $ FalseX�$
* NAME��$
+ GUID�� �� *LookUpDISCIPLINES.Entities+  $ RATE1��$
 RATE!  ,  $ 0X�$ Numeric=�$ n2�� . $ 10~�    $
 UOM1��$	 UOM!     $
 NAME��  $ REVISION��  $ COMMENTS��  $ ACTUAL_UNITS�� ,  $ Numeric=�$ n��$ 0X�$ 10~� .   $ BUDGETED_UNITS�� ,  $ Numeric=�$ n��$ 0X�$ 10~� .   $ ALLOW_EXCEED�� +    $ STATUS�� #  $
 TrueX� .  &  " 0   $ P6BASELINE_NAME��  $ UNBOUND_ORIGINALBASELINE��$ ORIGINAL BASELINE! $- Boolean1 $. False2 8/   80    $ P6MODBASELINE_NAME��  $ UNBOUND_MODBASELINE��$ MODIFIED BASELINE! $- Boolean1 $. False2 8/  80      $! BASELINE_ITEM.GUID_WORKPACK��$ WORKPACK! , �� *DataContext+$ userControl�� #  $ FalseX�$
+ GUID�� �� *LookUpWORKPACKS.Entities+* �� *WORKPACKDisplayMember+$ OneWay=�  $ 
GUID_PHASE��$ PHASE! , �� *DataContext+$ userControl�� #  $ FalseX�$* INTERNAL_NUM��$
+ GUID�� �� *LookUpPHASES.Entities+  $ 	GUID_AREA��$
 AREA! , �� *DataContext+$ userControl�� #  $ FalseX�$* INTERNAL_NUM��$
+ GUID�� �� *LookUpAREAS.Entities+  $ GUID_DOCTYPE��$ DOCTYPE! , �� *DataContext+$ userControl�� #  $ FalseX�$
* NAME��$
+ GUID�� �� *LookUpDOCTYPES.Entities+  $ GUID_DISCIPLINE��$ 
DISCIPLINE! , �� *DataContext+$ userControl�� #  $ FalseX�$
* NAME��$
+ GUID�� �� *LookUpDISCIPLINES.Entities+  $ GUID_DEPARTMENT��$ 
DEPARTMENT! , �� *DataContext+$ userControl�� #  $ FalseX�$
* NAME��$
+ GUID�� �� *LookUpDEPARTMENTS.Entities+  $ INTERNAL_NUM��  $ 
CLIENT_NUM��  $ REVISION_NUMBER��  $ DELIVERABLE_TYPE�� #  $
 TrueX� .  &  " 3   $ PRIMARY_TITLE��  $ SECONDARY_TITLE��  $ COMMENTS��  $ ESTIMATED_HOURS�� ,  $ Numeric=�$ n��$ 0X�$ 1~� .   $ DC_HOURS��1 .   $ TOTAL_COSTS��$ COSTS! $- Decimal1 $2 False2 ! �� .R�/  $��PART_Editor��# . $$ Numeric=�$( c��    $! BASELINE_ITEM.GUID_WORKPACK��$ WORKPACK! , �� *DataContext+$ userControl�� #  $ FalseX�$
+ GUID�� �� *LookUpWORKPACKS.Entities+* �� *WORKPACKDisplayMember+$ OneWay=�  $ BASELINE_ITEM.GUID_PHASE��$ PHASE! , �� *DataContext+$ userControl�� #  $ FalseX�$* INTERNAL_NUM��$
+ GUID�� �� *LookUpPHASES.Entities+  $ BASELINE_ITEM.GUID_AREA��$
 AREA! , �� *DataContext+$ userControl�� #  $ FalseX�$* INTERNAL_NUM��$
+ GUID�� �� *LookUpAREAS.Entities+  $  BASELINE_ITEM.GUID_DOCTYPE��$ DOCTYPE! , �� *DataContext+$ userControl�� #  $ FalseX�$
* NAME��$
+ GUID�� �� *LookUpDOCTYPES.Entities+  $# BASELINE_ITEM.GUID_DISCIPLINE��$ 
DISCIPLINE! , �� *DataContext+$ userControl�� #  $ FalseX�$
* NAME��$
+ GUID�� �� *LookUpDISCIPLINES.Entities+  $# BASELINE_ITEM.GUID_DEPARTMENT��$ 
DEPARTMENT! , �� *DataContext+$ userControl�� #  $ FalseX�$
* NAME��$
+ GUID�� �� *LookUpDEPARTMENTS.Entities+  $  BASELINE_ITEM.INTERNAL_NUM��$ INTERNAL_NUM!   $ BASELINE_ITEM.CLIENT_NUM��$ 
CLIENT_NUM!   $# BASELINE_ITEM.REVISION_NUMBER��$ REVISION_NUMBER!   $$ BASELINE_ITEM.DELIVERABLE_TYPE��$ DELIVERABLE_TYPE!  #  $
 TrueX� .  &  " 3   $! BASELINE_ITEM.PRIMARY_TITLE��$ PRIMARY_TITLE!   $# BASELINE_ITEM.SECONDARY_TITLE��$ SECONDARY_TITLE!   $ BASELINE_ITEM.COMMENTS��$ COMMENTS!   $# BASELINE_ITEM.ESTIMATED_HOURS��$ ESTIMATED_HOURS!  ,  $ Numeric=�$ n��$ 0X�$ 1~� .   $ BASELINE_ITEM.DC_HOURS��1 . $ DC_HOURS!   $ TOTAL_COSTS��$ COSTS! $- Decimal1 $2 False2 ! �� .R�/  $��PART_Editor��# . $$ Numeric=�$( c��    $ PHASE! $ GUID_DPHASE��, �� *DataContext+$ userControl�� #  $ FalseX�$* INTERNAL_NUM��$
+ GUID�� �� *LookUpPHASES.Entities+  $
 AREA! $ 
GUID_DAREA��, �� *DataContext+$ userControl�� #  $ FalseX�$* INTERNAL_NUM��$
+ GUID�� �� *LookUpAREAS.Entities+  $ DOCTYPE! $ GUID_DDOCTYPE��, �� *DataContext+$ userControl�� #  $ FalseX�$
* NAME��$
+ GUID�� �� *LookUpDOCTYPES.Entities+  $ 
DISCIPLINE! $ GUID_DDISCIPLINE��, �� *DataContext+$ userControl�� #  $ FalseX�$
* NAME��$
+ GUID�� �� *LookUpDISCIPLINES.Entities+  $ 
DEPARTMENT! $ GUID_DDEPARTMENT��, �� *DataContext+$ userControl�� #  $ FalseX�$
* NAME��$
+ GUID�� �� *LookUpDEPARTMENTS.Entities+  $ INTERNAL_NAME1��  $ INTERNAL_NAME2��  $ 	STARTDATE�� 4  $3 
1753-01-01X�$4 12/31/9999 23:59:59X�  $ ENDDATE�� 4  $3 
1753-01-01X�$4 12/31/9999 23:59:59X�  $ REVIEWSTARTDATE�� 4  $3 
1753-01-01X�$4 12/31/9999 23:59:59X�  $ REVIEWENDDATE�� 4  $3 
1753-01-01X�$4 12/31/9999 23:59:59X�  $ FORECASTSTARTDATE�� 4  $3 
1753-01-01X�$4 12/31/9999 23:59:59X�  $ FORECASTENDDATE�� 4  $3 
1753-01-01X�$4 12/31/9999 23:59:59X�  $ AUTOGENERATED��1 .     $
 NAME��  $ PROGRESS_START�� 4  $3 
1753-01-01X�$4 12/31/9999 23:59:59X�  $ 	DATA_DATE�� 4  $3 
1753-01-01X�$4 12/31/9999 23:59:59X�  $ INTERVAL_COUNT�� ,  $ Numeric=�$ n0��$ 0X�$ 1~�$  99X� .   $ INTERVAL_TYPE�� #  $
 TrueX� .  &  " 5   $ STATUS�� #  $
 TrueX� .  &  " 6   $ COMMENTS��    $! BASELINE_ITEM.GUID_WORKPACK��$ WORKPACK! 1 . 5 .  ! �� .R�8  $��PART_Editor��$6 FalseX�$
7 GUID��8 �� *&$DataContext.LookUpWORKPACKS.Entities+$ userControl��9 �� *#!DataContext.WORKPACKDisplayMember+$ OneWay=�$ userControl��  $&  BASELINE_ITEM.PHASE.INTERNAL_NUM��$ PHASE! 1 . 5 .    $% BASELINE_ITEM.AREA.INTERNAL_NUM��$
 AREA! 1 . 5 .    $  BASELINE_ITEM.DOCTYPE.NAME��$ DOCTYPE! 1 . 5 .    $# BASELINE_ITEM.DISCIPLINE.NAME��$ 
DISCIPLINE! 1 . 5 .    $# BASELINE_ITEM.DEPARTMENT.NAME��$ 
DEPARTMENT! 1 . 5 .    $  BASELINE_ITEM.INTERNAL_NUM��$ INTERNAL NUM! 1 . 5 .    $ BASELINE_ITEM.CLIENT_NUM��$ 
CLIENT NUM! 1 . 5 .    $# BASELINE_ITEM.REVISION_NUMBER��$ REVISION NUM! 1 . 5 .    $$ BASELINE_ITEM.DELIVERABLE_TYPE��$
 TYPE! 1 . 5 .    $! BASELINE_ITEM.PRIMARY_TITLE��$ PRIMARY TITLE! 1 . 5 .    $# BASELINE_ITEM.SECONDARY_TITLE��$ SECONDARY TITLE! 1 . 5 .    $ BASELINE_ITEM.COMMENTS��$ COMMENTS!   $ CurrentPercentage��$ 	CURRENT %! $
. True2 ! �� .R�� : �� $��3 ��� $
��Auto ��� $��* �.+��� $; No progress��$��1������ $1< +DevExpress.Xpf.Core.Editors.Images.info.png��8   y�`� 8=  $	 Hidden��> ��	False��.1��� $ PART_Editor��$ MinValue��� $ PART_Editor��$ 	EditValue��� $; Max progress��$��1������ $1< +DevExpress.Xpf.Core.Editors.Images.info.png��8  y�`� 8=  $	 Hidden��> ��	False��.1��� $ PART_Editor��$ MaxValue��� $ PART_Editor��$ 	EditValue�.  $��PART_Editor��$$ Numeric=�$( p2��# . $
% 0.01~�$��2��' �� *RowData.Row.MaxPercentage+& �� *RowData.Row.MinPercentage+  $# BASELINE_ITEM.ESTIMATED_HOURS��$ ESTIMATED HOURS! 1 . 5 .   ,  $ Numeric=�$ n��$ 0X�$ 1~� .   $ BASELINE_ITEM.DC_HOURS��$ DC HOURS! 1 . 5 .   ,  $ Numeric=�$ n��$ 0X�$ 1~� .   $ BASELINE_ITEM.TOTAL_COSTS��$ TOTAL COSTS! 1 . 5 .  ! �� .R�/  $��PART_Editor��# . $$ Numeric=�$( c��  $ CURRENT_EARNED_COSTS��$ CURRENT EARNED COSTS! 1 . 5 .  ! �� .R�/  $��PART_Editor��# . $$ Numeric=�$( c��  $ TOTAL_EARNED_COSTS��$ TOTAL COSTS! 1 . 5 .  ! �� .R�/  $��PART_Editor��# . $$ Numeric=�$( c��    $
 NAME��  $ COMMENTS��  $ 	SUBMITTED��  $ APPROVED��  $ FROM BASELINE! $ GUID_ORIBASELINE��1 . $. False2  #  $ FalseX�$* REVISION��$
+ GUID�� �� *&$DataContext.LookUpBASELINES.Entities+$ userControl��  $ TO BASELINE! $ GUID_BASELINE��1 . $. False2  #  $ FalseX�$* REVISION��$
+ GUID�� �� *&$DataContext.LookUpBASELINES.Entities+$ userControl��    $! BASELINE_ITEM.GUID_WORKPACK��$ WORKPACK! ! �� .R�8  $��PART_Editor��$6 FalseX�$
7 GUID��8 �� *&$DataContext.LookUpWORKPACKS.Entities+$ userControl��9 �� *#!DataContext.WORKPACKDisplayMember+$ OneWay=�$ userControl��{��� *RowData.Row.IsReadOnly+8     $ BASELINE_ITEM.GUID_PHASE��$ PHASE! ! �� .R�8  $��PART_Editor��$6 FalseX�$9 INTERNAL_NUM��$
7 GUID��8 �� *#!DataContext.LookUpPHASES.Entities+$ userControl��{��� *RowData.Row.IsReadOnly+8    $ BASELINE_ITEM.GUID_AREA��$
 AREA! ! �� .R�8  $��PART_Editor��$6 FalseX�$9 INTERNAL_NUM��$
7 GUID��8 �� *" DataContext.LookUpAREAS.Entities+$ userControl��{��� *RowData.Row.IsReadOnly+8    $  BASELINE_ITEM.GUID_DOCTYPE��$ DOCTYPE! ! �� .R�8  $��PART_Editor��$6 FalseX�$
9 NAME��$
7 GUID��8 �� *%#DataContext.LookUpDOCTYPES.Entities+$ userControl��{��� *RowData.Row.IsReadOnly+8    $# BASELINE_ITEM.GUID_DISCIPLINE��$ 
DISCIPLINE! ! �� .R�8  $��PART_Editor��$6 FalseX�$
9 NAME��$
7 GUID��8 �� *(&DataContext.LookUpDISCIPLINES.Entities+$ userControl��{��� *RowData.Row.IsReadOnly+8    $# BASELINE_ITEM.GUID_DEPARTMENT��$ 
DEPARTMENT! ! �� .R�8  $��PART_Editor��$6 FalseX�$
9 NAME��$
7 GUID��8 �� *(&DataContext.LookUpDEPARTMENTS.Entities+$ userControl��{��� *RowData.Row.IsReadOnly+8    $  BASELINE_ITEM.INTERNAL_NUM��$ INTERNAL NUM! ! �� .R�/  $��PART_Editor��? �� *RowData.Row.IsReadOnly+  $ BASELINE_ITEM.CLIENT_NUM��$ 
CLIENT NUM! ! �� .R�/  $��PART_Editor��? �� *RowData.Row.IsReadOnly+  $# BASELINE_ITEM.REVISION_NUMBER��$ REVISION NUM! ! �� .R�/  $��PART_Editor��? �� *RowData.Row.IsReadOnly+  $$ BASELINE_ITEM.DELIVERABLE_TYPE��$
 TYPE!  #  $
 TrueX� .  &  " 3 5 �� *RowData.Row.IsReadOnly+8    $! BASELINE_ITEM.PRIMARY_TITLE��$ PRIMARY TITLE! ! �� .R�/  $��PART_Editor��? �� *RowData.Row.IsReadOnly+  $# BASELINE_ITEM.SECONDARY_TITLE��$ SECONDARY TITLE! ! �� .R�/  $��PART_Editor��? �� *RowData.Row.IsReadOnly+  $ BASELINE_ITEM.COMMENTS��$ COMMENTS! ! �� .R�/  $��PART_Editor��? �� *RowData.Row.IsReadOnly+  $# BASELINE_ITEM.ESTIMATED_HOURS��$ ESTIMATED HOURS! 1 . 5 .   ,  $ Numeric=�$ n��$ 0X�$ 1~� .   $ VARIATION_UNITS��$ DC HOURS!  ,  $ Numeric=�$ n��$ 0X�$ 1~� . 5 �� *RowData.Row.IsReadOnly+8    $ UNBOUND_TOTAL_COSTS��$ TOTAL COSTS! $- Decimal1 1 . 5 .  ! �� .R�/  $��PART_Editor��# . $$ Numeric=�$( c��  $ ACTION��1 . 5 .   #  $
 TrueX� . @ .   &  " <   $ UNBOUND_CANCEL��$ CANCEL! $- Boolean1 $. False2 8A  ! �� .R��� $��18������ *(&DataContext.CancelBASELINE_ITEMCommand+$ userControl������ *RowData.Row+.���� $
 
Horizontal=�.+��� $��
0, 0, 3, 0q����� $ RowData.Row.ACTION�8 	 �� $��Center=����� $ RowData.Row.ACTION�8 
     $
 NAME��  $ 
DEPARTMENT��  $ TITLE��  $ DESCRIPTION��  $
 ROLE! $ 	GUID_ROLE�� #  $ FalseX�$
* NAME��$
+ GUID�� �� *" DataContext.LookUpROLES.Entities+$ userControl��