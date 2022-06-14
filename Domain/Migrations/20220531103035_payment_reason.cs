using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class payment_reason : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MyProperty",
                table: "Payments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Reason",
                table: "Payments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MyProperty",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "Reason",
                table: "Payments");
        }
    }
}
