using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMS.Web.Migrations
{
    public partial class AddTimeSlotCoulmn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Slot",
                table: "Schedules",
                newName: "DateSlot");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "TimeSlot",
                table: "Schedules",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeSlot",
                table: "Schedules");

            migrationBuilder.RenameColumn(
                name: "DateSlot",
                table: "Schedules",
                newName: "Slot");
        }
    }
}
