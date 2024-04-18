using BlazorServerSample.RestApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlazorServerSample.RestApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<BlogDataModel> Blogs { get; set; }
    }
}
