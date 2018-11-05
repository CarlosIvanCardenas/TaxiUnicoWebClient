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

namespace TaxiUnicoWebClient.Controllers
{
    public class HomeController : Controller
    {
        public static HttpClientHandler httpClientHandler = new HttpClientHandler(){ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; }};
        HttpClient client = new HttpClient(httpClientHandler){BaseAddress = new Uri("http://localhost:5000/")};
        public async Task<IActionResult> Index()
        {
            Cliente cliente = null; //await GetClienteAsync("api/clientes/08d63c34-47d5-b8ce-d7d0-f04d9c0a197f");
            var response = await client.GetAsync("/api/clientes/08d63c34-47d5-b8ce-d7d0-f04d9c0a197f");
            cliente = await response.Content.ReadAsAsync<Cliente>();
            Console.WriteLine($"AQUI: {cliente.PrimerNombre}");
            
            return View(cliente);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
