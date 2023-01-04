using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ChatService.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChatRooms",
                columns: table => new
                {
                    id = table.Column<int>(name: "_id", type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    roomName = table.Column<string>(name: "_roomName", type: "character varying(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatRooms", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<int>(name: "_id", type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(name: "_name", type: "character varying(60)", maxLength: 60, nullable: false),
                    ChatRoomid = table.Column<int>(name: "ChatRoom_id", type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                    table.ForeignKey(
                        name: "FK_Users_ChatRooms_ChatRoom_id",
                        column: x => x.ChatRoomid,
                        principalTable: "ChatRooms",
                        principalColumn: "_id");
                });

            migrationBuilder.CreateTable(
                name: "ChatMessages",
                columns: table => new
                {
                    id = table.Column<int>(name: "_id", type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    message = table.Column<string>(name: "_message", type: "text", nullable: false),
                    senderid = table.Column<int>(name: "_sender_id", type: "integer", nullable: false),
                    timeSent = table.Column<DateTime>(name: "_timeSent", type: "timestamp with time zone", nullable: false),
                    ChatRoomid = table.Column<int>(name: "ChatRoom_id", type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessages", x => x.id);
                    table.ForeignKey(
                        name: "FK_ChatMessages_ChatRooms_ChatRoom_id",
                        column: x => x.ChatRoomid,
                        principalTable: "ChatRooms",
                        principalColumn: "_id");
                    table.ForeignKey(
                        name: "FK_ChatMessages_Users__sender_id",
                        column: x => x.senderid,
                        principalTable: "Users",
                        principalColumn: "_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages__sender_id",
                table: "ChatMessages",
                column: "_sender_id");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_ChatRoom_id",
                table: "ChatMessages",
                column: "ChatRoom_id");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ChatRoom_id",
                table: "Users",
                column: "ChatRoom_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatMessages");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "ChatRooms");
        }
    }
}
