﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.UserDesigner;
using DevExpress.XtraReports.UI;
using System.IO;
using BluePrints.ViewModels;
using BluePrints.Data.Helpers;
using BluePrints.Data;
using BluePrints.Common;
using BluePrints.Reports;
using BluePrints.BluePrintsEntitiesDataModel;
using System.Windows.Threading;
using BluePrints.Common.ViewModel;

namespace BluePrints.Views
{
    public partial class REPORTDesigner : DevExpress.XtraEditors.XtraForm
    {
        XtraReport currentREPORT;
        PROJECT currentPROJECT;
        PROJECT_REPORT currentPROJECT_REPORT;
        ReportType currentReportType;
        CollectionViewModel<PROJECT_REPORT, PROJECT_REPORT, Guid, IBluePrintsEntitiesUnitOfWork> collectionViewModel;
        public REPORTDesigner(CollectionViewModel<PROJECT_REPORT, PROJECT_REPORT, Guid, IBluePrintsEntitiesUnitOfWork> collectionViewModel, ReportType currentReportType)
        {
            InitializeComponent();
            this.currentReportType = currentReportType;
            this.collectionViewModel = collectionViewModel;

            reportDesigner1.DesignPanelLoaded += new DesignerLoadedEventHandler(reportDesigner1_DesignPanelLoaded);
        }

        void reportDesigner1_DesignPanelLoaded(object sender, DesignerLoadedEventArgs e)
        {
            XRDesignPanel panel = (XRDesignPanel)sender;
            panel.AddCommandHandler(new SaveCommandHandler(panel, SaveReport));
        }

        private void REPORTDesigner_Load(object sender, EventArgs e)
        {
            currentPROJECT_REPORT = collectionViewModel.Entities.First();
            if (currentPROJECT_REPORT != null)
            {
                using (StreamWriter sw = new StreamWriter(new MemoryStream()))
                {
                    sw.Write(currentPROJECT_REPORT.REPORT);
                    sw.Flush();
                    currentREPORT = XtraReport.FromStream(sw.BaseStream, true);
                }
            }
            else
            {
                if (currentReportType == ReportType.Progress_Report)
                    currentREPORT = new XtraReportPROGRESS_ITEMS();
            }

            reportDesigner1.OpenReport(currentREPORT);
        }

        private void SaveReport()
        {
            if (currentREPORT == null)
                return;

            // Save the report to a stream.
            MemoryStream ms = new MemoryStream();
            currentREPORT.SaveLayout(ms);

            // Prepare the stream for reading.
            ms.Position = 0;
            // Insert the report to a database.
            using (StreamReader sr = new StreamReader(ms))
            {

                // Read the report from the stream to a string variable.
                string reportString = sr.ReadToEnd();
                if (currentPROJECT_REPORT == null)
                {
                    currentPROJECT_REPORT = new PROJECT_REPORT();
                    currentPROJECT_REPORT.GUID_PROJECT = currentPROJECT.GUID;
                    currentPROJECT_REPORT.REPORT_TYPE = currentReportType.ToString();
                    currentPROJECT_REPORT.REPORT = reportString;
                }
                else
                    currentPROJECT_REPORT.REPORT = reportString;

                collectionViewModel.Save(currentPROJECT_REPORT);
            }
        }

        public class SaveCommandHandler : DevExpress.XtraReports.UserDesigner.ICommandHandler
        {
            XRDesignPanel panel;
            public delegate void SaveCommandDelegate();
            public SaveCommandDelegate SaveDelegate;

            public SaveCommandHandler(XRDesignPanel panel, SaveCommandDelegate ReportSaveDelegate)
            {
                this.panel = panel;
                SaveDelegate = ReportSaveDelegate;
            }

            public void HandleCommand(DevExpress.XtraReports.UserDesigner.ReportCommand command,
            object[] args)
            {
                // Save the report.
                Save();
            }

            public bool CanHandleCommand(DevExpress.XtraReports.UserDesigner.ReportCommand command,
            ref bool useNextHandler)
            {
                useNextHandler = !(command == ReportCommand.SaveFile);
                return !useNextHandler;
            }

            void Save()
            {
                // For instance:
                if (SaveDelegate != null)
                    SaveDelegate();

                // Prevent the "Report has been changed" dialog from being shown.
                //panel.ReportState = ReportState.Saved;
            }
        }

        private void barButtonResetTemplate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (currentReportType == ReportType.Progress_Report)
                currentREPORT = new XtraReportPROGRESS_ITEMS();

            reportDesigner1.OpenReport(currentREPORT);
        }
    }
}