﻿using BluePrints.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Mvvm.POCO;
using BluePrints.Common.Projections;

namespace BluePrints.Common.ViewModel.Reporting
{
    /// <summary>
    /// Provides a set of extension methods to perform commonly used operations with ISupportProgressReporting.
    /// </summary>
    public static class ISupportProgressReportingExtensions
    {
        /// <summary>
        /// Default exception periods, may be replaced by calendar settings
        /// </summary>
        public static List<Period> NonWorkingPeriods
        {
            get
            {
                DateTime XmasStart = new DateTime(2015, 12, 21);
                DateTime XmasEnd = new DateTime(2016, 1, 3);
                List<Period> nonworkingperiod = new List<Period>();
                nonworkingperiod.Add(new Period(XmasStart.Date, XmasEnd.Date));
                return nonworkingperiod;
            }
        }

        #region Parameter Calculation
        /// <summary>
        /// Calculates productivity
        /// </summary>
        /// <param name="workingPeriod">Period to calculate the productivity</param>
        /// <param name="exceptionPeriod">Periods to not calculate the productivity</param>
        /// <param name="totalUnits">Total units to spread across the period</param>
        /// <returns>Calculated productivity</returns>
        public static decimal CalculatePlannedProductivity(Period workingPeriod, IEnumerable<Period> exceptionPeriod, decimal totalUnits, decimal exceptionProductivity = 0)
        {
            if (workingPeriod.StartDate.Date > workingPeriod.EndDate.Date || totalUnits == 0)
                return exceptionProductivity;

            DateTime countDate = workingPeriod.StartDate.Date;
            decimal workingPeriodInDays = 0;
            do
            {
                //if dates are not between
                if (!exceptionPeriod.Any(dates => dates.StartDate.Date <= countDate.Date && countDate.Date <= dates.EndDate.Date))
                    workingPeriodInDays += 1;

                countDate = countDate.AddDays(1);
            } while (countDate <= workingPeriod.EndDate.Date);

            if (workingPeriodInDays == 0)
                return 0;

            decimal earnedHours = workingPeriodInDays * Int32.Parse(CommonResources.ProgressReporting_DefaultHoursADay);
            decimal productivity = totalUnits / earnedHours;

            return productivity;
        }

        /// <summary>
        /// Calculates the data date backwards to get the first aligned data date as per the project start date
        /// </summary>
        public static DateTime GenerateFirstAlignedDataDate(PROGRESS principalProgress, TimeSpan? periodInterval = null)
        {
            if (periodInterval == null)
                periodInterval = ConvertProgressIntervalToPeriod(principalProgress);

            TimeSpan intervalPeriod = (TimeSpan)periodInterval;
            DateTime firstProgressDate = principalProgress.DATA_DATE;

            //rewind the first progress date to scan to before the datadate aligned to startdate day of week
            while (firstProgressDate.AddDays(-1 * intervalPeriod.Days) > principalProgress.PROGRESS_START.Date.AddSeconds(-1))
            {
                firstProgressDate = firstProgressDate.AddDays(-1 * intervalPeriod.Days);
            }

            return firstProgressDate;
        }

        /// <summary>
        /// Calculates the data date forward to get the last aligned data date as per the first aligned data date
        /// </summary>
        public static List<DateTime> GenerateAlignedDatesCollection(DateTime firstAlignedDataDate, DateTime lastDataPointDate, TimeSpan intervalPeriod)
        {
            DateTime lastProgressDate = firstAlignedDataDate;
            lastDataPointDate = lastDataPointDate.AddDays(intervalPeriod.Days);
            List<DateTime> alignedDataDatesCollection = new List<DateTime>();
            alignedDataDatesCollection.Add(firstAlignedDataDate);
            //forward the first progress date to scan to after the datadate aligned to end day of week
            do
            {
                lastProgressDate = lastProgressDate.AddDays(intervalPeriod.Days);
                alignedDataDatesCollection.Add(lastProgressDate);
            } while (lastProgressDate < lastDataPointDate);

            return alignedDataDatesCollection;
        }

        /// <summary>
        /// Calculates the aligned data date for workpack start date, used for remaining datapoints calculation
        /// </summary>
        public static DateTime GenerateWorkpackAlignedDataDate(DateTime firstAlignedDataDate, DateTime workpackStartDate, TimeSpan intervalPeriod)
        {
            DateTime weekEndingAlignedDataDate = firstAlignedDataDate;

            do
            {
                weekEndingAlignedDataDate = weekEndingAlignedDataDate.AddDays(intervalPeriod.Days);
            } while (weekEndingAlignedDataDate <= workpackStartDate);

            return weekEndingAlignedDataDate;
        }

        public static TimeSpan ConvertProgressIntervalToPeriod(PROGRESS PROGRESS)
        {
            return TimeSpan.FromDays((int)PROGRESS.INTERVAL_TYPE * PROGRESS.INTERVAL_COUNT);
        }
        #endregion

        #region Planned Calculation Methods

