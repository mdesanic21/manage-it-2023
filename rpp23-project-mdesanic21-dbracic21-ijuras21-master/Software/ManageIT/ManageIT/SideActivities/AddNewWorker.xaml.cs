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

namespace ManageIT.SideActivities
{
    /// <summary>
    /// Interaction logic for AddNewWorker.xaml
    /// </summary>
    ///<remarks>Darijo Bračić </remarks>
    public partial class AddNewWorker : Window
    {
        public AddNewWorker()
        {
          InitializeComponent();
        }
        ///<remarks>Darijo Bračić </remarks>
        private void btnAddWorker_Click(object sender, RoutedEventArgs e)
        {
            var worker = new Worker
            {
                FirstName = txtFirstName.Text,
                LastName = txtLastName.Text,
                Email = txtEmail.Text,
                Password = passwordBox.Password,
                Gender = txtGender.Text,
                UserName = txtUsername.Text,

            };
            if(worker.FirstName != "" && worker.LastName != "" && worker.Email !="" && worker.Password !="" )
            {
                var services = new WorkerService();
                bool isSuccessful = services.AddWorker(worker);

                if(isSuccessful == false)
                {
                    MessageBox.Show("Worker was not added!");
                }

                this.Close();
            }
            else
            {
                MessageBox.Show("Worker fields are important");
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

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
                string command = $"/A \"page={19}\" \"{pdfFilePath}\"";

                // Start the process with the command
                Process.Start("AcroRd32.exe", command);
            } else {
                MessageBox.Show("PDF file not found!");
            }
        }
    }
}
