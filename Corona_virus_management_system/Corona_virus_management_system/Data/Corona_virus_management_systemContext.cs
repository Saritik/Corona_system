using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Corona_virus_management_system.Models;

namespace Corona_virus_management_system.Data
{
    public class Corona_virus_management_systemContext : DbContext
    {
        public Corona_virus_management_systemContext (DbContextOptions<Corona_virus_management_systemContext> options)
            : base(options)
        {
        }

        public DbSet<Corona_virus_management_system.Models.Member> Member { get; set; } = default!;

        public DbSet<Corona_virus_management_system.Models.Vaccine>? Vaccine { get; set; }
    }
}