        public static void BuildPlannedDataPoints(ReportableObject reportableObject, bool isOriginal)
        {
            ////Populate the progressItem variation adjustments
            //reportableObject.NonCumulative_VariationAdjustments = new ObservableCollection<VariationAdjustment>(SummaryObject.NonCumulative_VariationAdjustments.Where(adjustment => adjustment.BaselineItemGuid == reportableObject.BASELINE_ITEM.GUID_ORIGINAL).ToList());
            //reportableObject.Cumulative_VariationAdjustments = ISupportProgressReportingExtensions.PopulateCumulativeVariationAdjustments(reportableObject.NonCumulative_VariationAdjustments, SummaryObject.FirstAlignedDataDate, SummaryObject.IntervalPeriod);

            ////Assign the report date for stats display
            //reportableObject.ReportingDataDate = SummaryObject.ReportingDataDate;

            //BASELINE_ITEMInfo baselineItem = reportableObject.BASELINE_ITEM;
            //if (baselineItem.WORKPACK == null)
            //    continue;

            //List<ProgressInfo> progressItemP6DataPoints;
            //WorkpackAssignmentLoadType assignmentLoadType = WorkpackAssignmentLoadType.Original;
            //if (!fromOriginalBaseline)
            //    assignmentLoadType = WorkpackAssignmentLoadType.Modified;

            //IEnumerable<TASK> BASELINE_TASKS = null;
            //P6Data.PROJECT BASELINE_P6PROJECT = null;
            //if (fromOriginalBaseline)
            //{
            //    if (this.SummaryObject.LiveBASELINE.P6BASELINE_NAME != null && this.SummaryObject.LiveBASELINE.P6BASELINE_NAME != string.Empty)
            //        BASELINE_TASKS = GetP6ScheduleTasks(summaryObject.LiveBASELINE.P6BASELINE_NAME, out BASELINE_P6PROJECT);
            //}
            //else
            //{
            //    if (this.SummaryObject.LiveBASELINE.P6MODBASELINE_NAME != null && this.SummaryObject.LiveBASELINE.P6MODBASELINE_NAME != string.Empty)
            //        BASELINE_TASKS = GetP6ScheduleTasks(summaryObject.LiveBASELINE.P6MODBASELINE_NAME, out BASELINE_P6PROJECT);
            //}

            //if (SummaryObject.LivePROGRESS.P6PROGRESS_NAME != null && TryBuildP6DataPoints(BASELINE_P6PROJECT, BASELINE_TASKS, reportableObject, DataPointsType.Planned, assignmentLoadType, out progressItemP6DataPoints))
            //{
            //    if (fromOriginalBaseline)
            //        reportableObject.NonCumulative_OriginalDataPoints = new ObservableCollection<ProgressInfo>(progressItemP6DataPoints);
            //    else
            //        reportableObject.NonCumulative_PlannedDataPoints = new ObservableCollection<ProgressInfo>(progressItemP6DataPoints);
            //}
            //else
            //{
            //    List<Period> workpackSuspensionPeriod = new List<Period>();
            //    workpackSuspensionPeriod.Add(new Period(baselineItem.WORKPACK.REVIEWSTARTDATE, baselineItem.WORKPACK.REVIEWENDDATE));


            //    decimal BaselineItemBaseUnits = baselineItem.ESTIMATED_HOURS;
            //    decimal BaselineItemBaseCosts = baselineItem.ESTIMATED_COSTS;
            //    decimal BaselineItemTotalUnits = baselineItem.TOTAL_HOURS;
            //    decimal BaselineItemTotalCosts = baselineItem.TOTAL_COSTS;

            //    List<ProgressInfo> plannedDataPoints;
            //    if (fromOriginalBaseline) //if it's generating from original baseline ignore variation
            //    {
            //        TimeSpan workingBaseTimeSpan = baselineItem.WORKPACK.ENDDATE - baselineItem.WORKPACK.STARTDATE;
            //        plannedDataPoints = ISupportProgressReportingExtensions.DataPointsGenerator(SummaryObject, workingBaseTimeSpan, BaselineItemBaseUnits, BaselineItemBaseCosts, baselineItem.WORKPACK.STARTDATE, baselineItem.GUID_ORIGINAL, this.CurrencyConversion, workpackSuspensionPeriod, BaselineItemTotalUnits, BaselineItemTotalCosts);
            //        reportableObject.NonCumulative_OriginalDataPoints = new ObservableCollection<ProgressInfo>(plannedDataPoints);
            //    }
            //    else
            //    {
            //        DateTime modifiedEndDateToUse = baselineItem.WORKPACK.ENDDATE;
            //        if (baselineItem.WORKPACK.FORECASTENDDATE != null)
            //            modifiedEndDateToUse = (DateTime)baselineItem.WORKPACK.FORECASTENDDATE;

            //        TimeSpan workingModifiedTimeSpan = modifiedEndDateToUse - baselineItem.WORKPACK.STARTDATE;
            //        if (baselineItem.WORKPACK.FORECASTSTARTDATE != null && ((DateTime)baselineItem.WORKPACK.FORECASTSTARTDATE) > baselineItem.WORKPACK.ENDDATE)
            //            workpackSuspensionPeriod.Add(new Period(baselineItem.WORKPACK.ENDDATE.AddDays(1), (DateTime)baselineItem.WORKPACK.FORECASTSTARTDATE));

            //        //Used to show sharktooth on variation
            //        plannedDataPoints = ISupportProgressReportingExtensions.DataPointsGenerator(SummaryObject, workingModifiedTimeSpan, BaselineItemBaseUnits, BaselineItemBaseCosts, baselineItem.WORKPACK.STARTDATE, baselineItem.GUID_ORIGINAL, this.CurrencyConversion, workpackSuspensionPeriod, null, null, reportableObject.Cumulative_VariationAdjustments);

            //        //Used to show normalized variation
            //        //plannedDataPoints = DataPointsGenerator(WorkingPeriod, progressInterval, BaselineItemTotalUnits, BaselineItemTotalCosts, this.CurrencyConversion, baselineItem.WORKPACK.STARTDATE, firstAlignedDataDate, baselineItem.GUID_ORIGINAL);
            //        reportableObject.NonCumulative_PlannedDataPoints = new ObservableCollection<ProgressInfo>(plannedDataPoints);
            //    }
            //}
        }
        #endregion

        #region Cumulative Calculation Methods
        /// <summary>
        /// Populate summary data point for reporting S-Curve
        /// </summary>
        public static ObservableCollection<ProgressInfo> PopulateCumulativeSummaryDataPoints(ObservableCollection<ProgressInfo> flatDataPoints, ObservableCollection<VariationAdjustment> variationAdjustments, decimal totalBudgetedUnits, decimal totalBudgetedCosts, DateTime firstAlignedDataDate, TimeSpan progressInterval, Guid aggregateGuid)
        {
            if (flatDataPoints == null || flatDataPoints.Count() == 0)
                return null;

            ObservableCollection<ProgressInfo> summaryDataPoints = new ObservableCollection<ProgressInfo>();
            DateTime progressLastDataDate = flatDataPoints.Max(dataPoint => dataPoint.ProgressDate);

            //Add zero UOM data point so that line graph starts at 0%
            summaryDataPoints.Add(new ProgressInfo()
            {
                BudgetedUnits = totalBudgetedUnits,
                BudgetedCosts = totalBudgetedCosts,
                Units = 0,
                Costs = 0,
                ProgressDate = firstAlignedDataDate.AddDays(-1 * progressInterval.Days),
                BaselineItemGuid = aggregateGuid
            });

            //Start going through each progress items to retrieve cumulative data point per period
            DateTime scanDate = firstAlignedDataDate;
            decimal individualPeriodCumulativeUnits = 0;
            decimal individualPeriodCumulativeCosts = 0;
            decimal cumulativeAdjustmentUnits = 0;
            decimal cumulativeAdjustmentCosts = 0;
            while (scanDate <= progressLastDataDate)
            {
                List<ProgressInfo> progressItemScanDateDataPoints;
                List<VariationAdjustment> progressItemAdjustments;

                if (scanDate == firstAlignedDataDate)
                {
                    progressItemScanDateDataPoints = flatDataPoints.Where(DataPoint => DataPoint.ProgressDate < scanDate.AddDays(progressInterval.Days)).ToList();
                    progressItemAdjustments = variationAdjustments == null ? new List<VariationAdjustment>() : variationAdjustments.Where(Adjustment => Adjustment.AdjustmentDate < scanDate.AddDays(progressInterval.Days)).ToList();
                }
                else
                {
                    progressItemScanDateDataPoints = flatDataPoints.Where(DataPoint => DataPoint.ProgressDate >= scanDate && DataPoint.ProgressDate < scanDate.AddDays(progressInterval.Days)).ToList();
                    progressItemAdjustments = variationAdjustments == null ? new List<VariationAdjustment>() : variationAdjustments.Where(Adjustment => Adjustment.AdjustmentDate == scanDate).ToList();
                }

                decimal incrementUnits = progressItemScanDateDataPoints.Sum(dataPoint => dataPoint.Units);
                decimal variationUnits = progressItemAdjustments.Sum(adjustment => adjustment.AdjustmentUnits);
                decimal variationCosts = progressItemAdjustments.Sum(adjustment => adjustment.AdjustmentCumulativeCosts);

                if (variationUnits > 0)
                {
                    cumulativeAdjustmentUnits += variationUnits;
                    cumulativeAdjustmentCosts += variationCosts;

                    //for sharktooth effect, add dip on a day after the previous period
                    summaryDataPoints.Add(new ProgressInfo()
                    {
                        BudgetedUnits = totalBudgetedUnits + cumulativeAdjustmentUnits,
                        BudgetedCosts = totalBudgetedCosts + cumulativeAdjustmentCosts,
                        Units = individualPeriodCumulativeUnits,
                        Costs = individualPeriodCumulativeCosts,
                        ProgressDate = scanDate.AddDays(-1 * progressInterval.Days).AddDays(1),
                        BaselineItemGuid = aggregateGuid
                    });
                }

                if (incrementUnits > 0)
                {
                    individualPeriodCumulativeUnits += progressItemScanDateDataPoints.Sum(dataPoint => dataPoint.Units);
                    individualPeriodCumulativeCosts += progressItemScanDateDataPoints.Sum(dataPoint => dataPoint.Costs);

                    summaryDataPoints.Add(new ProgressInfo()
                    {
                        BudgetedUnits = totalBudgetedUnits + cumulativeAdjustmentUnits,
                        BudgetedCosts = totalBudgetedCosts + cumulativeAdjustmentCosts,
                        Units = individualPeriodCumulativeUnits,
                        Costs = individualPeriodCumulativeCosts,
                        ProgressDate = scanDate,
                        BaselineItemGuid = aggregateGuid
                    });
                }

                scanDate = scanDate.AddDays(progressInterval.Days);
            }

            return summaryDataPoints;
        }

