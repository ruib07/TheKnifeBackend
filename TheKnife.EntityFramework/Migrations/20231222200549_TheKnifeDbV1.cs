using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheKnife.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class TheKnifeDbV1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Client_Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<int>(type: "int", nullable: false),
                    Subject = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Message = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RegisterUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "varchar(16)", unicode: false, maxLength: 16, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegisterUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RestaurantRegistrations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Phone = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    RName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Category = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Desc = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    Rphone = table.Column<int>(type: "int", nullable: false),
                    Location = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
                    Image = table.Column<string>(type: "varchar(400)", unicode: false, maxLength: 400, nullable: false),
                    Numberoftables = table.Column<int>(type: "int", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    Openingdays = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Openinghours = table.Column<TimeOnly>(type: "time", nullable: false),
                    Closinghours = table.Column<TimeOnly>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestaurantRegistrations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "varchar(16)", unicode: false, maxLength: 16, nullable: false),
                    Image = table.Column<string>(type: "varchar(400)", unicode: false, maxLength: 400, nullable: false),
                    RegisterUser_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_RegisterUsers_RegisterUser_Id",
                        column: x => x.RegisterUser_Id,
                        principalTable: "RegisterUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RestaurantResponsibles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Phone = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "varchar(16)", unicode: false, maxLength: 16, nullable: false),
                    RestaurantRegistration_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestaurantResponsibles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RestaurantResponsibles_RestaurantRegistrations_RestaurantRegistration_Id",
                        column: x => x.RestaurantRegistration_Id,
                        principalTable: "RestaurantRegistrations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Restaurants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Category = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Desc = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    Rphone = table.Column<int>(type: "int", nullable: false),
                    Location = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
                    Image = table.Column<string>(type: "varchar(400)", unicode: false, maxLength: 400, nullable: false),
                    Numberoftables = table.Column<int>(type: "int", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    Openingdays = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Openinghours = table.Column<TimeOnly>(type: "time", nullable: false),
                    Closinghours = table.Column<TimeOnly>(type: "time", nullable: false),
                    RestaurantRegistration_Id = table.Column<int>(type: "int", nullable: false),
                    Rresponsible_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Restaurants_RestaurantRegistrations_RestaurantRegistration_Id",
                        column: x => x.RestaurantRegistration_Id,
                        principalTable: "RestaurantRegistrations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Restaurants_RestaurantResponsibles_Rresponsible_Id",
                        column: x => x.Rresponsible_Id,
                        principalTable: "RestaurantResponsibles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Commentdate = table.Column<DateOnly>(type: "date", nullable: false),
                    Review = table.Column<decimal>(type: "decimal(3,1)", nullable: false),
                    Comment = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    User_Id = table.Column<int>(type: "int", nullable: false),
                    Restaurant_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Restaurants_Restaurant_Id",
                        column: x => x.Restaurant_Id,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Users_User_Id",
                        column: x => x.User_Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReservationHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reservationdate = table.Column<DateOnly>(type: "date", nullable: false),
                    Reservationtime = table.Column<TimeOnly>(type: "time", nullable: false),
                    Client_Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Restaurant_Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    User_Id = table.Column<int>(type: "int", nullable: false),
                    Restaurant_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReservationHistory_Restaurants_Restaurant_Id",
                        column: x => x.Restaurant_Id,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReservationHistory_Users_User_Id",
                        column: x => x.User_Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Client_Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<int>(type: "int", nullable: false),
                    Reservationdate = table.Column<DateOnly>(type: "date", nullable: false),
                    Reservationtime = table.Column<TimeOnly>(type: "time", nullable: false),
                    Numberofpeople = table.Column<int>(type: "int", nullable: false),
                    Restaurant_Id = table.Column<int>(type: "int", nullable: false),
                    User_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_Restaurants_Restaurant_Id",
                        column: x => x.Restaurant_Id,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_Users_User_Id",
                        column: x => x.User_Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReservationHistoryRestaurants",
                columns: table => new
                {
                    ReservationHistory_Id = table.Column<int>(type: "int", nullable: false),
                    Restaurant_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_ReservationHistoryRestaurants_ReservationHistory_ReservationHistory_Id",
                        column: x => x.ReservationHistory_Id,
                        principalTable: "ReservationHistory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReservationHistoryRestaurants_Restaurants_Restaurant_Id",
                        column: x => x.Restaurant_Id,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReservationHistoryReservations",
                columns: table => new
                {
                    ReservationHistory_Id = table.Column<int>(type: "int", nullable: false),
                    Reservation_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_ReservationHistoryReservations_ReservationHistory_ReservationHistory_Id",
                        column: x => x.ReservationHistory_Id,
                        principalTable: "ReservationHistory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReservationHistoryReservations_Reservations_Reservation_Id",
                        column: x => x.Reservation_Id,
                        principalTable: "Reservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_Restaurant_Id",
                table: "Comments",
                column: "Restaurant_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_User_Id",
                table: "Comments",
                column: "User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationHistory_Restaurant_Id",
                table: "ReservationHistory",
                column: "Restaurant_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationHistory_User_Id",
                table: "ReservationHistory",
                column: "User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationHistoryReservations_Reservation_Id",
                table: "ReservationHistoryReservations",
                column: "Reservation_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationHistoryReservations_ReservationHistory_Id",
                table: "ReservationHistoryReservations",
                column: "ReservationHistory_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationHistoryRestaurants_ReservationHistory_Id",
                table: "ReservationHistoryRestaurants",
                column: "ReservationHistory_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationHistoryRestaurants_Restaurant_Id",
                table: "ReservationHistoryRestaurants",
                column: "Restaurant_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_Restaurant_Id",
                table: "Reservations",
                column: "Restaurant_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_User_Id",
                table: "Reservations",
                column: "User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_RestaurantResponsibles_RestaurantRegistration_Id",
                table: "RestaurantResponsibles",
                column: "RestaurantRegistration_Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_RestaurantRegistration_Id",
                table: "Restaurants",
                column: "RestaurantRegistration_Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_Rresponsible_Id",
                table: "Restaurants",
                column: "Rresponsible_Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RegisterUser_Id",
                table: "Users",
                column: "RegisterUser_Id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "ReservationHistoryReservations");

            migrationBuilder.DropTable(
                name: "ReservationHistoryRestaurants");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "ReservationHistory");

            migrationBuilder.DropTable(
                name: "Restaurants");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "RestaurantResponsibles");

            migrationBuilder.DropTable(
                name: "RegisterUsers");

            migrationBuilder.DropTable(
                name: "RestaurantRegistrations");
        }
    }
}
