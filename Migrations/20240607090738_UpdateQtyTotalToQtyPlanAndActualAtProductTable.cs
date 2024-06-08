using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace warehouse.Migrations
{
    /// <inheritdoc />
    public partial class UpdateQtyTotalToQtyPlanAndActualAtProductTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "QtyTotal",
                table: "Products",
                newName: "QtyPlan");

            migrationBuilder.AddColumn<int>(
                name: "QtyActual",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QtyActual",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "QtyPlan",
                table: "Products",
                newName: "QtyTotal");
        }
    }
}
