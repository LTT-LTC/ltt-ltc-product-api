using System.Threading.Tasks;

namespace LTC.ProductService.Data;

public interface IProductServiceDbSchemaMigrator
{
    Task MigrateAsync();
}
