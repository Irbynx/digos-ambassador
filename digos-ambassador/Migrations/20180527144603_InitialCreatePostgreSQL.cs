﻿// <auto-generated />
#pragma warning disable CS1591
// ReSharper disable RedundantArgumentDefaultValue
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable RedundantUsingDirective
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DIGOS.Ambassador.Migrations
{
    public partial class InitialCreatePostgreSQL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Appearance",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    GenderScale = table.Column<double>(nullable: false),
                    Height = table.Column<double>(nullable: false),
                    Muscularity = table.Column<double>(nullable: false),
                    Weight = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appearance", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Colour",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Modifier = table.Column<int>(nullable: true),
                    Shade = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colour", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Dossiers",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Path = table.Column<string>(nullable: true),
                    Summary = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dossiers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "GlobalPermissions",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Permission = table.Column<int>(nullable: false),
                    Target = table.Column<int>(nullable: false),
                    UserDiscordID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlobalPermissions", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Kinks",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Category = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    FListID = table.Column<uint>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kinks", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "LocalPermissions",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Permission = table.Column<int>(nullable: false),
                    ServerDiscordID = table.Column<long>(nullable: false),
                    Target = table.Column<int>(nullable: false),
                    UserDiscordID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalPermissions", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Servers",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    DiscordID = table.Column<long>(nullable: false),
                    IsNSFW = table.Column<bool>(nullable: false),
                    SuppressPermissonWarnings = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Species",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Description = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    ParentID = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Species", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Species_Species_ParentID",
                        column: x => x.ParentID,
                        principalTable: "Species",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Transformations",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    DefaultBaseColourID = table.Column<long>(nullable: false),
                    DefaultPattern = table.Column<int>(nullable: true),
                    DefaultPatternColourID = table.Column<long>(nullable: true),
                    Description = table.Column<string>(nullable: false),
                    GrowMessage = table.Column<string>(nullable: false),
                    IsNSFW = table.Column<bool>(nullable: false),
                    Part = table.Column<int>(nullable: false),
                    ShiftMessage = table.Column<string>(nullable: false),
                    SingleDescription = table.Column<string>(nullable: false),
                    SpeciesID = table.Column<long>(nullable: false),
                    UniformDescription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transformations", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Transformations_Colour_DefaultBaseColourID",
                        column: x => x.DefaultBaseColourID,
                        principalTable: "Colour",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transformations_Colour_DefaultPatternColourID",
                        column: x => x.DefaultPatternColourID,
                        principalTable: "Colour",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transformations_Species_SpeciesID",
                        column: x => x.SpeciesID,
                        principalTable: "Species",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppearanceComponent",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AppearanceID = table.Column<long>(nullable: true),
                    BaseColourID = table.Column<long>(nullable: true),
                    Chirality = table.Column<int>(nullable: false),
                    Pattern = table.Column<int>(nullable: true),
                    PatternColourID = table.Column<long>(nullable: true),
                    Size = table.Column<int>(nullable: false),
                    TransformationID = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppearanceComponent", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AppearanceComponent_Appearance_AppearanceID",
                        column: x => x.AppearanceID,
                        principalTable: "Appearance",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppearanceComponent_Colour_BaseColourID",
                        column: x => x.BaseColourID,
                        principalTable: "Colour",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppearanceComponent_Colour_PatternColourID",
                        column: x => x.PatternColourID,
                        principalTable: "Colour",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppearanceComponent_Transformations_TransformationID",
                        column: x => x.TransformationID,
                        principalTable: "Transformations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AvatarUrl = table.Column<string>(nullable: true),
                    CurrentAppearanceID = table.Column<long>(nullable: true),
                    DefaultAppearanceID = table.Column<long>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    IsCurrent = table.Column<bool>(nullable: false),
                    IsNSFW = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Nickname = table.Column<string>(nullable: true),
                    OwnerID = table.Column<long>(nullable: true),
                    PronounProviderFamily = table.Column<string>(nullable: true),
                    ServerID = table.Column<long>(nullable: false),
                    Summary = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Characters_Appearance_CurrentAppearanceID",
                        column: x => x.CurrentAppearanceID,
                        principalTable: "Appearance",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Characters_Appearance_DefaultAppearanceID",
                        column: x => x.DefaultAppearanceID,
                        principalTable: "Appearance",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Caption = table.Column<string>(nullable: true),
                    CharacterID = table.Column<long>(nullable: true),
                    IsNSFW = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Images_Characters_CharacterID",
                        column: x => x.CharacterID,
                        principalTable: "Characters",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Bio = table.Column<string>(nullable: true),
                    Class = table.Column<int>(nullable: false),
                    DefaultCharacterID = table.Column<long>(nullable: false),
                    DiscordID = table.Column<long>(nullable: false),
                    GlobalUserProtectionID = table.Column<long>(nullable: true),
                    GlobalUserProtectionID1 = table.Column<long>(nullable: true),
                    RoleplayID = table.Column<long>(nullable: true),
                    RoleplayID1 = table.Column<long>(nullable: true),
                    RoleplayID2 = table.Column<long>(nullable: true),
                    ServerID = table.Column<long>(nullable: true),
                    Timezone = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Users_Characters_DefaultCharacterID",
                        column: x => x.DefaultCharacterID,
                        principalTable: "Characters",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Servers_ServerID",
                        column: x => x.ServerID,
                        principalTable: "Servers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GlobalUserProtections",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    DefaultOptIn = table.Column<bool>(nullable: false),
                    DefaultType = table.Column<int>(nullable: false),
                    UserID = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlobalUserProtections", x => x.ID);
                    table.ForeignKey(
                        name: "FK_GlobalUserProtections_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Roleplays",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ActiveChannelID = table.Column<long>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsNSFW = table.Column<bool>(nullable: false),
                    IsPublic = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    OwnerID = table.Column<long>(nullable: true),
                    ServerID = table.Column<long>(nullable: false),
                    Summary = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roleplays", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Roleplays_Users_OwnerID",
                        column: x => x.OwnerID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ServerUserProtections",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    HasOptedIn = table.Column<bool>(nullable: false),
                    ServerID = table.Column<long>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    UserID = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerUserProtections", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ServerUserProtections_Servers_ServerID",
                        column: x => x.ServerID,
                        principalTable: "Servers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServerUserProtections_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserKink",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    KinkID = table.Column<long>(nullable: true),
                    Preference = table.Column<int>(nullable: false),
                    UserID = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserKink", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserKink_Kinks_KinkID",
                        column: x => x.KinkID,
                        principalTable: "Kinks",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserKink_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserMessage",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AuthorDiscordID = table.Column<long>(nullable: false),
                    AuthorNickname = table.Column<string>(nullable: true),
                    Contents = table.Column<string>(nullable: true),
                    DiscordMessageID = table.Column<long>(nullable: false),
                    RoleplayID = table.Column<long>(nullable: true),
                    Timestamp = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMessage", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserMessage_Roleplays_RoleplayID",
                        column: x => x.RoleplayID,
                        principalTable: "Roleplays",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppearanceComponent_AppearanceID",
                table: "AppearanceComponent",
                column: "AppearanceID");

            migrationBuilder.CreateIndex(
                name: "IX_AppearanceComponent_BaseColourID",
                table: "AppearanceComponent",
                column: "BaseColourID");

            migrationBuilder.CreateIndex(
                name: "IX_AppearanceComponent_PatternColourID",
                table: "AppearanceComponent",
                column: "PatternColourID");

            migrationBuilder.CreateIndex(
                name: "IX_AppearanceComponent_TransformationID",
                table: "AppearanceComponent",
                column: "TransformationID");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_CurrentAppearanceID",
                table: "Characters",
                column: "CurrentAppearanceID");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_DefaultAppearanceID",
                table: "Characters",
                column: "DefaultAppearanceID");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_OwnerID",
                table: "Characters",
                column: "OwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_GlobalUserProtections_UserID",
                table: "GlobalUserProtections",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Images_CharacterID",
                table: "Images",
                column: "CharacterID");

            migrationBuilder.CreateIndex(
                name: "IX_Roleplays_OwnerID",
                table: "Roleplays",
                column: "OwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_ServerUserProtections_ServerID",
                table: "ServerUserProtections",
                column: "ServerID");

            migrationBuilder.CreateIndex(
                name: "IX_ServerUserProtections_UserID",
                table: "ServerUserProtections",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Species_ParentID",
                table: "Species",
                column: "ParentID");

            migrationBuilder.CreateIndex(
                name: "IX_Transformations_DefaultBaseColourID",
                table: "Transformations",
                column: "DefaultBaseColourID");

            migrationBuilder.CreateIndex(
                name: "IX_Transformations_DefaultPatternColourID",
                table: "Transformations",
                column: "DefaultPatternColourID");

            migrationBuilder.CreateIndex(
                name: "IX_Transformations_SpeciesID",
                table: "Transformations",
                column: "SpeciesID");

            migrationBuilder.CreateIndex(
                name: "IX_UserKink_KinkID",
                table: "UserKink",
                column: "KinkID");

            migrationBuilder.CreateIndex(
                name: "IX_UserKink_UserID",
                table: "UserKink",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_UserMessage_RoleplayID",
                table: "UserMessage",
                column: "RoleplayID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_DefaultCharacterID",
                table: "Users",
                column: "DefaultCharacterID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_GlobalUserProtectionID",
                table: "Users",
                column: "GlobalUserProtectionID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_GlobalUserProtectionID1",
                table: "Users",
                column: "GlobalUserProtectionID1");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleplayID",
                table: "Users",
                column: "RoleplayID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleplayID1",
                table: "Users",
                column: "RoleplayID1");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleplayID2",
                table: "Users",
                column: "RoleplayID2");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ServerID",
                table: "Users",
                column: "ServerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Users_OwnerID",
                table: "Characters",
                column: "OwnerID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roleplays_RoleplayID",
                table: "Users",
                column: "RoleplayID",
                principalTable: "Roleplays",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roleplays_RoleplayID1",
                table: "Users",
                column: "RoleplayID1",
                principalTable: "Roleplays",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roleplays_RoleplayID2",
                table: "Users",
                column: "RoleplayID2",
                principalTable: "Roleplays",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_GlobalUserProtections_GlobalUserProtectionID",
                table: "Users",
                column: "GlobalUserProtectionID",
                principalTable: "GlobalUserProtections",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_GlobalUserProtections_GlobalUserProtectionID1",
                table: "Users",
                column: "GlobalUserProtectionID1",
                principalTable: "GlobalUserProtections",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Appearance_CurrentAppearanceID",
                table: "Characters");

            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Appearance_DefaultAppearanceID",
                table: "Characters");

            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Users_OwnerID",
                table: "Characters");

            migrationBuilder.DropForeignKey(
                name: "FK_GlobalUserProtections_Users_UserID",
                table: "GlobalUserProtections");

            migrationBuilder.DropForeignKey(
                name: "FK_Roleplays_Users_OwnerID",
                table: "Roleplays");

            migrationBuilder.DropTable(
                name: "AppearanceComponent");

            migrationBuilder.DropTable(
                name: "Dossiers");

            migrationBuilder.DropTable(
                name: "GlobalPermissions");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "LocalPermissions");

            migrationBuilder.DropTable(
                name: "ServerUserProtections");

            migrationBuilder.DropTable(
                name: "UserKink");

            migrationBuilder.DropTable(
                name: "UserMessage");

            migrationBuilder.DropTable(
                name: "Transformations");

            migrationBuilder.DropTable(
                name: "Kinks");

            migrationBuilder.DropTable(
                name: "Colour");

            migrationBuilder.DropTable(
                name: "Species");

            migrationBuilder.DropTable(
                name: "Appearance");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Characters");

            migrationBuilder.DropTable(
                name: "GlobalUserProtections");

            migrationBuilder.DropTable(
                name: "Roleplays");

            migrationBuilder.DropTable(
                name: "Servers");
        }
    }
}
