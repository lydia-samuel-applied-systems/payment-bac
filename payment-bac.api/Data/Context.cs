using Microsoft.EntityFrameworkCore;
using payment_bac.api.Models;

namespace payment_bac.api.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        public DbSet<Session> Sessions { get; set; }
        public DbSet<Policy> Policies { get; set; }
    }
}
