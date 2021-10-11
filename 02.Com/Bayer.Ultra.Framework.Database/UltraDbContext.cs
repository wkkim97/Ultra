using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Database
{
    public class UltraDbContext : DbContext
    {
        public UltraDbContext() 
            : this(Config.WebSiteConfigHandler.ConnectionStrings[Config.WebSiteConfigHandler.DefaultDbConnection.Name].ConnectionString)
        {

        }

        public UltraDbContext(string connectionString) : base(connectionString) { }
    }
}
