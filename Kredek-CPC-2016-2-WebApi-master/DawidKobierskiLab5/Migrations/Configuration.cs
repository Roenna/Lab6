using DawidKobierskiLab5.Models;

namespace DawidKobierskiLab5.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DawidKobierskiLab5.Models.EfDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DawidKobierskiLab5.Models.EfDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Students.AddOrUpdate(
                s => s.Id,
                new Student { Id = 1, Name = "Dawid", Surname = "Kobierski", Email = "dawid.kobierski@gmail.com", Index = "218152"},
                new Student { Id = 5, Name = "Karol", Surname = "Piątek", Email = "karol.piatek@gmail.com", Index = "218239"}
                );
        }
    }
}
