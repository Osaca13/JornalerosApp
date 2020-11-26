using JornalerosApp.Infrastructure.Data;
using JornalerosApp.Shared.Models;
using JornalerosApp.Shared.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace JornalerosApp.Services
{
    public class OfertaDbServices : DbServices<Oferta>
    {
        public OfertaDbServices(ApplicationDbContext applicationDbContext):base(applicationDbContext)
        {
        }
    }
}
