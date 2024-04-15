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
using System.Windows.Navigation;
using EntitiesLayer.Entities;
using ManageIT.MainActivity;
using ManageIT.SideActivities;

namespace ManageIT {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window {

        /// <remarks>
        /// Matej Desanić
        /// </remarks>
        public bool IsAdmin { get; private set; }
        public int ID_Worker { get; set; }
        Worker currentWorker { get; set; }

        Worker givenWorker;
       
        public MainWindow()
        {
            InitializeComponent();
            InitializeUI();
        }

        /// <remarks>
        /// Matej Desanić
        /// </remarks>
        public MainWindow(int user_role, int id_worker, Worker worker) : this()
        {
            givenWorker = worker;
            ID_Worker = id_worker;
            if (user_role == 1)
            {
                IsAdmin = true;
            }
            else
            {
                IsAdmin = false;
            }
            currentWorker = worker;
            lblLoggedUser.Content = worker.UserName;
            InitializeUI();
        }

        private void InitializeUI()
        {
            btnWorkers.Visibility = IsAdmin ? Visibility.Visible : Visibility.Collapsed;
        }
        /// <remarks>
        /// Ivan Juras
        /// </remarks>
        private void btnSchedule_Click(object sender, RoutedEventArgs e)
        {
            contentPanel.Content = new MainActivity.UcCalendar(givenWorker);
        }
        /// <remarks>
        /// Matej Desanić
        /// </remarks>
        private void btnReciepts_Click(object sender, RoutedEventArgs e)
        {
            contentPanel.Content = new MainActivity.UcReciepts();
        }
        /// <remarks>
        /// Matej Desanić
        /// </remarks>
        private void btnClients_Click(object sender, RoutedEventArgs e)
        {
            contentPanel.Content = new MainActivity.UcClients();
        }
        /// <remarks>
        /// Ivan Juras
        /// </remarks>
        private void btnWorkOrder_Click(object sender, RoutedEventArgs e)
        {
            contentPanel.Content = new MainActivity.UcWorkOrders(ID_Worker, currentWorker);
        }
        /// <remarks>
        /// Matej Desanić
        /// </remarks>
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnWorkers_Click(object sender, RoutedEventArgs e)
        {
            contentPanel.Content = new MainActivity.UcWorkers();
        }

        public static implicit operator MainWindow(WorkOrderAdd v) {
            throw new NotImplementedException();
        }
        /// <remarks>
        /// Matej Desanić
        /// </remarks>
        private void btnReports_Click(object sender, RoutedEventArgs e)
        {
            contentPanel.Content = new MainActivity.UcReports(currentWorker);
        }

        /// <remarks>
        /// Ivan Juras
        /// </remarks>
         private void Window_PreviewKeyDown(object sender, KeyEventArgs e) {
             if(e.Key == Key.F1) {
                 OpenPdf();
             }
         }
         /// <remarks>
         /// Ivan Juras
         /// </remarks>
         private void OpenPdf() {
             string pdfFilePath = "C:\\Users\\ivanj\\Documents\\GitHub\\rpp23-project-mdesanic21-dbracic21-ijuras21\\Software\\ManageIT\\ManageIT\\Helpers\\UserDocumentation.pdf";

             // Check if the file exists before attempting to open
             if (System.IO.File.Exists(pdfFilePath)) {
                 string command = $"/A \"page={1}\" \"{pdfFilePath}\"";

                 // Start the process with the command
                 Process.Start("AcroRd32.exe", command);
             } else {
                 MessageBox.Show("PDF file not found!");
             }
         }

    }
}
