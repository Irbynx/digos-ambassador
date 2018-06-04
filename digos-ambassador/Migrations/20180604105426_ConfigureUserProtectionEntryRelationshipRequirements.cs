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
    public partial class ConfigureUserProtectionEntryRelationshipRequirements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProtectionEntry_GlobalUserProtections_GlobalProtectionID",
                table: "UserProtectionEntry");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProtectionEntry_Users_UserID",
                table: "UserProtectionEntry");

            migrationBuilder.AlterColumn<long>(
                name: "UserID",
                table: "UserProtectionEntry",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "GlobalProtectionID",
                table: "UserProtectionEntry",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProtectionEntry_GlobalUserProtections_GlobalProtectionID",
                table: "UserProtectionEntry",
                column: "GlobalProtectionID",
                principalTable: "GlobalUserProtections",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProtectionEntry_Users_UserID",
                table: "UserProtectionEntry",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProtectionEntry_GlobalUserProtections_GlobalProtectionID",
                table: "UserProtectionEntry");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProtectionEntry_Users_UserID",
                table: "UserProtectionEntry");

            migrationBuilder.AlterColumn<long>(
                name: "UserID",
                table: "UserProtectionEntry",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<long>(
                name: "GlobalProtectionID",
                table: "UserProtectionEntry",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_UserProtectionEntry_GlobalUserProtections_GlobalProtectionID",
                table: "UserProtectionEntry",
                column: "GlobalProtectionID",
                principalTable: "GlobalUserProtections",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProtectionEntry_Users_UserID",
                table: "UserProtectionEntry",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
