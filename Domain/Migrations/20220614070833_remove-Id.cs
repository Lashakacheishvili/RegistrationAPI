using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class removeId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Users_ParrentUserId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ParrentUserId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ParrentUserId",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "ParrentId",
                table: "Users",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ParrentId",
                table: "Users",
                column: "ParrentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Users_ParrentId",
                table: "Users",
                column: "ParrentId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Users_ParrentId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ParrentId",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "ParrentId",
                table: "Users",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParrentUserId",
                table: "Users",
                type: "integer",
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
    }
}
