using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace movie_reservation_system.Migrations
{
    /// <inheritdoc />
    public partial class FixTypo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Seats");

            migrationBuilder.RenameColumn(
                name: "PricaAtBooking",
                table: "ReservationSeats",
                newName: "PriceAtBooking");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalAmount",
                table: "Reservations",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PriceAtBooking",
                table: "ReservationSeats",
                newName: "PricaAtBooking");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Seats",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "TotalAmount",
                table: "Reservations",
                type: "integer",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");
        }
    }
}
