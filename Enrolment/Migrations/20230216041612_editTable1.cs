using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Enrolment.Migrations
{
    /// <inheritdoc />
    public partial class editTable1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailRegisters_Employees_EmployeeId",
                table: "EmailRegisters");

            migrationBuilder.DropForeignKey(
                name: "FK_EmailRegisters_Employers_EmployerId",
                table: "EmailRegisters");

            migrationBuilder.DropForeignKey(
                name: "FK_EmailRegisters_Payers_PayerId",
                table: "EmailRegisters");

            migrationBuilder.DropIndex(
                name: "IX_EmailRegisters_EmployeeId",
                table: "EmailRegisters");

            migrationBuilder.DropIndex(
                name: "IX_EmailRegisters_EmployerId",
                table: "EmailRegisters");

            migrationBuilder.DropIndex(
                name: "IX_EmailRegisters_PayerId",
                table: "EmailRegisters");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "EmailRegisters");

            migrationBuilder.DropColumn(
                name: "EmployerId",
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

            migrationBuilder.AlterColumn<int>(
                name: "TaxFileNumber",
                table: "Payees",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Signature",
                table: "Payees",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PreviousFamilyName",
                table: "Payees",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "OtherName",
                table: "Payees",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "EmailRegisterId",
                table: "Employers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EmailRegisterId",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Payers_EmailRegisterId",
                table: "Payers",
                column: "EmailRegisterId");

            migrationBuilder.CreateIndex(
                name: "IX_Employers_EmailRegisterId",
                table: "Employers",
                column: "EmailRegisterId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_EmailRegisterId",
                table: "Employees",
                column: "EmailRegisterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_EmailRegisters_EmailRegisterId",
                table: "Employees",
                column: "EmailRegisterId",
                principalTable: "EmailRegisters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employers_EmailRegisters_EmailRegisterId",
                table: "Employers",
                column: "EmailRegisterId",
                principalTable: "EmailRegisters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payers_EmailRegisters_EmailRegisterId",
                table: "Payers",
                column: "EmailRegisterId",
                principalTable: "EmailRegisters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_EmailRegisters_EmailRegisterId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employers_EmailRegisters_EmailRegisterId",
                table: "Employers");

            migrationBuilder.DropForeignKey(
                name: "FK_Payers_EmailRegisters_EmailRegisterId",
                table: "Payers");

            migrationBuilder.DropIndex(
                name: "IX_Payers_EmailRegisterId",
                table: "Payers");

            migrationBuilder.DropIndex(
                name: "IX_Employers_EmailRegisterId",
                table: "Employers");

            migrationBuilder.DropIndex(
                name: "IX_Employees_EmailRegisterId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "EmailRegisterId",
                table: "Payers");

            migrationBuilder.DropColumn(
                name: "EmailRegisterId",
                table: "Employers");

            migrationBuilder.DropColumn(
                name: "EmailRegisterId",
                table: "Employees");

            migrationBuilder.AlterColumn<string>(
                name: "TaxFileNumber",
                table: "Payees",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Signature",
                table: "Payees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PreviousFamilyName",
                table: "Payees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OtherName",
                table: "Payees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "EmailRegisters",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmployerId",
                table: "EmailRegisters",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PayerId",
                table: "EmailRegisters",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmailRegisters_EmployeeId",
                table: "EmailRegisters",
                column: "EmployeeId",
                unique: true,
                filter: "[EmployeeId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_EmailRegisters_EmployerId",
                table: "EmailRegisters",
                column: "EmployerId",
                unique: true,
                filter: "[EmployerId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_EmailRegisters_PayerId",
                table: "EmailRegisters",
                column: "PayerId",
                unique: true,
                filter: "[PayerId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailRegisters_Employees_EmployeeId",
                table: "EmailRegisters",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailRegisters_Employers_EmployerId",
                table: "EmailRegisters",
                column: "EmployerId",
                principalTable: "Employers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailRegisters_Payers_PayerId",
                table: "EmailRegisters",
                column: "PayerId",
                principalTable: "Payers",
                principalColumn: "Id");
        }
    }
}
