using BusinessLogicLayer.Services;
using EntitiesLayer.Entities;
using ManageIT.SideActivities;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ManageIT.MainActivity {
    /// <summary>
    /// Interaction logic for UcWorkOrders.xaml
    /// </summary>
    /// <remarks>
    /// Ivan Juras
    /// </remarks>
    public partial class UcWorkOrders : UserControl {
        public int ID_Worker { get; set; }
        public Worker currentWorker { get; set; }
        private WorkOrderService service = new WorkOrderService();
        private OrderDetailService OrderDetailService = new OrderDetailService();
        /// <remarks>
        /// Ivan Juras
        /// </remarks>
        public UcWorkOrders(int id_worker,  Worker worker) {
            currentWorker = worker;
            ID_Worker = id_worker;
            InitializeComponent();
        }


        /// <remarks>
        /// Ivan Juras
        /// </remarks>
        private void btnSearchWorkOrders_Click(object sender, RoutedEventArgs e) {
            string phrase = txtSearchWorkOrders.Text;
            var workOrders = service.GetWorkOrdersByName(phrase);
            dgWorkOrders.ItemsSource = workOrders;
        }
        /// <remarks>
        /// Ivan Juras
        /// </remarks>
        private void btnClearWorkOrders_Click(object sender, RoutedEventArgs e) {
            txtSearchWorkOrders.Clear();
            LoadWorkOrders();
        }
        /// <remarks>
        /// Ivan Juras
        /// </remarks>
        private void btnAddWorkOrder_Click(object sender, RoutedEventArgs e) {
            WorkOrderAdd mainWindow = new WorkOrderAdd(ID_Worker, currentWorker);
            mainWindow.Show();
        }
        /// <remarks>
        /// Ivan Juras
        /// </remarks>
        private void btnUpdateWorkOrder_Click(object sender, RoutedEventArgs e) {
            var selectedWorkOrder = GetSelectedWorkOrder() as WorkOrder;
            if(selectedWorkOrder != null) { 
            var selectedOrderDetail = selectedWorkOrder.Id_Order_Details;
            var orderDetail = OrderDetailService.GetOrderDetail(selectedOrderDetail) as OrderDetail;
            if(selectedWorkOrder != null) {
                WorkOrderUpdate mainWindow = new WorkOrderUpdate(orderDetail as OrderDetail);
                mainWindow.Show();
            }
            } else {
                MessageBox.Show("Please select a work order to view details!");
            }
        }
        /// <remarks>
        /// Ivan Juras
        /// </remarks>
        private void btnRemoveWorkOrder_Click(object sender, RoutedEventArgs e) {
            var selectedWorkOrder = GetSelectedWorkOrder();
            if(selectedWorkOrder != null) {
                service.RemoveWorkOrder(selectedWorkOrder as WorkOrder);
                LoadWorkOrders();
            } else {
                MessageBox.Show("Please select a work order to delete!");
            }
        }
        /// <remarks>
        /// Ivan Juras
        /// </remarks>
        private object GetSelectedWorkOrder() {
            return dgWorkOrders.SelectedItem as WorkOrder;
        }
        /// <remarks>
        /// Ivan Juras
        /// </remarks>
        private void LoadWorkOrders() {
            WorkOrderService service = new WorkOrderService();
            var workOrders = service.GetWorkOrders();
            dgWorkOrders.ItemsSource = workOrders;
            HideColumns();
            DisplayNames();
        }
        /// <remarks>
        /// Ivan Juras
        /// </remarks>
        private void DisplayNames() {
            dgWorkOrders.Columns[0].Header = "ID of work order";
            dgWorkOrders.Columns[1].Header = "Finished work order";
            dgWorkOrders.Columns[2].Header = "Date of creation";
            dgWorkOrders.Columns[4].Header = "ID of order detail related to the work order";
            dgWorkOrders.Columns[7].Header = "Work order created by";
        }
        /// <remarks>
        /// Ivan Juras
        /// </remarks>
        private void HideColumns() {
            dgWorkOrders.Columns[5].Visibility = Visibility.Hidden;
            dgWorkOrders.Columns[6].Visibility = Visibility.Hidden;
            dgWorkOrders.Columns[3].Visibility = Visibility.Hidden;
        }
        /// <remarks>
        /// Ivan Juras
        /// </remarks>
        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            LoadWorkOrders();
        }

    }
}
