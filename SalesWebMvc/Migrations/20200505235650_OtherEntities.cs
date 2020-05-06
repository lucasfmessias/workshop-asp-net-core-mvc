using Microsoft.EntityFrameworkCore.Migrations;

namespace SalesWebMvc.Migrations
{
    public partial class OtherEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesRecord_Sallers_SellerId",
                table: "SalesRecord");

            migrationBuilder.DropForeignKey(
                name: "FK_Sallers_Department_DepartmentId",
                table: "Sallers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sallers",
                table: "Sallers");

            migrationBuilder.RenameTable(
                name: "Sallers",
                newName: "Seller");

            migrationBuilder.RenameIndex(
                name: "IX_Sallers_DepartmentId",
                table: "Seller",
                newName: "IX_Seller_DepartmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Seller",
                table: "Seller",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesRecord_Seller_SellerId",
                table: "SalesRecord",
                column: "SellerId",
                principalTable: "Seller",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Seller_Department_DepartmentId",
                table: "Seller",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesRecord_Seller_SellerId",
                table: "SalesRecord");

            migrationBuilder.DropForeignKey(
                name: "FK_Seller_Department_DepartmentId",
                table: "Seller");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Seller",
                table: "Seller");

            migrationBuilder.RenameTable(
                name: "Seller",
                newName: "Sallers");

            migrationBuilder.RenameIndex(
                name: "IX_Seller_DepartmentId",
                table: "Sallers",
                newName: "IX_Sallers_DepartmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sallers",
                table: "Sallers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesRecord_Sallers_SellerId",
                table: "SalesRecord",
                column: "SellerId",
                principalTable: "Sallers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sallers_Department_DepartmentId",
                table: "Sallers",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
