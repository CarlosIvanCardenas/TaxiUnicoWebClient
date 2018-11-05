using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaxiUnicoWebClient.Models;
using TaxiUnicoWebClient.Models.Classes;
using TaxiUnicoWebClient.Controllers.Services;

namespace TaxiUnicoWebClient.Controllers
{
    public class ClientesController : Controller
    {
        ClientesServices service = new ClientesServices();

        [HttpGet]
        public IActionResult Index()
        {
            Cliente cliente = new Cliente();
            return View(cliente);
        }

        [HttpGet, ActionName("GetAll")]
        public async Task<IActionResult> GetAllClientesAsync()
        {
            var clientes = await service.GetAllClientes();
            return View(clientes);
        }

        [HttpGet, ActionName("GetById")]
        public async Task<IActionResult> GetClienteByIdAsync(Guid id)
        {
            var cliente = await service.GetClienteByIdAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        [HttpPost, ActionName("GetById")]
        public async Task<IActionResult> GetClienteByIdAsync(Cliente cliente)
        {
            var updated = await service.UpdateClienteAsync(cliente);
            return View(updated);
        }

        [HttpGet, ActionName("GetByEmail")]
        public async Task<IActionResult> GetClienteByEmailAsync(string correo)
        {
            var cliente = await service.GetClienteByEmailAsync(correo);
            if (cliente == null)
            {
                return NotFound();
            }
            return RedirectToAction("GetById", new {id = cliente.Id});
        }

        [ActionName("Delete")]
        public async Task<IActionResult> DeleteClienteAsync(Guid id)
        {
            var cliente = await service.GetClienteByIdAsync(id);
            cliente.Estatus = "Inactivo";
            var updated = await service.UpdateClienteAsync(cliente);
            return RedirectToAction("GetAll");
        }

        [ActionName("Activate")]
        public async Task<IActionResult> ActivateClienteAsync(Guid id)
        {
            var cliente = await service.GetClienteByIdAsync(id);
            cliente.Estatus = "Activo";
            var updated = await service.UpdateClienteAsync(cliente);
            return RedirectToAction("GetAll");
        }
    }
}