        /// <summary>
        /// Populate summary data point for reporting S-Curve
        /// </summary>
        public static ObservableCollection<VariationAdjustment> PopulateCumulativeVariationAdjustments(ObservableCollection<VariationAdjustment> flatVariationAdjustments, DateTime firstAlignedDataDate, TimeSpan progressInterval)
        {
            ObservableCollection<VariationAdjustment> summaryDataPoints = new ObservableCollection<VariationAdjustment>();
            if (flatVariationAdjustments == null || flatVariationAdjustments.Count() == 0)
                return summaryDataPoints;

            DateTime progressLastDataDate = flatVariationAdjustments.Max(dataPoint => dataPoint.AdjustmentDate);

            //Add zero UOM data point so that line graph starts at 0%
            summaryDataPoints.Add(new VariationAdjustment()
            {
                AdjustmentDate = firstAlignedDataDate.AddDays(-1 * progressInterval.Days),
                AdjustmentUnits = 0
            });

            //Start going through each progress items to retrieve cumulative data point per period
            DateTime scanDate = firstAlignedDataDate;
            decimal individualPeriodValue = 0;
            decimal individualPeriodCosts = 0;

            while (scanDate <= progressLastDataDate)
            {
                List<VariationAdjustment> progressItemScanDateDataPoints;

                if (scanDate == firstAlignedDataDate)
                    progressItemScanDateDataPoints = flatVariationAdjustments.Where(DataPoint => DataPoint.AdjustmentDate < scanDate.AddDays(progressInterval.Days)).ToList();
                else
                    progressItemScanDateDataPoints = flatVariationAdjustments.Where(DataPoint => DataPoint.AdjustmentDate >= scanDate && DataPoint.AdjustmentDate < scanDate.AddDays(progressInterval.Days)).ToList();

                decimal incrementUnits = progressItemScanDateDataPoints.Sum(dataPoint => dataPoint.AdjustmentUnits);
                if (incrementUnits != 0)
                {
                    individualPeriodValue = progressItemScanDateDataPoints.Sum(dataPoint => dataPoint.AdjustmentUnits);
                    individualPeriodCosts = progressItemScanDateDataPoints.Sum(dataPoint => dataPoint.AdjustmentNativeCosts);

                    summaryDataPoints.Add(new VariationAdjustment()
                    {
                        AdjustmentDate = scanDate.AddDays(progressInterval.Days),
                        AdjustmentUnits = individualPeriodValue,
                        AdjustmentRate = progressItemScanDateDataPoints.First().AdjustmentRate,
                        AdjustmentCumulativeCosts = individualPeriodCosts
                    });
                }

                scanDate = scanDate.AddDays(progressInterval.Days);
            }

            return summaryDataPoints;
        }

        /// <summary>
        /// Populate remaining summary data point for reporting S-Curve
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<ProgressInfo> PopulateCumulativeRemainingSummaryDataPoints(ObservableCollection<ProgressInfo> flatDataPointsBeforeDataDate, ObservableCollection<VariationAdjustment> variationAdjustments, ObservableCollection<ProgressInfo> flatDataPointsAfterDataDate, decimal totalBudgetedUnits, decimal totalBudgetedCosts, DateTime firstAlignedDataDate, TimeSpan progressInterval, Guid aggregateGuid)
        {
            if (flatDataPointsBeforeDataDate == null || flatDataPointsBeforeDataDate.Count == 0 || flatDataPointsAfterDataDate == null || flatDataPointsAfterDataDate.Count == 0)
                return null;

            decimal collectionEarnedUnits = flatDataPointsBeforeDataDate.Sum(obj => obj.Units);
            decimal collectionEarnedUnitsAfter = flatDataPointsAfterDataDate.Sum(obj => obj.Units);

            ObservableCollection<ProgressInfo> summaryDataPoints = new ObservableCollection<ProgressInfo>();
            DateTime remainingFirstDataDate = flatDataPointsAfterDataDate.Min(dataPoint => dataPoint.ProgressDate);
            DateTime remainingLastDataDate = flatDataPointsAfterDataDate.Max(dataPoint => dataPoint.ProgressDate);

            //Add zero UOM data point so that line graph starts at 0%
            summaryDataPoints.Add(new ProgressInfo()
            {
                BudgetedUnits = totalBudgetedUnits,
                BudgetedCosts = totalBudgetedCosts,
                Units = 0,
                Costs = 0,
                ProgressDate = firstAlignedDataDate.AddDays(-1 * progressInterval.Days),
                BaselineItemGuid = aggregateGuid
            });

            //Start going through each progress items to retrieve cumulative data point per period
            DateTime scanDate = firstAlignedDataDate;
            decimal individualPeriodCumulativeUnits = 0;
            decimal individualPeriodCumulativeCosts = 0;
            decimal cumulativeAdjustmentUnits = 0;
            decimal cumulativeAdjustmentCosts = 0;
            bool executedDataDatePoints = true;

            while (scanDate.Date <= remainingLastDataDate)
            {
                List<ProgressInfo> progressItemScanDateDataPoints;
                List<VariationAdjustment> progressItemAdjustments;

                if (scanDate == firstAlignedDataDate)
                {
                    progressItemScanDateDataPoints = flatDataPointsBeforeDataDate.Where(DataPoint => DataPoint.ProgressDate < scanDate.AddDays(progressInterval.Days)).ToList();
                    progressItemAdjustments = variationAdjustments == null ? new List<VariationAdjustment>() : variationAdjustments.Where(adjustment => adjustment.AdjustmentDate < scanDate.AddDays(progressInterval.Days)).ToList();
                }
                else if (scanDate >= remainingFirstDataDate)
                {
                    progressItemScanDateDataPoints = flatDataPointsAfterDataDate.Where(DataPoint => (!executedDataDatePoints && DataPoint.ProgressDate.Date == remainingFirstDataDate.Date) || (DataPoint.ProgressDate >= scanDate && DataPoint.ProgressDate < scanDate.AddDays(progressInterval.Days))).ToList();
                    progressItemAdjustments = variationAdjustments == null ? new List<VariationAdjustment>() : variationAdjustments.Where(adjustment => (!executedDataDatePoints && adjustment.AdjustmentDate.Date == remainingFirstDataDate.Date) || (adjustment.AdjustmentDate >= scanDate && adjustment.AdjustmentDate < scanDate.AddDays(progressInterval.Days))).ToList();
                    executedDataDatePoints = true;
                }
                else
                {
                    progressItemScanDateDataPoints = flatDataPointsBeforeDataDate.Where(DataPoint => DataPoint.ProgressDate >= scanDate && DataPoint.ProgressDate < scanDate.AddDays(progressInterval.Days)).ToList();
                    progressItemAdjustments = variationAdjustments == null ? new List<VariationAdjustment>() : variationAdjustments.Where(adjustment => adjustment.AdjustmentDate == scanDate).ToList();
                }

                decimal incrementUnits = progressItemScanDateDataPoints.Sum(dataPoint => dataPoint.Units);
                decimal variationUnits = progressItemAdjustments.Sum(adjustment => adjustment.AdjustmentUnits);
                decimal variationCosts = progressItemAdjustments.Sum(adjustment => adjustment.AdjustmentCumulativeCosts);
                if (variationUnits > 0)
                {
                    cumulativeAdjustmentUnits += variationUnits;
                    cumulativeAdjustmentCosts += variationCosts;

                    //for sharktooth effect, add dip on a day after the previous period
                    summaryDataPoints.Add(new ProgressInfo()
                    {
                        BudgetedUnits = totalBudgetedUnits + cumulativeAdjustmentUnits,
                        BudgetedCosts = totalBudgetedCosts + cumulativeAdjustmentCosts,
                        Units = individualPeriodCumulativeUnits,
                        Costs = individualPeriodCumulativeCosts,
                        ProgressDate = scanDate.AddDays(-1 * progressInterval.Days).AddDays(1),
                        BaselineItemGuid = aggregateGuid
                    });
                }

                if (incrementUnits > 0)
                {
                    individualPeriodCumulativeUnits += progressItemScanDateDataPoints.Sum(dataPoint => dataPoint.Units);
                    individualPeriodCumulativeCosts += progressItemScanDateDataPoints.Sum(dataPoint => dataPoint.Costs);


                    summaryDataPoints.Add(new ProgressInfo()
                    {
                        BudgetedUnits = totalBudgetedUnits + cumulativeAdjustmentUnits,
                        BudgetedCosts = totalBudgetedCosts + cumulativeAdjustmentCosts,
                        Units = individualPeriodCumulativeUnits,
                        Costs = individualPeriodCumulativeCosts,
                        ProgressDate = scanDate,
                        BaselineItemGuid = aggregateGuid
                    });
                }

                scanDate = scanDate.AddDays(progressInterval.Days);
            }

            return summaryDataPoints;
        }

