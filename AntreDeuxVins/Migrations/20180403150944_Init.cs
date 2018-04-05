using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace AntreDeuxVins.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Aliments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    Nom = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aliments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Couleurs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nom = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Couleurs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pays",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nom = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pays", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nom = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nom = table.Column<string>(nullable: true),
                    PaysId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Regions_Pays_PaysId",
                        column: x => x.PaysId,
                        principalTable: "Pays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Utilisateurs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Mail = table.Column<string>(nullable: true),
                    Nom = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Prenom = table.Column<string>(nullable: true),
                    RoleId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilisateurs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Utilisateurs_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vins",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CouleurId = table.Column<int>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    Millesime = table.Column<DateTime>(nullable: false),
                    Nom = table.Column<string>(nullable: true),
                    PaysId = table.Column<int>(nullable: true),
                    RegionId = table.Column<int>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Volume = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vins_Couleurs_CouleurId",
                        column: x => x.CouleurId,
                        principalTable: "Couleurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vins_Pays_PaysId",
                        column: x => x.PaysId,
                        principalTable: "Pays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vins_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Caves",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    Nom = table.Column<string>(nullable: true),
                    UtilisateurId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Caves", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Caves_Utilisateurs_UtilisateurId",
                        column: x => x.UtilisateurId,
                        principalTable: "Utilisateurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VinAliment",
                columns: table => new
                {
                    VinId = table.Column<int>(nullable: false),
                    AlimentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VinAliment", x => new { x.VinId, x.AlimentId });
                    table.ForeignKey(
                        name: "FK_VinAliment_Aliments_AlimentId",
                        column: x => x.AlimentId,
                        principalTable: "Aliments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VinAliment_Vins_VinId",
                        column: x => x.VinId,
                        principalTable: "Vins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CaveVins",
                columns: table => new
                {
                    CaveId = table.Column<int>(nullable: false),
                    VinId = table.Column<int>(nullable: false),
                    Quantite = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaveVins", x => new { x.CaveId, x.VinId });
                    table.ForeignKey(
                        name: "FK_CaveVins_Caves_CaveId",
                        column: x => x.CaveId,
                        principalTable: "Caves",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CaveVins_Vins_VinId",
                        column: x => x.VinId,
                        principalTable: "Vins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Caves_UtilisateurId",
                table: "Caves",
                column: "UtilisateurId");

            migrationBuilder.CreateIndex(
                name: "IX_CaveVins_VinId",
                table: "CaveVins",
                column: "VinId");

            migrationBuilder.CreateIndex(
                name: "IX_Regions_PaysId",
                table: "Regions",
                column: "PaysId");

            migrationBuilder.CreateIndex(
                name: "IX_Utilisateurs_RoleId",
                table: "Utilisateurs",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_VinAliment_AlimentId",
                table: "VinAliment",
                column: "AlimentId");

            migrationBuilder.CreateIndex(
                name: "IX_Vins_CouleurId",
                table: "Vins",
                column: "CouleurId");

            migrationBuilder.CreateIndex(
                name: "IX_Vins_PaysId",
                table: "Vins",
                column: "PaysId");

            migrationBuilder.CreateIndex(
                name: "IX_Vins_RegionId",
                table: "Vins",
                column: "RegionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CaveVins");

            migrationBuilder.DropTable(
                name: "VinAliment");

            migrationBuilder.DropTable(
                name: "Caves");

            migrationBuilder.DropTable(
                name: "Aliments");

            migrationBuilder.DropTable(
                name: "Vins");

            migrationBuilder.DropTable(
                name: "Utilisateurs");

            migrationBuilder.DropTable(
                name: "Couleurs");

            migrationBuilder.DropTable(
                name: "Regions");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Pays");
        }
    }
}
