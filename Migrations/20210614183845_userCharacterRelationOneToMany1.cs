using Microsoft.EntityFrameworkCore.Migrations;

namespace project.Migrations
{
    public partial class userCharacterRelationOneToMany1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_characters_Users_UserId",
                table: "characters");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "characters",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_characters_Users_UserId",
                table: "characters",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_characters_Users_UserId",
                table: "characters");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "characters",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_characters_Users_UserId",
                table: "characters",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