        /// <summary>
        /// Summarizes non-cumulative data points into cumulative data points by default period
        /// </summary>
        /// <param name="isAggregate">Indicate whether interface is an aggregate so that budgeted units/costs will be recalculated</param>
        public static void GenerateCumulativeSummaryDataPoints(SummarizableObject summarizableObject)
        {
            decimal totalBudgetedUnits = summarizableObject.Total_BudgetedUnits;
            decimal totalBudgetedCosts = summarizableObject.Total_BudgetedCosts;
            decimal finalBudgetedUnits = summarizableObject.Final_BudgetedUnits;
            decimal finalBudgetedCosts = summarizableObject.Final_BudgetedCosts;
            DateTime firstDataDate = summarizableObject.FirstAlignedDataDate;
            TimeSpan intervalPeriod = summarizableObject.IntervalPeriod;
            bool isPOCOViewModel = (summarizableObject as IPOCOViewModel) != null;

            if (summarizableObject.NonCumulative_VariationAdjustments.Count > 0)
                summarizableObject.Cumulative_VariationAdjustments = PopulateCumulativeVariationAdjustments(summarizableObject.NonCumulative_VariationAdjustments, firstDataDate, intervalPeriod);

            if (summarizableObject.NonCumulative_OriginalDataPoints.Count > 0)
            {
                summarizableObject.Summary_CumulativeOriginalDataPoints = PopulateCumulativeSummaryDataPoints(summarizableObject.NonCumulative_OriginalDataPoints, new ObservableCollection<VariationAdjustment>(), finalBudgetedUnits, finalBudgetedCosts, firstDataDate, intervalPeriod, Guid.Empty);
                if (isPOCOViewModel)
                {
                    summarizableObject.RaisePropertyChanged(x => x.Summary_CumulativeOriginalDataPoints);
                    summarizableObject.RaisePropertyChanged(x => x.Summary_PeriodOriginalDataPoints);
                }
            }

            if (summarizableObject.NonCumulative_PlannedDataPoints.Count > 0)
            {
                //Used to show normalized variation
                //ReportableObject.Summary_CumulativePlannedDataPoints = this.ISupportProgressReportingCollection.PopulateCumulativeSummaryDataPoints(ReportableObject.NonCumulative_PlannedDataPoints, new List<VariationAdjustment>(), finalBudgetedUnits, finalBudgetedCosts, firstDataDate, intervalPeriod, Guid.Empty);
                //Used to show sharktooth on variation
                summarizableObject.Summary_CumulativePlannedDataPoints = PopulateCumulativeSummaryDataPoints(summarizableObject.NonCumulative_PlannedDataPoints, summarizableObject.Cumulative_VariationAdjustments, totalBudgetedUnits, totalBudgetedCosts, firstDataDate, intervalPeriod, Guid.Empty);
                if (isPOCOViewModel)
                {
                    summarizableObject.RaisePropertyChanged(x => x.Summary_CumulativePlannedDataPoints);
                    summarizableObject.RaisePropertyChanged(x => x.Summary_PeriodPlannedDataPoints);
                }
            }

            if (summarizableObject.NonCumulative_EarnedDataPoints.Count > 0)
            {
                summarizableObject.Summary_CumulativeEarnedDataPoints = PopulateCumulativeSummaryDataPoints(summarizableObject.NonCumulative_EarnedDataPoints, summarizableObject.Cumulative_VariationAdjustments, totalBudgetedUnits, totalBudgetedCosts, firstDataDate, intervalPeriod, Guid.Empty);
                if (isPOCOViewModel)
                {
                    summarizableObject.RaisePropertyChanged(x => x.Summary_CumulativeEarnedDataPoints);
                    summarizableObject.RaisePropertyChanged(x => x.Summary_PeriodEarnedDataPoints);
                }
            }

            if (summarizableObject.NonCumulative_BurnedDataPoints.Count > 0)
            {
                summarizableObject.Summary_CumulativeBurnedDataPoints = PopulateCumulativeSummaryDataPoints(summarizableObject.NonCumulative_BurnedDataPoints, summarizableObject.Cumulative_VariationAdjustments, totalBudgetedUnits, totalBudgetedCosts, firstDataDate, intervalPeriod, Guid.Empty);
                if (isPOCOViewModel)
                {
                    summarizableObject.RaisePropertyChanged(x => x.Summary_CumulativeBurnedDataPoints);
                    summarizableObject.RaisePropertyChanged(x => x.Summary_PeriodBurnedDataPoints);
                }
            }

            if (summarizableObject.NonCumulative_ActualDataPoints.Count > 0)
            {
                summarizableObject.Summary_CumulativeActualDataPoints = PopulateCumulativeSummaryDataPoints(summarizableObject.NonCumulative_ActualDataPoints, summarizableObject.Cumulative_VariationAdjustments, totalBudgetedUnits, totalBudgetedCosts, firstDataDate, intervalPeriod, Guid.Empty);
                if (isPOCOViewModel)
                {
                    summarizableObject.RaisePropertyChanged(x => x.Summary_CumulativeActualDataPoints);
                    summarizableObject.RaisePropertyChanged(x => x.Summary_PeriodActualDataPoints);
                }
            }

            if (summarizableObject.NonCumulative_EarnedDataPoints.Count > 0)
            {
                summarizableObject.Summary_CumulativeRemainingPlannedDataPoints = PopulateCumulativeRemainingSummaryDataPoints(summarizableObject.NonCumulative_EarnedDataPoints, summarizableObject.Cumulative_VariationAdjustments, summarizableObject.NonCumulative_RemainingPlannedDataPoints, totalBudgetedUnits, totalBudgetedCosts, firstDataDate, intervalPeriod, Guid.Empty);
                if (isPOCOViewModel)
                {
                    summarizableObject.RaisePropertyChanged(x => x.Summary_CumulativeRemainingPlannedDataPoints);
                    summarizableObject.RaisePropertyChanged(x => x.Summary_PeriodRemainingPlannedDataPoints);
                }
            }

            if (summarizableObject.NonCumulative_EarnedDataPoints.Count > 0)
            {
                summarizableObject.Summary_CumulativeRemainingVariationDataPoints = PopulateCumulativeRemainingSummaryDataPoints(summarizableObject.NonCumulative_EarnedDataPoints, summarizableObject.Cumulative_VariationAdjustments, summarizableObject.NonCumulative_RemainingVariationDataPoints, totalBudgetedUnits, totalBudgetedCosts, firstDataDate, intervalPeriod, Guid.Empty);
                if (isPOCOViewModel)
                {
                    summarizableObject.RaisePropertyChanged(x => x.Summary_CumulativeRemainingVariationDataPoints);
                    summarizableObject.RaisePropertyChanged(x => x.Summary_PeriodRemainingVariationDataPoints);
                }
            }

            if (summarizableObject.NonCumulative_EarnedDataPoints.Count > 0)
            {
                summarizableObject.Summary_CumulativeRemainingCurrentDataPoints = PopulateCumulativeRemainingSummaryDataPoints(summarizableObject.NonCumulative_EarnedDataPoints, summarizableObject.Cumulative_VariationAdjustments, summarizableObject.NonCumulative_RemainingCurrentDataPoints, totalBudgetedUnits, totalBudgetedCosts, firstDataDate, intervalPeriod, Guid.Empty);
                if (isPOCOViewModel)
                {
                    summarizableObject.RaisePropertyChanged(x => x.Summary_CumulativeRemainingCurrentDataPoints);
                    summarizableObject.RaisePropertyChanged(x => x.Summary_PeriodRemainingCurrentDataPoints);
                }
            }
        }

