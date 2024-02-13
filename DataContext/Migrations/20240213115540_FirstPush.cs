using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace DataContext.Migrations
{
    /// <inheritdoc />
    public partial class FirstPush : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "annonce",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    image = table.Column<string>(type: "longtext", nullable: true),
                    title = table.Column<string>(type: "longtext", nullable: true),
                    description = table.Column<string>(type: "longtext", nullable: true),
                    date_creation = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    est_visible = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    est_suspendu = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    utilisateur_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_annonce", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "conversation",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    date_creation = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    expediteur_id = table.Column<long>(type: "bigint", nullable: false),
                    destinataire_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_conversation", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "entretien",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    nom_plante = table.Column<string>(type: "longtext", nullable: true),
                    image = table.Column<string>(type: "longtext", nullable: true),
                    description = table.Column<string>(type: "longtext", nullable: true),
                    debut_entretien = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    fin_entretien = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    utilisateur_id = table.Column<long>(type: "bigint", nullable: false),
                    botaniste_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_entretien", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "jwt_users",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "longtext", nullable: true),
                    password = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_jwt_users", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "message",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    contenu = table.Column<string>(type: "longtext", nullable: true),
                    date_envoi = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    conversation_id = table.Column<long>(type: "bigint", nullable: false),
                    expedition_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_message", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "suivi",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    image = table.Column<string>(type: "longtext", nullable: true),
                    remarque = table.Column<string>(type: "longtext", nullable: true),
                    date_entretien = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    entretien_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_suivi", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "utilisateur",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    prenom = table.Column<string>(type: "longtext", nullable: true),
                    nom = table.Column<string>(type: "longtext", nullable: true),
                    mail = table.Column<string>(type: "longtext", nullable: true),
                    mot_de_passe = table.Column<string>(type: "longtext", nullable: true),
                    telephone = table.Column<string>(type: "longtext", nullable: true),
                    date_creation = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    est_botaniste = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    est_moderateur = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_utilisateur", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "annonce");

            migrationBuilder.DropTable(
                name: "conversation");

            migrationBuilder.DropTable(
                name: "entretien");

            migrationBuilder.DropTable(
                name: "jwt_users");

            migrationBuilder.DropTable(
                name: "message");

            migrationBuilder.DropTable(
                name: "suivi");

            migrationBuilder.DropTable(
                name: "utilisateur");
        }
    }
}
