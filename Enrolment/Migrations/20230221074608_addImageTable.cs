using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Enrolment.Migrations
{
    /// <inheritdoc />
    public partial class addImageTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdentityProofImageId",
                table: "EmailRegisters",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "IdentityProofImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityProofImages", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmailRegisters_IdentityProofImageId",
                table: "EmailRegisters",
                column: "IdentityProofImageId",
                unique: true,
                filter: "[IdentityProofImageId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailRegisters_IdentityProofImages_IdentityProofImageId",
                table: "EmailRegisters",
                column: "IdentityProofImageId",
                principalTable: "IdentityProofImages",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailRegisters_IdentityProofImages_IdentityProofImageId",
                table: "EmailRegisters");

            migrationBuilder.DropTable(
                name: "IdentityProofImages");

            migrationBuilder.DropIndex(
                name: "IX_EmailRegisters_IdentityProofImageId",
                table: "EmailRegisters");

            migrationBuilder.DropColumn(
                name: "IdentityProofImageId",
                table: "EmailRegisters");
        }
    }
}
