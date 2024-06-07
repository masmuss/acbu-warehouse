using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace warehouse.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Shippings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingConfirmation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActualShipment = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CargoReady = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ContNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SealNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Destination = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shippings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShippingId = table.Column<int>(type: "int", nullable: false),
                    InvoiceCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoices_Shippings_ShippingId",
                        column: x => x.ShippingId,
                        principalTable: "Shippings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShippingId = table.Column<int>(type: "int", nullable: false),
                    ContainerSize = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContainerQ = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Po = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Packing = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QtyTotal = table.Column<int>(type: "int", nullable: false),
                    NwPack = table.Column<double>(type: "float", nullable: false),
                    NwTotal = table.Column<double>(type: "float", nullable: false),
                    GwPack = table.Column<double>(type: "float", nullable: false),
                    GwTotal = table.Column<double>(type: "float", nullable: false),
                    M3L = table.Column<double>(type: "float", nullable: false),
                    M3Total = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Shippings_ShippingId",
                        column: x => x.ShippingId,
                        principalTable: "Shippings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SalesDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShippingId = table.Column<int>(type: "int", nullable: false),
                    SalesDocumentCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesDocuments_Shippings_ShippingId",
                        column: x => x.ShippingId,
                        principalTable: "Shippings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_ShippingId",
                table: "Invoices",
                column: "ShippingId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ShippingId",
                table: "Products",
                column: "ShippingId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesDocuments_ShippingId",
                table: "SalesDocuments",
                column: "ShippingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "SalesDocuments");

            migrationBuilder.DropTable(
                name: "Shippings");
        }
    }
}
