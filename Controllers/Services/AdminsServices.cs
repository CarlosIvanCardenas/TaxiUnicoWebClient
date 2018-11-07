using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TaxiUnicoWebClient.Models.Classes;

namespace TaxiUnicoWebClient.Controllers.Services
{
    public class AdminsServices
    {
        public static HttpClientHandler httpClientHandler = new HttpClientHandler(){ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; }};
        public static HttpClient client = new HttpClient(httpClientHandler){BaseAddress = new Uri("http://206.189.164.14:80/")};
        
        public async Task<Administrador> GetAdminByIdAsync(Guid id)
        {
            var response = await client.GetAsync($"/api/admins/{id}");
            Administrador admin = await response.Content.ReadAsAsync<Administrador>();
            return admin;
        }

        public async Task<Administrador> GetAdminByEmailAsync(string email)
        {
            var response = await client.GetAsync($"/api/admins/email/{email}");
            Administrador admin = await response.Content.ReadAsAsync<Administrador>();
            return admin;
        }

        public async Task<List<Administrador>> GetAllAdmins()
        {
            var response = await client.GetAsync("/api/admins");
            List<Administrador> admins = await response.Content.ReadAsAsync<List<Administrador>>();
            return admins;
        }

        public async Task<Uri> CreateAdminAsync(Administrador admin)
        {
            var response = await client.PostAsJsonAsync("api/admins", admin);
            response.EnsureSuccessStatusCode();
            // URI of the created resource.
            var createdAt = response.Headers.Location;
            return createdAt;
        }

        public async Task<Administrador> UpdateAdminAsync(Administrador admin)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync($"api/admins/{admin.Id}", admin);
            response.EnsureSuccessStatusCode();

            // Deserialize the updated product from the response body.
            admin = await response.Content.ReadAsAsync<Administrador>();
            return admin;
        }
    }
}