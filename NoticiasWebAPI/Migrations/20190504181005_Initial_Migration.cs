using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NoticiasWebAPI.Migrations
{
    public partial class Initial_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Autor",
                columns: table => new
                {
                    AutorID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NOMBRE = table.Column<string>(maxLength: 50, nullable: true),
                    APELLIDO = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Autor", x => x.AutorID);
                });

            migrationBuilder.CreateTable(
                name: "Noticia",
                columns: table => new
                {
                    NoticiaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TITULO = table.Column<string>(maxLength: 50, nullable: true),
                    DESCRIPCIÓN = table.Column<string>(maxLength: 100, nullable: true),
                    CONTENIDO = table.Column<string>(maxLength: 2147483647, nullable: true),
                    FECHA = table.Column<DateTime>(type: "Datetime", nullable: false),
                    AUTOR_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Noticia", x => x.NoticiaID);
                    table.ForeignKey(
                        name: "FK_Noticia_Autor_AUTOR_ID",
                        column: x => x.AUTOR_ID,
                        principalTable: "Autor",
                        principalColumn: "AutorID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Noticia_AUTOR_ID",
                table: "Noticia",
                column: "AUTOR_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Noticia");

            migrationBuilder.DropTable(
                name: "Autor");
        }
    }
}
