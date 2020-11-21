using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JornalerosApp.Infrastructure.Data
{
    public class SQLDatabaseSettings : ISQLDatabaseSettings
    {

        public string DataSource { get; set; }
        public string InitialCatalog { get; set; }

        public string UserId { get; set; }

        public string Password { get; set; }

        public bool PersistsecurityInfo { get; set; }


    }
}
