using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Sodimac.Orders.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2025, 8, 24, 18, 52, 47, 720, DateTimeKind.Local).AddTicks(5875)),
                    Activo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    EsEstadoFinal = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClienteId = table.Column<int>(type: "int", nullable: false),
                    FechaPedido = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaEntrega = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MontoTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Observaciones = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pedidos_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryRoutes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PedidoId = table.Column<int>(type: "int", nullable: false),
                    DeliveryStatusId = table.Column<int>(type: "int", nullable: false),
                    NombreRuta = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FechaAsignacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaEntregaReal = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Observaciones = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryRoutes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryRoutes_DeliveryStatus_DeliveryStatusId",
                        column: x => x.DeliveryStatusId,
                        principalTable: "DeliveryStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeliveryRoutes_Pedidos_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "Pedidos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PedidoId = table.Column<int>(type: "int", nullable: false),
                    Codigo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    PrecioUnitario = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Productos_Pedidos_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "Pedidos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Clientes",
                columns: new[] { "Id", "Activo", "Direccion", "Email", "FechaCreacion", "Nombre" },
                values: new object[,]
                {
                    { 1, true, "Av. Las Condes 1234, Las Condes, Santiago", "juan.perez@email.com", new DateTime(2025, 8, 24, 23, 52, 47, 721, DateTimeKind.Utc).AddTicks(8860), "Juan Carlos Pérez" },
                    { 2, true, "Calle San Martín 567, Providencia, Santiago", "maria.gonzalez@email.com", new DateTime(2025, 8, 24, 23, 52, 47, 721, DateTimeKind.Utc).AddTicks(8865), "María González López" },
                    { 3, true, "Av. Libertador Bernardo O'Higgins 890, Santiago Centro", "roberto.silva@email.com", new DateTime(2025, 8, 24, 23, 52, 47, 721, DateTimeKind.Utc).AddTicks(8867), "Roberto Silva Martínez" },
                    { 4, true, "Calle Manuel Montt 321, Ñuñoa, Santiago", "ana.rodriguez@email.com", new DateTime(2025, 8, 24, 23, 52, 47, 721, DateTimeKind.Utc).AddTicks(8870), "Ana Patricia Rodríguez" },
                    { 5, true, "Av. Pedro de Valdivia 654, Vitacura, Santiago", "carlos.morales@email.com", new DateTime(2025, 8, 24, 23, 52, 47, 721, DateTimeKind.Utc).AddTicks(8872), "Carlos Eduardo Morales" }
                });

            migrationBuilder.InsertData(
                table: "DeliveryStatus",
                columns: new[] { "Id", "Descripcion", "EsEstadoFinal", "Nombre" },
                values: new object[,]
                {
                    { 1, "Pedido creado, pendiente de asignación", false, "Pendiente" },
                    { 2, "Pedido asignado a ruta de entrega", false, "Asignado" },
                    { 3, "Pedido en camino al cliente", false, "En Tránsito" },
                    { 4, "Pedido entregado exitosamente", true, "Entregado" },
                    { 5, "Pedido cancelado", true, "Cancelado" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_Email",
                table: "Clientes",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryRoutes_DeliveryStatusId",
                table: "DeliveryRoutes",
                column: "DeliveryStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryRoutes_PedidoId",
                table: "DeliveryRoutes",
                column: "PedidoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_ClienteId",
                table: "Pedidos",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_FechaPedido",
                table: "Pedidos",
                column: "FechaPedido");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_PedidoId",
                table: "Productos",
                column: "PedidoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeliveryRoutes");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "DeliveryStatus");

            migrationBuilder.DropTable(
                name: "Pedidos");

            migrationBuilder.DropTable(
                name: "Clientes");
        }
    }
}
