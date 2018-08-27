using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace cqrs.Data.Sql.EF.Migrations
{
    public partial class AuctionDurationLong : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "Duration", table: "Auctions");
            migrationBuilder.AddColumn<long>(
                name: "Duration",
                table: "Auctions",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "Duration", table: "Auctions");
            migrationBuilder.AddColumn<TimeSpan>(
                name: "Duration",
                table: "Auctions",
                nullable: false);
        }
    }
}
