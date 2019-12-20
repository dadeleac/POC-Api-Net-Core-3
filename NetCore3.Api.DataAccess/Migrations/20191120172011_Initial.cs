using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NetCore3.Api.DataAccess.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Author",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    Job = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Author", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    Birthday = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    AuthorId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courses_Author_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Author",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentCourses",
                columns: table => new
                {
                    StudentId = table.Column<Guid>(nullable: false),
                    CourseId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentCourses", x => new { x.StudentId, x.CourseId });
                    table.ForeignKey(
                        name: "FK_StudentCourses_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentCourses_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Author",
                columns: new[] { "Id", "Job", "Name", "Surname" },
                values: new object[,]
                {
                    { new Guid("f11f51ad-355e-4bc6-9c9a-9acfb564bdba"), "Computer Science", "Adan", "Smith" },
                    { new Guid("b2fd6b81-3a0e-4e8f-9eed-4dd659049831"), "Software Developer", "Ben", "Anderson" },
                    { new Guid("51751abd-82a9-443e-bf6a-1566d3cc0ff4"), "Scrum Master", "Elliot", "Simpson" }
                });

            migrationBuilder.InsertData(
                 table: "Students",
                 columns: new[] { "Id", "Birthday", "Name", "Surname" },
                 values: new object[,]
                 {
                    { new Guid("239ad284-edea-41e8-9e28-29fe4030e43f"), new DateTimeOffset(new DateTime(1995, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), "Helen", "Murray" },
                    { new Guid("fcd3a962-bab8-4f29-b9f5-77d60ecfd5e6"), new DateTimeOffset(new DateTime(2000, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Bill", "Mirren" }
                 });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "AuthorId", "Description", "Title" },
                values: new object[] { new Guid("98f83831-f789-45fc-9a12-45f6198bf8ba"), new Guid("f11f51ad-355e-4bc6-9c9a-9acfb564bdba"), "Python MOOC", "Python MOOC" });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "AuthorId", "Description", "Title" },
                values: new object[] { new Guid("b398859a-7fb6-4866-a474-d043bff65e52"), new Guid("51751abd-82a9-443e-bf6a-1566d3cc0ff4"), "Scrum Master MOOC", "Scrum Master Certification" });

            migrationBuilder.InsertData(
                table: "StudentCourses",
                columns: new[] { "StudentId", "CourseId" },
                values: new object[] { new Guid("239ad284-edea-41e8-9e28-29fe4030e43f"), new Guid("b398859a-7fb6-4866-a474-d043bff65e52") });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_AuthorId",
                table: "Courses",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCourses_CourseId",
                table: "StudentCourses",
                column: "CourseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentCourses");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Author");
        }
    }
}
