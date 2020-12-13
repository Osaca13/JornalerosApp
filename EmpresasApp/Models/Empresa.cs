using System;
using System.Collections.Generic;

namespace EmpresasApp.Models
{
    public partial class Empresa
    {
        public string IdEmpresa { get; set; }
        public string NombreEmpresa { get; set; }
        public string NombreContacto { get; set; }
        public string Cargo { get; set; }
        public string Nifcif { get; set; }
        public string CorreoElectronico { get; set; }
        public string Actividad { get; set; }
        public string CodigoPostal { get; set; }
        public string Dirección { get; set; }
        public decimal? Telefono { get; set; }
        public string Provincia { get; set; }
    }
}
