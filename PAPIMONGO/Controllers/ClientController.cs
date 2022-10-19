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
        public ClientController(ClientServices clientServices)
        {
            _clientServices = clientServices;
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
        [HttpPost]
        public ActionResult<Client> Create(Client client)
        {
            _clientServices.Create(client);
            return CreatedAtRoute("GetClient", new {id = client.Id.ToString()}, client);
        }
        [HttpPut]
        public ActionResult<Client> Update(string id, Client clientIn)
        {
            var client = _clientServices.Get(id); //apenas verificação se o cliente existe
            if (client == null)
                return NotFound();

            _clientServices.Update(id, clientIn); //manipulação do objeto criado!!
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
