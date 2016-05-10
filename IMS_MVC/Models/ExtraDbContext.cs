using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IMS_MVC.Models
{
    public class ExtraDbContext : DbContext
    {
        public ExtraDbContext(): base("DefaultConnection") { }
        public DbSet<IntType> IntTypes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<IntInfo> IntInfos { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<District> Districts { get; set; }
        
    }
}