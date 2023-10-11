using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapi.Migrations
{
    /// <inheritdoc />
    public partial class Migration_Update_Data_Context_And_Cart_Structure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_carts_product_categories_product_variant_id",
                table: "carts");

            migrationBuilder.AddForeignKey(
                name: "FK_carts_product_variants_product_variant_id",
                table: "carts",
                column: "product_variant_id",
                principalTable: "product_variants",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_carts_product_variants_product_variant_id",
                table: "carts");

            migrationBuilder.AddForeignKey(
                name: "FK_carts_product_categories_product_variant_id",
                table: "carts",
                column: "product_variant_id",
                principalTable: "product_categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
