using Microsoft.EntityFrameworkCore.Migrations;

namespace TwitterAPI.Migrations
{
    public partial class AccountNotifications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NoNewNotifications",
                table: "Accounts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NoNewNotifications",
                table: "Accounts");
        }
    }
}
