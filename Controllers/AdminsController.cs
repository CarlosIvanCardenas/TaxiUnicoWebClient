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
    public class AdminsController : Controller
    {
        AdminsServices service = new AdminsServices();

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet, ActionName("GetAll")]
        public async Task<IActionResult> GetAllAdminsAsync()
        {
            var admins = await service.GetAllAdmins();
            return View(admins);
        }

        [HttpGet, ActionName("GetById")]
        public async Task<IActionResult> GetAdminByIdAsync(Guid id)
        {
            var admin = await service.GetAdminByIdAsync(id);
            if (admin == null)
            {
                return NotFound();
            }
            return View(admin);
        }

        [HttpPost, ActionName("GetById")]
        public async Task<IActionResult> GetAdminByIdAsync(Administrador admin)
        {
            var updated = await service.UpdateAdminAsync(admin);
            return View(updated);
        }

        [HttpGet, ActionName("GetByEmail")]
        public async Task<IActionResult> GetAdminByEmailAsync(string correo)
        {
            var admin = await service.GetAdminByEmailAsync(correo);
            if (admin == null)
            {
                return NotFound();
            }
            return RedirectToAction("GetById", new {id = admin.Id});
        }
        
        [HttpGet, ActionName("Create")]
        public IActionResult CreateAdmin()
        {
            return View();
        }
        
        [HttpPost, ActionName("Create")]
        public async Task<IActionResult> CreateAdminAsync(Administrador admin)
        {
            admin.Id = Guid.NewGuid();
            var createdAt = await service.CreateAdminAsync(admin);
            return RedirectToAction("GetById", new {id = admin.Id});
        }
    }
}
