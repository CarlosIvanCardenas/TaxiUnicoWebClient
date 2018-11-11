using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TaxiUnicoWebClient.Models.Classes;

namespace TaxiUnicoWebClient.Controllers.Services
{
    public class ClientesServices
    {
        public static HttpClientHandler httpClientHandler = new HttpClientHandler(){ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; }};
        public static HttpClient client = new HttpClient(httpClientHandler){BaseAddress = new Uri("http://206.189.164.14:80/")};
        public async Task<Cliente> GetClienteByIdAsync(Guid id)
        {
            var response = await client.GetAsync($"/api/clientes/{id}");
            Cliente cliente = await response.Content.ReadAsAsync<Cliente>();
            return cliente;
        }

        public async Task<Cliente> GetClienteByEmailAsync(string email)
        {
            var response = await client.GetAsync($"/api/clientes/email/{email}");
            Cliente cliente = await response.Content.ReadAsAsync<Cliente>();
            return cliente;
        }

        public async Task<List<Cliente>> GetAllClientes()
        {
            var response = await client.GetAsync("/api/clientes");
            List<Cliente> clientes = await response.Content.ReadAsAsync<List<Cliente>>();
            return clientes;
        }

        public async Task<Uri> CreateClienteAsync(Cliente cliente)
        {
            var response = await client.PostAsJsonAsync("api/clientes", cliente);
            response.EnsureSuccessStatusCode();
            // URI of the created resource.
            var createdAt = response.Headers.Location;
            return createdAt;
        }

        public async Task<Cliente> UpdateClienteAsync(Cliente cliente)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync(
                $"api/clientes/{cliente.Id}", cliente);
            response.EnsureSuccessStatusCode();

            // Deserialize the updated product from the response body.
            cliente = await response.Content.ReadAsAsync<Cliente>();
            return cliente;
        }

        public async Task<List<Viaje>> GetViajesByCliente(Guid id)
        {
            var response = await client.GetAsync($"/api/viajes/cliente/{id}");
            List<Viaje> viajes = await response.Content.ReadAsAsync<List<Viaje>>();
            return viajes;
        }
    }
}