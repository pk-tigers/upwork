using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UpWork.Database.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbsenceTypeModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    NeedApproval = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    OrganizationId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbsenceTypeModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AbsenceTypeModel_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: true),
                    LastName = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    Avatar = table.Column<byte[]>(type: "BLOB", maxLength: 102400, nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    OrganizationId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TimeOffSupervisorId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Users_TimeOffSupervisorId",
                        column: x => x.TimeOffSupervisorId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AbsenceModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    From = table.Column<DateTime>(type: "TEXT", nullable: false),
                    To = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    AbsenceTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    AbsenceTypeId1 = table.Column<Guid>(type: "TEXT", nullable: true),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserModelId = table.Column<Guid>(type: "TEXT", nullable: true),
                    ApprovalState = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbsenceModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AbsenceModel_AbsenceTypeModel_AbsenceTypeId1",
                        column: x => x.AbsenceTypeId1,
                        principalTable: "AbsenceTypeModel",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AbsenceModel_Users_UserModelId",
                        column: x => x.UserModelId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AbsenceModel_AbsenceTypeId1",
                table: "AbsenceModel",
                column: "AbsenceTypeId1");

            migrationBuilder.CreateIndex(
                name: "IX_AbsenceModel_UserModelId",
                table: "AbsenceModel",
                column: "UserModelId");

            migrationBuilder.CreateIndex(
                name: "IX_AbsenceTypeModel_OrganizationId",
                table: "AbsenceTypeModel",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_OrganizationId",
                table: "Users",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_TimeOffSupervisorId",
                table: "Users",
                column: "TimeOffSupervisorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AbsenceModel");

            migrationBuilder.DropTable(
                name: "AbsenceTypeModel");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Organizations");
        }
    }
}
