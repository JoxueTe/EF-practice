using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace projectef.Migrations
{
    /// <inheritdoc />
    public partial class InitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Task",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Category",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "CategoryId", "Description", "Name", "Weight" },
                values: new object[,]
                {
                    { new Guid("7bbef3f5-d64f-41ec-b201-38fb50cb9d56"), null, "Study", 10 },
                    { new Guid("7bbef3f5-d64f-41ec-b201-38fb50cb9d5d"), null, "Physical Activity", 7 }
                });

            migrationBuilder.InsertData(
                table: "Task",
                columns: new[] { "TaskId", "CategoryId", "CreationDate", "Description", "TaskPriority", "Title" },
                values: new object[,]
                {
                    { new Guid("7bbef3f5-d64f-41ec-b201-38fb50cb9d10"), new Guid("7bbef3f5-d64f-41ec-b201-38fb50cb9d5d"), new DateTime(2023, 10, 24, 19, 28, 13, 717, DateTimeKind.Utc).AddTicks(3885), null, 1, "Excercise" },
                    { new Guid("7bbef3f5-d64f-41ec-b201-38fb50cb9d11"), new Guid("7bbef3f5-d64f-41ec-b201-38fb50cb9d56"), new DateTime(2023, 10, 24, 19, 28, 13, 717, DateTimeKind.Utc).AddTicks(3894), null, 2, "Blazor Course" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Task",
                keyColumn: "TaskId",
                keyValue: new Guid("7bbef3f5-d64f-41ec-b201-38fb50cb9d10"));

            migrationBuilder.DeleteData(
                table: "Task",
                keyColumn: "TaskId",
                keyValue: new Guid("7bbef3f5-d64f-41ec-b201-38fb50cb9d11"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: new Guid("7bbef3f5-d64f-41ec-b201-38fb50cb9d56"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: new Guid("7bbef3f5-d64f-41ec-b201-38fb50cb9d5d"));

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Task",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Category",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
