using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DentalClinic.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PATIENT",
                columns: table => new
                {
                    patient_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    identification = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    first_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    last_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    birth_date = table.Column<DateOnly>(type: "date", nullable: false),
                    phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    address = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    gender = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PATIENT", x => x.patient_id);
                });

            migrationBuilder.CreateTable(
                name: "ROLE",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleResourceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ROLE", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "USER",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_resource_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    first_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    last_name = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    password_hash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "MEDICAL_RECORD",
                columns: table => new
                {
                    record_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    patient_id = table.Column<int>(type: "int", nullable: false),
                    created_date = table.Column<DateOnly>(type: "date", nullable: false),
                    medical_history = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    allergies = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    general_notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MEDICAL_RECORD", x => x.record_id);
                    table.ForeignKey(
                        name: "FK_MEDICAL_RECORD_PATIENT_patient_id",
                        column: x => x.patient_id,
                        principalTable: "PATIENT",
                        principalColumn: "patient_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "APPOINTMENT",
                columns: table => new
                {
                    appointment_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    patient_id = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    appointment_date = table.Column<DateOnly>(type: "date", nullable: false),
                    appointment_time = table.Column<TimeOnly>(type: "time", nullable: false),
                    duration_minutes = table.Column<int>(type: "int", nullable: false),
                    reason = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_APPOINTMENT", x => x.appointment_id);
                    table.ForeignKey(
                        name: "FK_APPOINTMENT_PATIENT_patient_id",
                        column: x => x.patient_id,
                        principalTable: "PATIENT",
                        principalColumn: "patient_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_APPOINTMENT_USER_user_id",
                        column: x => x.user_id,
                        principalTable: "USER",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "USER_ROLE",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    UserRoleResourceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER_ROLE", x => new { x.user_id, x.RoleId });
                    table.ForeignKey(
                        name: "FK_USER_ROLE_ROLE_RoleId",
                        column: x => x.RoleId,
                        principalTable: "ROLE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_USER_ROLE_USER_user_id",
                        column: x => x.user_id,
                        principalTable: "USER",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CONSULTATION",
                columns: table => new
                {
                    consultation_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    record_id = table.Column<int>(type: "int", nullable: false),
                    appointment_id = table.Column<int>(type: "int", nullable: true),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    consultation_date = table.Column<DateOnly>(type: "date", nullable: false),
                    reason = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    observations = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    odontogram = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CONSULTATION", x => x.consultation_id);
                    table.ForeignKey(
                        name: "FK_CONSULTATION_APPOINTMENT_appointment_id",
                        column: x => x.appointment_id,
                        principalTable: "APPOINTMENT",
                        principalColumn: "appointment_id");
                    table.ForeignKey(
                        name: "FK_CONSULTATION_MEDICAL_RECORD_record_id",
                        column: x => x.record_id,
                        principalTable: "MEDICAL_RECORD",
                        principalColumn: "record_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CONSULTATION_USER_user_id",
                        column: x => x.user_id,
                        principalTable: "USER",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DIAGNOSIS",
                columns: table => new
                {
                    diagnosis_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    consultation_id = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    diagnosis_date = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DIAGNOSIS", x => x.diagnosis_id);
                    table.ForeignKey(
                        name: "FK_DIAGNOSIS_CONSULTATION_consultation_id",
                        column: x => x.consultation_id,
                        principalTable: "CONSULTATION",
                        principalColumn: "consultation_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TREATMENT",
                columns: table => new
                {
                    treatment_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    consultation_id = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    start_date = table.Column<DateOnly>(type: "date", nullable: false),
                    end_date = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TREATMENT", x => x.treatment_id);
                    table.ForeignKey(
                        name: "FK_TREATMENT_CONSULTATION_consultation_id",
                        column: x => x.consultation_id,
                        principalTable: "CONSULTATION",
                        principalColumn: "consultation_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_APPOINTMENT_patient_id",
                table: "APPOINTMENT",
                column: "patient_id");

            migrationBuilder.CreateIndex(
                name: "IX_APPOINTMENT_user_id",
                table: "APPOINTMENT",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_CONSULTATION_appointment_id",
                table: "CONSULTATION",
                column: "appointment_id");

            migrationBuilder.CreateIndex(
                name: "IX_CONSULTATION_record_id",
                table: "CONSULTATION",
                column: "record_id");

            migrationBuilder.CreateIndex(
                name: "IX_CONSULTATION_user_id",
                table: "CONSULTATION",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_DIAGNOSIS_consultation_id",
                table: "DIAGNOSIS",
                column: "consultation_id");

            migrationBuilder.CreateIndex(
                name: "IX_MEDICAL_RECORD_patient_id",
                table: "MEDICAL_RECORD",
                column: "patient_id");

            migrationBuilder.CreateIndex(
                name: "IX_ROLE_Name",
                table: "ROLE",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ROLE_RoleResourceId",
                table: "ROLE",
                column: "RoleResourceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TREATMENT_consultation_id",
                table: "TREATMENT",
                column: "consultation_id");

            migrationBuilder.CreateIndex(
                name: "IX_USER_ROLE_RoleId",
                table: "USER_ROLE",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_USER_ROLE_UserRoleResourceId",
                table: "USER_ROLE",
                column: "UserRoleResourceId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DIAGNOSIS");

            migrationBuilder.DropTable(
                name: "TREATMENT");

            migrationBuilder.DropTable(
                name: "USER_ROLE");

            migrationBuilder.DropTable(
                name: "CONSULTATION");

            migrationBuilder.DropTable(
                name: "ROLE");

            migrationBuilder.DropTable(
                name: "APPOINTMENT");

            migrationBuilder.DropTable(
                name: "MEDICAL_RECORD");

            migrationBuilder.DropTable(
                name: "USER");

            migrationBuilder.DropTable(
                name: "PATIENT");
        }
    }
}
