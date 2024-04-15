using BusinessLogicLayer.Services;
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
    /// Interaction logic for UcWorkers.xaml
    /// </summary>
    ///<remarks>Darijo Bračić </remarks>
    public partial class UcWorkers : UserControl
    {
        WorkerService services = new WorkerService();
        public UcWorkers()
        {
            InitializeComponent();
        }
        ///<remarks>Darijo Bračić </remarks>
        private void btnAddNewUser_Click(object sender, RoutedEventArgs e)
        {
          AddNewWorker addNewWorker = new AddNewWorker();
            addNewWorker.Show();
           var allworkers = services.GetWorkers();
            dgUsers.ItemsSource = null;
            dgUsers.ItemsSource = allworkers;
            HideColumns();
           
        }
        ///<remarks>Darijo Bračić </remarks>

        private void btnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            Worker selectedWorker = GetSelectedWorker();
            if (selectedWorker != null)
            {
                bool isSuccessful = services.RemoveWorker(selectedWorker);
                if (isSuccessful == false)
                {
                    MessageBox.Show("Application failed to remove selected worker!");
                }
                ShowAllWorkers();

            }
        }
        ///<remarks>Darijo Bračić </remarks>

        private Worker GetSelectedWorker()
        {
           return dgUsers.SelectedItem as Worker;
        }
        ///<remarks>Darijo Bračić </remarks>

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ShowAllWorkers();
            KeyDown += OnKeyDown;

        }
        ///<remarks>Darijo Bračić </remarks>

        private void ShowAllWorkers()
        {
            var allWorkers = services.GetWorkers();
            dgUsers.ItemsSource = allWorkers;
            HideColumns();
        }
        ///<remarks>Darijo Bračić </remarks>

        private void HideColumns()
        {
            dgUsers.Columns[3].Visibility = Visibility.Hidden;
            dgUsers.Columns[6].Visibility = Visibility.Hidden;
            dgUsers.Columns[8].Visibility = Visibility.Hidden;
            dgUsers.Columns[9].Visibility = Visibility.Hidden;
            dgUsers.Columns[10].Visibility = Visibility.Hidden;
            dgUsers.Columns[11].Visibility = Visibility.Hidden;
            dgUsers.Columns[12].Visibility = Visibility.Hidden;
            dgUsers.Columns[13].Visibility = Visibility.Hidden;
        }
   

        private void dgUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        ///<remarks>Darijo Bračić </remarks>

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (dgUsers.SelectedItem != null)
            {
                Worker selectedWorker = (Worker)dgUsers.SelectedItem;
                int workerIdToUpdate = selectedWorker.ID_worker;

               UpdateWorker updateWorker = new UpdateWorker(workerIdToUpdate);
                updateWorker.Show();
                ShowAllWorkers();
                HideColumns();
            }
            else MessageBox.Show("You have to select a worker first!");

           
        }
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F1)
            {
                OpenPdfDocument("Helpers\\Helper.pdf");
            }
        }

        private void OpenPdfDocument(string pdfPath)
        {

            string fullPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, pdfPath);

            Process.Start(fullPath);

        }

    }
}
