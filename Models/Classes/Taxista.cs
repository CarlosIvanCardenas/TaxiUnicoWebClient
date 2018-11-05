using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaxiUnicoWebClient.Models.Classes
{
    public class Taxista
    {
        [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public string Correo { get; set; }

        [Required]
        public string Contraseña { get; set; }

        [Required]
        public string PrimerNombre { get; set; }

        public string SegundoNombre { get; set; }

        [Required]
        public string Apellidos { get; set; }

        [Required]
        public string Telefono { get; set; }

        public string Direccion { get; set; }

        [Column(TypeName = "decimal(2, 1)")]
        public decimal Puntuacion { get; set; }

        //RegistradoPor
        [Required]
        public Guid AdministradorId { get; set; }

        [ForeignKey("AdministradorId")]
        public Administrador Administrador { get; set; }

        [Required]
        public DateTime FechaRegistro { get; set; }
        
	    public DateTime FechaModificado { get; set; }

	    public string Estatus { get; set; }
    }
}
