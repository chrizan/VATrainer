using Microsoft.EntityFrameworkCore.Migrations;

namespace VATrainer.Migrations
{
    public partial class AddIsNextToQuestion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsNext",
                table: "Question",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsNext",
                table: "Question");
        }
    }
}
