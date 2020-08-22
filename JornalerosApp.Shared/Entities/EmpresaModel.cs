using System;
using System.Collections.Generic;
using System.Text;

namespace JornalerosApp.Shared.Entities
{
    public class EmpresaModel
    {
        public EmpresaModel()
        {
            Oferta = new OfertaModel[] { };
        }

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

        public OfertaModel[] Oferta { get; set; }
    }
}
