using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCartTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cart_Users_UsersId",
                table: "Cart");

            migrationBuilder.DropIndex(
                name: "IX_Cart_UsersId",
                table: "Cart");

            migrationBuilder.DropColumn(
                name: "UsersId",
                table: "Cart");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Cart",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Cart_UserId",
                table: "Cart",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_Users_UserId",
                table: "Cart",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cart_Users_UserId",
                table: "Cart");

            migrationBuilder.DropIndex(
                name: "IX_Cart_UserId",
                table: "Cart");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Cart");

            migrationBuilder.AddColumn<int>(
                name: "UsersId",
                table: "Cart",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cart_UsersId",
                table: "Cart",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_Users_UsersId",
                table: "Cart",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
