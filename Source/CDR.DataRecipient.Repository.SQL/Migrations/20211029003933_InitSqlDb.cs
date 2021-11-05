using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CDR.DataRecipient.Repository.SQL.Migrations
{
    public partial class InitSqlDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CdrArrangement",
                columns: table => new
                {
                    CdrArrangementId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JsonDocument = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CdrArrangement", x => x.CdrArrangementId);
                });

            migrationBuilder.CreateTable(
                name: "DataHolderBrand",
                columns: table => new
                {
                    DataHolderBrandId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JsonDocument = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataHolderBrand", x => x.DataHolderBrandId);
                });

            migrationBuilder.CreateTable(
                name: "Registration",
                columns: table => new
                {
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JsonDocument = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registration", x => x.ClientId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CdrArrangement");

            migrationBuilder.DropTable(
                name: "DataHolderBrand");

            migrationBuilder.DropTable(
                name: "Registration");
        }
    }
}
