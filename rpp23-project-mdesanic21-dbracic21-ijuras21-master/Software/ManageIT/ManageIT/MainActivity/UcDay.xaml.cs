using BusinessLogicLayer.Services;
using EntitiesLayer.Entities;
using ManageIT.SideActivities;
using System;
using System.Collections.Generic;
using System.Drawing;
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
    /// Interaction logic for UcDay.xaml
    /// </summary>

    /// <remarks>
    /// Ivan Juras
    /// </remarks>
    public partial class UcDay : UserControl {
        Worker givenWorker;

        OrderDetailService service = new OrderDetailService();
        WorkOrderService workOrderService = new WorkOrderService();

        int year;
        int month;
        int day;
        /// <remarks>
        /// Ivan Juras
        /// </remarks>
        public UcDay(Worker worker, int year, int month) {
            this.year = year;
            this.month = month;
            givenWorker = worker;
            InitializeComponent();
        }
        /// <remarks>
        /// Ivan Juras
        /// </remarks>
        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            ShowButtonsIfWorkOrdersExist(Convert.ToInt32(lbDay.Content));
        }
        /// <remarks>
        /// Ivan Juras
        /// </remarks>
        public void days(int numday) {
            lbDay.Content = numday + "";
            ShowButtonsIfWorkOrdersExist(numday);
            day = numday;
        }
        /// <remarks>
        /// Ivan Juras
        /// </remarks>
        private void ShowButtonsIfWorkOrdersExist(int day) {
            DateTime dateToCheck = new DateTime(year, month, day);
            var today = DateTime.Now.Date;

            // Fetch OrderDetails for the given worker on the specified date
            var orderDetails = service.GetOrderDetailsForWorkerAndDate(givenWorker.ID_worker, dateToCheck);

            if (orderDetails.Any()) {
                btnConclude.Visibility = Visibility.Visible;
                btnDetails.Visibility = Visibility.Visible;
            } else {
                btnConclude.Visibility = Visibility.Hidden;
                btnDetails.Visibility = Visibility.Hidden;
            }

            if (dateToCheck > today) {
                btnConclude.IsEnabled = false;
            }
        }
        /// <remarks>
        /// Ivan Juras
        /// </remarks>
        private void btnConclude_Click(object sender, RoutedEventArgs e) {
            DateTime dateToCheck = new DateTime(year, month, day);
            workOrderService.ConcludeWorkOrder(givenWorker.ID_worker, dateToCheck);
            MessageBox.Show("Work order successfully concluded!");
        }
        /// <remarks>
        /// Ivan Juras
        /// </remarks>
        private void btnDetails_Click(object sender, RoutedEventArgs e) {
            DateTime dateToCheck = new DateTime(year, month, day);
            var orderDetails = service.GetOrderDetailsForWorkerAndDate(givenWorker.ID_worker, dateToCheck);

            if (orderDetails.Any()) {
                var selectedOrderDetail = orderDetails.First();
                WorkOrderUpdate orderDetailDetails = new WorkOrderUpdate(selectedOrderDetail);
                orderDetailDetails.Show();
            }
        }
    }
}
