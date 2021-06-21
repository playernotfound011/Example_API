using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace FirstAPI.Models.DB
{
    public partial class WEBSITEContext : DbContext
    {
        public WEBSITEContext()
        {
        }

        public WEBSITEContext(DbContextOptions<WEBSITEContext> options)
            : base(options)
        {
        }

        public virtual DbSet<OrdenCompra> OrdenCompras { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<UserW> UserWs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json")
                    .Build();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DB_Connection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<OrdenCompra>(entity =>
            {
                entity.HasKey(e => e.NumPedido)
                    .HasName("PK__ORDEN_CO__5311FF2EF4D788AC");

                entity.ToTable("ORDEN_COMPRA");

                entity.Property(e => e.NumPedido).HasColumnName("NUM_PEDIDO");

                entity.Property(e => e.Direccion)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("DIRECCION");

                entity.Property(e => e.FechaCompra)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_COMPRA");

                entity.Property(e => e.IdUser).HasColumnName("ID_USER");

                entity.Property(e => e.MontoTotal)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("MONTO_TOTAL");

                entity.Property(e => e.Product)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("PRODUCT");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.OrdenCompras)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ORDEN_COM__ID_US__4BAC3F29");

                entity.HasOne(d => d.ProductNavigation)
                    .WithMany(p => p.OrdenCompras)
                    .HasForeignKey(d => d.Product)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ORDEN_COM__PRODU__4CA06362");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.CodigoProd)
                    .HasName("PK__PRODUCT__434F6E27A2E0DD8E");

                entity.ToTable("PRODUCT");

                entity.Property(e => e.CodigoProd)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("CODIGO_PROD");

                entity.Property(e => e.PrecioUnit)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("PRECIO_UNIT");

                entity.Property(e => e.TipoProd)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("TIPO_PROD");
            });

            modelBuilder.Entity<UserW>(entity =>
            {
                entity.HasKey(e => e.IdUser)
                    .HasName("PK__USER_WS__95F4844004649A3D");

                entity.ToTable("USER_WS");

                entity.Property(e => e.IdUser).HasColumnName("ID_USER");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CREATION_DATE");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.TypeUser)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("TYPE_USER");

                entity.Property(e => e.UserPass)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("USER_PASS");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("USERNAME");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
