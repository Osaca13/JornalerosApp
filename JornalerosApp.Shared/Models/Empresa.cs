using System;
using System.Collections.Generic;

namespace JornalerosApp.Shared.Models
{
    public class Empresa
    {
        public Empresa()
        {
            Oferta = new HashSet<Oferta>();
        }

        public string IdEmpresa { get; set; }
        public string Nombre { get; set; }
        public string CorreoElectronico { get; set; }
        public string RazonSocial { get; set; }
        public string Direccion { get; set; }
        public decimal? Telefono { get; set; }
        public string Provincia { get; set; }

        public virtual ICollection<Oferta> Oferta { get; set; }
    }
}
