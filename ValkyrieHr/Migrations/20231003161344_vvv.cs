using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ValkyrieHr.Migrations
{
    public partial class vvv : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vacations_VacationBalances_VacationBalanceId",
                table: "Vacations");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Vacations");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "VacationBalances");

            migrationBuilder.RenameColumn(
                name: "VacationBalanceId",
                table: "Vacations",
                newName: "VacationTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Vacations_VacationBalanceId",
                table: "Vacations",
                newName: "IX_Vacations_VacationTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vacations_VacationTypes_VacationTypeId",
                table: "Vacations",
                column: "VacationTypeId",
                principalTable: "VacationTypes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vacations_VacationTypes_VacationTypeId",
                table: "Vacations");

            migrationBuilder.RenameColumn(
                name: "VacationTypeId",
                table: "Vacations",
                newName: "VacationBalanceId");

            migrationBuilder.RenameIndex(
                name: "IX_Vacations_VacationTypeId",
                table: "Vacations",
                newName: "IX_Vacations_VacationBalanceId");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Vacations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "VacationBalances",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Vacations_VacationBalances_VacationBalanceId",
                table: "Vacations",
                column: "VacationBalanceId",
                principalTable: "VacationBalances",
                principalColumn: "Id");
        }
    }
}
