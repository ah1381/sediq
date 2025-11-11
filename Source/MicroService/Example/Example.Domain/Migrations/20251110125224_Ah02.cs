using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Example.Domain.Migrations
{
    /// <inheritdoc />
    public partial class Ah02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_sediqs_SediqEntityRowId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_sediqs_sediqRowId",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_sediqs",
                table: "sediqs");

            migrationBuilder.RenameTable(
                name: "sediqs",
                newName: "Sediqs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sediqs",
                table: "Sediqs",
                column: "RowId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Sediqs_SediqEntityRowId",
                table: "Students",
                column: "SediqEntityRowId",
                principalTable: "Sediqs",
                principalColumn: "RowId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Sediqs_sediqRowId",
                table: "Students",
                column: "sediqRowId",
                principalTable: "Sediqs",
                principalColumn: "RowId",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Sediqs_SediqEntityRowId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Sediqs_sediqRowId",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sediqs",
                table: "Sediqs");

            migrationBuilder.RenameTable(
                name: "Sediqs",
                newName: "sediqs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_sediqs",
                table: "sediqs",
                column: "RowId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_sediqs_SediqEntityRowId",
                table: "Students",
                column: "SediqEntityRowId",
                principalTable: "sediqs",
                principalColumn: "RowId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_sediqs_sediqRowId",
                table: "Students",
                column: "sediqRowId",
                principalTable: "sediqs",
                principalColumn: "RowId",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
