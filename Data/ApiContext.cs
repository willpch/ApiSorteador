using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using apiSorteio.Models;

namespace apiSorteio.Data
{
    public partial class ApiContext : DbContext
    {
        public ApiContext()
        {
        }

        public ApiContext(DbContextOptions<ApiContext> options)
            : base(options)
        {
        }
       
        public virtual DbSet<Cliente> Clientes { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("latin1_swedish_ci")
                .HasCharSet("latin1");


            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("clientes");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.ClienteID).HasColumnName("ClienteID");

                entity.Property(e => e.ClienteNome).HasMaxLength(40);
                entity.Property(e => e.ClienteEmail).HasMaxLength(40);
                entity.Property(e => e.ClienteTelefone).HasMaxLength(11);
                entity.Property(e => e.ClienteCPF).HasMaxLength(11);

              

            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
