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
    public class ClientTypeRepo: Repository<ClientType>
    {
        public ClientTypeRepo() : base(new ManageIT())
        {

        }

        public IQueryable<ClientType> GetClientTypes()
        {
            var query = from ct in Entities
                        select ct;

            return query;
        }
    }
}
