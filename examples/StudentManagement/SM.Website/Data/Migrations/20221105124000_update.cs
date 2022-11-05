using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SM.Website.Data.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Specialtys_SpecialtyId",
                table: "Students");

            migrationBuilder.AlterColumn<int>(
                name: "SpecialtyId",
                table: "Students",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Specialtys_SpecialtyId",
                table: "Students",
                column: "SpecialtyId",
                principalTable: "Specialtys",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Specialtys_SpecialtyId",
                table: "Students");

            migrationBuilder.AlterColumn<int>(
                name: "SpecialtyId",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Specialtys_SpecialtyId",
                table: "Students",
                column: "SpecialtyId",
                principalTable: "Specialtys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
