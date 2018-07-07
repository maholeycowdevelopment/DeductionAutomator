using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DeductionAutomator.Data.Migrations
{
  public partial class AddDeductionEntries : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropIndex(
          name: "IX_AspNetUserRoles_UserId",
          table: "AspNetUserRoles");

      migrationBuilder.DropIndex(
          name: "RoleNameIndex",
          table: "AspNetRoles");

      migrationBuilder.CreateTable(
          name: "Deductions",
          columns: table => new
          {
            Id = table.Column<Guid>(nullable: false),
            Dependents = table.Column<string>(nullable: true),
            EmployeeName = table.Column<string>(nullable: false),
            YearlyDeduction = table.Column<float>(nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Deductions", x => x.Id);
          });

      migrationBuilder.CreateIndex(
          name: "RoleNameIndex",
          table: "AspNetRoles",
          column: "NormalizedName",
          unique: true);

      /*migrationBuilder.AddForeignKey(
          name: "FK_AspNetUserTokens_AspNetUsers_UserId",
          table: "AspNetUserTokens",
          column: "UserId",
          principalTable: "AspNetUsers",
          principalColumn: "Id",
          onDelete: ReferentialAction.Cascade);*/
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      /*migrationBuilder.DropForeignKey(
          name: "FK_AspNetUserTokens_AspNetUsers_UserId",
          table: "AspNetUserTokens");*/

      migrationBuilder.DropTable(
          name: "Deductions");

      migrationBuilder.DropIndex(
          name: "RoleNameIndex",
          table: "AspNetRoles");

      migrationBuilder.CreateIndex(
          name: "IX_AspNetUserRoles_UserId",
          table: "AspNetUserRoles",
          column: "UserId");

      migrationBuilder.CreateIndex(
          name: "RoleNameIndex",
          table: "AspNetRoles",
          column: "NormalizedName");
    }
  }
}
