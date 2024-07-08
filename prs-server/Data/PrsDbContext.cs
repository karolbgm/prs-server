using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using prs_server.Models;

namespace prs_server.Data
{
    public class PrsDbContext : DbContext
    {
        public PrsDbContext (DbContextOptions<PrsDbContext> options)
            : base(options)
        {
        }

        public DbSet<prs_server.Models.User> Users { get; set; } = default!;
        public DbSet<prs_server.Models.Vendor> Vendors { get; set; } = default!;
        public DbSet<prs_server.Models.Product> Products { get; set; } = default!;
        public DbSet<prs_server.Models.Request> Requests { get; set; } = default!;
        public DbSet<prs_server.Models.RequestLine> RequestLines { get; set; } = default!;
    }
}
