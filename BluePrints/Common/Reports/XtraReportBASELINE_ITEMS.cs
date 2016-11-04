﻿using System;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.Parameters;


namespace BluePrints.Reports
{
    public partial class XtraReportBASELINE_ITEMS : DevExpress.XtraReports.UI.XtraReport
    {
        public XtraReportBASELINE_ITEMS()
        {
            InitializeComponent();
            this.ParametersRequestSubmit += rptBaselineItem_ParametersRequestSubmit;
        }

        //public void AssignProperties(BaseViewModel_BaselineItem BaselineItem)
        //{
        //    objectDataSource1.DataSource = BaselineItem.Items;
        //    title1.Value = BaselineItem.Principal_Baseline.Baseline_Name;
        //    projectName.Value = BaselineItem.Principal_Project.Project_Name;
        //    date1.Value = DateTime.Now;
        //}

        #region Parameter Events
        private void rptBaselineItem_ParametersRequestSubmit(object sender, DevExpress.XtraReports.Parameters.ParametersRequestEventArgs e)
        {
            bool showHours = true;
            bool showCosts = true;
            bool showType = true;
            bool showDeliverableType = true;

            foreach (ParameterInfo info in e.ParametersInformation)
            {
                if (info.Parameter.Name == "showHours")
                    showHours = (bool)info.Parameter.Value;
                else if(info.Parameter.Name == "showCosts")
                    showCosts = (bool)info.Parameter.Value;

                if (info.Parameter.Name == "showType")
                    showType = (bool)info.Parameter.Value;

                if (info.Parameter.Name == "showDeliverableType")
                    showDeliverableType = (bool)info.Parameter.Value;
            }

            if(showType)
            {
                xrlblNumber.LocationF = new System.Drawing.PointF(356F, 0);

                if(showDeliverableType)
                {
                    xrlblDeliverableType.Visible = true;
                    xrlblDataDeliverableType.Visible = true;

                    xrlblDeliverableType.LocationF = new System.Drawing.PointF(506F, 0);
                    xrlblDataDeliverableType.LocationF = new System.Drawing.PointF(506F, 0);

                    xrlblTitle.LocationF = new System.Drawing.PointF(606F, 0);
                    xrlblDataTitle.LocationF = new System.Drawing.PointF(606F, 0);
                }
                else
                {
                    xrlblDeliverableType.Visible = false;
                    xrlblDataDeliverableType.Visible = false;

                    xrlblTitle.LocationF = new System.Drawing.PointF(506F, 0);
                    xrlblDataTitle.LocationF = new System.Drawing.PointF(506F, 0);
                }

                xrlblNumber.LocationF = new System.Drawing.PointF(356F, 0);
                xrlblDataNumber.LocationF = new System.Drawing.PointF(356F, 0);

                xrlblType.Visible = true;
                xrlblDataType.Visible = true;
            }
            else
            {
                if (showDeliverableType)
                {
                    xrlblDeliverableType.Visible = true;
                    xrlblDataDeliverableType.Visible = true;

                    xrlblDeliverableType.LocationF = new System.Drawing.PointF(410F, 0);
                    xrlblTitle.LocationF = new System.Drawing.PointF(510F, 0);

                    xrlblDataDeliverableType.LocationF = new System.Drawing.PointF(410F, 0);
                    xrlblDataTitle.LocationF = new System.Drawing.PointF(510F, 0);
                }
                else
                {
                    xrlblDeliverableType.Visible = false;
                    xrlblDataDeliverableType.Visible = false;

                    xrlblDeliverableType.LocationF = new System.Drawing.PointF(410F, 0);
                    xrlblDataDeliverableType.LocationF = new System.Drawing.PointF(410F, 0);

                    xrlblTitle.LocationF = new System.Drawing.PointF(410F, 0);
                    xrlblDataTitle.LocationF = new System.Drawing.PointF(410F, 0);
                }

                xrlblNumber.LocationF = new System.Drawing.PointF(260F, 0);
                xrlblDataNumber.LocationF = new System.Drawing.PointF(260F, 0);
                xrlblType.Visible = false;
                xrlblDataType.Visible = false;
            }

            if (showHours && showCosts)
            {
                xrlblTitle.WidthF = showType ? 199.06F : 295.06F;
                xrlblDataTitle.WidthF = showType ? 199.06F : 295.06F;
                xrlblHours.LocationF = new System.Drawing.PointF(805.06F, 0);
                xrlblDataHours.LocationF = new System.Drawing.PointF(805.06F, 0);
                xrlblTotalHours.LocationF = new System.Drawing.PointF(804.06F, 0);
                xrlblHours.Visible = true;
                xrlblDataHours.Visible = true;
                xrlblTotalHours.Visible = true;
                xrlblCosts.Visible = true;
                xrlblDataCosts.Visible = true;
                xrlblTotalCosts.Visible = true;
                xrlblTotalCosts.WidthF = 133.9366F;
                xrlblTotalCosts.LocationF = new System.Drawing.PointF(905.06F, 0);
                xrlblTotalCosts.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom;
            }
            else if(showHours)
            {
                xrlblTitle.WidthF = showType ? 332.1F : 428.1F;
                xrlblDataTitle.WidthF = showType ? 332.1F : 428.1F;

                xrlblHours.Visible = true;
                xrlblDataHours.Visible = true;
                xrlblTotalHours.Visible = true;
                xrlblHours.LocationF = new System.Drawing.PointF(939.06F, 0);
                xrlblDataHours.LocationF = new System.Drawing.PointF(939.06F, 0);
                xrlblTotalHours.LocationF = new System.Drawing.PointF(938.06F, 0);

                xrlblCosts.Visible = false;
                xrlblDataCosts.Visible = false;
                xrlblTotalCosts.Visible = false;
            }
            else if (showCosts)
            {
                xrlblTitle.WidthF = showType ? 299.06F : 395.06F;
                xrlblDataTitle.WidthF = showType ? 299.06F : 395.06F;

                xrlblHours.Visible = false;
                xrlblDataHours.Visible = false;
                xrlblTotalHours.Visible = false;
                xrlblCosts.Visible = true;
                xrlblDataCosts.Visible = true;
                xrlblTotalCosts.Visible = true;

                xrlblTotalCosts.LocationF = new System.Drawing.PointF(904.06F, 0);
                xrlblTotalCosts.WidthF = 134.9366F;
                xrlblTotalCosts.Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom;
            }
            else
            {
                xrlblTitle.WidthF = showType ? 432.1F : 528.1F;
                xrlblDataTitle.WidthF = showType ? 432.1F : 528.1F;
                xrlblHours.LocationF = new System.Drawing.PointF(805.06F, 0);
                xrlblDataHours.LocationF = new System.Drawing.PointF(805.06F, 0);
                xrlblHours.Visible = false;
                xrlblDataHours.Visible = false;
                xrlblTotalHours.Visible = false;
                xrlblCosts.Visible = false;
                xrlblDataCosts.Visible = false;
                xrlblTotalCosts.Visible = false;
            }

            if(!showDeliverableType)
            {
                xrlblTitle.WidthF += 100F;
                xrlblDataTitle.WidthF += 100F;
            }
        }
        #endregion
    }
}
