using Microsoft.EntityFrameworkCore.Migrations;

namespace account_event_sourcing.Migrations
{
    public partial class add_onDelete_AccountAggergateId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DomainEvent_AccountAggergate_AccountAggergateId",
                schema: "EVENTSOURCING",
                table: "DomainEvent");

            migrationBuilder.AddForeignKey(
                name: "FK_DomainEvent_AccountAggergate_AccountAggergateId",
                schema: "EVENTSOURCING",
                table: "DomainEvent",
                column: "AccountAggergateId",
                principalSchema: "BANK",
                principalTable: "AccountAggergate",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DomainEvent_AccountAggergate_AccountAggergateId",
                schema: "EVENTSOURCING",
                table: "DomainEvent");

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
    }
}
