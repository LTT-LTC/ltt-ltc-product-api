using Microsoft.AspNetCore.Http;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;

namespace LTC.ProductService.MultiTenancy;

public class TenantSchemaResolver : ITenantSchemaResolver, ITransientDependency
{
    private readonly ICurrentTenant _currentTenant;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TenantSchemaResolver(
        ICurrentTenant currentTenant,
        IHttpContextAccessor httpContextAccessor)
    {
        _currentTenant = currentTenant;
        _httpContextAccessor = httpContextAccessor;
    }

    public string GetSchemaName()
    {
        if (!string.IsNullOrWhiteSpace(_currentTenant.Name))
            return _currentTenant.Name;

        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext != null)
        {
            var tenantKey = "X-Tenant";
            if (httpContext.Request.Headers.TryGetValue(tenantKey, out var headerValue))
            {
                var tenantName = headerValue.ToString().Trim();
                if (!string.IsNullOrWhiteSpace(tenantName))
                    return tenantName;
            }
        }

        return "dbo";
    }
}

public interface ITenantSchemaResolver
{
    string GetSchemaName();
}