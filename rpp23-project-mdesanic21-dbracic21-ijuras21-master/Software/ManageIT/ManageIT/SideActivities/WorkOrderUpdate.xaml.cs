using BusinessLogicLayer.Services;
using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace ManageIT.SideActivities {
    /// <summary>
    /// Interaction logic for WorkOrderUpdate.xaml
    /// </summary>

    /// <remarks>
    /// Ivan Juras
    /// </remarks>
    public partial class WorkOrderUpdate : Window {
        private OrderDetail orderDetail;
        /// <remarks>
        /// Ivan Juras
        /// </remarks>
        public WorkOrderUpdate(OrderDetail selectedOrder) {
            InitializeComponent();
            orderDetail = selectedOrder;
        }
        /// <remarks>
        /// Ivan Juras
        /// </remarks>
        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            txtLocation.Text = orderDetail.Location;
            txtStartTime.Text = orderDetail.Date.ToString();
            txtTime.Text = orderDetail.Duration.ToString();
            txtLocation.IsEnabled = false;
            txtStartTime.IsEnabled = false;
            txtTime.IsEnabled = false;
            cmbClient.IsEnabled = false;
            cmbWorker.IsEnabled = false;
            cmbWorkType.IsEnabled = false;

            LoadClients();
            SelectClient(orderDetail.ID_Client);
            LoadWorkers();
            SelectWorker(orderDetail.ID_Worker);
            LoadWorkTypes();
            SelectWorkType(orderDetail.ID_Work_Type);
        }
        /// <remarks>
        /// Ivan Juras
        /// </remarks>
        private void SelectWorkType(int? iD_Work_Type) {
            for (int i = 0; i < cmbWorkType.Items.Count; i++) {
                WorkType c = cmbWorkType.Items[i] as WorkType;
                if (c.ID_Work_Type == iD_Work_Type) {
                    cmbWorkType.SelectedIndex = i;
                    break;
                }
            }
        }
        /// <remarks>
        /// Ivan Juras
        /// </remarks>
        private void SelectWorker(int? iD_Worker) {
            for (int i = 0; i < cmbWorker.Items.Count; i++) {
                Worker c = cmbWorker.Items[i] as Worker;
                if (c.ID_worker == iD_Worker) {
                    cmbWorker.SelectedIndex = i;
                    break;
                }
            }
        }
        /// <remarks>
        /// Ivan Juras
        /// </remarks>
        private void SelectClient(int? iD_Client) {
            for (int i = 0; i < cmbClient.Items.Count; i++) {
                Client c = cmbClient.Items[i] as Client;
                if(c.ID_client == iD_Client) {
                    cmbClient.SelectedIndex = i;
                    break;
                }
            }
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
        private void LoadWorkTypes() {
            var workTypeService = new WorkTypeService();
            var workType = workTypeService.GetWorkTypes();
            cmbWorkType.ItemsSource = workType;
        }
        /// <remarks>
        /// Ivan Juras
        /// </remarks>
        private void LoadClients() {
            var clientService = new ClientService();
            var clients = clientService.GetClients();
            cmbClient.ItemsSource = clients;
        }
        /// <remarks>
        /// Ivan Juras
        /// </remarks>
        private void btnCancel_Click(object sender, RoutedEventArgs e) {
            Close();
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
