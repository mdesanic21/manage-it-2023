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
    /// <remarks>
    /// Matej Desanić
    /// </remarks>
    public partial class ClientUpdate : Window
    {
        private Client clientForUpdate;
        ClientService clientService = new ClientService();
        public ClientUpdate(Client client)
        {
            InitializeComponent();
            clientForUpdate = client;
        }

        // Note: decided to enable or disable some fileds based on the already set ID_type of the client is individual or corporation because we save different data for different types of clients.
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtID.Text = clientForUpdate.ID_client.ToString();
            if(clientForUpdate.ID_type == 1)
            {
                txtTypeName.Text = "Individual";
                txtCompanyName.Text = "";
                txtCompanyName.IsEnabled = false;
                txtIBAN.Text = "";
                txtIBAN.IsEnabled = false;
                txtIBAN.Background = Brushes.Gray;
                txtCompanyName.Background = Brushes.Gray;
                txtLastName.Text = clientForUpdate.LastName;
                txtFirstName.Text = clientForUpdate.FirstName;
            }
            else
            {
                txtTypeName.Text = "Corporation";
                txtFirstName.Text = "";
                txtLastName.Text = "";
                txtFirstName.IsEnabled = false;
                txtLastName.IsEnabled = false;
                txtLastName.Background = Brushes.Gray;
                txtFirstName.Background = Brushes.Gray;
                txtCompanyName.Text = clientForUpdate.CompanyName;
                txtIBAN.Text = clientForUpdate.IBAN;
            }
            txtEmail.Text = clientForUpdate.Email.ToString();
            txtAddress.Text = clientForUpdate.Client_Address.ToString();
            txtNumber.Text = clientForUpdate.Number.ToString();
        }

        private void btnSaveUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (LoadUpdatedClient(out Client updatedClient))
            {
                if (clientService.UpdateClient(updatedClient))
                {
                    MessageBox.Show("Successfully updated client!");
                    Close();
                }
                else
                {
                    MessageBox.Show("That email address is already in usage!");
                }
            }
        }

        private bool LoadUpdatedClient(out Client updatedClient)
        {
            updatedClient = new Client
            {
                ID_client = clientForUpdate.ID_client,
                Email = txtEmail.Text.Trim(),
                FirstName = txtFirstName.Text.Trim(),
                LastName = txtLastName.Text.Trim(),
                CompanyName = txtCompanyName.Text.Trim(),
                IBAN = txtIBAN.Text.Trim(),
                Client_Address = txtAddress.Text.Trim(),
                Number = txtNumber.Text.Trim()
            };

            if (string.IsNullOrWhiteSpace(updatedClient.Email) ||
                string.IsNullOrWhiteSpace(updatedClient.Number) ||
                string.IsNullOrWhiteSpace(updatedClient.Client_Address))
            {
                MessageBox.Show("Please fill in all required fields.");
                return false;
            }

            if (clientForUpdate.ID_type == 1)
            {
                if (string.IsNullOrWhiteSpace(updatedClient.FirstName) ||
                    string.IsNullOrWhiteSpace(updatedClient.LastName))
                {
                    MessageBox.Show("Please fill in all required fields.");
                    return false;
                }
            }
            else if (clientForUpdate.ID_type == 2)
            {
                if (string.IsNullOrWhiteSpace(updatedClient.CompanyName) ||
                    string.IsNullOrWhiteSpace(updatedClient.IBAN))
                {
                    MessageBox.Show("Please fill in all required fields.");
                    return false;
                }
            }

            return true;
        }

        private void btnExitUpdate_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_PreviewKeyUp(object sender, KeyEventArgs e) {
            if (e.Key == Key.F1) {
                OpenPdf();
            }
        }

        private void OpenPdf() {
            string pdfFilePath = "C:\\Users\\ivanj\\Documents\\GitHub\\rpp23-project-mdesanic21-dbracic21-ijuras21\\Software\\ManageIT\\ManageIT\\Helpers\\UserDocumentation.pdf";

            // Check if the file exists before attempting to open
            if (System.IO.File.Exists(pdfFilePath)) {
                string command = $"/A \"page={2}\" \"{pdfFilePath}\"";

                // Start the process with the command
                Process.Start("AcroRd32.exe", command);
            } else {
                MessageBox.Show("PDF file not found!");
            }
        }
    }
}
