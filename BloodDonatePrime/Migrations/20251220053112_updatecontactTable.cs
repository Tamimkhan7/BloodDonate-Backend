using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloodBankAPI.Migrations
{
    /// <inheritdoc />
    public partial class updatecontactTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Subject",
                table: "ContactMessages",
                newName: "Phone");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "ContactMessages",
                newName: "Subject");
        }
    }
}
