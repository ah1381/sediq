using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Example.Domain.Migrations
{
    /// <inheritdoc />
    public partial class Ah03 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Sediqs_SediqEntityRowId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Sediqs_sediqRowId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_SediqEntityRowId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "SediqEntityRowId",
                table: "Students");

            migrationBuilder.AlterColumn<string>(
                name: "RandId",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "RandId",
                table: "Sediqs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "RandId",
                table: "ScoreForms",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "RandId",
                table: "Programs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "RandId",
                table: "PhoneNumbers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "RandId",
                table: "Images",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "RandId",
                table: "DurationDateEntitys",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "RandId",
                table: "ActivityForms",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Sediqs_SediqCode",
                table: "Sediqs",
                column: "SediqCode");

            migrationBuilder.CreateIndex(
                name: "IX_Sediqs_SediqCode",
                table: "Sediqs",
                column: "SediqCode",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Sediqs_sediqRowId",
                table: "Students",
                column: "sediqRowId",
                principalTable: "Sediqs",
                principalColumn: "SediqCode",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Sediqs_sediqRowId",
                table: "Students");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Sediqs_SediqCode",
                table: "Sediqs");

            migrationBuilder.DropIndex(
                name: "IX_Sediqs_SediqCode",
                table: "Sediqs");

            migrationBuilder.AlterColumn<string>(
                name: "RandId",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "SediqEntityRowId",
                table: "Students",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RandId",
                table: "Sediqs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RandId",
                table: "ScoreForms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RandId",
                table: "Programs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RandId",
                table: "PhoneNumbers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RandId",
                table: "Images",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RandId",
                table: "DurationDateEntitys",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RandId",
                table: "ActivityForms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_SediqEntityRowId",
                table: "Students",
                column: "SediqEntityRowId");

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
    }
}
