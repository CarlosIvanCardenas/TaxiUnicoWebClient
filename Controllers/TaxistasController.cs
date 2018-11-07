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
    public class TaxistasController : Controller
    {
        TaxistasServices service = new TaxistasServices();

        [HttpGet]
        public IActionResult Index()
        {
            Taxista taxista = new Taxista();
            return View(taxista);
        }

        [HttpGet, ActionName("GetAll")]
        public async Task<IActionResult> GetAllTaxistasAsync()
        {
            var taxistas = await service.GetAllTaxistas();
            return View(taxistas);
        }

        [HttpGet, ActionName("GetById")]
        public async Task<IActionResult> GetTaxistaByIdAsync(Guid id)
        {
            var taxista = await service.GetTaxistaByIdAsync(id);
            if (taxista == null)
            {
                return NotFound();
            }
            return View(taxista);
        }

        [HttpPost, ActionName("GetById")]
        public async Task<IActionResult> GetTaxistaByIdAsync(Taxista taxista)
        {
            var updated = await service.UpdateTaxistaAsync(taxista);
            return View(updated);
        }

        [HttpGet, ActionName("GetByEmail")]
        public async Task<IActionResult> GetTaxistaByEmailAsync(string correo)
        {
            var taxista = await service.GetTaxistaByEmailAsync(correo);
            if (taxista == null)
            {
                return NotFound();
            }
            return RedirectToAction("GetById", new {id = taxista.Id});
        }

        [ActionName("Delete")]
        public async Task<IActionResult> DeleteTaxistaAsync(Guid id)
        {
            var taxista = await service.GetTaxistaByIdAsync(id);
            taxista.Estatus = "Inactivo";
            var updated = await service.UpdateTaxistaAsync(taxista);
            return RedirectToAction("GetAll");
        }

        [ActionName("Activate")]
        public async Task<IActionResult> ActivateTaxistaAsync(Guid id)
        {
            var taxista = await service.GetTaxistaByIdAsync(id);
            taxista.Estatus = "Activo";
            var updated = await service.UpdateTaxistaAsync(taxista);
            return RedirectToAction("GetAll");
        }
        
        [HttpGet, ActionName("Create")]
        public IActionResult CreateTaxista()
        {
            return View();
        }
        
        [HttpPost, ActionName("Create")]
        public async Task<IActionResult> CreateTaxistaAsync(Taxista taxista)
        {
            taxista.Id = Guid.NewGuid();
            taxista.Estatus = "Activo";
            var createdAt = await service.CreateTaxistaAsync(taxista);
            Console.WriteLine($"URL: {createdAt}");
            return Redirect(createdAt.ToString());
        }
    }
}
