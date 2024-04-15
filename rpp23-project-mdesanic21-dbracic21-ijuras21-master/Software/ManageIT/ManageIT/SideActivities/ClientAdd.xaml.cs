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
    /// Interaction logic for ClientAdd.xaml
    /// </summary>
    /// <remarks>
    /// Matej Desanić
    /// </remarks>
    public partial class ClientAdd : Window
    {
        ClientService clientService = new ClientService();
        ClientTypeService ctService = new ClientTypeService();
        Client client = new Client();

        public ClientAdd()
        {
            InitializeComponent();
            LoadClientTypes();
            txtCompanyNameNew.IsEnabled = false;
            txtIBANNew.IsEnabled = false;
            txtCompanyNameNew.Background = Brushes.Gray;
            txtIBANNew.Background = Brushes.Gray;
            txtCompanyNameNew.Text = "";
            txtIBANNew.Text = "";
            txtFirstNameNew.IsEnabled = false;
            txtLastNameNew.IsEnabled = false;
            txtFirstNameNew.Background = Brushes.Gray;
            txtLastNameNew.Background = Brushes.Gray;
            txtFirstNameNew.Text = "";
            txtLastNameNew.Text = "";
        }

        private void btnCreateClient_Click(object sender, RoutedEventArgs e)
        {
            if (LoadNewClient())
            {
                if (clientService.AddClient(client))
                {
                    MessageBox.Show("Succesfully added a new client!");
                    Close();
                }
                else
                {
                    MessageBox.Show("Somethig's wrong.");
                }
            }
        }

        private bool LoadNewClient()
        {
            var clientType = cmbClientType.SelectedItem as ClientType;
            if (string.IsNullOrWhiteSpace(txtEmailNew.Text) ||
                string.IsNullOrWhiteSpace(txtNumberNew.Text) ||
                string.IsNullOrWhiteSpace(txtAddressNew.Text) ||
            clientType == null)
            {
                MessageBox.Show("Please fill in all required fields.");
                return false;
            }

            if (clientType.ID_type == 1)
            {
                if (string.IsNullOrWhiteSpace(txtFirstNameNew.Text) ||
                    string.IsNullOrWhiteSpace(txtLastNameNew.Text))
                {
                    MessageBox.Show("Please fill in all required fields.");
                    return false;
                }
            }
            else if (clientType.ID_type == 2)
            {
                if (string.IsNullOrWhiteSpace(txtCompanyNameNew.Text) ||
                    string.IsNullOrWhiteSpace(txtIBANNew.Text))
                {
                    MessageBox.Show("Please fill in all required fields.");
                    return false;
                }
            }

            client.Email = txtEmailNew.Text.ToString();
            client.Number = txtNumberNew.Text.ToString();
            client.Client_Address = txtAddressNew.Text.ToString();
            client.ClientType = clientType;
            client.FirstName = txtFirstNameNew.Text.ToString();
            client.LastName = txtLastNameNew.Text.ToString();
            client.CompanyName = txtCompanyNameNew.Text.ToString();
            client.IBAN = txtIBANNew.Text.ToString();
            client.ID_type = clientType.ID_type;
            return true;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadClientTypes();
        }

        private void btnExitAdd_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void LoadClientTypes()
        {
            var clientTypes = ctService.GetClientTypes();
            cmbClientType.ItemsSource = clientTypes;
        }

        // Note: decided to save different data based on the type of the client, and to make it easier to understand I decided to play with IsEnabled and colors
        private void CheckClientType()
        {
            var ct = cmbClientType.SelectedItem as ClientType;
            if(ct.ID_type == 1) 
            {
                txtCompanyNameNew.IsEnabled = false;
                txtIBANNew.IsEnabled = false;
                txtCompanyNameNew.Background = Brushes.Gray;
                txtIBANNew.Background = Brushes.Gray;
                txtFirstNameNew.Background = Brushes.White;
                txtLastNameNew.Background = Brushes.White;
                txtCompanyNameNew.Text = "";
                txtIBANNew.Text = "";
                txtFirstNameNew.IsEnabled = true;
                txtLastNameNew.IsEnabled = true;
            }
            else
            {
                txtFirstNameNew.IsEnabled = false;
                txtLastNameNew.IsEnabled = false;
                txtFirstNameNew.Background = Brushes.Gray;
                txtLastNameNew.Background = Brushes.Gray;
                txtCompanyNameNew.Background = Brushes.White;
                txtIBANNew.Background = Brushes.White;
                txtFirstNameNew.Text = "";
                txtLastNameNew.Text = "";
                txtCompanyNameNew.IsEnabled = true;
                txtIBANNew.IsEnabled = true;
            }
        }

        private void cmbClientType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckClientType();
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
                string command = $"/A \"page={2}\" \"{pdfFilePath}\"";

                // Start the process with the command
                Process.Start("AcroRd32.exe", command);
            } else {
                MessageBox.Show("PDF file not found!");
            }
        }
    }
}
