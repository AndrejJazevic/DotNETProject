using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class EditApplicationProps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Reasoning",
                table: "Applicants",
                newName: "Reason");

            migrationBuilder.RenameColumn(
                name: "ListingId",
                table: "Applicants",
                newName: "Item");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Reason",
                table: "Applicants",
                newName: "Reasoning");

            migrationBuilder.RenameColumn(
                name: "Item",
                table: "Applicants",
                newName: "ListingId");
        }
    }
}
