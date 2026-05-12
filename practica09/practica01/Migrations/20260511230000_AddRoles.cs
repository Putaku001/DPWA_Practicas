using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace practica01.Migrations
{
    /// <inheritdoc />
    public partial class AddRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RoleModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleModel", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "RoleModel",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Administrador del sistema", "admin" },
                    { 2, "Usuario general", "user" }
                });

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "UserModel",
                type: "int",
                nullable: false,
                defaultValue: 2);

            migrationBuilder.CreateIndex(
                name: "IX_UserModel_RoleId",
                table: "UserModel",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserModel_RoleModel_RoleId",
                table: "UserModel",
                column: "RoleId",
                principalTable: "RoleModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserModel_RoleModel_RoleId",
                table: "UserModel");

            migrationBuilder.DropIndex(
                name: "IX_UserModel_RoleId",
                table: "UserModel");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "UserModel");

            migrationBuilder.DropTable(
                name: "RoleModel");
        }
    }
}
