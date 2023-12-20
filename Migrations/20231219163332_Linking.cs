using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIProject.Migrations
{
    /// <inheritdoc />
    public partial class Linking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InterestLinks_Interests_InterestId",
                table: "InterestLinks");

            migrationBuilder.DropForeignKey(
                name: "FK_InterestLinks_Persons_PersonId",
                table: "InterestLinks");

            migrationBuilder.AlterColumn<int>(
                name: "PersonId",
                table: "InterestLinks",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "InterestId",
                table: "InterestLinks",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_InterestLinks_Interests_InterestId",
                table: "InterestLinks",
                column: "InterestId",
                principalTable: "Interests",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InterestLinks_Persons_PersonId",
                table: "InterestLinks",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InterestLinks_Interests_InterestId",
                table: "InterestLinks");

            migrationBuilder.DropForeignKey(
                name: "FK_InterestLinks_Persons_PersonId",
                table: "InterestLinks");

            migrationBuilder.AlterColumn<int>(
                name: "PersonId",
                table: "InterestLinks",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "InterestId",
                table: "InterestLinks",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_InterestLinks_Interests_InterestId",
                table: "InterestLinks",
                column: "InterestId",
                principalTable: "Interests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InterestLinks_Persons_PersonId",
                table: "InterestLinks",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
