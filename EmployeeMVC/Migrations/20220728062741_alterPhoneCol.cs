using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeMVC.Migrations
{
    public partial class alterPhoneCol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Employees",
                type: "char(10)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(20,0)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Phone",
                table: "Employees",
                type: "numeric(20,0)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(10)");
        }
    }
}
