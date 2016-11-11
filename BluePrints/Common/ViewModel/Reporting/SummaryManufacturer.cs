using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluePrints.Common.ViewModel.Reporting
{
    public class BuildProjectStats
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

    public class BuildMinimalStatsForCurrentPercentage
    {
        public void Manufacture(SummaryBuilder summaryBuilder)
        {
            Build(summaryBuilder);
        }

        public void Build(SummaryBuilder summaryBuilder)
        {
            summaryBuilder.BuildOriginalPlannedDataPoints();
            summaryBuilder.SummarizeDataPoints();
        }
    }

    public class BuildFullStatsIncludingPROGRESS_ITEM
    {
        BuildProjectStats buildProjectStats = new BuildProjectStats();
        public void Manufacture(SummaryBuilder summaryBuilder)
        {
            buildProjectStats.Build(summaryBuilder);
            Build(summaryBuilder);
        }

        public void Build(SummaryBuilder summaryBuilder)
        {
            summaryBuilder.SummarizeNestedSummaryObjectDataPoints();
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
            summaryBuilder.BuildEarnedDataPoints();
            summaryBuilder.BuildRemainingDataPoints();
            summaryBuilder.SummarizeDataPoints();
            summaryBuilder.SummarizeNestedSummaryObjectDataPoints();
        }
    }
}
