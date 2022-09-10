using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APSS.Infrastructure.Repositories.EntityFramework.Migrations
{
    public partial class FixUserAccountRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Users_AddedById",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Users_UserId",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_AddedById",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "AddedById",
                table: "Accounts");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Users_UserId",
                table: "Accounts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Users_UserId",
                table: "Accounts");

            migrationBuilder.AddColumn<long>(
                name: "AddedById",
                table: "Accounts",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_AddedById",
                table: "Accounts",
                column: "AddedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Users_AddedById",
                table: "Accounts",
                column: "AddedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Users_UserId",
                table: "Accounts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
