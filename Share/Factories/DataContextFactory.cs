
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Share.Contexts;

namespace Share.Factories
{
    public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=B:\DataStorageCourse\IceCreamApp\Share\Data\DataBaseIceCreamApp.mdf;Integrated Security=True");

            return new DataContext(optionsBuilder.Options);
        }
    }

    public class DataContextFactoryOrder : IDesignTimeDbContextFactory<DataContextOrders> 
    {
        public DataContextOrders CreateDbContext(string[] args) 
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataContextOrders>();
            optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=B:\DataStorageCourse\IceCreamApp\Share\Data\DataBaseFirstIceCreamApp.mdf;Integrated Security=True");

            return new DataContextOrders(optionsBuilder.Options);
        }
    }
}
