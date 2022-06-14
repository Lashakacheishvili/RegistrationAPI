using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Domain.Migrations
{
    public partial class addtableReasonfields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Reasons_ReasonId1",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_ReasonId1",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "MyProperty",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "ReasonId1",
                table: "Payments");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Reasons",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteDate",
                table: "Reasons",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsSuccessed",
                table: "Reasons",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_ReasonId",
                table: "Payments",
                column: "ReasonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Reasons_ReasonId",
                table: "Payments",
                column: "ReasonId",
                principalTable: "Reasons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Reasons_ReasonId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_ReasonId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "DeleteDate",
                table: "Reasons");

            migrationBuilder.DropColumn(
                name: "IsSuccessed",
                table: "Reasons");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Reasons",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<string>(
                name: "MyProperty",
                table: "Payments",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ReasonId1",
                table: "Payments",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_ReasonId1",
                table: "Payments",
                column: "ReasonId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Reasons_ReasonId1",
                table: "Payments",
                column: "ReasonId1",
                principalTable: "Reasons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
