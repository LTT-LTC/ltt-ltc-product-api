using LTC.ProductService;
using Microsoft.AspNetCore.Http;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;

namespace LTC.ProductService.MultiTenancy;

/// <summary>
/// Same resolution order as <c>LTC.CustomerService.MultiTenancy.TenantSchemaResolver</c>:
/// <c>X-Tenant</c> header/cookie, then ABP tenant name, then default schema.
/// <para />
/// Customer falls back to <c>dbo</c>; <c>LTC_Product</c> catalog data lives under <see cref="ProductServiceConsts.DefaultCatalogSchema"/>
/// (there is no usable catalog in <c>dbo</c> for this service).
/// </summary>
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
        // 1. Prefer explicit client-provided tenant key from header/cookie (same as Customer service).
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

            if (httpContext.Request.Cookies.TryGetValue(tenantKey, out var cookieValue))
            {
                var tenantName = cookieValue?.Trim();
                if (!string.IsNullOrWhiteSpace(tenantName))
                    return tenantName;
            }
        }

        // 2. Fall back to ABP-resolved tenant name (from tenant store) — same as Customer service.
        if (!string.IsNullOrWhiteSpace(_currentTenant.Name))
            return _currentTenant.Name;

        // 3. Default schema — Customer uses "dbo"; catalog tables for this API are under [LTC].
        return ProductServiceConsts.DefaultCatalogSchema;
    }
}

public interface ITenantSchemaResolver
{
    string GetSchemaName();
}
