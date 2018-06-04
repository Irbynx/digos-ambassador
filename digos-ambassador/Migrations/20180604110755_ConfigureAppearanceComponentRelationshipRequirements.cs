﻿// <auto-generated />
#pragma warning disable CS1591
// ReSharper disable RedundantArgumentDefaultValue
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable RedundantUsingDirective
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DIGOS.Ambassador.Migrations
{
    public partial class ConfigureAppearanceComponentRelationshipRequirements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppearanceComponent_Appearance_AppearanceID",
                table: "AppearanceComponent");

            migrationBuilder.AlterColumn<long>(
                name: "AppearanceID",
                table: "AppearanceComponent",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AppearanceComponent_Appearance_AppearanceID",
                table: "AppearanceComponent",
                column: "AppearanceID",
                principalTable: "Appearance",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppearanceComponent_Appearance_AppearanceID",
                table: "AppearanceComponent");

            migrationBuilder.AlterColumn<long>(
                name: "AppearanceID",
                table: "AppearanceComponent",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_AppearanceComponent_Appearance_AppearanceID",
                table: "AppearanceComponent",
                column: "AppearanceID",
                principalTable: "Appearance",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
