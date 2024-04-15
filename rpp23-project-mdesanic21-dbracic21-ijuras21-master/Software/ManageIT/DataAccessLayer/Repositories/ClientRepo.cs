using DataAccessLayer.Models;
using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class ClientRepo : Repository<Client>
    {
        public ClientRepo() : base(new ManageIT())
        {

        }

        public IQueryable<Client> GetAllClients()
        {
            var query = from c in Entities
                        select c;

            return query;
        }
        public IQueryable<ClientViewModel> GetClientView()
        {
            var query = from c in Entities
                        select new ClientViewModel
                        {
                            ID_Client = c.ID_client,
                            TypeName = c.ClientType.Title,
                            FirstName = c.FirstName,
                            LastName = c.LastName,
                            CompanyName = c.CompanyName,
                            IBAN = c.IBAN,
                            Email = c.Email,
                            Address = c.Client_Address,
                            Number = c.Number
                        };
            return query;
        }

        public IQueryable<ClientViewModel> SearchClients(string key)
        {
            var query = from c in Entities
                        where c.CompanyName.Contains(key) || c.FirstName.Contains(key) || c.LastName.Contains(key)
                        select new ClientViewModel
                        {
                            ID_Client = c.ID_client,
                            TypeName = c.ClientType.Title,
                            FirstName = c.FirstName,
                            LastName = c.LastName,
                            CompanyName = c.CompanyName,
                            IBAN = c.IBAN,
                            Email = c.Email,
                            Address = c.Client_Address,
                            Number = c.Number
                        };
            return query;
        }

        public IQueryable<Client> GetClientById(int id)
        {
            var query = from c in Entities
                        where c.ID_client == id
                        select c;

            return query;
        }

        public bool DeleteClient (int id)
        {
            var clientToDelete = Entities.FirstOrDefault(c => c.ID_client == id);
            if (clientToDelete != null)
            {
                Entities.Remove(clientToDelete);
                SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateClient (Client client)
        {
            var clientToBeUpdated = Entities.FirstOrDefault(c => c.ID_client == client.ID_client) as Client;
            if (clientToBeUpdated != null)
            {
                clientToBeUpdated.ID_client = client.ID_client;
                clientToBeUpdated.Email = client.Email;
                clientToBeUpdated.FirstName = client.FirstName;
                clientToBeUpdated.LastName = client.LastName;
                clientToBeUpdated.CompanyName = client.CompanyName;
                clientToBeUpdated.IBAN = client.IBAN;
                clientToBeUpdated.Client_Address = client.Client_Address;
                clientToBeUpdated.Number = client.Number;

                SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AddClient (Client client, bool saveChanges = true)
        {
            var clientType = Context.ClientTypes.SingleOrDefault(c => c.ID_type == client.ClientType.ID_type);
            Client newClient = new Client();

            newClient.ID_client = client.ID_client;
            newClient.Email = client.Email;
            newClient.FirstName = client.FirstName;
            newClient.LastName = client.LastName;
            newClient.CompanyName = client.CompanyName;
            newClient.IBAN = client.IBAN;
            newClient.Client_Address = client.Client_Address.ToString();
            newClient.Number = client.Number;
            newClient.ID_type = client.ID_type;

            Entities.Add(newClient);

            if (saveChanges)
            {
                SaveChanges();
                return true;
            }
            else { return false; };
        }
    }
}