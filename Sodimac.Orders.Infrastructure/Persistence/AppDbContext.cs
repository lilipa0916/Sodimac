using Microsoft.EntityFrameworkCore;
using Sodimac.Orders.Domain.Entities;

namespace Sodimac.Orders.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // DbSets
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<DeliveryRoute> DeliveryRoutes { get; set; }
        public DbSet<DeliveryStatus> DeliveryStatuses { get; set; } // tabla lookup (enum)

        //public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        //=> await base.SaveChangesAsync(cancellationToken);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cliente
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("Clientes");
                entity.HasKey(c => c.Id);
                entity.HasIndex(c => c.Email).IsUnique();
                entity.Property(c => c.Nombre).IsRequired().HasMaxLength(100);
                entity.Property(c => c.Email).IsRequired().HasMaxLength(100);
                entity.Property(c => c.Direccion).HasMaxLength(200);
                entity.Property(c => c.FechaCreacion).HasDefaultValue(DateTime.Now);
                entity.Property(c => c.Activo).HasDefaultValue(true);

                entity.HasMany(c => c.Pedidos)
                      .WithOne(p => p.Cliente)
                      .HasForeignKey(p => p.ClienteId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Pedido
            modelBuilder.Entity<Pedido>(entity =>
            {
                entity.ToTable("Pedidos");
                entity.HasKey(p => p.Id);
                entity.HasIndex(p => p.ClienteId);
                entity.HasIndex(p => p.FechaPedido);

                entity.Property(p => p.FechaEntrega).IsRequired();
                entity.Property(p => p.FechaPedido).IsRequired();
                entity.Property(p => p.MontoTotal).HasColumnType("decimal(18,2)");
                entity.Property(p => p.Observaciones).HasMaxLength(500);

                entity.HasMany(p => p.Productos)
                      .WithOne(i => i.Pedido)
                      .HasForeignKey(i => i.PedidoId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(p => p.RutaEntrega)
                      .WithOne(r => r.Pedido)
                      .HasForeignKey<DeliveryRoute>(r => r.PedidoId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // PedidoItem
            modelBuilder.Entity<Producto>(entity =>
            {
                entity.ToTable("Productos");
                entity.HasKey(i => i.Id);
                entity.HasIndex(i => i.PedidoId);

                entity.Property(i => i.Nombre).IsRequired().HasMaxLength(100);
                entity.Property(i => i.Codigo).IsRequired().HasMaxLength(50);
                entity.Property(i => i.Cantidad).IsRequired();
                entity.Property(i => i.PrecioUnitario).HasColumnType("decimal(18,2)").IsRequired();
                entity.Ignore(i => i.Subtotal); // Computed property
            });

            // DeliveryRoute
            modelBuilder.Entity<DeliveryRoute>(entity =>
            {
                entity.ToTable("DeliveryRoutes");
                entity.HasKey(r => r.Id);
                entity.HasIndex(r => r.PedidoId).IsUnique();
                entity.HasIndex(r => r.DeliveryStatusId);

                entity.Property(r => r.NombreRuta).IsRequired().HasMaxLength(100);
                entity.Property(r => r.FechaAsignacion).IsRequired();
                entity.Property(r => r.Observaciones).HasMaxLength(500);

                entity.HasOne(r => r.Estado)
                      .WithMany(s => s.Rutas)
                      .HasForeignKey(r => r.DeliveryStatusId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // DeliveryStatus 
            modelBuilder.Entity<DeliveryStatus>(entity =>
            {
                entity.ToTable("DeliveryStatus");
                entity.HasKey(s => s.Id);

                entity.Property(s => s.Nombre).IsRequired().HasMaxLength(50);
                entity.Property(s => s.Descripcion).HasMaxLength(200);
            });

            modelBuilder.Entity<DeliveryStatus>().HasData(
               new DeliveryStatus { Id = 1, Nombre = "Pendiente", Descripcion = "Pedido creado, pendiente de asignación", EsEstadoFinal = false },
                new DeliveryStatus { Id = 2, Nombre = "Asignado", Descripcion = "Pedido asignado a ruta de entrega", EsEstadoFinal = false },
                new DeliveryStatus { Id = 3, Nombre = "En Tránsito", Descripcion = "Pedido en camino al cliente", EsEstadoFinal = false },
                new DeliveryStatus { Id = 4, Nombre = "Entregado", Descripcion = "Pedido entregado exitosamente", EsEstadoFinal = true },
                new DeliveryStatus { Id = 5, Nombre = "Cancelado", Descripcion = "Pedido cancelado", EsEstadoFinal = true }

            );

            modelBuilder.Entity<Cliente>().HasData(
               new Cliente { Id = 1,Nombre = "Juan Carlos Pérez",Email = "juan.perez@email.com",Direccion = "Av. Las Condes 1234, Las Condes, Santiago"},
                new Cliente{Id = 2,Nombre = "María González López",Email = "maria.gonzalez@email.com",Direccion = "Calle San Martín 567, Providencia, Santiago"},
                new Cliente{Id = 3,Nombre = "Roberto Silva Martínez",Email = "roberto.silva@email.com",Direccion = "Av. Libertador Bernardo O'Higgins 890, Santiago Centro"},
                new Cliente{Id = 4,Nombre = "Ana Patricia Rodríguez",Email = "ana.rodriguez@email.com",Direccion = "Calle Manuel Montt 321, Ñuñoa, Santiago"},
                new Cliente{Id = 5,Nombre = "Carlos Eduardo Morales",Email = "carlos.morales@email.com",Direccion = "Av. Pedro de Valdivia 654, Vitacura, Santiago"}
            );


         


            base.OnModelCreating(modelBuilder);
        }


    }
}
