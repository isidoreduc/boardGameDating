using BoardGameDating.api.Models;
using Microsoft.EntityFrameworkCore;

namespace BoardGameDating.api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<Value> Values { get; set; }
    }
}