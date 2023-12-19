using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIProject.Migrations
{
    /// <inheritdoc />
    public partial class InterestLink : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PhoneNumber",
                table: "Persons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "InterestLinks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterestLinks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InterestInterestLink",
                columns: table => new
                {
                    InterestLinksId = table.Column<int>(type: "int", nullable: false),
                    InterestsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterestInterestLink", x => new { x.InterestLinksId, x.InterestsId });
                    table.ForeignKey(
                        name: "FK_InterestInterestLink_InterestLinks_InterestLinksId",
                        column: x => x.InterestLinksId,
                        principalTable: "InterestLinks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InterestInterestLink_Interests_InterestsId",
                        column: x => x.InterestsId,
                        principalTable: "Interests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InterestLinkPerson",
                columns: table => new
                {
                    InterestLinksId = table.Column<int>(type: "int", nullable: false),
                    PersonsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterestLinkPerson", x => new { x.InterestLinksId, x.PersonsId });
                    table.ForeignKey(
                        name: "FK_InterestLinkPerson_InterestLinks_InterestLinksId",
                        column: x => x.InterestLinksId,
                        principalTable: "InterestLinks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InterestLinkPerson_Persons_PersonsId",
                        column: x => x.PersonsId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InterestInterestLink_InterestsId",
                table: "InterestInterestLink",
                column: "InterestsId");

            migrationBuilder.CreateIndex(
                name: "IX_InterestLinkPerson_PersonsId",
                table: "InterestLinkPerson",
                column: "PersonsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InterestInterestLink");

            migrationBuilder.DropTable(
                name: "InterestLinkPerson");

            migrationBuilder.DropTable(
                name: "InterestLinks");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Persons");
        }
    }
}
