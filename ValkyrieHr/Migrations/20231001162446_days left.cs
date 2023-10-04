using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ValkyrieHr.Migrations
{
    public partial class daysleft : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NumberOdDays",
                table: "VacationBalances",
                newName: "NumberOfDays");

            migrationBuilder.AddColumn<int>(
                name: "DaysLeft",
                table: "VacationBalances",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DaysLeft",
                table: "VacationBalances");

            migrationBuilder.RenameColumn(
                name: "NumberOfDays",
                table: "VacationBalances",
                newName: "NumberOdDays");
        }
    }
}
