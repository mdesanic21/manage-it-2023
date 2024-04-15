using DataAccessLayer.Repositories;
using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    /// <remarks>
    /// Matej Desanić
    /// </remarks>
    // Used to get the list of client types for usage in selecting client type inside the form.
    public class ClientTypeService
    {
        public List<ClientType> GetClientTypes() 
        {
            using (var clientTypeRepo = new ClientTypeRepo())
            {
                return clientTypeRepo.GetClientTypes().ToList();
            }
        }
    }
}
