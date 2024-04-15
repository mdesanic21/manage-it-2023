using DataAccessLayer.Repositories;
using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services {
    /// <remarks>
    /// Ivan Juras
    /// </remarks>
    public class OrderDetailService {
        /// <remarks>
        /// Ivan Juras
        /// </remarks>
        public bool AddOrderDetail(OrderDetail orderDetail) {
            bool isSuccessful = false;
            using(var repo = new OrderDetailsRepository()) {
               int affectedRows = repo.Add(orderDetail);
               isSuccessful = affectedRows > 0;
            }
            return isSuccessful;
        }
        /// <remarks>
        /// Ivan Juras
        /// </remarks>
        public OrderDetail GetOrderDetail(int id) {
            using (var repo = new OrderDetailsRepository()) {
                var orderDetail = repo.GetOrderDetailById(id);
                return orderDetail;
            }
        }
        /// <remarks>
        /// Ivan Juras
        /// </remarks>
        public bool UpdateOrderDetail(OrderDetail orderDetail) {
            bool isSuccessful = false;
            using (var repo = new OrderDetailsRepository()) {
                int affectedRows = repo.Update(orderDetail);
                isSuccessful = affectedRows > 0;
            }
            return isSuccessful;
        }
        /// <remarks>
        /// Ivan Juras
        /// </remarks>
        public List<OrderDetail> GetOrderDetailsForWorkerAndDate(int workerId, DateTime date) {
            using (var repo = new OrderDetailsRepository()) {
                var orderDetail = repo.GetOrderDetailsForWorkerAndDate(workerId, date);
                return orderDetail;
            }
        }
    }
}
