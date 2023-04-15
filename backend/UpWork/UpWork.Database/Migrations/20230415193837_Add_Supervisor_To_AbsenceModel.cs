using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UpWork.Database.Migrations
{
    /// <inheritdoc />
    public partial class Add_Supervisor_To_AbsenceModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Users_TimeOffSupervisorId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "TimeOffSupervisorId",
                table: "Users",
                newName: "CurrentTimeOffSupervisorId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_TimeOffSupervisorId",
                table: "Users",
                newName: "IX_Users_CurrentTimeOffSupervisorId");

            migrationBuilder.RenameColumn(
                name: "To",
                table: "AbsenceModel",
                newName: "ToDate");

            migrationBuilder.RenameColumn(
                name: "From",
                table: "AbsenceModel",
                newName: "TimeOffSupervisorId");

            migrationBuilder.AddColumn<DateTime>(
                name: "FromDate",
                table: "AbsenceModel",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_AbsenceModel_TimeOffSupervisorId",
                table: "AbsenceModel",
                column: "TimeOffSupervisorId");

            migrationBuilder.AddForeignKey(
                name: "FK_AbsenceModel_Users_TimeOffSupervisorId",
                table: "AbsenceModel",
                column: "TimeOffSupervisorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Users_CurrentTimeOffSupervisorId",
                table: "Users",
                column: "CurrentTimeOffSupervisorId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AbsenceModel_Users_TimeOffSupervisorId",
                table: "AbsenceModel");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Users_CurrentTimeOffSupervisorId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_AbsenceModel_TimeOffSupervisorId",
                table: "AbsenceModel");

            migrationBuilder.DropColumn(
                name: "FromDate",
                table: "AbsenceModel");

            migrationBuilder.RenameColumn(
                name: "CurrentTimeOffSupervisorId",
                table: "Users",
                newName: "TimeOffSupervisorId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_CurrentTimeOffSupervisorId",
                table: "Users",
                newName: "IX_Users_TimeOffSupervisorId");

            migrationBuilder.RenameColumn(
                name: "ToDate",
                table: "AbsenceModel",
                newName: "To");

            migrationBuilder.RenameColumn(
                name: "TimeOffSupervisorId",
                table: "AbsenceModel",
                newName: "From");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Users_TimeOffSupervisorId",
                table: "Users",
                column: "TimeOffSupervisorId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
