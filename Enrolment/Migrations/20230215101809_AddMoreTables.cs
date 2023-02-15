using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Enrolment.Migrations
{
    /// <inheritdoc />
    public partial class AddMoreTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EmailRegister",
                table: "EmailRegister");

            migrationBuilder.RenameTable(
                name: "EmailRegister",
                newName: "EmailRegisters");

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
                name: "PayeeId",
                table: "EmailRegisters",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PayerId",
                table: "EmailRegisters",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmailRegisters",
                table: "EmailRegisters",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SuperannuationFund = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdentificationNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaxFileNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FundNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FundAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Suburb = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MemberNo = table.Column<int>(type: "int", nullable: false),
                    AccountName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BusinessNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SuperannuationProductIdentificationNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DaytimePhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HaveAttached = table.Column<int>(type: "int", nullable: false),
                    Signature = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeclarationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinessName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BusinessNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Signature = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeclarationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FundName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SuperannuationProductIdentificationNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FundPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FundWebsite = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateValidChoice = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActDateValidChoice = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Payees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaxFileNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameTitle = table.Column<int>(type: "int", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OtherName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HomeAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Suburb = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PreviousFamilyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DayOfBirth = table.Column<int>(type: "int", nullable: false),
                    MonthOfBirth = table.Column<int>(type: "int", nullable: false),
                    YearOfBirth = table.Column<int>(type: "int", nullable: false),
                    PaidBasis = table.Column<int>(type: "int", nullable: false),
                    ResidencyStatus = table.Column<int>(type: "int", nullable: false),
                    ClaimTaxFree = table.Column<int>(type: "int", nullable: false),
                    HaveLoanProgram = table.Column<int>(type: "int", nullable: false),
                    Signature = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeclarationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Payers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinessNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BranchNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppliedForNumber = table.Column<int>(type: "int", nullable: false),
                    LegalName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BusinessAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Suburb = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactPerson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BusinessPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MakePayment = table.Column<int>(type: "int", nullable: false),
                    Signature = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeclarationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payers", x => x.Id);
                });

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
                name: "IX_EmailRegisters_PayeeId",
                table: "EmailRegisters",
                column: "PayeeId",
                unique: true,
                filter: "[PayeeId] IS NOT NULL");

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
                name: "FK_EmailRegisters_Payees_PayeeId",
                table: "EmailRegisters",
                column: "PayeeId",
                principalTable: "Payees",
                principalColumn: "Id");

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
                name: "FK_EmailRegisters_Employees_EmployeeId",
                table: "EmailRegisters");

            migrationBuilder.DropForeignKey(
                name: "FK_EmailRegisters_Employers_EmployerId",
                table: "EmailRegisters");

            migrationBuilder.DropForeignKey(
                name: "FK_EmailRegisters_Payees_PayeeId",
                table: "EmailRegisters");

            migrationBuilder.DropForeignKey(
                name: "FK_EmailRegisters_Payers_PayerId",
                table: "EmailRegisters");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Employers");

            migrationBuilder.DropTable(
                name: "Payees");

            migrationBuilder.DropTable(
                name: "Payers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmailRegisters",
                table: "EmailRegisters");

            migrationBuilder.DropIndex(
                name: "IX_EmailRegisters_EmployeeId",
                table: "EmailRegisters");

            migrationBuilder.DropIndex(
                name: "IX_EmailRegisters_EmployerId",
                table: "EmailRegisters");

            migrationBuilder.DropIndex(
                name: "IX_EmailRegisters_PayeeId",
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
                name: "PayeeId",
                table: "EmailRegisters");

            migrationBuilder.DropColumn(
                name: "PayerId",
                table: "EmailRegisters");

            migrationBuilder.RenameTable(
                name: "EmailRegisters",
                newName: "EmailRegister");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmailRegister",
                table: "EmailRegister",
                column: "Id");
        }
    }
}
