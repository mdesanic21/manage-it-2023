using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories {
    /// <remarks>
    /// Ivan Juras
    /// </remarks>
    public class WorkTypeRepository : Repository<WorkType> {
        /// <remarks>
        /// Ivan Juras
        /// </remarks>
        public WorkTypeRepository() : base(new ManageIT())
        {
            
        }
    }
}
