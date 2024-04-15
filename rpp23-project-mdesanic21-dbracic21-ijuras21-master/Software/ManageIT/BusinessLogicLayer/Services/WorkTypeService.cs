using DataAccessLayer.Repositories;
using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services {
    public class WorkTypeService {
        public List<WorkType> GetWorkTypes() {
            using(var repo = new WorkTypeRepository()) {
                return repo.GetAll().ToList();
            }
        }
    }
}
