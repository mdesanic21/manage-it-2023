using BusinessLogicLayer.Generator;
using BusinessLogicLayer.Services;
using DataAccessLayer.Models;
using EntitiesLayer.Entities;
using QuestPDF.Fluent;
using System;
using System.IO;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using NuGet.Packaging;


namespace ManageIT.MainActivity
{
    /// <remarks>
    /// Matej Desanić
    /// </remarks>
    public partial class UcReports : UserControl
    {
        private int ID_Worker { get; set; }
        public ObservableCollection<string> ReportList { get; set; }
        Worker currentWorker = new Worker();
        ReportService reportService = new ReportService();
        WorkerService workerService = new WorkerService();
        public UcReports(Worker worker)
        {
            ID_Worker = 0;
            DataContext = this;
            ReportList = new ObservableCollection<string>(reportService.GetAllReports());
            currentWorker = worker;
            InitializeComponent();
            chkSelectAll.IsChecked = true;
            LoadWorkers();
        }

        private void RefreshList()
        {
            DataContext = this;
            ReportList.Clear(); 
            ReportList.AddRange(reportService.GetAllReports());
        }
        private void btnGenerateReport_Click(object sender, RoutedEventArgs e)
        {
            // ID_Worker is used to generate reports based on the worker id; if the ID_Worker is 0, report is generated for all the workers for a specific time period.
            ID_Worker = 0;
            Worker selectedWorker = cmbWorkers.SelectedItem as Worker;
            if(selectedWorker != null)
            {
                ID_Worker = selectedWorker.ID_worker;
            }
            DateTime fromDate = (DateTime)dtpStartDate.SelectedDate;
            DateTime endDate = (DateTime)dtpEndDate.SelectedDate;

            // Conditions to access the generation of the PDF; we need all this data to create a fully functional report
            if(dtpEndDate == null || dtpStartDate == null)
            {
                MessageBox.Show("You must select both dates to generate PDF.");
            }
            else if(fromDate < endDate)
            {
                if (!int.TryParse(txtReportID.Text, out int ID_Report))
                {
                    MessageBox.Show("Report ID is invalid!");
                    return;
                }
                List<ReportView> reportViewList = new List<ReportView>();
                ReportModel reportModel = new ReportModel();

                // Needed to bind both reportViewList and reportModel separately because I am using it in reportGenerator to forward all the data to it

                reportViewList = reportService.DefineListItem(fromDate, endDate, currentWorker, ID_Report, ID_Worker);
                reportModel = reportService.FillDataToModel(fromDate, endDate, currentWorker, ID_Report, ID_Worker);

                var report = new ReportGenerator(reportModel, reportViewList);

                // Important parts of the name, used to create different name for every report, and for the report to be easy to identify

                string formattedStartDate = fromDate.ToString("d.M.yyyy");
                string formattedFinishDate = endDate.ToString("d.M.yyyy");
                string date = DateTime.Now.ToString("d.M.yyyy");
                string time = DateTime.Now.ToString("HH.mm");


                var fileName = $"Report{reportModel.ID_Report}-IDWorker_{ID_Worker}-{date}_{time}-Start-{formattedStartDate}_Finish-{formattedFinishDate}.pdf";
                string filePath = Path.Combine("../../../BusinessLogicLayer/Reports", fileName);
                report.GeneratePdf(filePath);
                RefreshList();
            }
            else
            {
                MessageBox.Show("Your First day must be earlier than your Last day.");
            }
            
        }

        private void btnOpenReport_Click(object sender, RoutedEventArgs e)
        {
            Button btnOpenReport = (Button)sender;
            string reportName = btnOpenReport.Tag.ToString();
            reportService.OpenReport(reportName);
        }

        private void btnDeleteReport_Click(object sender, RoutedEventArgs e)
        {
            Button btnDeleteReport = (Button)sender;
            string reportName = btnDeleteReport.Tag.ToString();

            if (reportService.deleteReport(reportName))
            {
                MessageBox.Show("Succesfully deleted report!");
            }
            else MessageBox.Show("There are some problems. Plesae contact the IT support.");
            RefreshList();
        }

        private void LoadWorkers()
        {
            var workersForReport = workerService.GetAllWorkers();
            cmbWorkers.ItemsSource = workersForReport;
        }

        private void chkSelectAll_Checked(object sender, RoutedEventArgs e)
        {
            cmbWorkers.IsEnabled = false;
            cmbWorkers.SelectedIndex = -1;
            cmbWorkers.Background = System.Windows.Media.Brushes.Gray;
            ID_Worker = 0;
        }

        private void chkSelectAll_Unchecked(object sender, RoutedEventArgs e)
        {
            cmbWorkers.IsEnabled = true;
            cmbWorkers.Background = System.Windows.Media.Brushes.White;
        }


    }
}
