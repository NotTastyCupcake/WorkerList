using Microsoft.EntityFrameworkCore;
using NotTastyCupcake.WorkerList.ApplicationCore.Entities;
using NotTastyCupcake.WorkerList.ApplicationCore.Entities.Organization;
using NotTastyCupcake.WorkerList.ApplicationCore.Entities.Person;
using NotTastyCupcake.WorkerList.ApplicationCore.Entities.Position;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NotTastyCupcake.WorkerList.Infrastructure.Data
{
    public class PersonContext : DbContext
    {
        public DbSet<PersonItem> People { get; set; }
        public DbSet<PositionItem> Positions { get; set; }
        public DbSet<OrganizationItem> Organizations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
