using System;
using System.Collections.Generic;
using TaxiUnicoWebClient.Models.Classes;

namespace TaxiUnicoWebClient.Models
{
    public class NewVehiculoModel
    {
        public List<Vehiculo> Vehiculos { get; set; }

        public Guid TaxistaId { get; set;}
    }
}