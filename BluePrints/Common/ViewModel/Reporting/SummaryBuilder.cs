using BluePrints.BluePrintsEntitiesDataModel;
using BluePrints.Data;
using BluePrints.P6Data;
using BluePrints.P6EntitiesDataModel;
using BluePrints.PrimeroData.PrimeroEntitiesDataModel;
using DevExpress.Mvvm.POCO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BluePrints.Common.ViewModel.Reporting
{
    public abstract class SummaryBuilder
    {
        protected SummarizableObject summaryObject;
        public SummarizableObject SummaryObject
        {
            get { return summaryObject; }
        }

        public abstract void BuildVariationDataPoints();
        public abstract void BuildOriginalPlannedDataPoints();
        public abstract void BuildModifiedPlannedDataPoints();
        public abstract void BuildEarnedDataPoints();
        public abstract void BuildBurnedDataPoints();
        public abstract void BuildRemainingDataPoints();
        public abstract void BuildActualDataPoints();
        public abstract void SummarizeDataPoints();
        public abstract void SummarizeIndividualSummaryObjectsDataPoints();

        public void RecalculateStats(bool isCosts = false)
        {
            SummaryObject.RecalculateStats(isCosts);
        }
    }

    public class ReportableObjectRollUp : SummaryBuilder
    {
        public ReportableObjectRollUp(SummarizableObject summaryObject, WORKPACK workpack, SummarizableObject projectSummary)
        {
            summaryObject.ReportableObjects = projectSummary.ReportableObjects.Where(x => x.BASELINE_ITEMJoinRATE.BASELINE_ITEM.GUID_WORKPACK == workpack.GUID).ToArray().ToList();
            var nonCumulativeBurnedList = projectSummary.NonCumulative_BurnedDataPoints.Where(x => x.WorkpackName == workpack.INTERNAL_NAME1).OrderByDescending(x => x.ProgressDate).ToArray().ToList();
            var nonCumulativeActualList = projectSummary.NonCumulative_ActualDataPoints.Where(x => x.WorkpackName == workpack.INTERNAL_NAME1).OrderByDescending(x => x.ProgressDate).ToArray().ToList();
            summaryObject.NonCumulative_BurnedDataPoints = new ObservableCollection<ProgressInfo>(nonCumulativeBurnedList);
            summaryObject.NonCumulative_ActualDataPoints = new ObservableCollection<ProgressInfo>(nonCumulativeActualList);
            summaryObject.FirstAlignedDataDate = projectSummary.FirstAlignedDataDate;
            summaryObject.LivePROGRESS = projectSummary.LivePROGRESS;
            summaryObject.LiveBASELINE = projectSummary.LiveBASELINE;
            summaryObject.ReportingDataDate = projectSummary.LivePROGRESS.DATA_DATE;
            summaryObject.IntervalPeriod = projectSummary.IntervalPeriod;
            this.summaryObject = summaryObject;
        }

        public override void BuildVariationDataPoints()
        {
            summaryObject.NonCumulative_VariationAdjustments =  new ObservableCollection<VariationAdjustment>(summaryObject.ReportableObjects.SelectMany(x => x.NonCumulative_VariationAdjustments));
        }

        public override void BuildOriginalPlannedDataPoints()
        {
            summaryObject.NonCumulative_OriginalDataPoints = new ObservableCollection<ProgressInfo>(summaryObject.ReportableObjects.SelectMany(x => x.NonCumulative_OriginalDataPoints));
        }

        public override void BuildModifiedPlannedDataPoints()
        {
            summaryObject.NonCumulative_PlannedDataPoints = new ObservableCollection<ProgressInfo>(summaryObject.ReportableObjects.SelectMany(x => x.NonCumulative_PlannedDataPoints));
        }

        public override void BuildEarnedDataPoints()
        {
            summaryObject.NonCumulative_EarnedDataPoints = new ObservableCollection<ProgressInfo>(summaryObject.ReportableObjects.SelectMany(x => x.NonCumulative_EarnedDataPoints));
        }

        public override void BuildActualDataPoints()
        {
            throw new ArgumentException("there is no need to roll up non cumulative actual data points from reportableObjects.");
        }

        public override void BuildBurnedDataPoints()
        {
            throw new ArgumentException("there is no need to roll up non cumulative burned data points from reportableObjects.");
        }

        public override void BuildRemainingDataPoints()
        {
            summaryObject.NonCumulative_RemainingCurrentDataPoints = new ObservableCollection<ProgressInfo>(summaryObject.ReportableObjects.SelectMany(x => x.NonCumulative_RemainingCurrentDataPoints));
            summaryObject.NonCumulative_RemainingPlannedDataPoints = new ObservableCollection<ProgressInfo>(summaryObject.ReportableObjects.SelectMany(x => x.NonCumulative_RemainingPlannedDataPoints));
        }

        public override void SummarizeDataPoints()
        {
            ISupportProgressReportingExtensions.GenerateCumulativeSummaryDataPoints(summaryObject);
        }

        public override void SummarizeIndividualSummaryObjectsDataPoints()
        {
            throw new NotImplementedException();
        }
    }

    public class ProjectSummaryBuilder : SummaryBuilder
    {
        IBluePrintsEntitiesUnitOfWork BluePrintsUnitOfWork { get; set; }
        IP6EntitiesUnitOfWork P6UnitOfWork { get; set; }
        decimal CurrencyConversion { get; set; }
        IEnumerable<TASK> PROGRESS_TASKS { get; set; }
        BluePrints.P6Data.PROJECT PROGRESS_PROJECT = null;
        public ProjectSummaryBuilder(SummarizableObject summaryObject, IEnumerable<ReportableObject> ProgressReportables, PROGRESS LivePROGRESS, BASELINE LiveBASELINE, IBluePrintsEntitiesUnitOfWork BluePrintsUOW = null, IP6EntitiesUnitOfWork P6UOW = null)
        {
            if (LivePROGRESS == null || LiveBASELINE == null)
                return;

            this.CurrencyConversion = LiveBASELINE.PROJECT.CURRENCYCONVERSION;
            this.summaryObject = summaryObject;
            this.summaryObject.ReportableObjects = ProgressReportables;
            this.summaryObject.LiveBASELINE = LiveBASELINE;
            this.summaryObject.LivePROGRESS = LivePROGRESS;
            this.summaryObject.ReportingDataDate = LivePROGRESS.DATA_DATE;

            if (BluePrintsUOW == null)
                BluePrintsUOW = BluePrintsEntitiesUnitOfWorkSource.GetUnitOfWorkFactory().CreateUnitOfWork();
            else
                this.BluePrintsUnitOfWork = BluePrintsUOW;

            if (P6UOW == null)
                this.P6UnitOfWork = P6EntitiesUnitOfWorkSource.GetUnitOfWorkFactory().CreateUnitOfWork();
            else
                this.P6UnitOfWork = P6UOW;

            this.summaryObject.IntervalPeriod = ISupportProgressReportingExtensions.ConvertProgressIntervalToPeriod(LivePROGRESS);
            this.summaryObject.FirstAlignedDataDate = ISupportProgressReportingExtensions.GenerateFirstAlignedDataDate(LivePROGRESS);
        }

        public override void SummarizeDataPoints()
        {
            ISupportProgressReportingExtensions.GenerateCumulativeSummaryDataPoints(this.summaryObject);
        }

        public override void SummarizeIndividualSummaryObjectsDataPoints()
        {
            foreach (ReportableObject reportableObject in SummaryObject.ReportableObjects)
            {
                ISupportProgressReportingExtensions.GenerateCumulativeSummaryDataPoints(reportableObject, this.summaryObject.FirstAlignedDataDate, this.summaryObject.IntervalPeriod);
            }
        }

        public override void BuildOriginalPlannedDataPoints()
        {
            PlannedDataPointsBuilder(true);
        }

        public override void BuildModifiedPlannedDataPoints()
        {
            PlannedDataPointsBuilder(false);
        }

        private void PlannedDataPointsBuilder(bool fromOriginalBaseline)
        {
            foreach (ReportableObject reportableObject in SummaryObject.ReportableObjects)
            {
                //Populate the progressItem variation adjustments
                reportableObject.NonCumulative_VariationAdjustments = new ObservableCollection<VariationAdjustment>(SummaryObject.NonCumulative_VariationAdjustments.Where(adjustment => adjustment.BaselineItemGuid == reportableObject.BASELINE_ITEMJoinRATE.BASELINE_ITEM.GUID_ORIGINAL).ToList());
                reportableObject.Cumulative_VariationAdjustments = ISupportProgressReportingExtensions.PopulateCumulativeVariationAdjustments(reportableObject.NonCumulative_VariationAdjustments, SummaryObject.FirstAlignedDataDate, SummaryObject.IntervalPeriod);

                //Assign the report date for stats display
                reportableObject.ReportingDataDate = SummaryObject.ReportingDataDate;

                BASELINE_ITEM baselineItem = reportableObject.BASELINE_ITEMJoinRATE.BASELINE_ITEM;
                if (baselineItem.WORKPACK == null)
                    continue;

                List<ProgressInfo> progressItemP6DataPoints;
                WorkpackAssignmentLoadType assignmentLoadType = WorkpackAssignmentLoadType.Original;
                if (!fromOriginalBaseline)
                    assignmentLoadType = WorkpackAssignmentLoadType.Modified;

                IEnumerable<TASK> BASELINE_TASKS = null;
                P6Data.PROJECT BASELINE_P6PROJECT = null;
                if(fromOriginalBaseline)
                {
                    if (this.SummaryObject.LiveBASELINE.P6BASELINE_NAME != null && this.SummaryObject.LiveBASELINE.P6BASELINE_NAME != string.Empty)
                        BASELINE_TASKS = GetP6ScheduleTasks(summaryObject.LiveBASELINE.P6BASELINE_NAME, out BASELINE_P6PROJECT);
                }
                else
                {
                    if (this.SummaryObject.LiveBASELINE.P6MODBASELINE_NAME != null && this.SummaryObject.LiveBASELINE.P6MODBASELINE_NAME != string.Empty)
                        BASELINE_TASKS = GetP6ScheduleTasks(summaryObject.LiveBASELINE.P6MODBASELINE_NAME, out BASELINE_P6PROJECT);
                }

                if (SummaryObject.LivePROGRESS.P6PROGRESS_NAME != null && TryBuildP6DataPoints(BASELINE_P6PROJECT, BASELINE_TASKS, reportableObject, DataPointsType.Planned, assignmentLoadType, out progressItemP6DataPoints))
                {
                    if (fromOriginalBaseline)
                        reportableObject.NonCumulative_OriginalDataPoints = new ObservableCollection<ProgressInfo>(progressItemP6DataPoints);
                    else
                        reportableObject.NonCumulative_PlannedDataPoints = new ObservableCollection<ProgressInfo>(progressItemP6DataPoints);
                }
                else
                {
                    List<Period> workpackSuspensionPeriod = new List<Period>();
                    workpackSuspensionPeriod.Add(new Period(baselineItem.WORKPACK.REVIEWSTARTDATE, baselineItem.WORKPACK.REVIEWENDDATE));


                    decimal BaselineItemBaseUnits = baselineItem.ESTIMATED_HOURS;
                    decimal BaselineItemBaseCosts = baselineItem.ESTIMATED_COSTS;
                    decimal BaselineItemTotalUnits = baselineItem.TOTAL_HOURS;
                    decimal BaselineItemTotalCosts = baselineItem.TOTAL_COSTS;

                    List<ProgressInfo> plannedDataPoints;
                    if (fromOriginalBaseline) //if it's generating from original baseline ignore variation
                    {
                        TimeSpan workingBaseTimeSpan = baselineItem.WORKPACK.ENDDATE - baselineItem.WORKPACK.STARTDATE;
                        plannedDataPoints = ISupportProgressReportingExtensions.DataPointsGenerator(SummaryObject, workingBaseTimeSpan, BaselineItemBaseUnits, BaselineItemBaseCosts, baselineItem.WORKPACK.STARTDATE, baselineItem.GUID_ORIGINAL, this.CurrencyConversion, workpackSuspensionPeriod, BaselineItemTotalUnits, BaselineItemTotalCosts);
                        reportableObject.NonCumulative_OriginalDataPoints = new ObservableCollection<ProgressInfo>(plannedDataPoints);
                    }
                    else
                    {
                        DateTime modifiedEndDateToUse = baselineItem.WORKPACK.ENDDATE;
                        if (baselineItem.WORKPACK.FORECASTENDDATE != null)
                            modifiedEndDateToUse = (DateTime)baselineItem.WORKPACK.FORECASTENDDATE;

                        TimeSpan workingModifiedTimeSpan = modifiedEndDateToUse - baselineItem.WORKPACK.STARTDATE;
                        if (baselineItem.WORKPACK.FORECASTSTARTDATE != null && ((DateTime)baselineItem.WORKPACK.FORECASTSTARTDATE) > baselineItem.WORKPACK.ENDDATE)
                            workpackSuspensionPeriod.Add(new Period(baselineItem.WORKPACK.ENDDATE.AddDays(1), (DateTime)baselineItem.WORKPACK.FORECASTSTARTDATE));

                        //Used to show sharktooth on variation
                        plannedDataPoints = ISupportProgressReportingExtensions.DataPointsGenerator(SummaryObject, workingModifiedTimeSpan, BaselineItemBaseUnits, BaselineItemBaseCosts, baselineItem.WORKPACK.STARTDATE, baselineItem.GUID_ORIGINAL, this.CurrencyConversion, workpackSuspensionPeriod, null, null, reportableObject.Cumulative_VariationAdjustments);

                        //Used to show normalized variation
                        //plannedDataPoints = DataPointsGenerator(WorkingPeriod, progressInterval, BaselineItemTotalUnits, BaselineItemTotalCosts, this.CurrencyConversion, baselineItem.WORKPACK.STARTDATE, firstAlignedDataDate, baselineItem.GUID_ORIGINAL);
                        reportableObject.NonCumulative_PlannedDataPoints = new ObservableCollection<ProgressInfo>(plannedDataPoints);
                    }
                }
            }

            if (fromOriginalBaseline)
                SummaryObject.NonCumulative_OriginalDataPoints = new ObservableCollection<ProgressInfo>(SummaryObject.ReportableObjects.SelectMany(progressItem => progressItem.NonCumulative_OriginalDataPoints));
            else
                SummaryObject.NonCumulative_PlannedDataPoints = new ObservableCollection<ProgressInfo>(SummaryObject.ReportableObjects.SelectMany(progressItem => progressItem.NonCumulative_PlannedDataPoints));
        }

        /// <summary>
        /// Try to generate non-cumulative data points from P6 TASKs repository
        /// </summary>
        /// <param name="progressItem">current progress item to generate against, also populate progressItem nonCumulative datapoints collection</param>
        /// <param name="p6ScheduleTasks">context P6 tasks</param>
        /// <param name="firstAlignedDataDate">universal chart first aligned data date</param>
        /// <param name="progressInterval">period iteration interval</param>
        /// <param name="this.CurrencyConversion">currency conversion factor</param>
        /// <param name="nonCumulativeP6DataPoints">current progress item non cumulative data points</param>
        /// <returns>is generation success</returns>
        private bool TryBuildP6DataPoints(P6Data.PROJECT P6PROJECT, IEnumerable<TASK> P6TASKS, ReportableObject reportableObject, DataPointsType processingType, WorkpackAssignmentLoadType assignmentLoadType, out List<ProgressInfo> nonCumulativeP6DataPoints)
        {
            nonCumulativeP6DataPoints = new List<ProgressInfo>();
            if (reportableObject.BASELINE_ITEMJoinRATE.BASELINE_ITEM.WORKPACK == null)
                return false;

            IEnumerable<WORKPACK_ASSIGNMENT> FilteredWORKPACK_ASSIGNMENTS;
            if (assignmentLoadType == WorkpackAssignmentLoadType.Modified)
            {
                FilteredWORKPACK_ASSIGNMENTS = reportableObject.BASELINE_ITEMJoinRATE.BASELINE_ITEM.WORKPACK.WORKPACK_ASSIGNMENTS.Where(assignment => assignment.ISMODIFIEDBASELINE == true);
                if (FilteredWORKPACK_ASSIGNMENTS.Count() == 0)
                    FilteredWORKPACK_ASSIGNMENTS = reportableObject.BASELINE_ITEMJoinRATE.BASELINE_ITEM.WORKPACK.WORKPACK_ASSIGNMENTS; //try to get original if modified is empty
            }
            else
                FilteredWORKPACK_ASSIGNMENTS = reportableObject.BASELINE_ITEMJoinRATE.BASELINE_ITEM.WORKPACK.WORKPACK_ASSIGNMENTS.Where(assignment => assignment.ISMODIFIEDBASELINE == false);

            if (P6PROJECT != null && FilteredWORKPACK_ASSIGNMENTS != null && P6TASKS != null && FilteredWORKPACK_ASSIGNMENTS.Count() != 0 && P6TASKS.Count() != 0)
            {
                DateTime? lastRecalcDate = P6PROJECT.last_recalc_date;
                BASELINE_ITEM baselineItem = reportableObject.BASELINE_ITEMJoinRATE.BASELINE_ITEM;
                decimal progressItemTotalHours = baselineItem.TOTAL_HOURS;
                decimal progressItemTotalCosts = baselineItem.TOTAL_COSTS;

                foreach (var WORKPACK_ASSIGNMENTS in FilteredWORKPACK_ASSIGNMENTS)
                {
                    decimal CurrentAssignmentUnits;
                    decimal CurrentAssignmentCosts;
                    decimal CurrentAssignmentMaxUnits;
                    decimal CurrentAssignmentMinUnits;
                    DateTime CurrentAssignmentStartDate;
                    TimeSpan CurrentAssignmentWorkingPeriod;
                    TASK currentAssignmentTASK = P6TASKS.FirstOrDefault(task => task.task_code == WORKPACK_ASSIGNMENTS.P6_ACTIVITYID);

                    if (processingType == DataPointsType.Planned)
                    {
                        //routine failed so report to revert to workpack dates calculation
                        if (currentAssignmentTASK == null || currentAssignmentTASK.target_start_date == null)
                            return false;

                        CurrentAssignmentStartDate = (DateTime)currentAssignmentTASK.target_start_date;
                        CurrentAssignmentWorkingPeriod = (DateTime)currentAssignmentTASK.target_end_date - (DateTime)currentAssignmentTASK.target_start_date;
                        CurrentAssignmentMaxUnits = progressItemTotalHours;
                        CurrentAssignmentMinUnits = WORKPACK_ASSIGNMENTS.LOW_VALUE;
                    }
                    else if (processingType == DataPointsType.Earned)
                    {
                        CurrentAssignmentMaxUnits = reportableObject.TOTAL_EARNED_UNITS;
                        CurrentAssignmentMinUnits = WORKPACK_ASSIGNMENTS.LOW_VALUE;
                        if (WORKPACK_ASSIGNMENTS.LOW_VALUE > CurrentAssignmentMaxUnits)
                            continue;

                        if (currentAssignmentTASK.act_work_qty == null || currentAssignmentTASK.act_start_date == null || (currentAssignmentTASK.act_end_date == null && lastRecalcDate == null))
                            return false;

                        CurrentAssignmentStartDate = (DateTime)currentAssignmentTASK.act_start_date;
                        if (currentAssignmentTASK.act_end_date == null)
                            CurrentAssignmentWorkingPeriod = (DateTime)lastRecalcDate - (DateTime)currentAssignmentTASK.act_start_date;
                        else
                            CurrentAssignmentWorkingPeriod = (DateTime)currentAssignmentTASK.act_end_date - (DateTime)currentAssignmentTASK.act_start_date;
                    }
                    else
                    {
                        if (currentAssignmentTASK.early_start_date == null || currentAssignmentTASK.early_end_date == null)
                            return false;

                        if (WORKPACK_ASSIGNMENTS.HIGH_VALUE < reportableObject.TOTAL_EARNED_UNITS)
                            continue;

                        CurrentAssignmentMaxUnits = progressItemTotalHours;
                        decimal earnedUnits = reportableObject.TOTAL_EARNED_UNITS;
                        if (earnedUnits > WORKPACK_ASSIGNMENTS.LOW_VALUE)
                            CurrentAssignmentMinUnits = earnedUnits + 1;
                        else
                            CurrentAssignmentMinUnits = WORKPACK_ASSIGNMENTS.LOW_VALUE;

                        CurrentAssignmentStartDate = (DateTime)currentAssignmentTASK.early_start_date;
                        CurrentAssignmentWorkingPeriod = (DateTime)currentAssignmentTASK.early_end_date - (DateTime)currentAssignmentTASK.early_start_date;
                    }

                    if (WORKPACK_ASSIGNMENTS.HIGH_VALUE > CurrentAssignmentMaxUnits)
                        CurrentAssignmentUnits = (CurrentAssignmentMaxUnits - CurrentAssignmentMinUnits) + 1;
                    else
                        CurrentAssignmentUnits = (WORKPACK_ASSIGNMENTS.HIGH_VALUE - CurrentAssignmentMinUnits) + 1;

                    //use assignment units instead of estimated units because we are working on a subset of total units, also, this cost will be processed by conversion later
                    CurrentAssignmentCosts = CurrentAssignmentUnits * reportableObject.BASELINE_ITEMJoinRATE.ITEMRATE;
                    nonCumulativeP6DataPoints.AddRange(ISupportProgressReportingExtensions.DataPointsGenerator(SummaryObject, CurrentAssignmentWorkingPeriod, CurrentAssignmentUnits, CurrentAssignmentCosts, CurrentAssignmentStartDate, baselineItem.GUID_ORIGINAL, this.CurrencyConversion, null, null, null));
                }

                return true;
            }
            else
                return false;
        }

        public override void BuildVariationDataPoints()
        {
            var VARIATIONS = BluePrintsUnitOfWork.VARIATIONS.Where(x => x.GUID_PROJECT == SummaryObject.LivePROGRESS.GUID_PROJECT && x.APPROVED != null && x.ORIBASELINE != null && x.TOBASELINE != null).Include(x => x.ORIBASELINE.BASELINE_ITEMS).Include(x => x.TOBASELINE.BASELINE_ITEMS);
            var RATES = BluePrintsUnitOfWork.RATES.Where(x => x.GUID_PROJECT == SummaryObject.LivePROGRESS.GUID_PROJECT);

            ObservableCollection<VariationAdjustment> approvedVariation = new ObservableCollection<VariationAdjustment>();
            foreach (VARIATION VARIATION in VARIATIONS)
            {
                DateTime? approvedDate = VARIATION.APPROVED;
                if (VARIATION.GUID_PROJECT == SummaryObject.LivePROGRESS.GUID_PROJECT && approvedDate != null && VARIATION.ORIBASELINE != null && VARIATION.TOBASELINE != null)
                {
                    IEnumerable<BASELINE_ITEM> contextBASELINE_ITEMS = VARIATION.ORIBASELINE.BASELINE_ITEMS.Concat(VARIATION.TOBASELINE.BASELINE_ITEMS);

                    foreach (VARIATION_ITEM VARIATION_ITEM in VARIATION.VARIATION_ITEMS)
                    {
                        if (VARIATION_ITEM.ACTION != VariationAction.Add && VARIATION_ITEM.ACTION != VariationAction.Append)
                            continue;

                        var contextBASELINE_ITEM = contextBASELINE_ITEMS.FirstOrDefault(x => x.GUID_ORIGINAL == VARIATION_ITEM.GUID_ORIBASEITEM);
                        RATE RATE;
                        if (contextBASELINE_ITEM != null)
                        {
                            RATE = RATES.FirstOrDefault(x => x.GUID_DEPARTMENT == contextBASELINE_ITEM.GUID_DEPARTMENT && x.GUID_DISCIPLINE == contextBASELINE_ITEM.GUID_DISCIPLINE);
                            if (RATE != null)
                                approvedVariation.Add(new VariationAdjustment()
                                {
                                    AdjustmentDate = (DateTime)approvedDate,
                                    AdjustmentUnits = VARIATION_ITEM.VARIATION_UNITS,
                                    AdjustmentRate = (decimal)RATE.RATE1,
                                    BaselineItemGuid = contextBASELINE_ITEM.GUID_ORIGINAL
                                });
                        }
                    }
                }
            }

            foreach (ReportableObject ReportableObject in SummaryObject.ReportableObjects)
            {
                ReportableObject.NonCumulative_VariationAdjustments = new ObservableCollection<VariationAdjustment>(approvedVariation.Where(variation => variation.BaselineItemGuid == ReportableObject.BASELINE_ITEMJoinRATE.BASELINE_ITEM.GUID_ORIGINAL).ToList());
            }

            SummaryObject.NonCumulative_VariationAdjustments = new ObservableCollection<VariationAdjustment>(approvedVariation);
        }

        /// <summary>
        /// Calculates each baselineItem earned data point while populating aggregate non cumulative earned data points
        /// </summary>
        /// <returns>Non cumulative earned progress data points</returns>
        public override void BuildEarnedDataPoints()
        {
            foreach (ReportableObject reportableObject in SummaryObject.ReportableObjects)
            {
                //Assign the report date for stats display
                reportableObject.ReportingDataDate = SummaryObject.ReportingDataDate;
                BASELINE_ITEM baselineItem = reportableObject.BASELINE_ITEMJoinRATE.BASELINE_ITEM;

                List<ProgressInfo> progressItemP6DataPoints;
                if (this.SummaryObject.LivePROGRESS.P6PROGRESS_NAME != null && this.SummaryObject.LivePROGRESS.P6PROGRESS_NAME != string.Empty)
                    this.PROGRESS_TASKS = GetP6ScheduleTasks(summaryObject.LivePROGRESS.P6PROGRESS_NAME, out this.PROGRESS_PROJECT);
                
                bool isProgressDataDateMatch = (this.PROGRESS_PROJECT != null && this.PROGRESS_PROJECT.last_recalc_date != null && ((DateTime)this.PROGRESS_PROJECT.last_recalc_date).Date == SummaryObject.LivePROGRESS.DATA_DATE);

                if (isProgressDataDateMatch && TryBuildP6DataPoints(this.PROGRESS_PROJECT, this.PROGRESS_TASKS, reportableObject, DataPointsType.Earned, WorkpackAssignmentLoadType.Modified, out progressItemP6DataPoints))
                {
                    reportableObject.NonCumulative_EarnedDataPoints = new ObservableCollection<ProgressInfo>(progressItemP6DataPoints);
                    reportableObject.isDataPointsGeneratedFromP6 = true;
                }

                else
                {
                    IQueryable<ProgressInfo> progressItemEarnedDataPoints = reportableObject.PROGRESS_ITEMSUpToCurrentDate.Select(x => new ProgressInfo()
                    {
                        BudgetedUnits = reportableObject.BASELINE_ITEMJoinRATE.BASELINE_ITEM.TOTAL_HOURS,
                        BudgetedCosts = reportableObject.BASELINE_ITEMJoinRATE.BASELINE_ITEM.TOTAL_COSTS * this.CurrencyConversion,
                        BaselineItemGuid = reportableObject.BASELINE_ITEMJoinRATE.BASELINE_ITEM.GUID_ORIGINAL,
                        Units = x.EARNED_UNITS,
                        Costs = x.EARNED_UNITS * reportableObject.BASELINE_ITEMJoinRATE.ITEMRATE * this.CurrencyConversion,
                        ProgressDate = x.EARNED_DATE,
                    }).AsQueryable();

                    reportableObject.NonCumulative_EarnedDataPoints = new ObservableCollection<ProgressInfo>(progressItemEarnedDataPoints);
                }
            }

            SummaryObject.NonCumulative_EarnedDataPoints = new ObservableCollection<ProgressInfo>(SummaryObject.ReportableObjects.SelectMany(progressItem => progressItem.NonCumulative_EarnedDataPoints));
        }

        public override void BuildRemainingDataPoints()
        {
            BuildProductivity();
            //Establishing aligned week ending dates
            List<DateTime> alignedWeekEndingDates = ISupportProgressReportingExtensions.GenerateAlignedDatesCollection(SummaryObject.FirstAlignedDataDate, SummaryObject.FirstAlignedDataDate.AddYears(1), SummaryObject.IntervalPeriod);

            IQueryable<ProgressInfo> progressItemsEarnedDataPointsBeforeDataDate = SummaryObject.ReportableObjects.SelectMany(progressItem => progressItem.NonCumulative_EarnedDataPoints.Where(dataPoint => dataPoint.ProgressDate.Date <= SummaryObject.ReportingDataDate.Date)).AsQueryable();
            decimal totalEarnedUnits = progressItemsEarnedDataPointsBeforeDataDate.Sum(dataPoint => dataPoint.Units);
            if (totalEarnedUnits == 0)
                return;

            List<Period> exceptionPeriods = new List<Period>();
            exceptionPeriods.AddRange(ISupportProgressReportingExtensions.NonWorkingPeriods);

            foreach (ReportableObject reportableObject in SummaryObject.ReportableObjects)
            {
                //when remaining units is more than 0 continue calculation
                if (reportableObject.FuturePROGRESS_ITEMS_UNITS > 0)
                {
                    List<ProgressInfo> progressItemP6DataPoints;
                    if (reportableObject.isDataPointsGeneratedFromP6 && TryBuildP6DataPoints(this.PROGRESS_PROJECT, this.PROGRESS_TASKS, reportableObject, DataPointsType.Remaining, WorkpackAssignmentLoadType.Modified, out progressItemP6DataPoints))
                        reportableObject.NonCumulative_RemainingPlannedDataPoints = new ObservableCollection<ProgressInfo>(progressItemP6DataPoints);
                    else
                    {
                        DateTime startDateToUse;
                        DateTime firstAlignedWeekEndingDataDate;
                        decimal firstPeriodProRate;

                        if (reportableObject.BASELINE_ITEMJoinRATE.BASELINE_ITEM.WORKPACK.FORECASTSTARTDATE != null)
                            startDateToUse = (DateTime)reportableObject.BASELINE_ITEMJoinRATE.BASELINE_ITEM.WORKPACK.FORECASTSTARTDATE;
                        else
                            startDateToUse = reportableObject.BASELINE_ITEMJoinRATE.BASELINE_ITEM.WORKPACK.STARTDATE;

                        //when workpack dates are later than data date use workpack dates but have a prorate value ready for first period
                        if (startDateToUse > SummaryObject.LivePROGRESS.DATA_DATE)
                        {
                            firstAlignedWeekEndingDataDate = alignedWeekEndingDates.FirstOrDefault(dates => dates.Date >= startDateToUse);
                            firstPeriodProRate = Convert.ToDecimal((firstAlignedWeekEndingDataDate.AddSeconds(1) - startDateToUse).TotalDays / SummaryObject.IntervalPeriod.TotalDays);
                        }
                        else
                        {
                            firstAlignedWeekEndingDataDate = SummaryObject.LivePROGRESS.DATA_DATE.AddDays(SummaryObject.IntervalPeriod.Days);
                            firstPeriodProRate = 1;
                        }

                        decimal maxInefficiency = 0.5M;

                        decimal currentEfficiency = (reportableObject.ActualProductivity / reportableObject.BaselineProductivity);

                        reportableObject.NonCumulative_RemainingPlannedDataPoints = ISupportProgressReportingExtensions.RemainingDataPointsGenerator(SummaryObject, reportableObject, firstAlignedWeekEndingDataDate, exceptionPeriods, reportableObject.FuturePROGRESS_ITEMS_UNITS, reportableObject.BaselineProductivity, this.CurrencyConversion, firstPeriodProRate);

                        //if there's a planned finish date based on baseline productivity, inflate periodic units/costs
                        DateTime? plannedLimitDate = (reportableObject.NonCumulative_RemainingPlannedDataPoints == null || reportableObject.NonCumulative_RemainingPlannedDataPoints.Count == 0) ? (DateTime?)null : reportableObject.NonCumulative_RemainingPlannedDataPoints.Last().ProgressDate;

                        if (currentEfficiency < maxInefficiency)
                            currentEfficiency = maxInefficiency;

                        decimal inflatedInefficientUnits = currentEfficiency > 0 ? (reportableObject.FuturePROGRESS_ITEMS_UNITS / currentEfficiency) : reportableObject.FuturePROGRESS_ITEMS_UNITS;

                        reportableObject.NonCumulative_RemainingCurrentDataPoints = ISupportProgressReportingExtensions.RemainingDataPointsGenerator(SummaryObject, reportableObject, firstAlignedWeekEndingDataDate, exceptionPeriods, inflatedInefficientUnits, reportableObject.ActualProductivity, this.CurrencyConversion, firstPeriodProRate, plannedLimitDate);
                    }
                }
            }

            //extract all data points out to be used as an overall summary
            SummaryObject.NonCumulative_RemainingPlannedDataPoints = new ObservableCollection<ProgressInfo>(SummaryObject.ReportableObjects.SelectMany(progressItem => progressItem.NonCumulative_RemainingPlannedDataPoints));
            SummaryObject.NonCumulative_RemainingCurrentDataPoints = new ObservableCollection<ProgressInfo>(SummaryObject.ReportableObjects.SelectMany(progressItem => progressItem.NonCumulative_RemainingCurrentDataPoints));
        }

        /// <summary>
        /// Populate all progress item productivity
        /// </summary>
        private void BuildProductivity()
        {
            //Establish exception periods
            List<Period> exceptionPeriods = new List<Period>();
            exceptionPeriods.AddRange(ISupportProgressReportingExtensions.NonWorkingPeriods);
            foreach (ReportableObject reportableItem in SummaryObject.ReportableObjects)
            {
                //when remaining units is more than 0 continue calculation
                if (reportableItem.FuturePROGRESS_ITEMS_UNITS > 0)
                    BuildReportableObjectProductivity(reportableItem, exceptionPeriods);
            }
        }

        private void BuildReportableObjectProductivity(ReportableObject reportableObject, List<Period> exceptionPeriods)
        {
            decimal remainingUnitsAfterDataDate = reportableObject.BASELINE_ITEMJoinRATE.BASELINE_ITEM.TOTAL_HOURS - reportableObject.TOTAL_EARNED_UNITS;
            //When productivity is below this threshold, escalate to workpack or project
            decimal minimumProductivityBeforeEscalating = 0.001M;

            //establish dates for productivity assessment
            DateTime workpackStart = reportableObject.BASELINE_ITEMJoinRATE.BASELINE_ITEM.WORKPACK.STARTDATE;
            DateTime workpackEnd = reportableObject.BASELINE_ITEMJoinRATE.BASELINE_ITEM.WORKPACK.ENDDATE;
            DateTime? workpackForecastStart = reportableObject.BASELINE_ITEMJoinRATE.BASELINE_ITEM.WORKPACK.FORECASTSTARTDATE;
            DateTime? workpackForecastEnd = reportableObject.BASELINE_ITEMJoinRATE.BASELINE_ITEM.WORKPACK.FORECASTENDDATE;

            DateTime assessmentStartDate;
            DateTime assessmentEndDate;

            if (workpackForecastStart != null)
                assessmentStartDate = (DateTime)workpackForecastStart;
            else
                assessmentStartDate = workpackStart;

            if (workpackForecastEnd != null)
                assessmentEndDate = (DateTime)workpackForecastEnd;
            else
                assessmentEndDate = workpackEnd;

            if (reportableObject.ReportingDataDate > assessmentStartDate)
                assessmentStartDate = reportableObject.ReportingDataDate;
            if (reportableObject.ReportingDataDate > assessmentEndDate)
                assessmentEndDate = reportableObject.ReportingDataDate;

            Period assessmentPeriod = new Period(assessmentStartDate.Date, assessmentEndDate.Date);

            //establish workpack productivity to be used when deliverable productivity is too low
            WORKPACK progressItemWorkpack = reportableObject.BASELINE_ITEMJoinRATE.BASELINE_ITEM.WORKPACK;

            //calculate deliverable productivity
            reportableObject.VariationProductivity = ISupportProgressReportingExtensions.CalculatePlannedProductivity(assessmentPeriod, exceptionPeriods, reportableObject.BASELINE_ITEMJoinRATE.BASELINE_ITEM.TOTAL_HOURS);
            //progressItem.ProgressItem_CurrentProductivity = UnifiedCalculationMethods.CalculateProductivity(assessmentPeriod)

            decimal workpackVarProductivity = 0;
            if (reportableObject.VariationProductivity < minimumProductivityBeforeEscalating)
            {
                //not checking for progressItemWorkpack null because all progress item should have workpacks assigned if the user 
                decimal totalWorkpackUnits = (progressItemWorkpack == null || progressItemWorkpack.BASELINE_ITEMS == null) ? 0 : progressItemWorkpack.BASELINE_ITEMS.Sum(pItem => pItem.TOTAL_HOURS);

                workpackVarProductivity = ISupportProgressReportingExtensions.CalculatePlannedProductivity(assessmentPeriod, exceptionPeriods, totalWorkpackUnits);
                if (workpackVarProductivity > 0)
                    reportableObject.VariationProductivity = workpackVarProductivity;
            }

            decimal workpackBaseProductivity = 0;
            //not checking for progressItemWorkpack null because all progress item should have workpacks assigned if the user 
            decimal totalWorkpackBudgetedUnits = (progressItemWorkpack == null || progressItemWorkpack.BASELINE_ITEMS == null) ? 0 : progressItemWorkpack.BASELINE_ITEMS.Sum(pItem => pItem.ESTIMATED_HOURS);
            workpackBaseProductivity = ISupportProgressReportingExtensions.CalculatePlannedProductivity(assessmentPeriod, exceptionPeriods, remainingUnitsAfterDataDate);

            if (reportableObject.BASELINE_ITEMJoinRATE.BASELINE_ITEM.ESTIMATED_HOURS == 0)
                reportableObject.BaselineProductivity = workpackBaseProductivity;
            else
            {
                reportableObject.BaselineProductivity = ISupportProgressReportingExtensions.CalculatePlannedProductivity(assessmentPeriod, exceptionPeriods, remainingUnitsAfterDataDate);
                //apply normalized productivity for unusually low calculated productivity
                if (reportableObject.BaselineProductivity < minimumProductivityBeforeEscalating)
                {
                    if (workpackBaseProductivity > 0)
                        reportableObject.BaselineProductivity = workpackBaseProductivity;
                }
            }

            List<ProgressInfo> deliverablePlannedDataPointsOnOrBeforeDataDate = reportableObject.NonCumulative_PlannedDataPoints.Where(dataPoint => dataPoint.ProgressDate <= SummaryObject.LivePROGRESS.DATA_DATE).ToList();
            decimal currentEfficiency = 0;
            if (reportableObject.TOTAL_EARNED_UNITS != 0 && deliverablePlannedDataPointsOnOrBeforeDataDate.Count() > 0)
            {
                decimal deliverablePlannedUnitsOnOrBeforeDataDate = deliverablePlannedDataPointsOnOrBeforeDataDate.Sum(dataPoint => dataPoint.Units);
                if (deliverablePlannedUnitsOnOrBeforeDataDate > 0)
                    currentEfficiency = reportableObject.TOTAL_EARNED_UNITS / deliverablePlannedUnitsOnOrBeforeDataDate;

                reportableObject.ActualProductivity = reportableObject.BaselineProductivity * currentEfficiency;
            }
            else
                reportableObject.ActualProductivity = reportableObject.BaselineProductivity; //assume productivity of 1 because there are no units to measure against

            if (reportableObject.ActualProductivity < minimumProductivityBeforeEscalating)
                reportableObject.ActualProductivity = reportableObject.BaselineProductivity;
        }

        /// <summary>
        /// Calculates each baselineItem burned/actual data point while populating aggregate non cumulative burned/actual data points
        /// </summary>
        /// <returns>Non cumulative earned progress data points</returns>
        public override void BuildBurnedDataPoints()
        {
            ObservableCollection<ProgressInfo> nonCumulative_BurnedDataPoints = new ObservableCollection<ProgressInfo>();
            DateTime firstAlignedDataDate = SummaryObject.FirstAlignedDataDate;
            TimeSpan progressInterval = SummaryObject.IntervalPeriod;
            DateTime loopDate = firstAlignedDataDate;

            IEnumerable<WORKPACK> WORKPACKS = summaryObject.LiveBASELINE.PROJECT.WORKPACKS;
            IEnumerable<string> qualifiedWorkpack = WORKPACKS == null ? new List<string>() : WORKPACKS.Select(x => x.INTERNAL_NAME1);
            var PrimeroUnitOfWork = PrimeroEntitiesUnitOfWorkSource.GetUnitOfWorkFactory().CreateUnitOfWork();
            var jobTransactions = from JOBTRANS in PrimeroUnitOfWork.JOB_TRANSACTIONS
                                  join JOBCOST_HDR2 in PrimeroUnitOfWork.JOBCOST_HDR
                                  on JOBTRANS.MASTER_JOBNO equals JOBCOST_HDR2.JOBNO
                                  join JOBCOST_HDR1 in PrimeroUnitOfWork.JOBCOST_HDR
                                  on JOBTRANS.JOBNO equals JOBCOST_HDR1.JOBNO
                                  join JOBCOST_RESOURCE in PrimeroUnitOfWork.JOBCOST_RESOURCE
                                  on JOBTRANS.STAFFNO equals JOBCOST_RESOURCE.SEQNO
                                  where JOBCOST_HDR2.JOBCODE == SummaryObject.LiveBASELINE.PROJECT.NUMBER && JOBTRANS.TRANSTYPE == "T"
                                  select new { JOBCOST_HDR1.JOBCODE, JOBTRANS.QUANTITY, JOBTRANS.LINETOTAL, JOBTRANS.LINECOST, JOBTRANS.TRANSDATE, JOBCOST_RESOURCE.RESOURCENAME };

            var jobTransactionsList = jobTransactions.ToList();
            if (jobTransactionsList.Count == 0)
                return;

            List<DateTime> alignedDataDates = ISupportProgressReportingExtensions.GenerateAlignedDatesCollection(firstAlignedDataDate, jobTransactionsList.Max(x => x.TRANSDATE).Value, progressInterval);
            foreach (var jobTransaction in jobTransactionsList)
            {
                if (qualifiedWorkpack.Contains(jobTransaction.JOBCODE))
                    nonCumulative_BurnedDataPoints.Add(new ProgressInfo()
                    {
                        BudgetedUnits = 0,
                        BudgetedCosts = 0,
                        Units = (decimal)jobTransaction.QUANTITY,
                        Costs = (decimal)jobTransaction.LINETOTAL * this.CurrencyConversion,
                        Actuals = (decimal)jobTransaction.LINECOST,
                        ProgressDate = alignedDataDates.FirstOrDefault(dates => dates.Date >= jobTransaction.TRANSDATE),
                        BaselineItemGuid = Guid.Empty,
                        WorkpackName = jobTransaction.JOBCODE,
                        ResourceName = jobTransaction.RESOURCENAME,
                        Quantity = (decimal)jobTransaction.QUANTITY
                    });
            }

            SummaryObject.NonCumulative_BurnedDataPoints = nonCumulative_BurnedDataPoints;
        }

        public override void BuildActualDataPoints()
        {
            List<ProgressInfo> convertBurnedToActualDataPoints = new List<ProgressInfo>();
            SummaryObject.NonCumulative_BurnedDataPoints.ToList().ForEach(dataPoint => convertBurnedToActualDataPoints.Add(new ProgressInfo()
            {
                BudgetedCosts = dataPoint.BudgetedCosts,
                BudgetedUnits = dataPoint.BudgetedUnits,
                Costs = dataPoint.Actuals,
                Actuals = dataPoint.Actuals,
                ProgressDate = dataPoint.ProgressDate,
                BaselineItemGuid = dataPoint.BaselineItemGuid,
                Units = dataPoint.Units,
                WorkpackGuid = dataPoint.WorkpackGuid,
                WorkpackName = dataPoint.WorkpackName,
                ResourceName = dataPoint.ResourceName,
                Quantity = dataPoint.Quantity
            }));

            SummaryObject.NonCumulative_ActualDataPoints = new ObservableCollection<ProgressInfo>(convertBurnedToActualDataPoints);
        }

        private IEnumerable<TASK> GetP6ScheduleTasks(string shortName, out P6Data.PROJECT P6Schedule)
        {
            if (shortName != string.Empty)
            {
                var PROJECTRepository = this.P6UnitOfWork.PROJECT;
                P6Schedule = PROJECTRepository.FirstOrDefault(x => x.proj_short_name == shortName);

                if (P6Schedule != null)
                {
                    return P6Schedule.TASK;
                }
            }
            else
                P6Schedule = null;

            return null;
        }

        private enum DataPointsType
        {
            Planned = 0,
            Earned = 1,
            Remaining = 2
        }

        private enum WorkpackAssignmentLoadType
        {
            Original,
            Modified,
            Both
        }
    }
}
