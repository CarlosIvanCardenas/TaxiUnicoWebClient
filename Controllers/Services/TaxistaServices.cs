using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TaxiUnicoWebClient.Models.Classes;

namespace TaxiUnicoWebClient.Controllers.Services
{
    public class TaxistasServices
    {
        public static HttpClientHandler httpClientHandler = new HttpClientHandler(){ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; }};
        public static HttpClient client = new HttpClient(httpClientHandler){BaseAddress = new Uri("http://206.189.164.14:80/")};
        public async Task<Taxista> GetTaxistaByIdAsync(Guid id)
        {
            var response = await client.GetAsync($"/api/taxistas/{id}");
            Taxista taxista = await response.Content.ReadAsAsync<Taxista>();
            return taxista;
        }

        public async Task<Taxista> GetTaxistaByEmailAsync(string email)
        {
            var response = await client.GetAsync($"/api/taxistas/email/{email}");
            Taxista taxista = await response.Content.ReadAsAsync<Taxista>();
            return taxista;
        }

        public async Task<List<Taxista>> GetAllTaxistas()
        {
            var response = await client.GetAsync("/api/taxistas");
            List<Taxista> taxistas = await response.Content.ReadAsAsync<List<Taxista>>();
            return taxistas;
        }

        public async Task<List<Viaje>> GetViajesByTaxista(Guid id)
        {
            var response = await client.GetAsync($"/api/viajes/taxista/{id}");
            List<Viaje> viajes = await response.Content.ReadAsAsync<List<Viaje>>();
            return viajes;
        }

        public async Task<List<Vehiculo>> GetVehiculosByTaxista(Guid id)
        {
            var response = await client.GetAsync($"/api/vehiculos/taxista/{id}");
            List<Vehiculo> vehiculos = await response.Content.ReadAsAsync<List<Vehiculo>>();
            return vehiculos;
        }

        public async Task<Uri> CreateTaxistaAsync(Taxista taxista)
        {
            var response = await client.PostAsJsonAsync("api/taxistas", taxista);
            response.EnsureSuccessStatusCode();
            // URI of the created resource.
            var createdAt = response.Headers.Location;
            return createdAt;
        }

        public async Task<Taxista> UpdateTaxistaAsync(Taxista taxista)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync($"api/taxistas/{taxista.Id}", taxista);
            response.EnsureSuccessStatusCode();

            // Deserialize the updated product from the response body.
            taxista = await response.Content.ReadAsAsync<Taxista>();
            return taxista;
        }

        public async Task<Uri> CreateVehiculoAsync(Vehiculo vehiculo)
        {
            var response = await client.PostAsJsonAsync("api/vehiculos", vehiculo);
            response.EnsureSuccessStatusCode();
            // URI of the created resource.
            var createdAt = response.Headers.Location;
            return createdAt;
        }
    }
}