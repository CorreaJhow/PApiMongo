using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver.Linq;
using PAPIMONGO.Models;
using PAPIMONGO.Services;

namespace PAPIMONGO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly AddressServices _addressServices;
        private readonly ClientServices _clientServices;
        public AddressController(AddressServices addressServices, ClientServices clientServices)
        {
            _addressServices = addressServices;
            _clientServices = clientServices;   
        }
        [HttpGet]
        public ActionResult<List<Address>> Get() => _addressServices.Get();

        [HttpGet("{id:length(24)}", Name = "GetAddress")]
        public ActionResult<Address> Get(string id)
        {
            var address = _addressServices.Get(id); //buscar endereço 
            if (address == null) //verificar se nao esta vazio
                return NotFound();

            return Ok(address);
        }
        [HttpPost]
        public ActionResult<Address> Create(Address address)
        {
            _addressServices.Create(address);
            return CreatedAtRoute("GetAddress", new { id = address.Id.ToString() }, address); ;
        }
        [HttpPut]
        public ActionResult<Address> Update(string id, Address address)
        {
            var client = _addressServices.Get(id); //apenas verificação se o cliente existe
            if (client == null)
                return NotFound();

            _addressServices.Update(id, address); //manipulação do objeto criado!!
            return NoContent();
        }
        [HttpDelete]
        public ActionResult Remove(string id)
        {
            var address = _addressServices.Get(id); //verificação se o objeto existe, se sim ele trás.
            if (address == null)
                return NotFound();

            _addressServices.Remove(address);
            return NoContent();
        }
    }
}
