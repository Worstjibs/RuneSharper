using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RuneSharper.Data.Migrations;

public partial class DropDateCreatedonSnapshotEntity : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "DateCreated",
            table: "SkillSnapshot");

        migrationBuilder.DropColumn(
            name: "DateCreated",
            table: "ActivitySnapshot");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<DateTime>(
            name: "DateCreated",
            table: "SkillSnapshot",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AddColumn<DateTime>(
            name: "DateCreated",
            table: "ActivitySnapshot",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
    }
}
