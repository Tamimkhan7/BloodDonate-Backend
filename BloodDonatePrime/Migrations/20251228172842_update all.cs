using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloodBankAPI.Migrations
{
    /// <inheritdoc />
    public partial class updateall : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "DonationHistories");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "DonationHistories");

            migrationBuilder.RenameColumn(
                name: "DonationDate",
                table: "DonationHistories",
                newName: "Date");

            migrationBuilder.AddColumn<double>(
                name: "Amount",
                table: "DonationHistories",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "DonationHistories");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "DonationHistories",
                newName: "DonationDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "DonationHistories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "DonationHistories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
