using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendTemplate.Infra.Data.Migrations
{
    public partial class Edit_5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Historico");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "UsuarioPerfil");

            migrationBuilder.DropColumn(
                name: "DataInativacao",
                table: "UsuarioPerfil");

            migrationBuilder.DropColumn(
                name: "DataInclusao",
                table: "UsuarioPerfil");

            migrationBuilder.DropColumn(
                name: "DataUltimaAlteracao",
                table: "UsuarioPerfil");

            migrationBuilder.DropColumn(
                name: "Hash",
                table: "UsuarioPerfil");

            migrationBuilder.DropColumn(
                name: "UsuarioInativacao",
                table: "UsuarioPerfil");

            migrationBuilder.DropColumn(
                name: "UsuarioInclusao",
                table: "UsuarioPerfil");

            migrationBuilder.DropColumn(
                name: "UsuarioUltimaAlteracao",
                table: "UsuarioPerfil");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "UsuarioPerfil",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataInativacao",
                table: "UsuarioPerfil",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataInclusao",
                table: "UsuarioPerfil",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataUltimaAlteracao",
                table: "UsuarioPerfil",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Hash",
                table: "UsuarioPerfil",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "UsuarioInativacao",
                table: "UsuarioPerfil",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioInclusao",
                table: "UsuarioPerfil",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioUltimaAlteracao",
                table: "UsuarioPerfil",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Historico",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Historico", x => x.Id);
                });
        }
    }
}
