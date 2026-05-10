using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LTC.ProductService.Migrations
{
    /// <inheritdoc />
    public partial class AlignProductEntitiesToPhysicalTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Align catalog tables with plain POCOs (no ABP audit/extra columns) and restore
            // Products.ProductType for tenant schemas that match LTC_Product.[LTC].
            // Idempotent + schema-per-tenant (same pattern as ProductDropVariantsAndComboItems):
            // some databases never had ConcurrencyStamp/CreationTime/etc.; drops must be conditional.

            foreach (var schema in new[] { "LTC", "dbo" })
            {
                migrationBuilder.Sql($@"
IF OBJECT_ID(N'[{schema}].[Products]', N'U') IS NOT NULL
BEGIN
  IF COL_LENGTH(N'{schema}.Products', N'ConcurrencyStamp') IS NOT NULL ALTER TABLE [{schema}].[Products] DROP COLUMN [ConcurrencyStamp];
  IF COL_LENGTH(N'{schema}.Products', N'CreationTime') IS NOT NULL ALTER TABLE [{schema}].[Products] DROP COLUMN [CreationTime];
  IF COL_LENGTH(N'{schema}.Products', N'CreatorId') IS NOT NULL ALTER TABLE [{schema}].[Products] DROP COLUMN [CreatorId];
  IF COL_LENGTH(N'{schema}.Products', N'ExtraProperties') IS NOT NULL ALTER TABLE [{schema}].[Products] DROP COLUMN [ExtraProperties];
  IF COL_LENGTH(N'{schema}.Products', N'LastModificationTime') IS NOT NULL ALTER TABLE [{schema}].[Products] DROP COLUMN [LastModificationTime];
  IF COL_LENGTH(N'{schema}.Products', N'LastModifierId') IS NOT NULL ALTER TABLE [{schema}].[Products] DROP COLUMN [LastModifierId];
END

IF OBJECT_ID(N'[{schema}].[ProductCategories]', N'U') IS NOT NULL
BEGIN
  IF COL_LENGTH(N'{schema}.ProductCategories', N'ConcurrencyStamp') IS NOT NULL ALTER TABLE [{schema}].[ProductCategories] DROP COLUMN [ConcurrencyStamp];
  IF COL_LENGTH(N'{schema}.ProductCategories', N'CreationTime') IS NOT NULL ALTER TABLE [{schema}].[ProductCategories] DROP COLUMN [CreationTime];
  IF COL_LENGTH(N'{schema}.ProductCategories', N'CreatorId') IS NOT NULL ALTER TABLE [{schema}].[ProductCategories] DROP COLUMN [CreatorId];
  IF COL_LENGTH(N'{schema}.ProductCategories', N'ExtraProperties') IS NOT NULL ALTER TABLE [{schema}].[ProductCategories] DROP COLUMN [ExtraProperties];
  IF COL_LENGTH(N'{schema}.ProductCategories', N'LastModificationTime') IS NOT NULL ALTER TABLE [{schema}].[ProductCategories] DROP COLUMN [LastModificationTime];
  IF COL_LENGTH(N'{schema}.ProductCategories', N'LastModifierId') IS NOT NULL ALTER TABLE [{schema}].[ProductCategories] DROP COLUMN [LastModifierId];
END

IF OBJECT_ID(N'[{schema}].[Combos]', N'U') IS NOT NULL
BEGIN
  IF COL_LENGTH(N'{schema}.Combos', N'ConcurrencyStamp') IS NOT NULL ALTER TABLE [{schema}].[Combos] DROP COLUMN [ConcurrencyStamp];
  IF COL_LENGTH(N'{schema}.Combos', N'CreationTime') IS NOT NULL ALTER TABLE [{schema}].[Combos] DROP COLUMN [CreationTime];
  IF COL_LENGTH(N'{schema}.Combos', N'CreatorId') IS NOT NULL ALTER TABLE [{schema}].[Combos] DROP COLUMN [CreatorId];
  IF COL_LENGTH(N'{schema}.Combos', N'ExtraProperties') IS NOT NULL ALTER TABLE [{schema}].[Combos] DROP COLUMN [ExtraProperties];
  IF COL_LENGTH(N'{schema}.Combos', N'LastModificationTime') IS NOT NULL ALTER TABLE [{schema}].[Combos] DROP COLUMN [LastModificationTime];
  IF COL_LENGTH(N'{schema}.Combos', N'LastModifierId') IS NOT NULL ALTER TABLE [{schema}].[Combos] DROP COLUMN [LastModifierId];
END

IF OBJECT_ID(N'[{schema}].[Products]', N'U') IS NOT NULL
BEGIN
  UPDATE [{schema}].[Products] SET [Description] = N'' WHERE [Description] IS NULL;
  UPDATE [{schema}].[Products] SET [ImageUrl] = N'' WHERE [ImageUrl] IS NULL;
  ALTER TABLE [{schema}].[Products] ALTER COLUMN [Description] nvarchar(max) NOT NULL;
  ALTER TABLE [{schema}].[Products] ALTER COLUMN [ImageUrl] nvarchar(max) NOT NULL;
END

IF OBJECT_ID(N'[{schema}].[Products]', N'U') IS NOT NULL
   AND COL_LENGTH(N'{schema}.Products', N'ProductType') IS NULL
    ALTER TABLE [{schema}].[Products] ADD [ProductType] nvarchar(max) NULL;

IF OBJECT_ID(N'[{schema}].[ProductCategories]', N'U') IS NOT NULL
BEGIN
  UPDATE [{schema}].[ProductCategories] SET [Description] = N'' WHERE [Description] IS NULL;
  ALTER TABLE [{schema}].[ProductCategories] ALTER COLUMN [Description] nvarchar(max) NOT NULL;
END

IF OBJECT_ID(N'[{schema}].[Combos]', N'U') IS NOT NULL
BEGIN
  UPDATE [{schema}].[Combos] SET [Description] = N'' WHERE [Description] IS NULL;
  UPDATE [{schema}].[Combos] SET [ImageUrl] = N'' WHERE [ImageUrl] IS NULL;
  UPDATE [{schema}].[Combos] SET [ProductIds] = N'' WHERE [ProductIds] IS NULL;
  ALTER TABLE [{schema}].[Combos] ALTER COLUMN [Description] nvarchar(max) NOT NULL;
  ALTER TABLE [{schema}].[Combos] ALTER COLUMN [ImageUrl] nvarchar(max) NOT NULL;
  ALTER TABLE [{schema}].[Combos] ALTER COLUMN [ProductIds] nvarchar(max) NOT NULL;
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
   AND COL_LENGTH(N'{schema}.Products', N'ProductType') IS NOT NULL
BEGIN
  DECLARE @dcPt sysname;
  SELECT @dcPt = dc.name
  FROM sys.tables t
  INNER JOIN sys.schemas s ON t.schema_id = s.schema_id
  INNER JOIN sys.columns c ON c.object_id = t.object_id AND c.name = N'ProductType'
  INNER JOIN sys.default_constraints dc ON dc.parent_object_id = c.object_id AND dc.parent_column_id = c.column_id
  WHERE s.name = N'{schema}' AND t.name = N'Products';

  IF @dcPt IS NOT NULL
  BEGIN
    DECLARE @dropPt nvarchar(max) = N'ALTER TABLE [{schema}].[Products] DROP CONSTRAINT ' + QUOTENAME(@dcPt);
    EXEC sp_executesql @dropPt;
  END

  ALTER TABLE [{schema}].[Products] DROP COLUMN [ProductType];
END

IF OBJECT_ID(N'[{schema}].[Products]', N'U') IS NOT NULL
BEGIN
  ALTER TABLE [{schema}].[Products] ALTER COLUMN [ImageUrl] nvarchar(max) NULL;
  ALTER TABLE [{schema}].[Products] ALTER COLUMN [Description] nvarchar(max) NULL;
END

IF OBJECT_ID(N'[{schema}].[ProductCategories]', N'U') IS NOT NULL
  ALTER TABLE [{schema}].[ProductCategories] ALTER COLUMN [Description] nvarchar(max) NULL;

IF OBJECT_ID(N'[{schema}].[Combos]', N'U') IS NOT NULL
BEGIN
  ALTER TABLE [{schema}].[Combos] ALTER COLUMN [ProductIds] nvarchar(max) NULL;
  ALTER TABLE [{schema}].[Combos] ALTER COLUMN [ImageUrl] nvarchar(max) NULL;
  ALTER TABLE [{schema}].[Combos] ALTER COLUMN [Description] nvarchar(max) NULL;
END

IF OBJECT_ID(N'[{schema}].[Products]', N'U') IS NOT NULL
BEGIN
  IF COL_LENGTH(N'{schema}.Products', N'ConcurrencyStamp') IS NULL ALTER TABLE [{schema}].[Products] ADD [ConcurrencyStamp] nvarchar(40) NOT NULL CONSTRAINT [DF_{schema}_Products_ConcurrencyStamp] DEFAULT (N'');
  IF COL_LENGTH(N'{schema}.Products', N'CreationTime') IS NULL ALTER TABLE [{schema}].[Products] ADD [CreationTime] datetime2 NOT NULL CONSTRAINT [DF_{schema}_Products_CreationTime] DEFAULT (CAST(N'0001-01-01' AS datetime2));
  IF COL_LENGTH(N'{schema}.Products', N'CreatorId') IS NULL ALTER TABLE [{schema}].[Products] ADD [CreatorId] uniqueidentifier NULL;
  IF COL_LENGTH(N'{schema}.Products', N'ExtraProperties') IS NULL ALTER TABLE [{schema}].[Products] ADD [ExtraProperties] nvarchar(max) NOT NULL CONSTRAINT [DF_{schema}_Products_ExtraProperties] DEFAULT (N'');
  IF COL_LENGTH(N'{schema}.Products', N'LastModificationTime') IS NULL ALTER TABLE [{schema}].[Products] ADD [LastModificationTime] datetime2 NULL;
  IF COL_LENGTH(N'{schema}.Products', N'LastModifierId') IS NULL ALTER TABLE [{schema}].[Products] ADD [LastModifierId] uniqueidentifier NULL;
END

IF OBJECT_ID(N'[{schema}].[ProductCategories]', N'U') IS NOT NULL
BEGIN
  IF COL_LENGTH(N'{schema}.ProductCategories', N'ConcurrencyStamp') IS NULL ALTER TABLE [{schema}].[ProductCategories] ADD [ConcurrencyStamp] nvarchar(40) NOT NULL CONSTRAINT [DF_{schema}_ProductCategories_ConcurrencyStamp] DEFAULT (N'');
  IF COL_LENGTH(N'{schema}.ProductCategories', N'CreationTime') IS NULL ALTER TABLE [{schema}].[ProductCategories] ADD [CreationTime] datetime2 NOT NULL CONSTRAINT [DF_{schema}_ProductCategories_CreationTime] DEFAULT (CAST(N'0001-01-01' AS datetime2));
  IF COL_LENGTH(N'{schema}.ProductCategories', N'CreatorId') IS NULL ALTER TABLE [{schema}].[ProductCategories] ADD [CreatorId] uniqueidentifier NULL;
  IF COL_LENGTH(N'{schema}.ProductCategories', N'ExtraProperties') IS NULL ALTER TABLE [{schema}].[ProductCategories] ADD [ExtraProperties] nvarchar(max) NOT NULL CONSTRAINT [DF_{schema}_ProductCategories_ExtraProperties] DEFAULT (N'');
  IF COL_LENGTH(N'{schema}.ProductCategories', N'LastModificationTime') IS NULL ALTER TABLE [{schema}].[ProductCategories] ADD [LastModificationTime] datetime2 NULL;
  IF COL_LENGTH(N'{schema}.ProductCategories', N'LastModifierId') IS NULL ALTER TABLE [{schema}].[ProductCategories] ADD [LastModifierId] uniqueidentifier NULL;
END

IF OBJECT_ID(N'[{schema}].[Combos]', N'U') IS NOT NULL
BEGIN
  IF COL_LENGTH(N'{schema}.Combos', N'ConcurrencyStamp') IS NULL ALTER TABLE [{schema}].[Combos] ADD [ConcurrencyStamp] nvarchar(40) NOT NULL CONSTRAINT [DF_{schema}_Combos_ConcurrencyStamp] DEFAULT (N'');
  IF COL_LENGTH(N'{schema}.Combos', N'CreationTime') IS NULL ALTER TABLE [{schema}].[Combos] ADD [CreationTime] datetime2 NOT NULL CONSTRAINT [DF_{schema}_Combos_CreationTime] DEFAULT (CAST(N'0001-01-01' AS datetime2));
  IF COL_LENGTH(N'{schema}.Combos', N'CreatorId') IS NULL ALTER TABLE [{schema}].[Combos] ADD [CreatorId] uniqueidentifier NULL;
  IF COL_LENGTH(N'{schema}.Combos', N'ExtraProperties') IS NULL ALTER TABLE [{schema}].[Combos] ADD [ExtraProperties] nvarchar(max) NOT NULL CONSTRAINT [DF_{schema}_Combos_ExtraProperties] DEFAULT (N'');
  IF COL_LENGTH(N'{schema}.Combos', N'LastModificationTime') IS NULL ALTER TABLE [{schema}].[Combos] ADD [LastModificationTime] datetime2 NULL;
  IF COL_LENGTH(N'{schema}.Combos', N'LastModifierId') IS NULL ALTER TABLE [{schema}].[Combos] ADD [LastModifierId] uniqueidentifier NULL;
END
");
            }
        }
    }
}
