using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Data.DataMigrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "accepted",
                table: "JobApplication",
                nullable: false,
                defaultValue: (short)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "accepted",
                table: "JobApplication");
        }
    }
}
