﻿// <auto-generated />
using System;
using DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataContext.Migrations
{
    [DbContext(typeof(DBContext))]
    [Migration("20240213115540_FirstPush")]
    partial class FirstPush
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Entities.Annonce", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<DateTime>("DateCreation")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("date_creation");

                    b.Property<string>("Description")
                        .HasColumnType("longtext")
                        .HasColumnName("description");

                    b.Property<bool>("EstSuspendu")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("est_suspendu");

                    b.Property<bool>("EstVisible")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("est_visible");

                    b.Property<string>("Image")
                        .HasColumnType("longtext")
                        .HasColumnName("image");

                    b.Property<string>("Title")
                        .HasColumnType("longtext")
                        .HasColumnName("title");

                    b.Property<long>("UtilisateurId")
                        .HasColumnType("bigint")
                        .HasColumnName("utilisateur_id");

                    b.HasKey("Id");

                    b.ToTable("annonce");
                });

            modelBuilder.Entity("Entities.Conversation", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<DateTime>("DateCreation")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("date_creation");

                    b.Property<long>("DestinataireId")
                        .HasColumnType("bigint")
                        .HasColumnName("destinataire_id");

                    b.Property<long>("ExpediteurId")
                        .HasColumnType("bigint")
                        .HasColumnName("expediteur_id");

                    b.HasKey("Id");

                    b.ToTable("conversation");
                });

            modelBuilder.Entity("Entities.Entretien", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<long>("BotanisteId")
                        .HasColumnType("bigint")
                        .HasColumnName("botaniste_id");

                    b.Property<DateTime>("DebutEntretien")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("debut_entretien");

                    b.Property<string>("Description")
                        .HasColumnType("longtext")
                        .HasColumnName("description");

                    b.Property<DateTime>("FinEntretien")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("fin_entretien");

                    b.Property<string>("Image")
                        .HasColumnType("longtext")
                        .HasColumnName("image");

                    b.Property<string>("NomPlante")
                        .HasColumnType("longtext")
                        .HasColumnName("nom_plante");

                    b.Property<long>("UtilisateurId")
                        .HasColumnType("bigint")
                        .HasColumnName("utilisateur_id");

                    b.HasKey("Id");

                    b.ToTable("entretien");
                });

            modelBuilder.Entity("Entities.JwtUsers", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .HasColumnType("longtext")
                        .HasColumnName("name");

                    b.Property<string>("Password")
                        .HasColumnType("longtext")
                        .HasColumnName("password");

                    b.HasKey("Id");

                    b.ToTable("jwt_users");
                });

            modelBuilder.Entity("Entities.Message", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<string>("Contenu")
                        .HasColumnType("longtext")
                        .HasColumnName("contenu");

                    b.Property<long>("ConversationId")
                        .HasColumnType("bigint")
                        .HasColumnName("conversation_id");

                    b.Property<DateTime>("DateEnvoi")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("date_envoi");

                    b.Property<long>("ExpediteurId")
                        .HasColumnType("bigint")
                        .HasColumnName("expedition_id");

                    b.HasKey("Id");

                    b.ToTable("message");
                });

            modelBuilder.Entity("Entities.Suivi", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<DateTime>("DateEntretien")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("date_entretien");

                    b.Property<long>("EntretienId")
                        .HasColumnType("bigint")
                        .HasColumnName("entretien_id");

                    b.Property<string>("Image")
                        .HasColumnType("longtext")
                        .HasColumnName("image");

                    b.Property<string>("Remarque")
                        .HasColumnType("longtext")
                        .HasColumnName("remarque");

                    b.HasKey("Id");

                    b.ToTable("suivi");
                });

            modelBuilder.Entity("Entities.Utilisateur", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<DateTime>("DateCreation")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("date_creation");

                    b.Property<bool>("EstBotaniste")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("est_botaniste");

                    b.Property<bool>("EstModerateur")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("est_moderateur");

                    b.Property<string>("Mail")
                        .HasColumnType("longtext")
                        .HasColumnName("mail");

                    b.Property<string>("Nom")
                        .HasColumnType("longtext")
                        .HasColumnName("nom");

                    b.Property<string>("Password")
                        .HasColumnType("longtext")
                        .HasColumnName("mot_de_passe");

                    b.Property<string>("Prenom")
                        .HasColumnType("longtext")
                        .HasColumnName("prenom");

                    b.Property<string>("Telephone")
                        .HasColumnType("longtext")
                        .HasColumnName("telephone");

                    b.HasKey("Id");

                    b.ToTable("utilisateur");
                });
#pragma warning restore 612, 618
        }
    }
}
