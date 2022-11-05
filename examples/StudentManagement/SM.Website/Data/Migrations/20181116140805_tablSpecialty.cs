using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SM.Website.Data.Migrations
{
    public partial class tablSpecialty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Students",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "SpecialtyId",
                table: "Students",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Specialtys",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialtys", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_SpecialtyId",
                table: "Students",
                column: "SpecialtyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Specialtys_SpecialtyId",
                table: "Students",
                column: "SpecialtyId",
                principalTable: "Specialtys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Specialtys_SpecialtyId",
                table: "Students");

            migrationBuilder.DropTable(
                name: "Specialtys");

            migrationBuilder.DropIndex(
                name: "IX_Students_SpecialtyId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "SpecialtyId",
                table: "Students");
        }
    }
}
