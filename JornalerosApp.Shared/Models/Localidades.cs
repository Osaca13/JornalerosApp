using System;
using System.Collections.Generic;

namespace JornalerosApp.Shared.Models
{
    public partial class Localidades
    {
        public string Id { get; set; }
        public string Provincia { get; set; }
        public string Localidad { get; set; }
        public int? CodigoPostal { get; set; }
    }
}
