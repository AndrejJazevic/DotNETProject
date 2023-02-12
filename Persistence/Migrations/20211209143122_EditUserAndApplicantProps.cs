using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class EditUserAndApplicantProps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applicants_Users_Username",
                table: "Applicants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Applicants",
                table: "Applicants");

            migrationBuilder.DropIndex(
                name: "IX_Applicants_Username",
                table: "Applicants");

            migrationBuilder.DropColumn(
                name: "User",
                table: "Applicants");

            migrationBuilder.RenameTable(
                name: "Applicants",
                newName: "Applicant");

            migrationBuilder.RenameColumn(
                name: "isVerified",
                table: "Users",
                newName: "IsVerified");

            migrationBuilder.RenameColumn(
                name: "KarmaPoints",
                table: "Users",
                newName: "Points");

            migrationBuilder.RenameColumn(
                name: "Item",
                table: "Applicant",
                newName: "ItemId");

            migrationBuilder.AddColumn<string>(
                name: "AuthId",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Applicant",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Applicant",
                table: "Applicant",
                columns: new[] { "Username", "ItemId" });

            migrationBuilder.CreateIndex(
                name: "IX_Applicant_ItemId",
                table: "Applicant",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applicant_Items_ItemId",
                table: "Applicant",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Applicant_Users_Username",
                table: "Applicant",
                column: "Username",
                principalTable: "Users",
                principalColumn: "Username",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applicant_Items_ItemId",
                table: "Applicant");

            migrationBuilder.DropForeignKey(
                name: "FK_Applicant_Users_Username",
                table: "Applicant");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Applicant",
                table: "Applicant");

            migrationBuilder.DropIndex(
                name: "IX_Applicant_ItemId",
                table: "Applicant");

            migrationBuilder.DropColumn(
                name: "AuthId",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Applicant",
                newName: "Applicants");

            migrationBuilder.RenameColumn(
                name: "IsVerified",
                table: "Users",
                newName: "isVerified");

            migrationBuilder.RenameColumn(
                name: "Points",
                table: "Users",
                newName: "KarmaPoints");

            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "Applicants",
                newName: "Item");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Applicants",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "User",
                table: "Applicants",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Applicants",
                table: "Applicants",
                columns: new[] { "User", "Item" });

            migrationBuilder.CreateIndex(
                name: "IX_Applicants_Username",
                table: "Applicants",
                column: "Username");

            migrationBuilder.AddForeignKey(
                name: "FK_Applicants_Users_Username",
                table: "Applicants",
                column: "Username",
                principalTable: "Users",
                principalColumn: "Username",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
