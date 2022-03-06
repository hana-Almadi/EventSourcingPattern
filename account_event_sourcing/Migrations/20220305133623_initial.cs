using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace account_event_sourcing.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "BANK");

            migrationBuilder.EnsureSchema(
                name: "EVENTSOURCING");

            migrationBuilder.CreateTable(
                name: "AccountAggergate",
                schema: "BANK",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "DateTime", nullable: false),
                    AccountNumber = table.Column<string>(type: "varchar(36)", nullable: false),
                    OwnerName = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    OwnerID = table.Column<string>(type: "varchar(12)", maxLength: 12, nullable: false),
                    OwnerEmail = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    OwnerPhone = table.Column<string>(type: "varchar(9)", maxLength: 9, nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "DateTime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountAggergate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DomainEvent",
                schema: "EVENTSOURCING",
                columns: table => new
                {
                    SequenceId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(type: "DateTime", nullable: false),
                    EventJson = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DomainEvent", x => x.SequenceId);
                });

            migrationBuilder.CreateTable(
                name: "ACCOUNT",
                schema: "BANK",
                columns: table => new
                {
                    AccountId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Money = table.Column<int>(type: "int", nullable: false, defaultValueSql: "0"),
                    ActionType = table.Column<int>(type: "int", nullable: false),
                    AccountAggergateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "DateTime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ACCOUNT", x => x.AccountId);
                    table.ForeignKey(
                        name: "FK_ACCOUNT_AccountAggergate_AccountAggergateId",
                        column: x => x.AccountAggergateId,
                        principalSchema: "BANK",
                        principalTable: "AccountAggergate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ACCOUNT_AccountAggergateId",
                schema: "BANK",
                table: "ACCOUNT",
                column: "AccountAggergateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ACCOUNT",
                schema: "BANK");

            migrationBuilder.DropTable(
                name: "DomainEvent",
                schema: "EVENTSOURCING");

            migrationBuilder.DropTable(
                name: "AccountAggergate",
                schema: "BANK");
        }
    }
}
