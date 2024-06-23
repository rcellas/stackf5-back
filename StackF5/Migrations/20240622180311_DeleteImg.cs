using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StackF5.Migrations
{
    /// <inheritdoc />
    public partial class DeleteImg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Incidences_IncidenceId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_IncidenceId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "IncidenceId",
                table: "Images");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IncidenceId",
                table: "Images",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_IncidenceId",
                table: "Images",
                column: "IncidenceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Incidences_IncidenceId",
                table: "Images",
                column: "IncidenceId",
                principalTable: "Incidences",
                principalColumn: "Id");
        }
    }
}
