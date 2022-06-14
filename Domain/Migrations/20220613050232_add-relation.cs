using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class addrelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParrentId",
                table: "Users",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ParrentUserId",
                table: "Users",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ParrentUserId",
                table: "Users",
                column: "ParrentUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Users_ParrentUserId",
                table: "Users",
                column: "ParrentUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Users_ParrentUserId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ParrentUserId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ParrentId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ParrentUserId",
                table: "Users");
        }
    }
}
