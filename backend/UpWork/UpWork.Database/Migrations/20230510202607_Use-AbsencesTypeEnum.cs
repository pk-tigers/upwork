using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UpWork.Database.Migrations
{
    /// <inheritdoc />
    public partial class UseAbsencesTypeEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Absences_AbsencesType_AbsenceTypeId",
                table: "Absences");

            migrationBuilder.DropForeignKey(
                name: "FK_AbsencesType_Organizations_OrganizationId",
                table: "AbsencesType");

            migrationBuilder.DropIndex(
                name: "IX_AbsencesType_OrganizationId",
                table: "AbsencesType");

            migrationBuilder.DropIndex(
                name: "IX_Absences_AbsenceTypeId",
                table: "Absences");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "AbsencesType");

            migrationBuilder.DropColumn(
                name: "AbsenceTypeId",
                table: "Absences");

            migrationBuilder.AlterColumn<Guid>(
                name: "TimeOffSupervisorId",
                table: "Absences",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<int>(
                name: "AbsenceType",
                table: "Absences",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AbsenceType",
                table: "Absences");

            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationId",
                table: "AbsencesType",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "TimeOffSupervisorId",
                table: "Absences",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AbsenceTypeId",
                table: "Absences",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AbsencesType_OrganizationId",
                table: "AbsencesType",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Absences_AbsenceTypeId",
                table: "Absences",
                column: "AbsenceTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Absences_AbsencesType_AbsenceTypeId",
                table: "Absences",
                column: "AbsenceTypeId",
                principalTable: "AbsencesType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AbsencesType_Organizations_OrganizationId",
                table: "AbsencesType",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
