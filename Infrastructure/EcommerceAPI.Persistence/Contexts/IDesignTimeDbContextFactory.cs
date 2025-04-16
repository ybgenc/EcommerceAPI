using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace EcommerceAPI.Persistence.Contexts
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using Microsoft.Extensions.Configuration;
    using System.IO;

    namespace EcommerceAPI.Persistence.Contexts
    {
        public class EcommerceAPIDbContextFactory : IDesignTimeDbContextFactory<EcommerceAPIDbContext>
        {
            public EcommerceAPIDbContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<EcommerceAPIDbContext>();
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory()) // Ensure it uses the correct directory path
                    .AddJsonFile("appsettings.json", optional: false)
                    .Build();

                optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
                return new EcommerceAPIDbContext(optionsBuilder.Options);
            }
        }

    }

}
