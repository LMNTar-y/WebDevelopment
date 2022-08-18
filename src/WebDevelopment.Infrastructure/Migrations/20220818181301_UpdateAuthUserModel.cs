using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebDevelopment.Infrastructure.Migrations
{
    public partial class UpdateAuthUserModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                table: "AuthUserModels",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "UC_User_Email1",
                table: "Users",
                column: "UserEmail",
                unique: true,
                filter: "[UserEmail] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UC_Secured_Password",
                table: "AuthUserModels",
                column: "SecuredPassword",
                unique: true,
                filter: "[SecuredPassword] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UC_User_Email",
                table: "AuthUserModels",
                column: "UserEmail",
                unique: true,
                filter: "[UserEmail] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UC_User_Name",
                table: "AuthUserModels",
                column: "UserName",
                unique: true,
                filter: "[UserName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
