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
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace TaxiUnicoWebClient.Controllers
{
    public class TaxistasController : Controller
    {
        TaxistasServices service = new TaxistasServices();

        [HttpGet, Authorize]
        public IActionResult Index()
        {
            Taxista taxista = new Taxista();
            return View(taxista);
        }

        [HttpGet, ActionName("GetAll"), Authorize]
        public async Task<IActionResult> GetAllTaxistasAsync()
        {
            var taxistas = await service.GetAllTaxistas();
            return View(taxistas);
        }

        [HttpGet, ActionName("GetById"), Authorize]
        public async Task<IActionResult> GetTaxistaByIdAsync(Guid id)
        {
            var taxista = await service.GetTaxistaByIdAsync(id);
            if (taxista == null)
            {
                return NotFound();
            }
            return View(taxista);
        }

        [HttpPost, ActionName("GetById"), Authorize]
        public async Task<IActionResult> GetTaxistaByIdAsync(Taxista taxista)
        {
            var updated = await service.UpdateTaxistaAsync(taxista);
            return View(updated);
        }

        [HttpGet, ActionName("GetByEmail"), Authorize]
        public async Task<IActionResult> GetTaxistaByEmailAsync(string correo)
        {
            var taxista = await service.GetTaxistaByEmailAsync(correo);
            if (taxista == null)
            {
                return NotFound();
            }
            return RedirectToAction("GetById", new {id = taxista.Id});
        }

        [ActionName("Delete"), Authorize]
        public async Task<IActionResult> DeleteTaxistaAsync(Guid id)
        {
            var taxista = await service.GetTaxistaByIdAsync(id);
            taxista.Estatus = "Inactivo";
            var updated = await service.UpdateTaxistaAsync(taxista);
            return RedirectToAction("GetAll");
        }

        [ActionName("Activate"), Authorize]
        public async Task<IActionResult> ActivateTaxistaAsync(Guid id)
        {
            var taxista = await service.GetTaxistaByIdAsync(id);
            taxista.Estatus = "Activo";
            var updated = await service.UpdateTaxistaAsync(taxista);
            return RedirectToAction("GetAll");
        }
        
        [HttpGet, ActionName("Create"), Authorize]
        public IActionResult CreateTaxista()
        {
            return View();
        }
        
        [HttpPost, ActionName("Create"), Authorize]
        public async Task<IActionResult> CreateTaxistaAsync(Taxista taxista)
        {
            taxista.Id = Guid.NewGuid();
            taxista.Estatus = "Activo";
            taxista.Puntuacion = 5.0M;
            taxista.AdministradorId = new Guid(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
            var createdAt = await service.CreateTaxistaAsync(taxista);
            return RedirectToAction("GetById", new {id = taxista.Id});
        }

        [HttpGet, ActionName("Viajes"), Authorize]
        public async Task<IActionResult> GetViajesByTaxista(Guid id)
        {
            var viajes = await service.GetViajesByTaxista(id);
            return View(viajes);
        }

        [HttpGet, ActionName("Vehiculos"), Authorize]
        public async Task<IActionResult> GetVehiculosByTaxista(Guid id)
        {
            var vehiculos = await service.GetVehiculosByTaxista(id);
            var model = new NewVehiculoModel()
            {
                Vehiculos = vehiculos,
                TaxistaId = id
            };
            return View(model);
        }

        [HttpGet, ActionName("NewVehiculo"), Authorize]
        public IActionResult NewVehiculo(Guid id)
        {
            var vehiculo = new Vehiculo();
            vehiculo.Id = Guid.NewGuid();
            vehiculo.TaxistaId = id;
            
            return View(vehiculo);
        }

        [HttpPost, ActionName("NewVehiculo"), Authorize]
        public async Task<IActionResult> NewVehiculoAsync(Vehiculo vehiculo)
        {
            var createdAt = await service.CreateVehiculoAsync(vehiculo);
            return RedirectToAction("Vehiculos", new {id = vehiculo.TaxistaId});
        }
    }
}