        /// <summary>
        /// Summarizes non-cumulative data points into cumulative data points by default period
        /// </summary>
        /// <param name="isAggregate">Indicate whether interface is an aggregate so that budgeted units/costs will be recalculated</param>
        public static void GenerateCumulativeSummaryDataPoints(ReportableObject reportableObject, DateTime firstAlignedDataDate, TimeSpan interval)
        {
            decimal totalBudgetedUnits = reportableObject.BASELINE_ITEMJoinRATE.BASELINE_ITEM.ESTIMATED_HOURS;
            decimal totalBudgetedCosts = reportableObject.BASELINE_ITEMJoinRATE.ESTIMATED_COSTS;
            decimal finalBudgetedUnits = reportableObject.BASELINE_ITEMJoinRATE.BASELINE_ITEM.TOTAL_HOURS;
            decimal finalBudgetedCosts = reportableObject.BASELINE_ITEMJoinRATE.TOTAL_COSTS;
            DateTime firstDataDate = firstAlignedDataDate;
            TimeSpan intervalPeriod = interval;
            bool isPOCOViewModel = (reportableObject as IPOCOViewModel) != null;

            if (reportableObject.NonCumulative_VariationAdjustments.Count > 0 && reportableObject.Cumulative_VariationAdjustments == null)
                reportableObject.Cumulative_VariationAdjustments = PopulateCumulativeVariationAdjustments(reportableObject.NonCumulative_VariationAdjustments, firstDataDate, intervalPeriod);

            if (reportableObject.NonCumulative_OriginalDataPoints.Count > 0 && reportableObject.Summary_CumulativeOriginalDataPoints == null)
            {
                reportableObject.Summary_CumulativeOriginalDataPoints = PopulateCumulativeSummaryDataPoints(reportableObject.NonCumulative_OriginalDataPoints, new ObservableCollection<VariationAdjustment>(), finalBudgetedUnits, finalBudgetedCosts, firstDataDate, intervalPeriod, Guid.Empty);
                if (isPOCOViewModel)
                {
                    reportableObject.RaisePropertyChanged(x => x.Summary_CumulativeOriginalDataPoints);
                    reportableObject.RaisePropertyChanged(x => x.Summary_PeriodOriginalDataPoints);
                }
            }

            if (reportableObject.NonCumulative_PlannedDataPoints.Count > 0 && reportableObject.Summary_CumulativePlannedDataPoints == null)
            {
                //Used to show normalized variation
                //ReportableObject.Summary_CumulativePlannedDataPoints = this.ISupportProgressReportingCollection.PopulateCumulativeSummaryDataPoints(ReportableObject.NonCumulative_PlannedDataPoints, new List<VariationAdjustment>(), finalBudgetedUnits, finalBudgetedCosts, firstDataDate, intervalPeriod, Guid.Empty);
                //Used to show sharktooth on variation
                reportableObject.Summary_CumulativePlannedDataPoints = PopulateCumulativeSummaryDataPoints(reportableObject.NonCumulative_PlannedDataPoints, reportableObject.Cumulative_VariationAdjustments, totalBudgetedUnits, totalBudgetedCosts, firstDataDate, intervalPeriod, Guid.Empty);
                if (isPOCOViewModel)
                {
                    reportableObject.RaisePropertyChanged(x => x.Summary_CumulativePlannedDataPoints);
                    reportableObject.RaisePropertyChanged(x => x.Summary_PeriodPlannedDataPoints);
                }
            }

            if (reportableObject.NonCumulative_EarnedDataPoints.Count > 0 && reportableObject.Summary_CumulativeEarnedDataPoints == null)
            {
                reportableObject.Summary_CumulativeEarnedDataPoints = PopulateCumulativeSummaryDataPoints(reportableObject.NonCumulative_EarnedDataPoints, reportableObject.Cumulative_VariationAdjustments, totalBudgetedUnits, totalBudgetedCosts, firstDataDate, intervalPeriod, Guid.Empty);
                if (isPOCOViewModel)
                {
                    reportableObject.RaisePropertyChanged(x => x.Summary_CumulativeEarnedDataPoints);
                    reportableObject.RaisePropertyChanged(x => x.Summary_PeriodEarnedDataPoints);
                }
            }

            if (reportableObject.NonCumulative_BurnedDataPoints.Count > 0 && reportableObject.Summary_CumulativeBurnedDataPoints == null)
            {
                reportableObject.Summary_CumulativeBurnedDataPoints = PopulateCumulativeSummaryDataPoints(reportableObject.NonCumulative_BurnedDataPoints, reportableObject.Cumulative_VariationAdjustments, totalBudgetedUnits, totalBudgetedCosts, firstDataDate, intervalPeriod, Guid.Empty);
                if (isPOCOViewModel)
                {
                    reportableObject.RaisePropertyChanged(x => x.Summary_CumulativeBurnedDataPoints);
                    reportableObject.RaisePropertyChanged(x => x.Summary_PeriodBurnedDataPoints);
                }
            }

            if (reportableObject.NonCumulative_ActualDataPoints.Count > 0 && reportableObject.Summary_CumulativeActualDataPoints == null)
            {
                reportableObject.Summary_CumulativeActualDataPoints = PopulateCumulativeSummaryDataPoints(reportableObject.NonCumulative_ActualDataPoints, reportableObject.Cumulative_VariationAdjustments, totalBudgetedUnits, totalBudgetedCosts, firstDataDate, intervalPeriod, Guid.Empty);
                if (isPOCOViewModel)
                {
                    reportableObject.RaisePropertyChanged(x => x.Summary_CumulativeActualDataPoints);
                    reportableObject.RaisePropertyChanged(x => x.Summary_PeriodActualDataPoints);
                }
            }

            if (reportableObject.NonCumulative_EarnedDataPoints.Count > 0 && reportableObject.Summary_CumulativeRemainingPlannedDataPoints == null)
            {
                reportableObject.Summary_CumulativeRemainingPlannedDataPoints = PopulateCumulativeRemainingSummaryDataPoints(reportableObject.NonCumulative_EarnedDataPoints, reportableObject.Cumulative_VariationAdjustments, reportableObject.NonCumulative_RemainingPlannedDataPoints, totalBudgetedUnits, totalBudgetedCosts, firstDataDate, intervalPeriod, Guid.Empty);
                if (isPOCOViewModel)
                {
                    reportableObject.RaisePropertyChanged(x => x.Summary_CumulativeRemainingPlannedDataPoints);
                    reportableObject.RaisePropertyChanged(x => x.Summary_PeriodRemainingPlannedDataPoints);
                }
            }

            if (reportableObject.NonCumulative_EarnedDataPoints.Count > 0 && reportableObject.Summary_CumulativeRemainingVariationDataPoints == null)
            {
                reportableObject.Summary_CumulativeRemainingVariationDataPoints = PopulateCumulativeRemainingSummaryDataPoints(reportableObject.NonCumulative_EarnedDataPoints, reportableObject.Cumulative_VariationAdjustments, reportableObject.NonCumulative_RemainingVariationDataPoints, totalBudgetedUnits, totalBudgetedCosts, firstDataDate, intervalPeriod, Guid.Empty);
                if (isPOCOViewModel)
                {
                    reportableObject.RaisePropertyChanged(x => x.Summary_CumulativeRemainingVariationDataPoints);
                    reportableObject.RaisePropertyChanged(x => x.Summary_PeriodRemainingVariationDataPoints);
                }
            }

            if (reportableObject.NonCumulative_EarnedDataPoints.Count > 0 && reportableObject.Summary_CumulativeRemainingCurrentDataPoints == null)
            {
                reportableObject.Summary_CumulativeRemainingCurrentDataPoints = PopulateCumulativeRemainingSummaryDataPoints(reportableObject.NonCumulative_EarnedDataPoints, reportableObject.Cumulative_VariationAdjustments, reportableObject.NonCumulative_RemainingCurrentDataPoints, totalBudgetedUnits, totalBudgetedCosts, firstDataDate, intervalPeriod, Guid.Empty);
                if (isPOCOViewModel)
                {
                    reportableObject.RaisePropertyChanged(x => x.Summary_CumulativeRemainingCurrentDataPoints);
                    reportableObject.RaisePropertyChanged(x => x.Summary_PeriodRemainingCurrentDataPoints);
                }
            }
        }
        #endregion

