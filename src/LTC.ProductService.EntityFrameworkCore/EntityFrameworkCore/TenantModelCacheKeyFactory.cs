using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace LTC.ProductService.EntityFrameworkCore;

/// <summary>
/// Same pattern as <c>LTC.CustomerService.EntityFrameworkCore.TenantModelCacheKeyFactory</c>:
/// the cached EF model must include the resolved SQL schema (<see cref="ProductServiceDbContext.GetCurrentSchema"/>),
/// otherwise queries target the wrong schema (e.g. design-time <c>dbo</c> vs runtime <c>LTC</c>).
/// </summary>
public class TenantModelCacheKeyFactory : IModelCacheKeyFactory
{
    public object Create(DbContext context, bool designTime)
    {
        if (context is ProductServiceDbContext tenantContext)
        {
            return (context.GetType(), tenantContext.GetCurrentSchema(), designTime);
        }

        return (context.GetType(), designTime);
    }
}
