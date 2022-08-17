using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebDevelopment.Infrastructure.Migrations
{
    public partial class AddAuthUserModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "AuthUserModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Role = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthUserModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuthUserModels_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });


            migrationBuilder.CreateIndex(
                name: "IX_AuthUserModels_UserId",
                table: "AuthUserModels",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
