using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaxiUnicoWebClient.Models.Classes
{
    public class Vehiculo
    {
        [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public string Placa { get; set; }

        [Required]
        public Guid TaxistaId { get; set; }

        [ForeignKey("TaxistaId")]
        public Taxista Taxista { get; set; }

        [Required]
        public string Marca { get; set; }

        [Required]
        public string Modelo { get; set; }

        [Required]
        public string Color { get; set; }
        public string PolizaSeguro { get; set; }
    }
}