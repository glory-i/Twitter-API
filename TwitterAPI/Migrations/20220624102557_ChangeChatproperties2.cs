using Microsoft.EntityFrameworkCore.Migrations;

namespace TwitterAPI.Migrations
{
    public partial class ChangeChatproperties2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LastMessageId1",
                table: "Chats",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Chats_LastMessageId1",
                table: "Chats",
                column: "LastMessageId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_Messages_LastMessageId1",
                table: "Chats",
                column: "LastMessageId1",
                principalTable: "Messages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_Messages_LastMessageId1",
                table: "Chats");

            migrationBuilder.DropIndex(
                name: "IX_Chats_LastMessageId1",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "LastMessageId1",
                table: "Chats");
        }
    }
}
