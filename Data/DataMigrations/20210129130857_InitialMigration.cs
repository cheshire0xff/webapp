using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Data.DataMigrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DatabaseFile",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(nullable: false),
                    Content = table.Column<byte[]>(nullable: false),
                    Hash = table.Column<byte[]>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatabaseFile", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JobApplication",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    jobOfferId = table.Column<int>(nullable: false),
                    userId = table.Column<string>(nullable: false),
                    fileId = table.Column<int>(nullable: false),
                    status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobApplication", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "JobOffer",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    employerId = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: false),
                    fileId = table.Column<int>(nullable: false),
                    localization = table.Column<string>(nullable: false),
                    tags = table.Column<string>(nullable: true),
                    expirationDate = table.Column<DateTime>(nullable: false),
                    addedDate = table.Column<DateTime>(nullable: false),
                    employementType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobOffer", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DatabaseFile");

            migrationBuilder.DropTable(
                name: "JobApplication");

            migrationBuilder.DropTable(
                name: "JobOffer");
        }
    }
}
