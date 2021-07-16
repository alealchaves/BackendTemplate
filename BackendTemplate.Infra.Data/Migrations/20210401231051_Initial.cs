using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendTemplate.Infra.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(400)", maxLength: 400, nullable: true),
                    Email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Senha = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    Cpf = table.Column<string>(type: "varchar(14)", maxLength: 14, nullable: true),
                    Usuario_Ativo = table.Column<bool>(type: "bit", nullable: false),
                    Hash = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: true),
                    DataInclusao = table.Column<DateTime>(type: "datetime", nullable: true),
                    UsuarioInclusao = table.Column<int>(type: "int", nullable: true),
                    DataUltimaAlteracao = table.Column<DateTime>(type: "datetime", nullable: true),
                    UsuarioUltimaAlteracao = table.Column<int>(type: "int", nullable: true),
                    DataInativacao = table.Column<DateTime>(type: "datetime", nullable: true),
                    UsuarioInativacao = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
