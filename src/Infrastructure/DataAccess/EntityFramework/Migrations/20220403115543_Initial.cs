using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.DataAccess.EntityFramework.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "courses",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "varchar", maxLength: 80, nullable: false),
                    coursePrefix = table.Column<string>(type: "varchar", maxLength: 10, nullable: false),
                    createdAt = table.Column<DateTimeOffset>(type: "time with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_courses", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "students",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    firstName = table.Column<string>(type: "varchar", maxLength: 25, nullable: false),
                    lastName = table.Column<string>(type: "varchar", maxLength: 25, nullable: false),
                    email = table.Column<string>(type: "varchar", maxLength: 80, nullable: false),
                    passwordHash = table.Column<string>(type: "varchar", nullable: false),
                    birthDate = table.Column<DateOnly>(type: "date", nullable: false),
                    createdAt = table.Column<DateTimeOffset>(type: "time with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_students", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    email = table.Column<string>(type: "varchar", maxLength: 80, nullable: false),
                    passwordHash = table.Column<string>(type: "varchar", nullable: false),
                    createdAt = table.Column<DateTimeOffset>(type: "time with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "courseStudent",
                schema: "public",
                columns: table => new
                {
                    coursesId = table.Column<Guid>(type: "uuid", nullable: false),
                    studentsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_courseStudent", x => new { x.coursesId, x.studentsId });
                    table.ForeignKey(
                        name: "fK_courseStudent_courses_coursesId",
                        column: x => x.coursesId,
                        principalSchema: "public",
                        principalTable: "courses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fK_courseStudent_students_studentsId",
                        column: x => x.studentsId,
                        principalSchema: "public",
                        principalTable: "students",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "iX_courses_coursePrefix",
                schema: "public",
                table: "courses",
                column: "coursePrefix",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "iX_courseStudent_studentsId",
                schema: "public",
                table: "courseStudent",
                column: "studentsId");

            migrationBuilder.CreateIndex(
                name: "iX_students_email",
                schema: "public",
                table: "students",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "iX_users_email",
                schema: "public",
                table: "users",
                column: "email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "courseStudent",
                schema: "public");

            migrationBuilder.DropTable(
                name: "users",
                schema: "public");

            migrationBuilder.DropTable(
                name: "courses",
                schema: "public");

            migrationBuilder.DropTable(
                name: "students",
                schema: "public");
        }
    }
}
