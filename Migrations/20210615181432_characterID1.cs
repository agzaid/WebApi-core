using Microsoft.EntityFrameworkCore.Migrations;

namespace project.Migrations
{
    public partial class characterID1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_characterSkills_characters_CharacterId",
                table: "characterSkills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_characterSkills",
                table: "characterSkills");

            migrationBuilder.DropIndex(
                name: "IX_characterSkills_CharacterId",
                table: "characterSkills");

            migrationBuilder.DropColumn(
                name: "CaharacterId",
                table: "characterSkills");

            migrationBuilder.AlterColumn<int>(
                name: "CharacterId",
                table: "characterSkills",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_characterSkills",
                table: "characterSkills",
                columns: new[] { "CharacterId", "SkillId" });

            migrationBuilder.AddForeignKey(
                name: "FK_characterSkills_characters_CharacterId",
                table: "characterSkills",
                column: "CharacterId",
                principalTable: "characters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_characterSkills_characters_CharacterId",
                table: "characterSkills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_characterSkills",
                table: "characterSkills");

            migrationBuilder.AlterColumn<int>(
                name: "CharacterId",
                table: "characterSkills",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CaharacterId",
                table: "characterSkills",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_characterSkills",
                table: "characterSkills",
                columns: new[] { "CaharacterId", "SkillId" });

            migrationBuilder.CreateIndex(
                name: "IX_characterSkills_CharacterId",
                table: "characterSkills",
                column: "CharacterId");

            migrationBuilder.AddForeignKey(
                name: "FK_characterSkills_characters_CharacterId",
                table: "characterSkills",
                column: "CharacterId",
                principalTable: "characters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
