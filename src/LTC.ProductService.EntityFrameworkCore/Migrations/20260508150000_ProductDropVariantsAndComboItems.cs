using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LTC.ProductService.Migrations
{
    /// <inheritdoc />
    public partial class ProductDropVariantsAndComboItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // ComboItems and ProductVariants are no longer used; the Combo now stores
            // its bundled products as a JSON array in Combos.ProductIds and exposes
            // its image via Combos.ImageUrl. Products.ProductType has been retired in
            // favour of ProductCategoryId being the sole categorisation hook.
            //
            // Idempotent + schema-per-tenant: tables may live under LTC or dbo; some DBs
            // never had ComboItems / ProductVariants in a given schema.

            foreach (var schema in new[] { "LTC", "dbo" })
            {
                migrationBuilder.Sql($@"
IF OBJECT_ID(N'[{schema}].[ComboItems]', N'U') IS NOT NULL
    DROP TABLE [{schema}].[ComboItems];
IF OBJECT_ID(N'[{schema}].[ProductVariants]', N'U') IS NOT NULL
    DROP TABLE [{schema}].[ProductVariants];
");

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

                migrationBuilder.Sql($@"
IF OBJECT_ID(N'[{schema}].[Combos]', N'U') IS NOT NULL
   AND COL_LENGTH(N'{schema}.Combos', N'ImageUrl') IS NULL
    ALTER TABLE [{schema}].[Combos] ADD [ImageUrl] nvarchar(max) NULL;
IF OBJECT_ID(N'[{schema}].[Combos]', N'U') IS NOT NULL
   AND COL_LENGTH(N'{schema}.Combos', N'ProductIds') IS NULL
    ALTER TABLE [{schema}].[Combos] ADD [ProductIds] nvarchar(max) NULL;
");
            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductIds",
                schema: "dbo",
                table: "Combos");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                schema: "dbo",
                table: "Combos");

            migrationBuilder.AddColumn<string>(
                name: "ProductType",
                schema: "dbo",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ProductVariants",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<System.Guid>(type: "uniqueidentifier", nullable: false),
                    AdditionalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ProductId = table.Column<System.Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<System.Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductVariants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductVariants_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "dbo",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: Microsoft.EntityFrameworkCore.Migrations.ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariants_ProductId",
                schema: "dbo",
                table: "ProductVariants",
                column: "ProductId");

            migrationBuilder.CreateTable(
                name: "ComboItems",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<System.Guid>(type: "uniqueidentifier", nullable: false),
                    ComboId = table.Column<System.Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<System.Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    TenantId = table.Column<System.Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComboItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComboItems_Combos_ComboId",
                        column: x => x.ComboId,
                        principalSchema: "dbo",
                        principalTable: "Combos",
                        principalColumn: "Id",
                        onDelete: Microsoft.EntityFrameworkCore.Migrations.ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComboItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "dbo",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: Microsoft.EntityFrameworkCore.Migrations.ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComboItems_ComboId",
                schema: "dbo",
                table: "ComboItems",
                column: "ComboId");

            migrationBuilder.CreateIndex(
                name: "IX_ComboItems_ProductId",
                schema: "dbo",
                table: "ComboItems",
                column: "ProductId");
        }
    }
}