        #region OnDemand Calculation

        public static ProgressInfo nullProgressDataPoint = new ProgressInfo() { BudgetedCosts = 0, BudgetedUnits = 0, Costs = 0, Units = 0 };
        /// <summary>
        /// Search for specific datapoint within a collection of data points by specific date
        /// </summary>
        /// <param name="progressDataPoints">Data points collection to search</param>
        /// <param name="dataPointDate">Date to retrieve data point</param>
        /// <returns>Data point on the particular date</returns>
        public static ProgressInfo FindDataPointByDate(ObservableCollection<ProgressInfo> progressDataPoints, DateTime dataPointDate)
        {
            if (progressDataPoints == null || progressDataPoints.Count == 0)
                return nullProgressDataPoint;

            //use last of default because during Earned list generation the current earned is added to the end of the list
            //this is necessary for the first period when earned contains a zero value data point and an actual value data point
            var specificDateDataPoint = progressDataPoints.LastOrDefault(obj => obj.ProgressDate.Date <= dataPointDate.Date);

            return specificDateDataPoint;
        }

        /// <summary>
        /// Searches for the period percentage on a collection of data points by specific date
        /// </summary>
        /// <param name="progressDataPoints">Data points collection to search</param>
        /// <param name="dataPointDate">Date to retrieve data point</param>
        /// <returns>Period period datapoint on the particular date</returns>
        public static ProgressInfo GeneratePeriodDataPointFromCumulative(ObservableCollection<ProgressInfo> progressDataPoints, DateTime dataPointDate)
        {
            if (progressDataPoints == null || progressDataPoints.Count == 0)
                return nullProgressDataPoint;

            ProgressInfo CumulativeProgressOnDataDate = progressDataPoints.FirstOrDefault(obj => obj.ProgressDate.Date == dataPointDate.Date);
            if (CumulativeProgressOnDataDate != null)
            {
                int CurrentPeriodIndex = progressDataPoints.IndexOf(CumulativeProgressOnDataDate);
                if (CurrentPeriodIndex == 0)
                    return null;
                else
                {
                    int PreviousPeriodIndex = CurrentPeriodIndex - 1;
                    ProgressInfo CumulativeProgressOnDataDatePrevious = progressDataPoints[PreviousPeriodIndex];
                    return new ProgressInfo()
                    {
                        BaselineItemGuid = Guid.Empty,
                        BudgetedUnits = CumulativeProgressOnDataDate.BudgetedUnits,
                        BudgetedCosts = CumulativeProgressOnDataDate.BudgetedCosts,
                        Units = CumulativeProgressOnDataDate.Units - CumulativeProgressOnDataDatePrevious.Units,
                        Costs = CumulativeProgressOnDataDate.Costs - CumulativeProgressOnDataDatePrevious.Costs,
                        ProgressDate = CumulativeProgressOnDataDate.ProgressDate
                    };
                }
            }
            else
                return nullProgressDataPoint;
        }

        /// <summary>
        /// Convert cumulative summary collection to period summary for bar histogram construction
        /// </summary>
        public static ObservableCollection<ProgressInfo> ConvertCumulativeToPeriodDataPoint(ObservableCollection<ProgressInfo> CumulativeDataPointCollection, DateTime? plotStartDate = null)
        {
            decimal periodUnits = 0;
            decimal periodCosts = 0;

            ObservableCollection<ProgressInfo> PeriodDataPointCollection = new ObservableCollection<ProgressInfo>();

            if (CumulativeDataPointCollection != null)
            {
                for (int i = 0; i < CumulativeDataPointCollection.Count; i++)
                {
                    if (plotStartDate != null && CumulativeDataPointCollection[i].ProgressDate <= plotStartDate)
                        continue;

                    if (i == 0)
                    {
                        periodUnits = CumulativeDataPointCollection[i].Units;
                        periodCosts = CumulativeDataPointCollection[i].Costs;
                    }
                    else
                    {
                        periodUnits = CumulativeDataPointCollection[i].Units - CumulativeDataPointCollection[i - 1].Units;
                        periodCosts = CumulativeDataPointCollection[i].Costs - CumulativeDataPointCollection[i - 1].Costs;

                        if (periodUnits < 0)
                            periodUnits = 0;

                        if (periodCosts < 0)
                            periodCosts = 0;
                    }

                    PeriodDataPointCollection.Add(new ProgressInfo
                    {
                        BudgetedCosts = CumulativeDataPointCollection[i].BudgetedCosts,
                        BudgetedUnits = CumulativeDataPointCollection[i].BudgetedUnits,
                        Costs = periodCosts < 0 ? 0 : periodCosts,
                        Units = periodUnits < 0 ? 0 : periodUnits,
                        ProgressDate = CumulativeDataPointCollection[i].ProgressDate,
                        BaselineItemGuid = Guid.Empty
                    });
                }
            }

            return PeriodDataPointCollection;
        }

        #endregion

