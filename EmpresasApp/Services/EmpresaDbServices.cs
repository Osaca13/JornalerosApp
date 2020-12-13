using EmpresasApp.Data;
using EmpresasApp.Services;
using EmpresasApp.Models;
using JornalerosApp.Shared.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EmpresaApp.Services
{
    public class EmpresaDbServices : DbServices<Empresa>
    {    

        public EmpresaDbServices(EmpresasDataContext empresasDataContext):base(empresasDataContext)
        {
        }

    }
}
