using System.Collections.Generic;
using MongoDB.Driver;
using PAPIMONGO.Models;
using PAPIMONGO.Utils;

namespace PAPIMONGO.Services
{
    public class ClientServices
    {
        private readonly IMongoCollection<Client> _clients;
        private readonly IMongoCollection<Address> _address;
        public ClientServices(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _clients = database.GetCollection<Client>(settings.ClientCollectionName);
        }

        public Client Create(Client client)
        {
            _clients.InsertOne(client);
            return client;
        }
        public List<Client> Get() => _clients.Find<Client>(client => true).ToList();

        public Client Get(string id) => _clients.Find<Client>(client => client.Id == id).FirstOrDefault();
        public Client GetAddress(string idAddress) => _clients.Find<Client>(client => client.Address.Id == idAddress).FirstOrDefault();
        public Client GetName(string nome) => _clients.Find<Client>(client => client.Name == nome).FirstOrDefault();
        public void Update(string id, Client clientIn)
        {
            _clients.ReplaceOne(client => client.Id == id, clientIn);
            Get(clientIn.Id);
        }
        public void Remove(Client clientIn) => _clients.DeleteOne(client => client.Id == clientIn.Id);
    }
}
