using EmpresasApp.Data;
using EmpresasApp.Services;
using JornalerosApp.Shared.Models;

namespace EmpresaApp.Services
{
    public class EmpresaDbServices : DbServices<Empresa>
    {    

        public EmpresaDbServices(EmpresasDataContext empresasDataContext):base(empresasDataContext)
        {
        }

    }
}
