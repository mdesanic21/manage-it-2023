using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories {
    /// <remarks>
    /// Ivan Juras
    /// </remarks>
    public class OrderDetailsRepository: Repository<OrderDetail> {
        /// <remarks>
        /// Ivan Juras
        /// </remarks>
        public OrderDetailsRepository(): base(new ManageIT())
        {
            
        }
        /// <remarks>
        /// Ivan Juras
        /// </remarks>
        public override int Add(OrderDetail entity, bool saveChanges = true) {
            var client = Context.Clients.SingleOrDefault(c => c.ID_client == entity.Client.ID_client);
            var workType = Context.WorkTypes.SingleOrDefault(w => w.ID_Work_Type == entity.WorkType.ID_Work_Type);
            var worker = Context.Workers.SingleOrDefault(w => w.ID_worker == entity.Worker.ID_worker);

            var orderDetail = new OrderDetail();
            orderDetail.Client = client;
            orderDetail.WorkType = workType;
            orderDetail.Worker = worker;
            orderDetail.Duration = entity.Duration;
            orderDetail.Date = entity.Date;
            orderDetail.Location = entity.Location;
            
            Entities.Add(orderDetail);
            if(saveChanges) {
                return SaveChanges();
            } else {
                return 0;
            }
        }
        /// <remarks>
        /// Ivan Juras
        /// </remarks>
        public OrderDetail GetOrderDetailById(int id) {
            var orderDetail = Context.OrderDetails.SingleOrDefault(o => o.Id_Order_Details == id);
            return orderDetail;
        }
        /// <remarks>
        /// Ivan Juras
        /// </remarks>
        public int Update(OrderDetail entity, bool saveChanges = true) {
            var client = Context.Clients.SingleOrDefault(c => c.ID_client == entity.Client.ID_client);
            var workType = Context.WorkTypes.SingleOrDefault(w => w.ID_Work_Type == entity.WorkType.ID_Work_Type);
            var worker = Context.Workers.SingleOrDefault(w => w.ID_worker == entity.Worker.ID_worker);

            var orderDetail = Context.OrderDetails.SingleOrDefault(o => o.Id_Order_Details == entity.Id_Order_Details);
            orderDetail.Client = client;
            orderDetail.WorkType = workType;
            orderDetail.Worker = worker;
            orderDetail.Duration = entity.Duration;
            orderDetail.Date = entity.Date;
            orderDetail.Location = entity.Location;

            if (saveChanges) {
                return SaveChanges();
            } else {
                return 0;
            }
        }
        /// <remarks>
        /// Ivan Juras
        /// </remarks>
        public List<OrderDetail> GetOrderDetailsForWorkerAndDate(int workerId, DateTime date) {
            return Context.OrderDetails
                .Where(od => od.ID_Worker == workerId && od.Date == date.Date)
                .ToList();
        }
    }
}
