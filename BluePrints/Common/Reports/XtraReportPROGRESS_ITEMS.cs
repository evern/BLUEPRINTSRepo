using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.Parameters;
using DevExpress.XtraSplashScreen;
using BluePrints.Common.ViewModel.Reporting;

namespace BluePrints.Reports
{
    public partial class XtraReportPROGRESS_ITEMS : DevExpress.XtraReports.UI.XtraReport
    {
        public XtraReportPROGRESS_ITEMS()
        {
            InitializeComponent();
            this.ParametersRequestSubmit += rptProgressItem_ParametersRequestSubmit;
        }

        void rptProgressItem_ParametersRequestSubmit(object sender, ParametersRequestEventArgs e)
        {
            string reportBy = "Units";

            foreach (ParameterInfo info in e.ParametersInformation)
            {
                if (info.Parameter.Name == "reportBy")
                    reportBy = (string)info.Parameter.Value;
            }

            string strReplaceFrom;
            string strReplaceTo;
            xrDataSummaryCumulativeBurnedPercent.DataBindings.Clear();
            xrDataSummaryCumulativeEarnedPercent.DataBindings.Clear();
            xrDataSummaryCumulativePlannedPercent.DataBindings.Clear();
            xrDataSummaryPeriodBurnedPercent.DataBindings.Clear();
            xrDataSummaryPeriodEarnedPercent.DataBindings.Clear();
            xrDataSummaryPeriodPlannedPercent.DataBindings.Clear();

            xrDataBaselineBudgeted.DataBindings.Clear();
            xrDataCumulativePlannedUOM.DataBindings.Clear();
            xrDataCumulativePlannedPercentage.DataBindings.Clear();
            xrDataCumulativeEarnedUOM.DataBindings.Clear();
            xrDataCumulativeEarnedPercentage.DataBindings.Clear();
            xrDataPeriodPlannedUOM.DataBindings.Clear();
            xrDataPeriodPlannedPercentage.DataBindings.Clear();
            xrDataPeriodCurrentUOM.DataBindings.Clear();
            xrDataPeriodCurrentPercentage.DataBindings.Clear();

            if (reportBy == "Units")
            {
                strReplaceFrom = "Costs";
                strReplaceTo = "Units";

                xrDataSummaryCumulativeBurnedPercent.DataBindings.Add(new XRBinding("Text", objectDataSource1, "Summary_CumulativeBurned.UnitsPercentage", "{0:0.00%}"));
                xrDataSummaryCumulativeEarnedPercent.DataBindings.Add(new XRBinding("Text", objectDataSource1, "Summary_CumulativeEarned.UnitsPercentage", "{0:0.00%}"));
                xrDataSummaryCumulativePlannedPercent.DataBindings.Add(new XRBinding("Text", objectDataSource1, "Summary_CumulativePlanned.UnitsPercentage", "{0:0.00%}"));
                xrDataSummaryPeriodBurnedPercent.DataBindings.Add(new XRBinding("Text", objectDataSource1, "Summary_PeriodBurned.UnitsPercentage", "{0:0.00%}"));
                xrDataSummaryPeriodEarnedPercent.DataBindings.Add(new XRBinding("Text", objectDataSource1, "Summary_PeriodEarned.UnitsPercentage", "{0:0.00%}"));
                xrDataSummaryPeriodPlannedPercent.DataBindings.Add(new XRBinding("Text", objectDataSource1, "Summary_PeriodPlanned.UnitsPercentage", "{0:0.00%}"));

                xrDataBaselineBudgeted.DataBindings.Add(new XRBinding("Text", objectDataSource1, "Items.ProgressItem_BaselineItem.BaselineItem_TotalUnits", "{0:n1}"));

                xrDataCumulativePlannedUOM.DataBindings.Add(new XRBinding("Text", objectDataSource1, "Items.Summary_CumulativePlanned.Units", "{0:n1}"));
                xrDataCumulativePlannedPercentage.DataBindings.Add(new XRBinding("Text", objectDataSource1, "Items.Summary_CumulativePlanned.UnitsPercentage", "{0:0.00%}"));

                xrDataCumulativeEarnedUOM.DataBindings.Add(new XRBinding("Text", objectDataSource1, "Items.Summary_CumulativeEarned.Units", "{0:n1}"));
                xrDataCumulativeEarnedPercentage.DataBindings.Add(new XRBinding("Text", objectDataSource1, "Items.Summary_CumulativeEarned.UnitsPercentage", "{0:0.00%}"));

                xrDataPeriodPlannedUOM.DataBindings.Add(new XRBinding("Text", objectDataSource1, "Items.Summary_PeriodPlanned.Units", "{0:n1}"));
                xrDataPeriodPlannedPercentage.DataBindings.Add(new XRBinding("Text", objectDataSource1, "Items.Summary_PeriodPlanned.UnitsPercentage", "{0:0.00%}"));

                xrDataPeriodCurrentUOM.DataBindings.Add(new XRBinding("Text", objectDataSource1, "Items.Summary_PeriodEarned.Units", "{0:n1}"));
                xrDataPeriodCurrentPercentage.DataBindings.Add(new XRBinding("Text", objectDataSource1, "Items.Summary_PeriodEarned.UnitsPercentage", "{0:0.00%}"));

                this.xrChart1.Series["Planned"].ValueDataMembersSerializable = "Summary_CumulativePlannedDataPoints.UnitsPercentage";
                this.xrChart1.Series["Earned"].ValueDataMembersSerializable = "Summary_CumulativeEarnedDataPoints.UnitsPercentage";
                this.xrChart1.Series["Burned"].ValueDataMembersSerializable = "Summary_CumulativeBurnedDataPoints.UnitsPercentage";
                this.xrChart1.Series["Remaining"].ValueDataMembersSerializable = "Summary_CumulativeRemainingPlannedDataPoints.UnitsPercentage";

                this.xrChart1.Series["Period Planned"].ValueDataMembersSerializable = "Summary_PeriodPlannedDataPoints.Units";
                this.xrChart1.Series["Period Earned"].ValueDataMembersSerializable = "Summary_PeriodEarnedDataPoints.Units";
                this.xrChart1.Series["Period Burned"].ValueDataMembersSerializable = "Summary_PeriodBurnedDataPoints.Units";
                this.xrChart1.Series["Period Remaining"].ValueDataMembersSerializable = "Summary_PeriodRemainingPlannedDataPoints.Units";
            }
            else
            {
                strReplaceFrom = "Units";
                strReplaceTo = "Costs";

                xrDataSummaryCumulativeBurnedPercent.DataBindings.Add(new XRBinding("Text", objectDataSource1, "Summary_CumulativeBurned.CostsPercentage", "{0:0.00%}"));
                xrDataSummaryCumulativeEarnedPercent.DataBindings.Add(new XRBinding("Text", objectDataSource1, "Summary_CumulativeEarned.CostsPercentage", "{0:0.00%}"));
                xrDataSummaryCumulativePlannedPercent.DataBindings.Add(new XRBinding("Text", objectDataSource1, "Summary_CumulativePlanned.CostsPercentage", "{0:0.00%}"));
                
                xrDataSummaryPeriodBurnedPercent.DataBindings.Add(new XRBinding("Text", objectDataSource1, "Summary_PeriodBurned.CostsPercentage", "{0:0.00%}"));
                xrDataSummaryPeriodEarnedPercent.DataBindings.Add(new XRBinding("Text", objectDataSource1, "Summary_PeriodEarned.CostsPercentage", "{0:0.00%}"));
                xrDataSummaryPeriodPlannedPercent.DataBindings.Add(new XRBinding("Text", objectDataSource1, "Summary_PeriodPlanned.CostsPercentage", "{0:0.00%}"));

                xrDataBaselineBudgeted.DataBindings.Add(new XRBinding("Text", objectDataSource1, "Items.ProgressItem_BaselineItem.BaselineItem_TotalCosts", "{0:n1}"));

                xrDataCumulativePlannedUOM.DataBindings.Add(new XRBinding("Text", objectDataSource1, "Items.Summary_CumulativePlanned.Costs", "{0:n1}"));
                xrDataCumulativePlannedPercentage.DataBindings.Add(new XRBinding("Text", objectDataSource1, "Items.Summary_CumulativePlanned.CostsPercentage", "{0:0.00%}"));

                xrDataCumulativeEarnedUOM.DataBindings.Add(new XRBinding("Text", objectDataSource1, "Items.Summary_CumulativeEarned.Costs", "{0:n1}"));
                xrDataCumulativeEarnedPercentage.DataBindings.Add(new XRBinding("Text", objectDataSource1, "Items.Summary_CumulativeEarned.CostsPercentage", "{0:0.00%}"));

                xrDataPeriodPlannedUOM.DataBindings.Add(new XRBinding("Text", objectDataSource1, "Items.Summary_PeriodPlanned.Costs", "{0:n1}"));
                xrDataPeriodPlannedPercentage.DataBindings.Add(new XRBinding("Text", objectDataSource1, "Items.Summary_PeriodPlanned.CostsPercentage", "{0:0.00%}"));

                xrDataPeriodCurrentUOM.DataBindings.Add(new XRBinding("Text", objectDataSource1, "Items.Summary_PeriodEarned.Costs", "{0:n1}"));
                xrDataPeriodCurrentPercentage.DataBindings.Add(new XRBinding("Text", objectDataSource1, "Items.Summary_PeriodEarned.CostsPercentage", "{0:0.00%}"));

                this.xrChart1.Series["Planned"].ValueDataMembersSerializable = "Summary_CumulativePlannedDataPoints.CostsPercentage";
                this.xrChart1.Series["Earned"].ValueDataMembersSerializable = "Summary_CumulativeEarnedDataPoints.CostsPercentage";
                this.xrChart1.Series["Burned"].ValueDataMembersSerializable = "Summary_CumulativeBurnedDataPoints.CostsPercentage";
                this.xrChart1.Series["Remaining"].ValueDataMembersSerializable = "Summary_CumulativeRemainingPlannedDataPoints.CostsPercentage";

                this.xrChart1.Series["Period Planned"].ValueDataMembersSerializable = "Summary_PeriodPlannedDataPoints.Costs";
                this.xrChart1.Series["Period Earned"].ValueDataMembersSerializable = "Summary_PeriodEarnedDataPoints.Costs";
                this.xrChart1.Series["Period Burned"].ValueDataMembersSerializable = "Summary_PeriodBurnedDataPoints.Costs";
                this.xrChart1.Series["Period Remaining"].ValueDataMembersSerializable = "Summary_PeriodRemainingPlannedDataPoints.Costs";
            }

            //labels
            xrLblCumulativeEarnedUOM.Text = xrLblCumulativeEarnedUOM.Text.Replace(strReplaceFrom, strReplaceTo);
            xrLblCumulativePlannedUOM.Text = xrLblCumulativePlannedUOM.Text.Replace(strReplaceFrom, strReplaceTo);
            xrLblPeriodCurrentUOM.Text = xrLblPeriodCurrentUOM.Text.Replace(strReplaceFrom, strReplaceTo);
            xrLblPeriodPlannedUOM.Text = xrLblPeriodPlannedUOM.Text.Replace(strReplaceFrom, strReplaceTo);

            //conditional formatting
            this.ItemCumulativeEarnedEfficiency_Good.Condition = this.ItemCumulativeEarnedEfficiency_Good.Condition.Replace(strReplaceFrom, strReplaceTo);
            this.ItemCumulativeEarnedEfficiency_Good.Condition = this.ItemCumulativeEarnedEfficiency_Good.Condition.Replace(strReplaceFrom, strReplaceTo);
            this.ItemPeriodEarnedEfficiency_Good.Condition = this.ItemPeriodEarnedEfficiency_Good.Condition.Replace(strReplaceFrom, strReplaceTo);
            this.SummaryPeriodBurnedEfficiency_Good.Condition = this.SummaryPeriodBurnedEfficiency_Good.Condition.Replace(strReplaceFrom, strReplaceTo);
            this.SummaryCumulativeBurnedEfficiency_Good.Condition = this.SummaryCumulativeBurnedEfficiency_Good.Condition.Replace(strReplaceFrom, strReplaceTo);
            this.SummaryCumulativeEarnedEfficiency_Good.Condition = this.SummaryCumulativeEarnedEfficiency_Good.Condition.Replace(strReplaceFrom, strReplaceTo);
            this.SummaryPeriodEarnedEfficiency_Good.Condition = this.SummaryPeriodEarnedEfficiency_Good.Condition.Replace(strReplaceFrom, strReplaceTo);
            this.ItemCumulativeEarnedEfficiency_Bad.Condition = this.ItemCumulativeEarnedEfficiency_Bad.Condition.Replace(strReplaceFrom, strReplaceTo);
            this.ItemPeriodEarnedEfficiency_Bad.Condition = this.ItemPeriodEarnedEfficiency_Bad.Condition.Replace(strReplaceFrom, strReplaceTo);
            this.SummaryPeriodBurnedEfficiency_Bad.Condition = this.SummaryPeriodBurnedEfficiency_Bad.Condition.Replace(strReplaceFrom, strReplaceTo);
            this.SummaryCumulativeBurnedEfficiency_Bad.Condition = this.SummaryCumulativeBurnedEfficiency_Bad.Condition.Replace(strReplaceFrom, strReplaceTo);
            this.SummaryCumulativeEarnedEfficiency_Bad.Condition = this.SummaryCumulativeEarnedEfficiency_Bad.Condition.Replace(strReplaceFrom, strReplaceTo);
            this.SummaryPeriodEarnedEfficiency_Bad.Condition = this.SummaryPeriodEarnedEfficiency_Bad.Condition.Replace(strReplaceFrom, strReplaceTo);
        }

        SummarizableObject ReportData { get; set; }
        public void AssignProperties(SummarizableObject reportData, string title)
        {
            ReportData = reportData;
            objectDataSource1.DataSource = ReportData;
            title1.Value = title;
            datadate1.Value = ReportData.ReportingDataDate;
        }

        private void rptProgressItem_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //objectDataSource1.DataSource = this._ProgressItems;
        }
    }
}
