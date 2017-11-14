namespace SuperZapatos.DA
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SuperZapatosContext : DbContext
    {
        public SuperZapatosContext()
            : base("name=SuperZapatosContext")
        {
        }

        public virtual DbSet<Articles> Articles { get; set; }
        public virtual DbSet<Store> Store { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Articles>()
                .Property(e => e.Name)
                .IsFixedLength();

            modelBuilder.Entity<Articles>()
                .Property(e => e.Description)
                .IsFixedLength();

            modelBuilder.Entity<Articles>()
                .Property(e => e.Price)
                .HasPrecision(18, 4);

            modelBuilder.Entity<Store>()
                .Property(e => e.Name)
                .IsFixedLength();

            modelBuilder.Entity<Store>()
                .Property(e => e.Address)
                .IsFixedLength();

            modelBuilder.Entity<Store>()
                .HasMany(e => e.Articles)
                .WithRequired(e => e.Store)
                .HasForeignKey(e => e.Store_Id)
                .WillCascadeOnDelete(false);
        }
    }
}
