using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LTC.ProductService.Migrations
{
    /// <inheritdoc />
    public partial class RemoveProductTypeFromProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
                        foreach (var schema in new[] { "LTC", "dbo" })
                        {
                                migrationBuilder.Sql($@"
IF OBJECT_ID(N'[{schema}].[Products]', N'U') IS NOT NULL
     AND COL_LENGTH(N'{schema}.Products', N'ProductType') IS NOT NULL
BEGIN
    DECLARE @dc sysname;
    SELECT @dc = dc.name
    FROM sys.tables t
    INNER JOIN sys.schemas s ON t.schema_id = s.schema_id
    INNER JOIN sys.columns c ON c.object_id = t.object_id AND c.name = N'ProductType'
    INNER JOIN sys.default_constraints dc ON dc.parent_object_id = c.object_id AND dc.parent_column_id = c.column_id
    WHERE s.name = N'{schema}' AND t.name = N'Products';

    IF @dc IS NOT NULL
    BEGIN
        DECLARE @drop nvarchar(max) = N'ALTER TABLE [{schema}].[Products] DROP CONSTRAINT ' + QUOTENAME(@dc);
        EXEC sp_executesql @drop;
    END

    ALTER TABLE [{schema}].[Products] DROP COLUMN [ProductType];
END
");
                        }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            foreach (var schema in new[] { "LTC", "dbo" })
            {
                migrationBuilder.Sql($@"
IF OBJECT_ID(N'[{schema}].[Products]', N'U') IS NOT NULL
   AND COL_LENGTH(N'{schema}.Products', N'ProductType') IS NULL
    ALTER TABLE [{schema}].[Products] ADD [ProductType] nvarchar(max) NULL;
");
            }
        }
    }
}
