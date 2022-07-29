using Microsoft.EntityFrameworkCore.Migrations;

namespace TwitterAPI.Migrations
{
    public partial class ReplyTweetId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReplyTweetId",
                table: "Replies",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReplyTweetId",
                table: "Replies");
        }
    }
}
