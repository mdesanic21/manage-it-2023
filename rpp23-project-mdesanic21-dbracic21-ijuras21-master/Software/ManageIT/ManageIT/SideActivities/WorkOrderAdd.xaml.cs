using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BusinessLogicLayer.Generator;
using BusinessLogicLayer.Services;
using EntitiesLayer.Entities;
using QuestPDF.Fluent;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using DataAccessLayer.Repositories;
using System.Diagnostics;

namespace ManageIT.SideActivities {
    /// <remarks>
    /// Ivan Juras
    /// </remarks>
    public partial class WorkOrderAdd : Window {
        public int id_worker { get; set; }
        public Worker currentWorker { get; set; }
        public int ID_Racun { get; set; }
        public WorkOrderAdd(int ID_Worker, Worker worker) {
            id_worker = ID_Worker;
            WorkOrderRepository woRepo = new WorkOrderRepository();
            ID_Racun = woRepo.GetLastWorkOrderID();
            currentWorker = worker;
            InitializeComponent();
            LoadClients();
            LoadWorkers();
            LoadWorkTypes();
            txtLocation.Text = "";
            txtLocation.IsEnabled = false;
        }
        /// <remarks>
        /// Ivan Juras
        /// </remarks>
        private void LoadWorkTypes() {
            var workTypeService = new WorkTypeService();
            var workType = workTypeService.GetWorkTypes();
            cmbWorkType.ItemsSource = workType;
        }
        /// <remarks>
        /// Ivan Juras
        /// </remarks>
        private void LoadWorkers() {
            var workerService = new WorkerService();
            var workers = workerService.GetWorkers();
            cmbWorker.ItemsSource = workers;
        }
        /// <remarks>
        /// Ivan Juras
        /// </remarks>
        private void LoadClients() {
            var clientService = new ClientService();
            var clients = clientService.GetClients();
            cmbClient.ItemsSource = clients;
        }
        ///<remarks>Darijo Bračić Ivan Juras </remarks>

        private void btnAdd_Click(object sender, RoutedEventArgs e) {

            if (cmbClient.SelectedItem == null) {
                MessageBox.Show("Please select a client!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (cmbWorker.SelectedItem == null) {
                MessageBox.Show("Please select a worker!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (cmbWorkType.SelectedItem == null) {
                MessageBox.Show("Please select a work type!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var orderDetail = new OrderDetail {
                Client = cmbClient.SelectedItem as Client,
                Worker = cmbWorker.SelectedItem as Worker,
                Location = txtLocation.Text,
                Date = CombineDateAndTime(dateWorkOrder.SelectedDate ?? DateTime.Now, TimeSpan.Parse(txtStartTime.Text)),
                WorkType = cmbWorkType.SelectedItem as WorkType,
                Duration = TimeSpan.Parse(txtTime.Text),
            };
            var orderDetailService = new OrderDetailService();
            orderDetailService.AddOrderDetail(orderDetail);

            bool receiptType = false;

            if (orderDetail.Client.ID_type == 2) {
                receiptType = true;
            }

            var workOrder = new WorkOrder {
                OrderDetail = orderDetail,
                ID_Worker = id_worker,
                DateCreated = DateTime.Now,
                IsFinished = false,
                Worker = currentWorker
            };
            var workOrderService = new WorkOrderService();


            if (workOrderService.AddWorkOrder(workOrder)) {
                MessageBox.Show("Succesfully added a work order!");
                Close();
            } else {
                MessageBox.Show("Please fill in all the fields!");
            }
            ID_Racun++;
            workOrder.ID_Work_Order = ID_Racun;

            /// <remarks>
            /// Matej Desanić
            /// </remarks>
            // A simple connection of the data for the reciept, and receipt generation
            Receipt receiptAdd = new Receipt();
            receiptAdd = workOrderService.AddReceipt(workOrder);

            var receipt = new RecieptGenerator(receiptAdd, workOrder, receiptType);
            var fileName = $"Receipt#{receiptAdd.ID_receipt}.pdf";
            string filePath = System.IO.Path.Combine("../../../BusinessLogicLayer/Receipts", fileName);
            receipt.GeneratePdf(filePath);
        }
        /// <remarks>
        /// Ivan Juras
        /// </remarks>
        private void btnCancel_Click(object sender, RoutedEventArgs e) {
            Close();
        }
        /// <remarks>
        /// Ivan Juras
        /// </remarks>
        private void cmbClient_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            var selectedClient = cmbClient.SelectedItem as Client;

            if (selectedClient != null) {
                txtLocation.Text = selectedClient.Client_Address;
            }
        }
        /// <remarks>
        /// Ivan Juras
        /// </remarks>
        private DateTime CombineDateAndTime(DateTime date, TimeSpan time) {
            return date.Date + time;
        }
        ///<remarks>Darijo Bračić </remarks>
        private void cmbWorker_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            var selectedWorker = cmbWorker.SelectedItem as Worker;
            if (selectedWorker != null) {
                txtEmail.Text = selectedWorker.Email;
            }
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.F1) {
                OpenPdf();
            }
        }

        private void OpenPdf() {
            string pdfFilePath = "C:\\Users\\ivanj\\Documents\\GitHub\\rpp23-project-mdesanic21-dbracic21-ijuras21\\Software\\ManageIT\\ManageIT\\Helpers\\UserDocumentation.pdf";

            // Check if the file exists before attempting to open
            if (System.IO.File.Exists(pdfFilePath)) {
                string command = $"/A \"page={10}\" \"{pdfFilePath}\"";

                // Start the process with the command
                Process.Start("AcroRd32.exe", command);
            } else {
                MessageBox.Show("PDF file not found!");
            }
        }
    }
}