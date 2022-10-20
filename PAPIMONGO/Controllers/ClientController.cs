using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PAPIMONGO.Models;
using PAPIMONGO.Services;

namespace PAPIMONGO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly ClientServices _clientServices;
        private readonly AddressServices _addressServices;
        public ClientController(ClientServices clientServices, AddressServices addressServices)
        {
            _clientServices = clientServices;
            _addressServices = addressServices;
        }

        [HttpGet]
        public ActionResult<List<Client>> Get() => _clientServices.Get();
        [HttpGet("{id:length(24)}", Name = "GetClient")]
        public ActionResult<Client> Get(string id)
        {
            var client = _clientServices.Get(id);
            if(client == null)
                return NotFound();

            return Ok(client);          
        }
        [HttpGet("GetAdress")] //achando por endereço
        public ActionResult<Client> GetAddress(string idAddress)
        {
            var client = _clientServices.Get(idAddress);
            if (client == null)
                return NotFound();
            return Ok(client);
        }
        [HttpGet("GetName")] //achando pelo nome
        public ActionResult<Client> GetName(string nome)
        {
            var client = _clientServices.Get(nome);
            if (client == null)
                return NotFound();
            return Ok(client);
        }


        [HttpPost]
        public ActionResult<Client> Create(Client client)
        {
            Address address = _addressServices.Create(client.Address);
            client.Address = address;

            _clientServices.Create(client);
            return CreatedAtRoute("GetClient", new {id = client.Id.ToString()}, client);
        }
        [HttpPut]
        public ActionResult<Client> Update(string id, Client clientIn)
        {
            var client = _clientServices.Get(id); //apenas verificação se o cliente existe
            if (client == null)
                return NotFound();

            _clientServices.Update(id, clientIn); //manipulação do objeto criado!! TAREFA AQUI!!
            return NoContent();         
        }
        [HttpDelete]
        public ActionResult Remove(string id)
        {
            var client = _clientServices.Get(id); //verificação se o objeto existe, se sim ele trás.
            if (client == null)
                return NotFound();

            _clientServices.Remove(client);
            return NoContent();
        }
    }
}
