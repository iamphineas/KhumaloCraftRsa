using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KhumaloCraftRsa.Data.Migrations
{
    /// <inheritdoc />
    public partial class approvalmessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Approved",
                table: "Orders",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Approved",
                table: "Orders");
        }
    }
}
