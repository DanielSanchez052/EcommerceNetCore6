using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations.Store
{
    /// <inheritdoc />
    public partial class ChangeDeliveryMethodsFieldNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Deliverytime",
                table: "DeliveryMethods",
                newName: "DeliveryTime");

            migrationBuilder.RenameColumn(
                name: "Descriotion",
                table: "DeliveryMethods",
                newName: "Description");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Order",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DeliveryTime",
                table: "DeliveryMethods",
                newName: "Deliverytime");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "DeliveryMethods",
                newName: "Descriotion");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Order",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);
        }
    }
}
