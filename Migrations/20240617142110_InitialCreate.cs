using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BDDNET.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Auteurs",
                columns: table => new
                {
                    id_auteur = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nom_auteur = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    prenom_auteur = table.Column<string>(type: "VARCHAR(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auteurs", x => x.id_auteur);
                });

            migrationBuilder.CreateTable(
                name: "Livres",
                columns: table => new
                {
                    id_livre = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    titre_livre = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    resumer_livre = table.Column<string>(type: "VARCHAR(255)", nullable: false),
                    id_auteur = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Livres", x => x.id_livre);
                    table.ForeignKey(
                        name: "FK_Livres_Auteurs_id_auteur",
                        column: x => x.id_auteur,
                        principalTable: "Auteurs",
                        principalColumn: "id_auteur",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Livres_id_auteur",
                table: "Livres",
                column: "id_auteur");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Livres");

            migrationBuilder.DropTable(
                name: "Auteurs");
        }
    }
}
