using JornalerosApp.Infrastructure.Data;
using JornalerosApp.Shared.Models;
using JornalerosApp.Shared.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JornalerosApp.Services
{
    public class MunicipiosDbServices : GetDbServices<RelacionMunicipioProvincia>
    {

        public MunicipiosDbServices(ApplicationDbContext applicationDbContext):base(applicationDbContext)
        {
        }
    }
}
