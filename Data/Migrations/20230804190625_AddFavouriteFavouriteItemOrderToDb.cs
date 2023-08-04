using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuperCarSpot.Data.Migrations
{
    public partial class AddFavouriteFavouriteItemOrderToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Favorites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Favorites_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FavouriteItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FavouriteId = table.Column<int>(type: "int", nullable: false),
                    CarId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavouriteItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FavouriteItems_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavouriteItems_Favorites_FavouriteId",
                        column: x => x.FavouriteId,
                        principalTable: "Favorites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FavouriteId = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<double>(type: "float", nullable: false),
                    ShippingAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentReceived = table.Column<bool>(type: "bit", nullable: false),
                    PaymentMethod = table.Column<int>(type: "int", nullable: false),
                    FavouriteId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Favorites_FavouriteId",
                        column: x => x.FavouriteId,
                        principalTable: "Favorites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Favorites_FavouriteId1",
                        column: x => x.FavouriteId1,
                        principalTable: "Favorites",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_UserId",
                table: "Favorites",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FavouriteItems_CarId",
                table: "FavouriteItems",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_FavouriteItems_FavouriteId",
                table: "FavouriteItems",
                column: "FavouriteId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_FavouriteId",
                table: "Orders",
                column: "FavouriteId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_FavouriteId1",
                table: "Orders",
                column: "FavouriteId1",
                unique: true,
                filter: "[FavouriteId1] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FavouriteItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Favorites");
        }
    }
}
