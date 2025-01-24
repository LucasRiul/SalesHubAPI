using DesafioBackEnd.Models;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace DesafioBackEnd.Data
{
    public class FrutariaContext : DbContext
    {
        public DbSet<EMPRESAS> EMPRESAS { get; set; }
        public DbSet<FORNECEDORES> FORNECEDORES { get; set; }
        public DbSet<LOTES> LOTES { get; set; }
        public DbSet<USUARIOS> USUARIOS { get; set; }    
        public DbSet<VENDAS> VENDAS { get; set; }
        public DbSet<PRODUTOS> PRODUTOS { get; set; }
        public DbSet<PRODUTO_VENDAS> PRODUTO_VENDAS { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Password=q1w2e3r4;Persist Security Info=true;User ID=QUALY;Initial Catalog=DesafioBackendLucas;Data Source=10.94.137.245\\SQLEXPRESS2017;Trusted_Connection=True;Encrypt=False;multipleactiveresultsets=True;application name=EntityFramework;Integrated Security=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PRODUTO_VENDAS>(e =>
            {
                e.HasKey(x => new { x.idVenda, x.idProduto });
            });

            modelBuilder.Entity<PRODUTO_VENDAS>()
                .HasOne(pv => pv.Produto)
                .WithMany(p => p.PRODUTO_VENDAS)
                .HasForeignKey(pv => pv.idProduto);

            modelBuilder.Entity<PRODUTO_VENDAS>()
                .HasOne(pv => pv.Venda)
                .WithMany(p => p.PRODUTO_VENDAS)
                .HasForeignKey(pv => pv.idVenda);

            modelBuilder.Entity<USUARIOS>()
                .HasOne(x => x.Empresa)
                .WithMany(x => x.USUARIOS)
                .HasForeignKey(x => x.idEmpresa);

            modelBuilder.Entity<LOTES>()
                .HasOne(l => l.FORNECEDORES)
                .WithMany(f => f.LOTES)
                .HasForeignKey(l => l.idFornecedor)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<VENDAS>()
                .HasOne(l => l.Usuario)
                .WithMany(f => f.VENDAS)
                .HasForeignKey(l => l.idUsuario)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
