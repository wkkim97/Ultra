using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Database
{
    public class DaoBase : IDisposable
    {
        protected UltraDbContext _context = null;
        public void Dispose()
        {
            if (_context != null)
                _context.Dispose();
        }
    }
}
