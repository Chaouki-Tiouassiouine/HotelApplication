using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HotelApplication.Data.Migrations
{
    public partial class Fifth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_AspNetUsers_GuestId",
                table: "Booking");

            migrationBuilder.AlterColumn<string>(
                name: "GuestId",
                table: "Booking",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Booking",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_AspNetUsers_GuestId",
                table: "Booking",
                column: "GuestId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_AspNetUsers_GuestId",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Booking");

            migrationBuilder.AlterColumn<string>(
                name: "GuestId",
                table: "Booking",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_AspNetUsers_GuestId",
                table: "Booking",
                column: "GuestId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
