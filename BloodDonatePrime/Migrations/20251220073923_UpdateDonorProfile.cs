using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloodBankAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDonorProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalDonations",
                table: "Donors");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Donors",
                newName: "PresentPoliceStation");

            migrationBuilder.AlterColumn<int>(
                name: "Age",
                table: "Donors",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "Donors",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Donors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Donors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MedicalInfo",
                table: "Donors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NationalIdNumber",
                table: "Donors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PermanentAddress",
                table: "Donors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PermanentDistrict",
                table: "Donors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PermanentPoliceStation",
                table: "Donors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Donors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "Donors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PresentAddress",
                table: "Donors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PresentDistrict",
                table: "Donors",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Donors");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Donors");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Donors");

            migrationBuilder.DropColumn(
                name: "MedicalInfo",
                table: "Donors");

            migrationBuilder.DropColumn(
                name: "NationalIdNumber",
                table: "Donors");

            migrationBuilder.DropColumn(
                name: "PermanentAddress",
                table: "Donors");

            migrationBuilder.DropColumn(
                name: "PermanentDistrict",
                table: "Donors");

            migrationBuilder.DropColumn(
                name: "PermanentPoliceStation",
                table: "Donors");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Donors");

            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "Donors");

            migrationBuilder.DropColumn(
                name: "PresentAddress",
                table: "Donors");

            migrationBuilder.DropColumn(
                name: "PresentDistrict",
                table: "Donors");

            migrationBuilder.RenameColumn(
                name: "PresentPoliceStation",
                table: "Donors",
                newName: "Address");

            migrationBuilder.AlterColumn<int>(
                name: "Age",
                table: "Donors",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TotalDonations",
                table: "Donors",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
