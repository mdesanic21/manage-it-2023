using BusinessLogicLayer.Services;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using EntitiesLayer.Entities;
using ManageIT.SideActivities;
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
using System.Windows.Shapes;

namespace ManageIT.MainActivity
{
    /// <summary>
    /// Interaction logic for ClientView.xaml
    /// </summary>
    /// <remarks>
    /// Matej Desanić
    /// </remarks>
    // Classic form for viewing the clients and working with them. Uses some established concepts for automatically refreshing the grid view on the neccesarry calls, loading the data into it and other things.
    public partial class UcClients : UserControl
    {
        ClientService clientService = new ClientService();
        List<ClientViewModel> clients = new List<ClientViewModel>();
        public UcClients()
        {
            InitializeComponent();
            GetAllClients();
            ShowClients();
        }

        private void btnSearchClients_Click(object sender, RoutedEventArgs e)
        {
            string key = txtSearchClients.Text.ToString();
            clients = clientService.SearchClients(key);
            ShowClients();
        }

        private void btnClearClients_Click(object sender, RoutedEventArgs e)
        {
            GetAllClients();
            ShowClients();
        }

        private void btnAddClient_Click(object sender, RoutedEventArgs e)
        {
            ClientAdd clientAddWindow = new ClientAdd();
            clientAddWindow.ShowDialog();

            GetAllClients();
            ShowClients();
        }

        private void btnUpdateClient_Click(object sender, RoutedEventArgs e)
        {
            if (dgClients.SelectedItem != null)
            {
                ClientViewModel selectedClient = (ClientViewModel)dgClients.SelectedItem;
                int clientIdToUpdate = selectedClient.ID_Client;

                Client clientForUpdate = clientService.GetClientById(clientIdToUpdate);
                var clientUpdateWindow = new ClientUpdate(clientForUpdate);
                clientUpdateWindow.ShowDialog();
            }
            else MessageBox.Show("You have to select a client first!");

            GetAllClients();
            ShowClients();
        }

        private void btnRemoveClient_Click(object sender, RoutedEventArgs e)
        {
            DeleteClient();
            GetAllClients();
            ShowClients();
        }

        private void DeleteClient()
        {
            if (dgClients.SelectedItem != null)
            {
                ClientViewModel selectedClient = (ClientViewModel)dgClients.SelectedItem;
                int clientIdToDelete = selectedClient.ID_Client;
                clientService.DeleteClient(clientIdToDelete);
            }
        }
        private void GetAllClients()
        {
            clients = clientService.GetClientsView();
        }

        private void ShowClients()
        {   
            dgClients.ItemsSource = clients;
        }
    }
}