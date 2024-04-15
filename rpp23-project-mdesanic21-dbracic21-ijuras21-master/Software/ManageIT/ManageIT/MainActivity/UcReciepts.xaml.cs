using BusinessLogicLayer.Services;
using EntitiesLayer.Entities;
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

namespace ManageIT.MainActivity
{
    /// <summary>
    /// Interaction logic for UcReciepts.xaml
    /// </summary>
    /// <remarks>
    /// Matej Desanić
    /// </remarks>
    // Classic form code for interacting with the data inside the data grid, in this example the receipts.
    public partial class UcReciepts : UserControl
    {
        ReceiptService recieptService = new ReceiptService();
        public UcReciepts()
        {
            InitializeComponent();
        }

        // Neccesarry logic for fetching the reciepts name for openig the receipt .pdf file.
        private void btnOpenReciept_Click(object sender, RoutedEventArgs e)
        {
            Receipt receiptToCancel = new Receipt();
            receiptToCancel = dgReciepts.SelectedItem as Receipt;
            Button btnOpenReciept = (Button)sender;
            string receiptName = $"Receipt#{receiptToCancel.ID_receipt}.pdf";
            recieptService.OpenReciept(receiptName);
        }

        private void btnCancelReciepts_Click(object sender, RoutedEventArgs e)
        {
            Receipt receiptToCancel = new Receipt();
            receiptToCancel = dgReciepts.SelectedItem as Receipt;

            recieptService.CancelReciept(receiptToCancel.ID_receipt);
            LoadReciepts();
            HideColumns();
        }

        private void btnShowCanceled_Click(object sender, RoutedEventArgs e)
        {
            LoadCanceledReciepts();
            HideColumns();
        }

        private void btnShowAll_Click(object sender, RoutedEventArgs e)
        {
            LoadReciepts();
            HideColumns();
        }

        private void LoadReciepts()
        {
            dgReciepts.ItemsSource = recieptService.GetReceipts();
            HideColumns();
        }

        private void LoadCanceledReciepts()
        {
            dgReciepts.ItemsSource = recieptService.GetCanceledReceipts();
            HideColumns();
        }

        private void HideColumns()
        {
            dgReciepts.Columns[5].Visibility = Visibility.Hidden;
            dgReciepts.Columns[6].Visibility = Visibility.Hidden;
            dgReciepts.Columns[7].Visibility = Visibility.Hidden;
            dgReciepts.Columns[8].Visibility = Visibility.Hidden;
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadReciepts();
        }
    }
}