        #region Generators
        /// <summary>
        /// Generate datapoint by spreading out units/costs across a specified timespan
        /// </summary>
        /// <param name="workingPeriod">Period to spread units/costs across</param>
        /// <param name="progressInterval">Interval for each period</param>
        /// <param name="assignmentUnits">Units to be spreaded</param>
        /// <param name="assignmentCosts">Costs to be spreaded</param>
        /// <param name="currencyConversionFactor">Factor for currency conversion</param>
        /// <param name="plotStartDate">The raw datetime of the entity to plot against</param>
        /// <param name="firstAlignedDataDate">Aligned date to correspond with other data points on chart</param>
        /// <param name="dataPointGuid">The deliverable guid</param>
        /// <param name="originalUnits">Override the total units to produce a different percentage</param>
        /// <param name="originalCosts">Override the total costs to produce a different percentage</param>
        /// <returns></returns>
        public static List<ProgressInfo> DataPointsGenerator(SummarizableObject SummaryObject, TimeSpan workingPeriod, decimal assignmentUnits, decimal assignmentCosts, DateTime plotStartDate, Guid dataPointGuid, decimal currencyConversion, IEnumerable<Period> suspensionPeriod = null, decimal? originalUnits = null, decimal? originalCosts = null, ObservableCollection<VariationAdjustment> cumulativeVariationAdjustment = null)
        {
            List<ProgressInfo> returnProgressDataPoints = new List<ProgressInfo>();
            TimeSpan progressInterval = SummaryObject.IntervalPeriod;
            DateTime firstAlignedDataDate = SummaryObject.FirstAlignedDataDate;

            decimal PeriodCount = 0;
            PeriodCount = Convert.ToDecimal(workingPeriod.TotalDays) / Convert.ToDecimal(progressInterval.TotalDays);

            //don't use assignment units for total units because it comes from workpack assignment
            //workpack assignment have incomplete units because it sometimes only describe a portion of the total units
            decimal TotalUnits = originalUnits == null ? assignmentUnits : (decimal)originalUnits;
            decimal TotalCosts = originalCosts == null ? assignmentCosts : (decimal)originalCosts;
            TotalCosts = TotalCosts * currencyConversion;

            assignmentCosts *= currencyConversion;
            decimal UnitsPerPeriod = PeriodCount == 0 ? 0 : assignmentUnits / PeriodCount;
            decimal CostsPerPeriod = PeriodCount == 0 ? 0 : assignmentCosts / PeriodCount;

            //because first progress date is not necessarily the next interval
            DateTime loopDate;

            if (firstAlignedDataDate > plotStartDate)
            {
                loopDate = firstAlignedDataDate;
                do
                {
                    loopDate = loopDate.AddDays(-1 * progressInterval.Days);
                } while (loopDate.Date.AddDays(-1 * progressInterval.Days) > plotStartDate);
            }
            else
            {
                loopDate = firstAlignedDataDate;
                do
                {
                    loopDate = loopDate.AddDays(progressInterval.Days);
                } while (loopDate.Date.AddDays(progressInterval.Days) < plotStartDate);
            }

            ProgressInfo firstProgressPlanned = new ProgressInfo()
            {
                BudgetedUnits = TotalUnits,
                BudgetedCosts = TotalCosts,
                Units = 0,
                Costs = 0,
                ProgressDate = plotStartDate,
                BaselineItemGuid = dataPointGuid
            };

            List<VariationAdjustment> progressItemAdjustments;
            decimal cumulativeAdjustmentUnits = 0;
            decimal cumulativeAdjustmentCosts = 0;
            decimal additionalProductivityUnits = 0;
            decimal additionalProductivityCosts = 0;

            progressItemAdjustments = cumulativeVariationAdjustment == null ? new List<VariationAdjustment>() : cumulativeVariationAdjustment.Where(adjustment => adjustment.AdjustmentDate <= loopDate).ToList();
            decimal variationUnits = progressItemAdjustments.Sum(adjustment => adjustment.AdjustmentUnits);
            decimal variationCosts = progressItemAdjustments.Sum(adjustment => adjustment.AdjustmentNativeCosts);

            if (variationUnits > 0)
            {
                variationCosts *= currencyConversion;
                assignmentUnits += variationUnits;
                assignmentCosts += variationCosts;
                TotalUnits += variationUnits;
                TotalCosts += variationCosts;
                cumulativeAdjustmentUnits += variationUnits;
                cumulativeAdjustmentCosts += variationCosts;
            }

            returnProgressDataPoints.Add(firstProgressPlanned);

            if (loopDate < plotStartDate)
                loopDate = loopDate.AddDays(progressInterval.Days);

            //first aligned progress data point, must be checked for pro-rata
            TimeSpan proRateTimeSpan = loopDate - plotStartDate;
            //convert to 5 days week
            decimal proRataPeriod = Convert.ToDecimal(proRateTimeSpan.TotalDays) / ((Convert.ToDecimal(progressInterval.TotalDays) / 7) * 5);
            if (proRataPeriod < 0)
                proRataPeriod = 0;

            if (proRataPeriod > PeriodCount)
                proRataPeriod = PeriodCount;

            PeriodCount -= proRataPeriod;

            decimal firstPeriodProRateUnits = 0;
            decimal firstPeriodProRateCosts = 0;

            firstPeriodProRateUnits += UnitsPerPeriod * proRataPeriod;
            firstPeriodProRateCosts += CostsPerPeriod * proRataPeriod;

            ProgressInfo firstPeriodAlignedProgressPlanned = new ProgressInfo()
            {
                BudgetedUnits = TotalUnits,
                BudgetedCosts = TotalCosts,
                Units = firstPeriodProRateUnits,
                Costs = firstPeriodProRateCosts,
                ProgressDate = loopDate,
                BaselineItemGuid = dataPointGuid
            };

            returnProgressDataPoints.Add(firstPeriodAlignedProgressPlanned);

            decimal adjustmentPeriodCount = 0;
            DateTime adjustmentLoopDate = new DateTime();
            loopDate = loopDate.AddDays(progressInterval.TotalDays);
            adjustmentLoopDate = loopDate;
            adjustmentPeriodCount = PeriodCount;

            //Establish exception periods
            List<Period> exceptionPeriods = new List<Period>();
            exceptionPeriods.AddRange(NonWorkingPeriods);
            if (suspensionPeriod != null)
                exceptionPeriods.AddRange(suspensionPeriod);

            //normalize units/costs by non-effective periods
            if (PeriodCount > 1)
            {
                do
                {
                    if (exceptionPeriods.Any(dates => dates.StartDate.Date <= adjustmentLoopDate && adjustmentLoopDate <= dates.EndDate.Date))
                    {
                        UnitsPerPeriod += (UnitsPerPeriod / PeriodCount);
                        CostsPerPeriod += (CostsPerPeriod / PeriodCount);
                    }

                    adjustmentPeriodCount -= 1;
                    adjustmentLoopDate = adjustmentLoopDate.AddDays(progressInterval.TotalDays);
                } while (adjustmentPeriodCount > 1);
            }
            else
            {
                UnitsPerPeriod = UnitsPerPeriod * PeriodCount; //last period pro-rate
                CostsPerPeriod = CostsPerPeriod * PeriodCount;
            }

            if (PeriodCount > 1)
            {
                //populate till the second last period
                do
                {
                    bool CalendarNonWorkingPeriod = false;

                    if (exceptionPeriods.Any(dates => dates.StartDate.Date <= loopDate && loopDate <= dates.EndDate.Date))
                        CalendarNonWorkingPeriod = true;

                    progressItemAdjustments = cumulativeVariationAdjustment == null ? new List<VariationAdjustment>() : cumulativeVariationAdjustment.Where(adjustment => adjustment.AdjustmentDate == loopDate).ToList();
                    variationUnits = progressItemAdjustments.Sum(adjustment => adjustment.AdjustmentUnits);
                    variationCosts = progressItemAdjustments.Sum(adjustment => adjustment.AdjustmentNativeCosts);

                    if (!CalendarNonWorkingPeriod && cumulativeVariationAdjustment != null)
                    {
                        if (variationUnits > 0)
                        {
                            variationCosts *= currencyConversion;
                            assignmentUnits += variationUnits;
                            assignmentCosts += variationCosts;
                            TotalUnits += variationUnits;
                            TotalCosts += variationCosts;
                            cumulativeAdjustmentUnits += variationUnits;
                            cumulativeAdjustmentCosts += variationCosts;
                        }
                    }

                    additionalProductivityUnits = (cumulativeAdjustmentUnits / PeriodCount);
                    additionalProductivityCosts = (cumulativeAdjustmentCosts / PeriodCount);

                    ProgressInfo newProgressPlanned = new ProgressInfo()
                    {
                        BudgetedUnits = TotalUnits,
                        BudgetedCosts = TotalCosts,
                        Units = CalendarNonWorkingPeriod ? 0 : UnitsPerPeriod + additionalProductivityUnits,
                        Costs = CalendarNonWorkingPeriod ? 0 : CostsPerPeriod + additionalProductivityCosts,
                        ProgressDate = loopDate,
                        BaselineItemGuid = dataPointGuid
                    };

                    cumulativeAdjustmentUnits -= additionalProductivityUnits;
                    cumulativeAdjustmentCosts -= additionalProductivityCosts;

                    returnProgressDataPoints.Add(newProgressPlanned);
                    PeriodCount -= 1;
                    loopDate = loopDate.AddDays(progressInterval.TotalDays);
                } while (PeriodCount > 1);
            }

            //revert the date back by 1 week because of last added interval before while look get's exited
            loopDate = loopDate.AddDays(-1 * progressInterval.TotalDays);
            if (PeriodCount > 0)
            {
                var remainingUnits = assignmentUnits - returnProgressDataPoints.Sum(dataPoint => dataPoint.Units);
                var remainingCosts = assignmentCosts - returnProgressDataPoints.Sum(dataPoint => dataPoint.Costs);

                progressItemAdjustments = cumulativeVariationAdjustment == null ? new List<VariationAdjustment>() : cumulativeVariationAdjustment.Where(adjustment => adjustment.AdjustmentDate > loopDate).ToList();
                variationUnits = progressItemAdjustments.Sum(adjustment => adjustment.AdjustmentUnits);
                variationCosts = progressItemAdjustments.Sum(adjustment => adjustment.AdjustmentNativeCosts);

                DateTime lastDataPointDate = loopDate.AddDays(progressInterval.TotalDays);
                if (variationUnits > 0)
                {
                    variationCosts *= currencyConversion;
                    remainingUnits += variationUnits;
                    remainingCosts += variationCosts;
                    TotalUnits += variationUnits;
                    TotalCosts += variationCosts;
                    lastDataPointDate = progressItemAdjustments.First().AdjustmentDate;
                }


                //populate last period
                ProgressInfo lastProgressPlanned = new ProgressInfo()
                {
                    BudgetedUnits = TotalUnits,
                    BudgetedCosts = TotalCosts,
                    Units = remainingUnits,
                    Costs = remainingCosts,
                    ProgressDate = lastDataPointDate,
                    BaselineItemGuid = dataPointGuid
                };

                returnProgressDataPoints.Add(lastProgressPlanned);
            }

            return returnProgressDataPoints;
        }


