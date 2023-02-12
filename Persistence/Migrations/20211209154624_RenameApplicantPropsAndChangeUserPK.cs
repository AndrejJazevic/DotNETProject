using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class RenameApplicantPropsAndChangeUserPK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applicant_Users_Username",
                table: "Applicant");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Users_Username",
                table: "Items");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Items",
                newName: "UserAuthId");

            migrationBuilder.RenameIndex(
                name: "IX_Items_Username",
                table: "Items",
                newName: "IX_Items_UserAuthId");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Applicant",
                newName: "UserId");

            migrationBuilder.AlterColumn<string>(
                name: "AuthId",
                table: "Users",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Users",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "AuthId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true,
                filter: "[Username] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Applicant_Users_UserId",
                table: "Applicant",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "AuthId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Users_UserAuthId",
                table: "Items",
                column: "UserAuthId",
                principalTable: "Users",
                principalColumn: "AuthId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applicant_Users_UserId",
                table: "Applicant");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Users_UserAuthId",
                table: "Items");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_Username",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "UserAuthId",
                table: "Items",
                newName: "Username");

            migrationBuilder.RenameIndex(
                name: "IX_Items_UserAuthId",
                table: "Items",
                newName: "IX_Items_Username");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Applicant",
                newName: "Username");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Users",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AuthId",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Username");

            migrationBuilder.AddForeignKey(
                name: "FK_Applicant_Users_Username",
                table: "Applicant",
                column: "Username",
                principalTable: "Users",
                principalColumn: "Username",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Users_Username",
                table: "Items",
                column: "Username",
                principalTable: "Users",
                principalColumn: "Username",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
