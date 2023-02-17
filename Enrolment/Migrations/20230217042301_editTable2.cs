using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Enrolment.Migrations
{
    /// <inheritdoc />
    public partial class editTable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payers_EmailRegisters_EmailRegisterId",
                table: "Payers");

            migrationBuilder.DropIndex(
                name: "IX_Payers_EmailRegisterId",
                table: "Payers");

            migrationBuilder.DropColumn(
                name: "EmailRegisterId",
                table: "Payers");

            migrationBuilder.AddColumn<int>(
                name: "PayerId",
                table: "EmailRegisters",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmailRegisters_PayerId",
                table: "EmailRegisters",
                column: "PayerId",
                unique: true,
                filter: "[PayerId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailRegisters_Payers_PayerId",
                table: "EmailRegisters",
                column: "PayerId",
                principalTable: "Payers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailRegisters_Payers_PayerId",
                table: "EmailRegisters");

            migrationBuilder.DropIndex(
                name: "IX_EmailRegisters_PayerId",
                table: "EmailRegisters");

            migrationBuilder.DropColumn(
                name: "PayerId",
                table: "EmailRegisters");

            migrationBuilder.AddColumn<int>(
                name: "EmailRegisterId",
                table: "Payers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Payers_EmailRegisterId",
                table: "Payers",
                column: "EmailRegisterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payers_EmailRegisters_EmailRegisterId",
                table: "Payers",
                column: "EmailRegisterId",
                principalTable: "EmailRegisters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
