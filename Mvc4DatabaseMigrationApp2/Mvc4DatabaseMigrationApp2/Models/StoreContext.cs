using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Mvc4DatabaseMigrationApp1.Models
{
    public class StoreContext: DbContext
    {
        public DbSet<Product> Production { get; set; }
    }
}