using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapi.Migrations
{
    /// <inheritdoc />
    public partial class Migration_Added_PK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "product_id",
                table: "carts");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "user_addresses",
                newName: "userid");

            migrationBuilder.RenameColumn(
                name: "product_category_id",
                table: "products",
                newName: "product_categoryid");

            migrationBuilder.RenameColumn(
                name: "product_id",
                table: "product_variants",
                newName: "productid");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "orders",
                newName: "usersid");

            migrationBuilder.RenameColumn(
                name: "product_variant_id",
                table: "order_items",
                newName: "product_variantid");

            migrationBuilder.RenameColumn(
                name: "order_id",
                table: "order_items",
                newName: "orderid");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "carts",
                newName: "userid");

            migrationBuilder.RenameColumn(
                name: "product_variant_id",
                table: "carts",
                newName: "product_variantid");

            migrationBuilder.CreateIndex(
                name: "IX_user_addresses_userid",
                table: "user_addresses",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "IX_products_product_categoryid",
                table: "products",
                column: "product_categoryid");

            migrationBuilder.CreateIndex(
                name: "IX_product_variants_productid",
                table: "product_variants",
                column: "productid");

            migrationBuilder.CreateIndex(
                name: "IX_orders_usersid",
                table: "orders",
                column: "usersid");

            migrationBuilder.CreateIndex(
                name: "IX_order_items_orderid",
                table: "order_items",
                column: "orderid");

            migrationBuilder.CreateIndex(
                name: "IX_order_items_product_variantid",
                table: "order_items",
                column: "product_variantid");

            migrationBuilder.CreateIndex(
                name: "IX_carts_product_variantid",
                table: "carts",
                column: "product_variantid");

            migrationBuilder.CreateIndex(
                name: "IX_carts_userid",
                table: "carts",
                column: "userid");

            migrationBuilder.AddForeignKey(
                name: "FK_carts_product_categories_product_variantid",
                table: "carts",
                column: "product_variantid",
                principalTable: "product_categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_carts_users_userid",
                table: "carts",
                column: "userid",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_order_items_orders_orderid",
                table: "order_items",
                column: "orderid",
                principalTable: "orders",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_order_items_product_variants_product_variantid",
                table: "order_items",
                column: "product_variantid",
                principalTable: "product_variants",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_orders_users_usersid",
                table: "orders",
                column: "usersid",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_product_variants_products_productid",
                table: "product_variants",
                column: "productid",
                principalTable: "products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_products_product_categories_product_categoryid",
                table: "products",
                column: "product_categoryid",
                principalTable: "product_categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_addresses_users_userid",
                table: "user_addresses",
                column: "userid",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_carts_product_categories_product_variantid",
                table: "carts");

            migrationBuilder.DropForeignKey(
                name: "FK_carts_users_userid",
                table: "carts");

            migrationBuilder.DropForeignKey(
                name: "FK_order_items_orders_orderid",
                table: "order_items");

            migrationBuilder.DropForeignKey(
                name: "FK_order_items_product_variants_product_variantid",
                table: "order_items");

            migrationBuilder.DropForeignKey(
                name: "FK_orders_users_usersid",
                table: "orders");

            migrationBuilder.DropForeignKey(
                name: "FK_product_variants_products_productid",
                table: "product_variants");

            migrationBuilder.DropForeignKey(
                name: "FK_products_product_categories_product_categoryid",
                table: "products");

            migrationBuilder.DropForeignKey(
                name: "FK_user_addresses_users_userid",
                table: "user_addresses");

            migrationBuilder.DropIndex(
                name: "IX_user_addresses_userid",
                table: "user_addresses");

            migrationBuilder.DropIndex(
                name: "IX_products_product_categoryid",
                table: "products");

            migrationBuilder.DropIndex(
                name: "IX_product_variants_productid",
                table: "product_variants");

            migrationBuilder.DropIndex(
                name: "IX_orders_usersid",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "IX_order_items_orderid",
                table: "order_items");

            migrationBuilder.DropIndex(
                name: "IX_order_items_product_variantid",
                table: "order_items");

            migrationBuilder.DropIndex(
                name: "IX_carts_product_variantid",
                table: "carts");

            migrationBuilder.DropIndex(
                name: "IX_carts_userid",
                table: "carts");

            migrationBuilder.RenameColumn(
                name: "userid",
                table: "user_addresses",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "product_categoryid",
                table: "products",
                newName: "product_category_id");

            migrationBuilder.RenameColumn(
                name: "productid",
                table: "product_variants",
                newName: "product_id");

            migrationBuilder.RenameColumn(
                name: "usersid",
                table: "orders",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "product_variantid",
                table: "order_items",
                newName: "product_variant_id");

            migrationBuilder.RenameColumn(
                name: "orderid",
                table: "order_items",
                newName: "order_id");

            migrationBuilder.RenameColumn(
                name: "userid",
                table: "carts",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "product_variantid",
                table: "carts",
                newName: "product_variant_id");

            migrationBuilder.AddColumn<int>(
                name: "product_id",
                table: "carts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
