using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Data.Migrations
{
    /// <inheritdoc />
    public partial class MigrationRebuild : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductsnBaskets_Baskets_BasketId",
                table: "ProductsnBaskets");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsnBaskets_Products_ProductId",
                table: "ProductsnBaskets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductsnBaskets",
                table: "ProductsnBaskets");

            migrationBuilder.RenameTable(
                name: "ProductsnBaskets",
                newName: "ProductInBaskets");

            migrationBuilder.RenameIndex(
                name: "IX_ProductsnBaskets_ProductId",
                table: "ProductInBaskets",
                newName: "IX_ProductInBaskets_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductsnBaskets_BasketId",
                table: "ProductInBaskets",
                newName: "IX_ProductInBaskets_BasketId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductInBaskets",
                table: "ProductInBaskets",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInBaskets_Baskets_BasketId",
                table: "ProductInBaskets",
                column: "BasketId",
                principalTable: "Baskets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInBaskets_Products_ProductId",
                table: "ProductInBaskets",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductInBaskets_Baskets_BasketId",
                table: "ProductInBaskets");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductInBaskets_Products_ProductId",
                table: "ProductInBaskets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductInBaskets",
                table: "ProductInBaskets");

            migrationBuilder.RenameTable(
                name: "ProductInBaskets",
                newName: "ProductsnBaskets");

            migrationBuilder.RenameIndex(
                name: "IX_ProductInBaskets_ProductId",
                table: "ProductsnBaskets",
                newName: "IX_ProductsnBaskets_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductInBaskets_BasketId",
                table: "ProductsnBaskets",
                newName: "IX_ProductsnBaskets_BasketId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductsnBaskets",
                table: "ProductsnBaskets",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsnBaskets_Baskets_BasketId",
                table: "ProductsnBaskets",
                column: "BasketId",
                principalTable: "Baskets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsnBaskets_Products_ProductId",
                table: "ProductsnBaskets",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
