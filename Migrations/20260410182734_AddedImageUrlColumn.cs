using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace movie_reservation_system.Migrations
{
    /// <inheritdoc />
    public partial class AddedImageUrlColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MovieAvtUrl",
                table: "Movies",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MovieAvtUrl",
                table: "Movies");
        }
    }
}
