using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluePrints.Common.ViewModel.Reporting
{
    public class SummaryManufacturer
    {
        public void Manufacture(SummaryBuilder summaryBuilder)
        {
            Build(summaryBuilder);
        }

        public void Build(SummaryBuilder summaryBuilder)
        {
            summaryBuilder.BuildVariationDataPoints();
            summaryBuilder.BuildOriginalPlannedDataPoints();
            summaryBuilder.BuildModifiedPlannedDataPoints();
            summaryBuilder.BuildEarnedDataPoints();
            summaryBuilder.BuildBurnedDataPoints();
            summaryBuilder.BuildActualDataPoints();
            summaryBuilder.BuildRemainingDataPoints();
            summaryBuilder.SummarizeDataPoints();
            summaryBuilder.RecalculateStats();
        }
    }

    public class SummaryRollUp
    {
        public void Manufacture(SummaryBuilder summaryBuilder)
        {
            Build(summaryBuilder);
        }

        public void Build(SummaryBuilder summaryBuilder)
        {
            summaryBuilder.BuildVariationDataPoints();
            summaryBuilder.BuildOriginalPlannedDataPoints();
            summaryBuilder.BuildModifiedPlannedDataPoints();
            summaryBuilder.BuildEarnedDataPoints();
            summaryBuilder.BuildRemainingDataPoints();
            summaryBuilder.SummarizeDataPoints();
            summaryBuilder.RecalculateStats();
        }
    }

    public class PROGRESS_ITEMSummaryManufacturer
    {
        public void Manufacture(SummaryBuilder summaryBuilder)
        {
            Build(summaryBuilder);
        }

        public void Build(SummaryBuilder summaryBuilder)
        {
            summaryBuilder.BuildOriginalPlannedDataPoints();
            summaryBuilder.SummarizeNestedSummaryObjectDataPoints();
        }
    }
}
