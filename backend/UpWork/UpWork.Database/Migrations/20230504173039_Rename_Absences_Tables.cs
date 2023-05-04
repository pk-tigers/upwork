using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UpWork.Database.Migrations
{
    /// <inheritdoc />
    public partial class Rename_Absences_Tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AbsenceModel_AbsenceTypeModel_AbsenceTypeId",
                table: "AbsenceModel");

            migrationBuilder.DropForeignKey(
                name: "FK_AbsenceModel_Users_TimeOffSupervisorId",
                table: "AbsenceModel");

            migrationBuilder.DropForeignKey(
                name: "FK_AbsenceModel_Users_UserId",
                table: "AbsenceModel");

            migrationBuilder.DropForeignKey(
                name: "FK_AbsenceTypeModel_Organizations_OrganizationId",
                table: "AbsenceTypeModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AbsenceTypeModel",
                table: "AbsenceTypeModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AbsenceModel",
                table: "AbsenceModel");

            migrationBuilder.RenameTable(
                name: "AbsenceTypeModel",
                newName: "AbsencesType");

            migrationBuilder.RenameTable(
                name: "AbsenceModel",
                newName: "Absences");

            migrationBuilder.RenameIndex(
                name: "IX_AbsenceTypeModel_OrganizationId",
                table: "AbsencesType",
                newName: "IX_AbsencesType_OrganizationId");

            migrationBuilder.RenameIndex(
                name: "IX_AbsenceModel_UserId",
                table: "Absences",
                newName: "IX_Absences_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AbsenceModel_TimeOffSupervisorId",
                table: "Absences",
                newName: "IX_Absences_TimeOffSupervisorId");

            migrationBuilder.RenameIndex(
                name: "IX_AbsenceModel_AbsenceTypeId",
                table: "Absences",
                newName: "IX_Absences_AbsenceTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AbsencesType",
                table: "AbsencesType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Absences",
                table: "Absences",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Absences_AbsencesType_AbsenceTypeId",
                table: "Absences",
                column: "AbsenceTypeId",
                principalTable: "AbsencesType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Absences_Users_TimeOffSupervisorId",
                table: "Absences",
                column: "TimeOffSupervisorId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Absences_Users_UserId",
                table: "Absences",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AbsencesType_Organizations_OrganizationId",
                table: "AbsencesType",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Absences_AbsencesType_AbsenceTypeId",
                table: "Absences");

            migrationBuilder.DropForeignKey(
                name: "FK_Absences_Users_TimeOffSupervisorId",
                table: "Absences");

            migrationBuilder.DropForeignKey(
                name: "FK_Absences_Users_UserId",
                table: "Absences");

            migrationBuilder.DropForeignKey(
                name: "FK_AbsencesType_Organizations_OrganizationId",
                table: "AbsencesType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AbsencesType",
                table: "AbsencesType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Absences",
                table: "Absences");

            migrationBuilder.RenameTable(
                name: "AbsencesType",
                newName: "AbsenceTypeModel");

            migrationBuilder.RenameTable(
                name: "Absences",
                newName: "AbsenceModel");

            migrationBuilder.RenameIndex(
                name: "IX_AbsencesType_OrganizationId",
                table: "AbsenceTypeModel",
                newName: "IX_AbsenceTypeModel_OrganizationId");

            migrationBuilder.RenameIndex(
                name: "IX_Absences_UserId",
                table: "AbsenceModel",
                newName: "IX_AbsenceModel_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Absences_TimeOffSupervisorId",
                table: "AbsenceModel",
                newName: "IX_AbsenceModel_TimeOffSupervisorId");

            migrationBuilder.RenameIndex(
                name: "IX_Absences_AbsenceTypeId",
                table: "AbsenceModel",
                newName: "IX_AbsenceModel_AbsenceTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AbsenceTypeModel",
                table: "AbsenceTypeModel",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AbsenceModel",
                table: "AbsenceModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AbsenceModel_AbsenceTypeModel_AbsenceTypeId",
                table: "AbsenceModel",
                column: "AbsenceTypeId",
                principalTable: "AbsenceTypeModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AbsenceModel_Users_TimeOffSupervisorId",
                table: "AbsenceModel",
                column: "TimeOffSupervisorId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AbsenceModel_Users_UserId",
                table: "AbsenceModel",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AbsenceTypeModel_Organizations_OrganizationId",
                table: "AbsenceTypeModel",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
