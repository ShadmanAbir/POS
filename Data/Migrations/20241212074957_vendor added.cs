using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UtopiaCatering.Data.Migrations
{
    /// <inheritdoc />
    public partial class vendoradded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrder_Organization_OrganizationID",
                table: "PurchaseOrder");

            migrationBuilder.RenameColumn(
                name: "OrganizationID",
                table: "PurchaseOrder",
                newName: "VendorID");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseOrder_OrganizationID",
                table: "PurchaseOrder",
                newName: "IX_PurchaseOrder_VendorID");

            migrationBuilder.CreateTable(
                name: "Vendor",
                columns: table => new
                {
                    VendorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    vendorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendor", x => x.VendorID);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrder_Vendor_VendorID",
                table: "PurchaseOrder",
                column: "VendorID",
                principalTable: "Vendor",
                principalColumn: "VendorID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrder_Vendor_VendorID",
                table: "PurchaseOrder");

            migrationBuilder.DropTable(
                name: "Vendor");

            migrationBuilder.RenameColumn(
                name: "VendorID",
                table: "PurchaseOrder",
                newName: "OrganizationID");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseOrder_VendorID",
                table: "PurchaseOrder",
                newName: "IX_PurchaseOrder_OrganizationID");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrder_Organization_OrganizationID",
                table: "PurchaseOrder",
                column: "OrganizationID",
                principalTable: "Organization",
                principalColumn: "OrganizationID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
