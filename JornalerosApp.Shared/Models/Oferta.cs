using System;
using System.Collections.Generic;

namespace JornalerosApp.Shared.Models
{
    public partial class Oferta
    {
        public Oferta()
        {
            RelacionOfertaPersona = new HashSet<RelacionOfertaPersona>();
        }

        public string IdOferta { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string IdEmpresa { get; set; }
        public DateTime? FechaPublicacion { get; set; }
        public DateTime? FechaCaducidad { get; set; }
        public string Alojamiento { get; set; }
        public string TipoContrato { get; set; }
        public string JornadaReal { get; set; }
        public string Salario { get; set; }
        public int? CantidadPersonas { get; set; }
        public string LugarTrabajo { get; set; }
        public string Provincia { get; set; }
        public string ContinuidadIgualLabor { get; set; }
        public string ContinuidadOtraLabor { get; set; }

        public virtual Empresa IdEmpresaNavigation { get; set; }
        public virtual ICollection<RelacionOfertaPersona> RelacionOfertaPersona { get; set; }
    }
}
