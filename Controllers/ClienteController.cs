using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using apiSorteio.Data;
using apiSorteio.Models;
using System.IO;
using apiSorteio.Entity;

namespace apiSorteio.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly ApiContext _context;
        public ClienteController(ApiContext context)
        {
            _context = context;
        }
        // GET: api/Cliente
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetCliente()
        {
            return await _context.Clientes.ToListAsync();
        }
        // GET: api/Cliente/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {
            return await _context.Clientes.FindAsync(id);
        }
        // POST: api/Cliente
        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente(Cliente cliente)
        {
            ClienteEntity clienteEntity = new ClienteEntity(_context);

            string Result  = await clienteEntity.AddCliente(cliente);
            if(Result == "OK")
            {
                return CreatedAtAction("GetCliente", new { id = cliente.ClienteID }, cliente);
            }
            else
            {
                return Conflict("Você já está cadastrado. Verifique suas credenciais.");
            }
        }


    }
}
