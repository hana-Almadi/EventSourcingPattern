using Microsoft.EntityFrameworkCore.Migrations;

namespace account_event_sourcing.Migrations
{
    public partial class fix_fk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DomainEvent_AccountAggergateId",
                schema: "EVENTSOURCING",
                table: "DomainEvent");

            migrationBuilder.CreateIndex(
                name: "IX_DomainEvent_AccountAggergateId",
                schema: "EVENTSOURCING",
                table: "DomainEvent",
                column: "AccountAggergateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DomainEvent_AccountAggergateId",
                schema: "EVENTSOURCING",
                table: "DomainEvent");

            migrationBuilder.CreateIndex(
                name: "IX_DomainEvent_AccountAggergateId",
                schema: "EVENTSOURCING",
                table: "DomainEvent",
                column: "AccountAggergateId",
                unique: true);
        }
    }
}
