using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaxiUnicoWebClient.Models.Classes
{
    public class TarjetaCredito
    {
        [Required]
        public Guid ClienteId { get; set;}

        [ForeignKey("ClienteId")]
        public Cliente Cliente { get; set; }

        [Required]
        public string NumeroTarjeta { get; set; }

        [Required]
        public string FechaExpiracion { get; set; }

        [Required]
        public string CVC { get; set; }
    }
}