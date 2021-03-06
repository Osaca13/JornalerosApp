﻿using JornalerosApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace JornalerosApp.Shared.Entities
{
    public class OfertaModel
    {
        public OfertaModel()
        {
            RelacionOfertaPersona = new RelacionOfertaPersona[] { };
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

        //public EmpresaModel IdEmpresaNavigation { get; set; }
        public RelacionOfertaPersona[] RelacionOfertaPersona { get; set; }
    }
}
