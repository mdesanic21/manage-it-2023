using DataAccessLayer.Repositories;
using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    /// <remarks>
    /// Matej Desanić
    /// </remarks>
    // Receipts are added inside the WorkOrder service because we decided that we should automatically add a reciept when we add a new work order. Only functionallities of this service are for fetching the receipts and changing one attribute = Canceled to true if something was wrong with the work order previously created.
    public class ReceiptService
    {
        public List<Receipt> GetReceipts()
        {
            using (var recieptRepo = new ReceiptRepository())
            {
                List<Receipt> recieptList = new List<Receipt>();
                recieptList = recieptRepo.GetAllNotCanceled().ToList();

                return recieptList;
            }
        }

        public List<Receipt> GetCanceledReceipts()
        {
            using (var recieptRepo = new ReceiptRepository())
            {
                List<Receipt> recieptList = new List<Receipt>();
                recieptList = recieptRepo.GetAllCanceled().ToList();

                return recieptList;
            }
        }
        // We have only function for canceling receipts because we know that in the real world, the receipt is automatically sent to the government tax facility, and there is no option to un-cancle the canceled receipt because it would be illegal
        public void CancelReciept(int id)
        {
            using (var recieptRepo = new ReceiptRepository())
            {
                Receipt receiptToCancel = new Receipt();
                receiptToCancel = recieptRepo.GetReceiptByID(id).FirstOrDefault();

                receiptToCancel.Canceled = 1;

                recieptRepo.UpdateReceipt(receiptToCancel);
            }
        }
        // Opens an another application for showing .pdf files. Opens up the defualt .pdf opener for the user.
        public void OpenReciept(string receiptName)
        {
            string filePath = Path.Combine("../../../BusinessLogicLayer/Receipts", receiptName);
            if (File.Exists(filePath))
            {
                try
                {
                    Process.Start(filePath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error opening report '{receiptName}': {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine($"Report '{receiptName}' does not exist.");
            }
        }
    }
}
