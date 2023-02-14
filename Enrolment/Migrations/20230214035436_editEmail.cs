using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Enrolment.Migrations
{
    /// <inheritdoc />
    public partial class editEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EmailRegisters",
                table: "EmailRegisters");

            migrationBuilder.RenameTable(
                name: "EmailRegisters",
                newName: "EmailRegister");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmailRegister",
                table: "EmailRegister",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EmailRegister",
                table: "EmailRegister");

            migrationBuilder.RenameTable(
                name: "EmailRegister",
                newName: "EmailRegisters");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmailRegisters",
                table: "EmailRegisters",
                column: "Id");
        }
    }
}
