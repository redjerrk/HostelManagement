using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.MVC.Migrations
{
    public partial class newpropertyintofee_member : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PaymentStatus",
                table: "Fees",
                newName: "DueAmount");

            migrationBuilder.RenameColumn(
                name: "OverDueDate",
                table: "Fees",
                newName: "PaymentDate");

            migrationBuilder.RenameColumn(
                name: "DueDate",
                table: "Fees",
                newName: "LastRentGeneratedDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "RegisterDate",
                table: "Members",
                type: "datetime(6)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegisterDate",
                table: "Members");

            migrationBuilder.RenameColumn(
                name: "PaymentDate",
                table: "Fees",
                newName: "OverDueDate");

            migrationBuilder.RenameColumn(
                name: "LastRentGeneratedDate",
                table: "Fees",
                newName: "DueDate");

            migrationBuilder.RenameColumn(
                name: "DueAmount",
                table: "Fees",
                newName: "PaymentStatus");
        }
    }
}
