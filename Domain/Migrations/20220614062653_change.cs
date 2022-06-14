using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class change : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reasons_Users_ChildUserId",
                table: "Reasons");

            migrationBuilder.DropForeignKey(
                name: "FK_Reasons_Users_ParrentUserId",
                table: "Reasons");

            migrationBuilder.DropIndex(
                name: "IX_Reasons_ChildUserId",
                table: "Reasons");

            migrationBuilder.DropIndex(
                name: "IX_Reasons_ParrentUserId",
                table: "Reasons");

            migrationBuilder.DropColumn(
                name: "ChildUserId",
                table: "Reasons");

            migrationBuilder.DropColumn(
                name: "ParrentUserId",
                table: "Reasons");

            migrationBuilder.CreateIndex(
                name: "IX_Reasons_ChildId",
                table: "Reasons",
                column: "ChildId");

            migrationBuilder.CreateIndex(
                name: "IX_Reasons_ParrentId",
                table: "Reasons",
                column: "ParrentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reasons_Users_ChildId",
                table: "Reasons",
                column: "ChildId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reasons_Users_ParrentId",
                table: "Reasons",
                column: "ParrentId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reasons_Users_ChildId",
                table: "Reasons");

            migrationBuilder.DropForeignKey(
                name: "FK_Reasons_Users_ParrentId",
                table: "Reasons");

            migrationBuilder.DropIndex(
                name: "IX_Reasons_ChildId",
                table: "Reasons");

            migrationBuilder.DropIndex(
                name: "IX_Reasons_ParrentId",
                table: "Reasons");

            migrationBuilder.AddColumn<int>(
                name: "ChildUserId",
                table: "Reasons",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParrentUserId",
                table: "Reasons",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reasons_ChildUserId",
                table: "Reasons",
                column: "ChildUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reasons_ParrentUserId",
                table: "Reasons",
                column: "ParrentUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reasons_Users_ChildUserId",
                table: "Reasons",
                column: "ChildUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reasons_Users_ParrentUserId",
                table: "Reasons",
                column: "ParrentUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
