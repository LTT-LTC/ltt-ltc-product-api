using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LTC.ProductService.Migrations;

/// <summary>
/// Drops soft-delete columns when present. Uses conditional DDL so databases that never had these columns (or already dropped them) still apply cleanly.
/// </summary>
public partial class RemoveSoftDeleteFromProductComboCategory : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql(
            """
            IF COL_LENGTH('dbo.Combos', 'IsDeleted') IS NOT NULL ALTER TABLE [dbo].[Combos] DROP COLUMN [IsDeleted];
            IF COL_LENGTH('dbo.Combos', 'DeleterId') IS NOT NULL ALTER TABLE [dbo].[Combos] DROP COLUMN [DeleterId];
            IF COL_LENGTH('dbo.Combos', 'DeletionTime') IS NOT NULL ALTER TABLE [dbo].[Combos] DROP COLUMN [DeletionTime];

            IF COL_LENGTH('dbo.Products', 'IsDeleted') IS NOT NULL ALTER TABLE [dbo].[Products] DROP COLUMN [IsDeleted];
            IF COL_LENGTH('dbo.Products', 'DeleterId') IS NOT NULL ALTER TABLE [dbo].[Products] DROP COLUMN [DeleterId];
            IF COL_LENGTH('dbo.Products', 'DeletionTime') IS NOT NULL ALTER TABLE [dbo].[Products] DROP COLUMN [DeletionTime];

            IF COL_LENGTH('dbo.ProductCategories', 'IsDeleted') IS NOT NULL ALTER TABLE [dbo].[ProductCategories] DROP COLUMN [IsDeleted];
            IF COL_LENGTH('dbo.ProductCategories', 'DeleterId') IS NOT NULL ALTER TABLE [dbo].[ProductCategories] DROP COLUMN [DeleterId];
            IF COL_LENGTH('dbo.ProductCategories', 'DeletionTime') IS NOT NULL ALTER TABLE [dbo].[ProductCategories] DROP COLUMN [DeletionTime];
            """);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<bool>(
            name: "IsDeleted",
            schema: "dbo",
            table: "Combos",
            type: "bit",
            nullable: false,
            defaultValue: false);

        migrationBuilder.AddColumn<Guid>(
            name: "DeleterId",
            schema: "dbo",
            table: "Combos",
            type: "uniqueidentifier",
            nullable: true);

        migrationBuilder.AddColumn<DateTime>(
            name: "DeletionTime",
            schema: "dbo",
            table: "Combos",
            type: "datetime2",
            nullable: true);

        migrationBuilder.AddColumn<bool>(
            name: "IsDeleted",
            schema: "dbo",
            table: "Products",
            type: "bit",
            nullable: false,
            defaultValue: false);

        migrationBuilder.AddColumn<Guid>(
            name: "DeleterId",
            schema: "dbo",
            table: "Products",
            type: "uniqueidentifier",
            nullable: true);

        migrationBuilder.AddColumn<DateTime>(
            name: "DeletionTime",
            schema: "dbo",
            table: "Products",
            type: "datetime2",
            nullable: true);

        migrationBuilder.AddColumn<bool>(
            name: "IsDeleted",
            schema: "dbo",
            table: "ProductCategories",
            type: "bit",
            nullable: false,
            defaultValue: false);

        migrationBuilder.AddColumn<Guid>(
            name: "DeleterId",
            schema: "dbo",
            table: "ProductCategories",
            type: "uniqueidentifier",
            nullable: true);

        migrationBuilder.AddColumn<DateTime>(
            name: "DeletionTime",
            schema: "dbo",
            table: "ProductCategories",
            type: "datetime2",
            nullable: true);
    }
}
