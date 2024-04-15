using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    /// <remarks>
    /// Matej Desanić
    /// </remarks>
    public class ReportRepo : Repository<WorkOrder>
    {
        public ReportRepo() : base(new ManageIT())
        {

        }

        public IQueryable<WorkOrder> GetWorkOrderByDate(DateTime startDate, DateTime finishDate)
        {
            var query = from wo in Entities
                        where wo.DateCreated >= startDate && wo.DateCreated <= finishDate
                        select wo;

            return query;
        }

        public IQueryable<WorkOrder> GetWorkOrdersByDateAndWorker(DateTime startDate, DateTime finishDate, int id)
        {
            var query = from wo in Entities
                        where wo.OrderDetail.ID_Worker == id &&
                              wo.DateCreated >= startDate && wo.DateCreated <= finishDate
                        select wo;

            return query;

        }
    }
}
