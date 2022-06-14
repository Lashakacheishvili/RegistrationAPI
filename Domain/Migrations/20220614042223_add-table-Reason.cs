using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class addtableReason : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Reason",
                table: "Payments");

            migrationBuilder.AddColumn<int>(
                name: "ChildUserId",
                table: "Payments",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReasonId",
                table: "Payments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "ReasonId1",
                table: "Payments",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Reasons",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ParrentId = table.Column<int>(nullable: true),
                    ParrentUserId = table.Column<int>(nullable: true),
                    ChildId = table.Column<int>(nullable: true),
                    ChildUserId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(type: "varchar(350)", nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(8, 2)", nullable: false),
                    DeleteDescription = table.Column<string>(nullable: true),
                    Required = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reasons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reasons_Users_ChildUserId",
                        column: x => x.ChildUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reasons_Users_ParrentUserId",
                        column: x => x.ParrentUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payments_ChildUserId",
                table: "Payments",
                column: "ChildUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_ReasonId1",
                table: "Payments",
                column: "ReasonId1");

            migrationBuilder.CreateIndex(
                name: "IX_Reasons_ChildUserId",
                table: "Reasons",
                column: "ChildUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reasons_ParrentUserId",
                table: "Reasons",
                column: "ParrentUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Users_ChildUserId",
                table: "Payments",
                column: "ChildUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Reasons_ReasonId1",
                table: "Payments",
                column: "ReasonId1",
                principalTable: "Reasons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Users_ChildUserId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Reasons_ReasonId1",
                table: "Payments");

            migrationBuilder.DropTable(
                name: "Reasons");

            migrationBuilder.DropIndex(
                name: "IX_Payments_ChildUserId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_ReasonId1",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "ChildUserId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "ReasonId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "ReasonId1",
                table: "Payments");

            migrationBuilder.AddColumn<string>(
                name: "Reason",
                table: "Payments",
                type: "text",
                nullable: true);
        }
    }
}
