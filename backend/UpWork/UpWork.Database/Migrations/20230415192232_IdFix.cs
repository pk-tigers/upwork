using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UpWork.Database.Migrations
{
    /// <inheritdoc />
    public partial class IdFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AbsenceModel_AbsenceTypeModel_AbsenceTypeId1",
                table: "AbsenceModel");

            migrationBuilder.DropIndex(
                name: "IX_AbsenceModel_AbsenceTypeId1",
                table: "AbsenceModel");

            migrationBuilder.DropColumn(
                name: "AbsenceTypeId1",
                table: "AbsenceModel");

            migrationBuilder.AlterColumn<Guid>(
                name: "AbsenceTypeId",
                table: "AbsenceModel",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.CreateIndex(
                name: "IX_AbsenceModel_AbsenceTypeId",
                table: "AbsenceModel",
                column: "AbsenceTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AbsenceModel_AbsenceTypeModel_AbsenceTypeId",
                table: "AbsenceModel",
                column: "AbsenceTypeId",
                principalTable: "AbsenceTypeModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AbsenceModel_AbsenceTypeModel_AbsenceTypeId",
                table: "AbsenceModel");

            migrationBuilder.DropIndex(
                name: "IX_AbsenceModel_AbsenceTypeId",
                table: "AbsenceModel");

            migrationBuilder.AlterColumn<int>(
                name: "AbsenceTypeId",
                table: "AbsenceModel",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddColumn<Guid>(
                name: "AbsenceTypeId1",
                table: "AbsenceModel",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AbsenceModel_AbsenceTypeId1",
                table: "AbsenceModel",
                column: "AbsenceTypeId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AbsenceModel_AbsenceTypeModel_AbsenceTypeId1",
                table: "AbsenceModel",
                column: "AbsenceTypeId1",
                principalTable: "AbsenceTypeModel",
                principalColumn: "Id");
        }
    }
}
