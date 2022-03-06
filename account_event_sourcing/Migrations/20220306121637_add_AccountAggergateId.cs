using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace account_event_sourcing.Migrations
{
    public partial class add_AccountAggergateId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AccountAggergateId",
                schema: "EVENTSOURCING",
                table: "DomainEvent",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_DomainEvent_AccountAggergateId",
                schema: "EVENTSOURCING",
                table: "DomainEvent",
                column: "AccountAggergateId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DomainEvent_AccountAggergate_AccountAggergateId",
                schema: "EVENTSOURCING",
                table: "DomainEvent",
                column: "AccountAggergateId",
                principalSchema: "BANK",
                principalTable: "AccountAggergate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DomainEvent_AccountAggergate_AccountAggergateId",
                schema: "EVENTSOURCING",
                table: "DomainEvent");

            migrationBuilder.DropIndex(
                name: "IX_DomainEvent_AccountAggergateId",
                schema: "EVENTSOURCING",
                table: "DomainEvent");

            migrationBuilder.DropColumn(
                name: "AccountAggergateId",
                schema: "EVENTSOURCING",
                table: "DomainEvent");
        }
    }
}
