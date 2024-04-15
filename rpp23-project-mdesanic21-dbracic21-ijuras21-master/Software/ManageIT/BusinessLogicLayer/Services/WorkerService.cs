using DataAccessLayer.Repositories;
using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    ///<remarks>Darijo Bračić </remarks>

    public class WorkerService
    {
        ///<remarks>Darijo Bračić </remarks>
        public List<Worker> GetWorkerByEmailAndPassword(string username, string password)
        {
            using (var workerRepo = new WorkerRepo())
            {
                return workerRepo.GetWorkerByEmailAndPassword(username, password).ToList();
            }
        }
        ///<remarks>Darijo Bračić </remarks>
        public Worker Authenticate(string username, string password)
        {
            using (var workerRepo = new WorkerRepo())
            {
                Worker authenticatedWorker = workerRepo.GetWorkerByEmailAndPassword(username, password).FirstOrDefault();

                return authenticatedWorker;
            }
        }
                ///<remarks>Darijo Bračić </remarks>


        public List<Worker> GetAllWorkers()
        {
            using (var workerRepo = new WorkerRepo())
            {
                return workerRepo.GetAll().ToList();
            }
        }
        ///<remarks>Darijo Bračić </remarks>
        public List<Worker> GetWorkers()
        {
            using (var workerRepo = new WorkerRepo())
            {
                return workerRepo.GetWorkers().ToList();
            }
        }
        /// <remarks>
        /// Matej Desanić
        /// </remarks>
        public Worker GetWorkersByID(int id)
        {
            using (var workerRepo = new WorkerRepo())
            {
                Worker worker = workerRepo.GetWorkerByID(id).FirstOrDefault();
                return worker;
            }
        }
        ///<remarks>Darijo Bračić </remarks>

        public bool RemoveWorker(Worker worker)
        {
            bool isSuccessful = false;
            bool canRemove = CheckIfWorkerCanBeRemoved(worker);
            using (var workerRepo = new WorkerRepo())
            {
                if (canRemove == true)
                {
                    int affectedRows = workerRepo.Remove(worker);
                    isSuccessful = affectedRows > 0;
                }
            }
            return isSuccessful;
        }

        ///<remarks>Darijo Bračić </remarks>

        private bool CheckIfWorkerCanBeRemoved(Worker worker)
        {
          if(worker == null) return false;
          using (var workerRepo = new WorkerRepo())
            {
                int numOfProducts = workerRepo.GetNumberOfProducts(worker).Single();
                if(numOfProducts > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        ///<remarks>Darijo Bračić </remarks>
        public bool AddWorker(Worker worker)
        {
            bool isSuccessful = false;
            using (var workerRepo = new WorkerRepo())
            {
                int affectedRows = workerRepo.Add(worker);
                isSuccessful = affectedRows > 0;
            }
            return isSuccessful;
        }
        ///<remarks>Darijo Bračić </remarks>
        public bool UpdateWorker(Worker worker)
        {
            bool isSuccessful = false;
            using (var workerRepo = new WorkerRepo())
            {
                int affectedRows = workerRepo.Update(worker);
                isSuccessful = affectedRows > 0;
            }
            return isSuccessful;
        }

     

        }   
    }

