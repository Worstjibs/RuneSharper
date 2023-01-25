using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RuneSharper.Data.Migrations;

public partial class AddedNameChangedtoCharacter : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<bool>(
            name: "NameChanged",
            table: "Characters",
            type: "bit",
            nullable: false,
            defaultValue: false);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "NameChanged",
            table: "Characters");
    }
}
