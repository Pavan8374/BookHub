using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BulkyBook.DataAccess.Migrations
{
    public partial class companyRecords : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "City", "Name", "PhoneNumber", "Postalcode", "State", "StreetAddress" },
                values: new object[] { 2, "Hyderabad", "A1 Solutions", "8883706744", "437723", "Telangaana", "1-19" });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "City", "Name", "PhoneNumber", "Postalcode", "State", "StreetAddress" },
                values: new object[] { 3, "Hyderabad", "N1 Solutions", "8803606344", "437723", "Telangaana", "1-23" });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "City", "Name", "PhoneNumber", "Postalcode", "State", "StreetAddress" },
                values: new object[] { 4, "Hyderabad", "c1 Solutions", "8803707344", "437723", "Telangaana", "1-34" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
