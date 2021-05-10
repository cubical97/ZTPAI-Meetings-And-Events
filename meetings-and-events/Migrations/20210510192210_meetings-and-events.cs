using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace meetings_and_events.Migrations
{
    public partial class meetingsandevents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Place",
                columns: table => new
                {
                    id_place = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    id_user = table.Column<int>(type: "integer", nullable: false),
                    image = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    create_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    multi_time = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Place", x => x.id_place);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id_user = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    username = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    password = table.Column<byte[]>(type: "bytea", maxLength: 256, nullable: false),
                    email = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    create_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id_user);
                });

            migrationBuilder.CreateTable(
                name: "Place_address",
                columns: table => new
                {
                    id_address = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_place = table.Column<int>(type: "integer", nullable: false),
                    country = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    city = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    street = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    number = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Place_address", x => x.id_address);
                    table.ForeignKey(
                        name: "FK_Place_address_Place_id_place",
                        column: x => x.id_place,
                        principalTable: "Place",
                        principalColumn: "id_place",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Place_data_multitime",
                columns: table => new
                {
                    id_data = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_place = table.Column<int>(type: "integer", nullable: false),
                    day_week = table.Column<int>(type: "integer", nullable: false),
                    start_date = table.Column<TimeSpan>(type: "interval", nullable: false),
                    end_date = table.Column<TimeSpan>(type: "interval", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Place_data_multitime", x => x.id_data);
                    table.ForeignKey(
                        name: "FK_Place_data_multitime_Place_id_place",
                        column: x => x.id_place,
                        principalTable: "Place",
                        principalColumn: "id_place",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Place_data_onetime",
                columns: table => new
                {
                    id_data = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_place = table.Column<int>(type: "integer", nullable: false),
                    start_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    end_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Place_data_onetime", x => x.id_data);
                    table.ForeignKey(
                        name: "FK_Place_data_onetime_Place_id_place",
                        column: x => x.id_place,
                        principalTable: "Place",
                        principalColumn: "id_place",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Place_special_close",
                columns: table => new
                {
                    id_data_closetime = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    id_place = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Place_special_close", x => x.id_data_closetime);
                    table.ForeignKey(
                        name: "FK_Place_special_close_Place_id_place",
                        column: x => x.id_place,
                        principalTable: "Place",
                        principalColumn: "id_place",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Place_comments",
                columns: table => new
                {
                    id_comment = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    comment_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    comment = table.Column<string>(type: "text", nullable: false),
                    id_user = table.Column<int>(type: "integer", nullable: false),
                    id_place = table.Column<int>(type: "integer", nullable: false),
                    Placeid_place = table.Column<int>(type: "integer", nullable: true),
                    Usersid_user = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Place_comments", x => x.id_comment);
                    table.ForeignKey(
                        name: "FK_Place_comments_Place_Placeid_place",
                        column: x => x.Placeid_place,
                        principalTable: "Place",
                        principalColumn: "id_place",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Place_comments_Users_Usersid_user",
                        column: x => x.Usersid_user,
                        principalTable: "Users",
                        principalColumn: "id_user",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Place_rate",
                columns: table => new
                {
                    id_rate = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    like = table.Column<bool>(type: "boolean", nullable: false),
                    id_user = table.Column<int>(type: "integer", nullable: false),
                    id_place = table.Column<int>(type: "integer", nullable: false),
                    Placeid_place = table.Column<int>(type: "integer", nullable: true),
                    Usersid_user = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Place_rate", x => x.id_rate);
                    table.ForeignKey(
                        name: "FK_Place_rate_Place_Placeid_place",
                        column: x => x.Placeid_place,
                        principalTable: "Place",
                        principalColumn: "id_place",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Place_rate_Users_Usersid_user",
                        column: x => x.Usersid_user,
                        principalTable: "Users",
                        principalColumn: "id_user",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "User_join",
                columns: table => new
                {
                    id_join = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_user = table.Column<int>(type: "integer", nullable: false),
                    id_place = table.Column<int>(type: "integer", nullable: false),
                    Placeid_place = table.Column<int>(type: "integer", nullable: true),
                    Usersid_user = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_join", x => x.id_join);
                    table.ForeignKey(
                        name: "FK_User_join_Place_Placeid_place",
                        column: x => x.Placeid_place,
                        principalTable: "Place",
                        principalColumn: "id_place",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_User_join_Users_Usersid_user",
                        column: x => x.Usersid_user,
                        principalTable: "Users",
                        principalColumn: "id_user",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users_follow",
                columns: table => new
                {
                    id_follow = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_user = table.Column<int>(type: "integer", nullable: false),
                    id_place = table.Column<int>(type: "integer", nullable: false),
                    Placeid_place = table.Column<int>(type: "integer", nullable: true),
                    Usersid_user = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users_follow", x => x.id_follow);
                    table.ForeignKey(
                        name: "FK_Users_follow_Place_Placeid_place",
                        column: x => x.Placeid_place,
                        principalTable: "Place",
                        principalColumn: "id_place",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_follow_Users_Usersid_user",
                        column: x => x.Usersid_user,
                        principalTable: "Users",
                        principalColumn: "id_user",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Place_address_id_place",
                table: "Place_address",
                column: "id_place",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Place_comments_Placeid_place",
                table: "Place_comments",
                column: "Placeid_place");

            migrationBuilder.CreateIndex(
                name: "IX_Place_comments_Usersid_user",
                table: "Place_comments",
                column: "Usersid_user");

            migrationBuilder.CreateIndex(
                name: "IX_Place_data_multitime_id_place",
                table: "Place_data_multitime",
                column: "id_place",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Place_data_onetime_id_place",
                table: "Place_data_onetime",
                column: "id_place",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Place_rate_Placeid_place",
                table: "Place_rate",
                column: "Placeid_place");

            migrationBuilder.CreateIndex(
                name: "IX_Place_rate_Usersid_user",
                table: "Place_rate",
                column: "Usersid_user");

            migrationBuilder.CreateIndex(
                name: "IX_Place_special_close_id_place",
                table: "Place_special_close",
                column: "id_place");

            migrationBuilder.CreateIndex(
                name: "IX_User_join_Placeid_place",
                table: "User_join",
                column: "Placeid_place");

            migrationBuilder.CreateIndex(
                name: "IX_User_join_Usersid_user",
                table: "User_join",
                column: "Usersid_user");

            migrationBuilder.CreateIndex(
                name: "IX_Users_follow_Placeid_place",
                table: "Users_follow",
                column: "Placeid_place");

            migrationBuilder.CreateIndex(
                name: "IX_Users_follow_Usersid_user",
                table: "Users_follow",
                column: "Usersid_user");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Place_address");

            migrationBuilder.DropTable(
                name: "Place_comments");

            migrationBuilder.DropTable(
                name: "Place_data_multitime");

            migrationBuilder.DropTable(
                name: "Place_data_onetime");

            migrationBuilder.DropTable(
                name: "Place_rate");

            migrationBuilder.DropTable(
                name: "Place_special_close");

            migrationBuilder.DropTable(
                name: "User_join");

            migrationBuilder.DropTable(
                name: "Users_follow");

            migrationBuilder.DropTable(
                name: "Place");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