        /// <summary>
        /// Generate the remaining data points based on productivity
        /// Prerequisites: this.ISupportProgressReportingCollection.FirstAlignedDataDate, this.ISupportProgressReportingCollection.IntervalPeriod and this.ISupportProgressReportingCollection.UnifiedCalculationMethod must be initialized
        /// </summary>
        public static ObservableCollection<ProgressInfo> RemainingDataPointsGenerator(SummarizableObject summaryObject, ReportableObject reportableObject, DateTime firstAlignedWeekEndingDataDate, List<Period> exceptionPeriod, decimal remainingUnits, decimal unitsPerHour, decimal firstPeriodProRate, decimal currencyConversion, DateTime? limitDate = null)
        {
            ObservableCollection<ProgressInfo> remainingDataPoints = new ObservableCollection<ProgressInfo>();
            BASELINE_ITEMProjection currentBASELINE_ITEM = reportableObject.BASELINE_ITEMJoinRATE;
            if (currentBASELINE_ITEM.BASELINE_ITEM.TOTAL_HOURS == 0 || unitsPerHour == 0)
                return remainingDataPoints;

            decimal unitsPerDay = unitsPerHour * Int32.Parse(CommonResources.ProgressReporting_DefaultHoursADay);
            decimal unitsPerPeriod;
            TimeSpan intervalPeriod = summaryObject.IntervalPeriod;

            if (limitDate != null)
            {
                DateTime dateLimit = (DateTime)limitDate;
                decimal remainingCountPeriod = Convert.ToDecimal(((dateLimit - firstAlignedWeekEndingDataDate).TotalDays) / intervalPeriod.Days);
                if (remainingCountPeriod <= 0)
                    unitsPerPeriod = remainingUnits; //needs to be completed immediately
                else
                    unitsPerPeriod = remainingUnits / remainingCountPeriod;
            }
            else
                unitsPerPeriod = unitsPerDay * intervalPeriod.Days;

            if (unitsPerPeriod > remainingUnits)
                unitsPerPeriod = remainingUnits;

            //remaining date is moved forward a period to categorize the datapoints as week ending
            DateTime remainingCountDataDate = firstAlignedWeekEndingDataDate;

            do
            {
                if (!exceptionPeriod.Any(dates => dates.StartDate.Date <= remainingCountDataDate.Date && remainingCountDataDate.Date <= dates.EndDate.Date))
                {
                    decimal periodUnits;
                    if (firstPeriodProRate > 0)
                    {
                        periodUnits = unitsPerPeriod * firstPeriodProRate;
                        firstPeriodProRate = 0;
                    }
                    else if (remainingUnits < unitsPerPeriod)
                        periodUnits = remainingUnits;
                    else
                        periodUnits = unitsPerPeriod;

                    ProgressInfo newDataPoint = new ProgressInfo()
                    {
                        BudgetedCosts = currentBASELINE_ITEM.TOTAL_COSTS,
                        BudgetedUnits = currentBASELINE_ITEM.BASELINE_ITEM.TOTAL_HOURS,
                        Units = periodUnits,
                        ProgressDate = remainingCountDataDate,
                        BaselineItemGuid = currentBASELINE_ITEM.BASELINE_ITEM.GUID_ORIGINAL
                    };

                    newDataPoint.Costs = currentBASELINE_ITEM.ITEMRATE * currencyConversion;
                    remainingDataPoints.Add(newDataPoint);
                    remainingUnits -= periodUnits;
                }
                else
                {
                    ProgressInfo newDataPoint = new ProgressInfo()
                    {
                        BudgetedCosts = currentBASELINE_ITEM.TOTAL_COSTS,
                        BudgetedUnits = currentBASELINE_ITEM.BASELINE_ITEM.TOTAL_HOURS,
                        Units = 0,
                        Costs = 0,
                        ProgressDate = remainingCountDataDate,
                        BaselineItemGuid = currentBASELINE_ITEM.BASELINE_ITEM.GUID_ORIGINAL
                    };

                    remainingDataPoints.Add(newDataPoint);
                }

                remainingCountDataDate = remainingCountDataDate.AddDays(intervalPeriod.Days);
            } while (remainingUnits > 0);

            return remainingDataPoints;
        }
        #endregion
    }
}
