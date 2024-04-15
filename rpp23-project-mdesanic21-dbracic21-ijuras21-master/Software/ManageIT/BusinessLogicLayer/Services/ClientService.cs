
using DataAccessLayer.Models;
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
    // Has all normal service methods working with repository. Used mainly to load client data, change client data, remove client, add client, and fetching client by some filters
    public class ClientService {
        public List<Client> GetClients() {
            using (var clientRepo = new ClientRepo()) {

                return clientRepo.GetAll().ToList();
            }
        }
        public List<ClientViewModel> GetClientsView() {
            using (var clientRepo = new ClientRepo()) {
                return clientRepo.GetClientView().ToList();
            }
        }

        public List<ClientViewModel> SearchClients(string key) {
            using (var clientRepo = new ClientRepo()) {
                return clientRepo.SearchClients(key).ToList();
            }
        }

        public Client GetClientById(int id) {
            using (var clientRepo = new ClientRepo()) {
                Client client = clientRepo.GetClientById(id).FirstOrDefault();
                return client;
            }
        }
        public void DeleteClient(int id) {
            using (var clientRepo = new ClientRepo()) {
                clientRepo.DeleteClient(id);
            }
        }

        public bool UpdateClient(Client client) {
            using (var clientRepo = new ClientRepo()) {
                List<Client> clients = GetAllClients();
                foreach (var clientSearch in clients) {
                    if (clientSearch.Email == client.Email) {
                        if (client.ID_client != clientSearch.ID_client) {
                            return false;
                        }
                    }
                }
                clientRepo.UpdateClient(client);
                return true;
            }
        }

        public List<Client> GetAllClients() {
            using (var clientRepo = new ClientRepo()) {
                return clientRepo.GetAllClients().ToList();
            }
        }

        public bool AddClient(Client client) {
            using (var clientRepo = new ClientRepo()) {
                List<Client> clients = clientRepo.GetAll().ToList();
                foreach (var clientSearch in clients) {
                    if (clientSearch.ID_client == client.ID_client) {
                        return false;
                    }
                    if (client.Email == clientSearch.Email) {
                        return false;
                    }
                }
                clientRepo.AddClient(client);
                return true;
            }
        }
    }
}



