using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TaskPaya_Back.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class initializers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Address", "CreateDate", "FullName", "IsDeleted", "LastUpdateDate" },
                values: new object[,]
                {
                    { 100L, "تهران پردیس", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "سینا یاری", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 101L, "قم خیابان امام خمینی", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "علی عباسی", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 102L, "مشهد الماس شرق", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ارشیا حسینی", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 100L);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 101L);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 102L);
        }
    }
}
