using BusinessLogicLayer.Services;
using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Diagnostics;
using System.Globalization;
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

namespace ManageIT.MainActivity {
    /// <summary>
    /// Interaction logic for UcCalendar.xaml
    /// </summary>
 
    /// <remarks>
    /// Ivan Juras
    /// </remarks>
    public partial class UcCalendar : UserControl {
        int month, year;

        Worker givenWorker;
        /// <remarks>
        /// Ivan Juras
        /// </remarks>
        public UcCalendar(Worker worker) {
            givenWorker = worker;
            InitializeComponent();
        }

        /// <remarks>
        /// Ivan Juras
        /// </remarks>
        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            displayDays(givenWorker);
            HideComboBox();
            LoadWorkers();
            SelectWorker(givenWorker.ID_worker);
        }

        /// <remarks>
        /// Ivan Juras
        /// </remarks>
        private void HideComboBox() {
            if(givenWorker.Id_type == 1) {
                cmbWorker.Visibility = Visibility.Visible;
            } else {
                cmbWorker.Visibility = Visibility.Hidden;
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
        private void LoadWorkers() {
            var workerService = new WorkerService();
            var workers = workerService.GetWorkers();
            cmbWorker.ItemsSource = workers;
        }

        /// <remarks>
        /// Ivan Juras
        /// </remarks>
        private void displayDays(Worker worker) {
            DateTime now = DateTime.Now;
            month = now.Month;
            year = now.Year;

            string monthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            lbDate.Content = monthName + " " + year;

            DateTime startOfTheMonth = new DateTime(year, month, 1);

            int days = DateTime.DaysInMonth(year, month);

            int daysOfTheWeek = Convert.ToInt32(startOfTheMonth.DayOfWeek.ToString("d")) + 1;

            for(int i = 1; i < daysOfTheWeek; i++) {
                UcEmptyDay ucEmptyDays = new UcEmptyDay();

                int row = 2;  
                int col = i - 1;  

                
                gridPanel.Children.Add(ucEmptyDays);
                Grid.SetRow(ucEmptyDays, row);
                Grid.SetColumn(ucEmptyDays, col);

            }
            for(int i = 1; i <= days; i++) {
                UcDay ucDays = new UcDay(worker, year, month);
                ucDays.days(i);

                int row = (daysOfTheWeek + i - 2) / 7 + 2;  
                int col = (daysOfTheWeek + i - 2) % 7;

                gridPanel.Children.Add(ucDays);
                Grid.SetRow(ucDays, row);
                Grid.SetColumn(ucDays, col);
            }
        }

        /// <remarks>
        /// Ivan Juras
        /// </remarks>
        private void btnNext_Click(object sender, RoutedEventArgs e) {
            foreach (var child in gridPanel.Children.OfType<UcDay>().ToList()) {
                gridPanel.Children.Remove(child);
            }
            foreach (var child in gridPanel.Children.OfType<UcEmptyDay>().ToList()) {
                gridPanel.Children.Remove(child);
            }

            var selectedWorker = cmbWorker.SelectedItem as Worker;

            month++;
            if (month == 13) {
                month = 1;
                year++;
            }

            string monthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            lbDate.Content = monthName + " " + year;

            DateTime startOfTheMonth = new DateTime(year, month, 1);

            int days = DateTime.DaysInMonth(year, month);

            int daysOfTheWeek = Convert.ToInt32(startOfTheMonth.DayOfWeek.ToString("d")) + 1;

            for (int i = 1; i < daysOfTheWeek; i++) {
                UcEmptyDay ucEmptyDays = new UcEmptyDay();

                int row = 2;
                int col = i - 1;


                gridPanel.Children.Add(ucEmptyDays);
                Grid.SetRow(ucEmptyDays, row);
                Grid.SetColumn(ucEmptyDays, col);

            }
            for (int i = 1; i <= days; i++) {
                UcDay ucDays = new UcDay(selectedWorker, year, month);
                ucDays.days(i);

                int row = (daysOfTheWeek + i - 2) / 7 + 2;  // Adjusted row calculation
                int col = (daysOfTheWeek + i - 2) % 7;

                gridPanel.Children.Add(ucDays);
                Grid.SetRow(ucDays, row);
                Grid.SetColumn(ucDays, col);
            }
        }
        /// <remarks>
        /// Ivan Juras
        /// </remarks>
        private void cmbWorker_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            var selectedWorker = cmbWorker.SelectedItem as Worker;
            displayDays(selectedWorker);
        }
        /// <remarks>
        /// Ivan Juras
        /// </remarks>
        private void btnPrevious_Click(object sender, RoutedEventArgs e) {
            foreach (var child in gridPanel.Children.OfType<UcDay>().ToList()) {
                gridPanel.Children.Remove(child);
            }
            foreach (var child in gridPanel.Children.OfType<UcEmptyDay>().ToList()) {
                gridPanel.Children.Remove(child);
            }

            var selectedWorker = cmbWorker.SelectedItem as Worker;

            month--;
            if (month == 0) {
                month = 12;
                year--;
            }

            string monthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            lbDate.Content = monthName + " " + year;

            DateTime startOfTheMonth = new DateTime(year, month, 1);

            int days = DateTime.DaysInMonth(year, month);

            int daysOfTheWeek = Convert.ToInt32(startOfTheMonth.DayOfWeek.ToString("d")) + 1;

            for (int i = 1; i < daysOfTheWeek; i++) {
                UcEmptyDay ucEmptyDays = new UcEmptyDay();

                int row = 2;
                int col = i - 1;


                gridPanel.Children.Add(ucEmptyDays);
                Grid.SetRow(ucEmptyDays, row);
                Grid.SetColumn(ucEmptyDays, col);

            }
            for (int i = 1; i <= days; i++) {
                UcDay ucDays = new UcDay(selectedWorker, year, month);
                ucDays.days(i);

                int row = (daysOfTheWeek + i - 2) / 7 + 2;  // Adjusted row calculation
                int col = (daysOfTheWeek + i - 2) % 7;

                gridPanel.Children.Add(ucDays);
                Grid.SetRow(ucDays, row);
                Grid.SetColumn(ucDays, col);
            }
        }
    }
}
