using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RuneSharper.Data.Migrations
{
    public partial class AddPhantomMuspah : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE ActivitySnapshot Set Type = Type + 1 WHERE Type > 44");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
