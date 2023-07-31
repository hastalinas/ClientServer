using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class addGuid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "tb_m_roles",
                columns: new[] { "guid", "created_date", "modified_date", "name" },
                values: new object[] { new Guid("4887ec13-b482-47b3-9b24-08db91a71770"), new DateTime(2023, 7, 31, 20, 11, 28, 306, DateTimeKind.Local).AddTicks(3268), new DateTime(2023, 7, 31, 20, 11, 28, 306, DateTimeKind.Local).AddTicks(3281), "Employee" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "tb_m_roles",
                keyColumn: "guid",
                keyValue: new Guid("4887ec13-b482-47b3-9b24-08db91a71770"));
        }
    }
}
