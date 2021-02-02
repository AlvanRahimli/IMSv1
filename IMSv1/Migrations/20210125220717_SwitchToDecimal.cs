using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IMSv1.Migrations
{
    public partial class SwitchToDecimal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Debt",
                table: "UserClients",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalAmount",
                table: "Transactions",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "SalePrice",
                table: "TransactionProducts",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "SalePrice",
                table: "Products",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "Value",
                table: "Prices",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AdditionDate", "Value" },
                values: new object[] { new DateTime(2021, 1, 26, 2, 7, 17, 475, DateTimeKind.Local).AddTicks(2023), 150m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ExpirationDate", "ProductionDate", "SalePrice" },
                values: new object[] { new DateTime(2021, 1, 31, 2, 7, 17, 471, DateTimeKind.Local).AddTicks(619), new DateTime(2021, 1, 21, 2, 7, 17, 474, DateTimeKind.Local).AddTicks(9162), 120m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Debt",
                table: "UserClients",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.AlterColumn<int>(
                name: "TotalAmount",
                table: "Transactions",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.AlterColumn<int>(
                name: "SalePrice",
                table: "TransactionProducts",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.AlterColumn<int>(
                name: "SalePrice",
                table: "Products",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.AlterColumn<int>(
                name: "Value",
                table: "Prices",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.UpdateData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AdditionDate", "Value" },
                values: new object[] { new DateTime(2020, 12, 5, 21, 31, 58, 406, DateTimeKind.Local).AddTicks(163), 150 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ExpirationDate", "ProductionDate", "SalePrice" },
                values: new object[] { new DateTime(2020, 12, 10, 21, 31, 58, 401, DateTimeKind.Local).AddTicks(3764), new DateTime(2020, 11, 30, 21, 31, 58, 405, DateTimeKind.Local).AddTicks(6416), 120 });
        }
    }
}
