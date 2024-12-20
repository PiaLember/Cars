using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarsApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class Pictures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Year",
                table: "Cars",
                type: "int",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "Picture",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Picture",
                table: "Cars");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Year",
                table: "Cars",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
