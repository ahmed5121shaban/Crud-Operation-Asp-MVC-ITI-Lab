using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Day1.Migrations
{
    /// <inheritdoc />
    public partial class third : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "CartItem",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_CartItem_UserID",
                table: "CartItem",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItem_AspNetUsers_UserID",
                table: "CartItem",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItem_AspNetUsers_UserID",
                table: "CartItem");

            migrationBuilder.DropIndex(
                name: "IX_CartItem_UserID",
                table: "CartItem");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "CartItem");
        }
    }
}
