﻿using System;
using System.Collections.Generic;

namespace JornalerosApp.Shared.Models
{
    public partial class Nacionalidad
    {
        public string IdNacionalidad { get; set; }
        public string IdPersona { get; set; }
        public string Tipo { get; set; }

        public virtual Persona IdPersonaNavigation { get; set; }
    }
}
