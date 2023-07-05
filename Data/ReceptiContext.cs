using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Recepti.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Recepti.Areas.Identity.Data;


namespace Recepti.Data
{
    public class ReceptiContext : IdentityDbContext<ReceptiUser>
    {
        public ReceptiContext (DbContextOptions<ReceptiContext> options)
            : base(options)
        {
        }

        public DbSet<Recepti.Models.KorisnikRecepti> KorisnikRecepti { get; set; } = default!;

        public DbSet<Recepti.Models.Recept>? Recept { get; set; }

        public DbSet<Recepti.Models.Recenzija>? Recenzija { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
