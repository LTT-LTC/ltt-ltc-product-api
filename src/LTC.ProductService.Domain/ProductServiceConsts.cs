namespace LTC.ProductService;

public static class ProductServiceConsts
{
    public const string DbTablePrefix = "App";

    public const string DbSchema = null;

    /// <summary>
    /// SQL Server schema where <c>LTC_Product</c> catalog tables live (<c>[LTC].[Products]</c>, …).
    /// Runtime default — not <c>dbo</c>. The word <c>dbo</c> appears in EF migration snapshots / design-time factories only.
    /// </summary>
    public const string DefaultCatalogSchema = "LTC";
}
