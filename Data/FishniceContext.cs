using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Fishnice.Models;

namespace Fishnice.Data
{
    public class FishniceContext : DbContext
    {
        public FishniceContext (DbContextOptions<FishniceContext> options)
            : base(options)
        {
        }

        public DbSet<Fish> Fish { get; set; } = default!;
    }
}
