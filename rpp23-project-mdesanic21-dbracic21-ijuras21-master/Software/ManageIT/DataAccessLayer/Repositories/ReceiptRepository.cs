using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    /// <remarks>
    /// Matej Desanić
    /// </remarks>
    public class ReceiptRepository: Repository<Receipt>
    {
        public ReceiptRepository() : base(new ManageIT())
        {

        }

        public IQueryable<Receipt> GetAllCanceled()
        {
            var query = from rec in Entities
                        where rec.Canceled == 1
                        select rec;
            return query;
        }

        public IQueryable<Receipt> GetAllNotCanceled()
        {
            var query = from rec in Entities
                        where rec.Canceled == 0
                        select rec;
            return query;
        }

        public IQueryable<Receipt> GetReceiptByID(int id)
        {
            var query = from rec in Entities
                        where rec.ID_receipt == id
                        select rec;

            return query;
        }

        public bool UpdateReceipt(Receipt receipt)
        {
            var receiptToCancel = Entities.FirstOrDefault(rec => rec.ID_receipt == receipt.ID_receipt) as Receipt;
            if (receiptToCancel != null)
            {
                receiptToCancel.ID_receipt = receipt.ID_receipt;
                receiptToCancel.ID_Worker =receipt.ID_Worker;
                receiptToCancel.ID_Work_Order = receipt.ID_Work_Order;
                receiptToCancel.OIB = receipt.OIB;
                receiptToCancel.Worker = receipt.Worker;
                receiptToCancel.WorkOrder = receipt.WorkOrder;
                SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
