using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using EntitiesLayer.Entities;

namespace DataAccessLayer
{
    ///<remarks>Darijo Bracic Ivan Juras Matej Desanic </remarks>


    public partial class ManageIT : DbContext
    {
        public ManageIT()
            : base("name=ManageITModel")
        {
        }

        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<ClientType> ClientTypes { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Receipt> Receipts { get; set; }
        public virtual DbSet<Worker> Workers { get; set; }
        public virtual DbSet<WorkerType> WorkerTypes { get; set; }
        public virtual DbSet<WorkOrder> WorkOrders { get; set; }
        public virtual DbSet<WorkType> WorkTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.Number)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.CompanyName)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.IBAN)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.Client_Address)
                .IsUnicode(false);

            modelBuilder.Entity<ClientType>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<OrderDetail>()
                .Property(e => e.Location)
                .IsUnicode(false);

            modelBuilder.Entity<OrderDetail>()
                .HasMany(e => e.WorkOrders)
                .WithRequired(e => e.OrderDetail)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Receipt>()
                .Property(e => e.Additional_info)
                .IsUnicode(false);

            modelBuilder.Entity<Worker>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Worker>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<Worker>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Worker>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Worker>()
                .Property(e => e.Gender)
                .IsUnicode(false);

            modelBuilder.Entity<Worker>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<Worker>()
                .HasMany(e => e.WorkOrders)
                .WithRequired(e => e.Worker)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Worker>()
                .HasMany(e => e.Worker1)
                .WithMany(e => e.Workers)
                .Map(m => m.ToTable("Work").MapLeftKey("Id_Order_Details").MapRightKey("ID_Worker"));

            modelBuilder.Entity<WorkType>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<WorkType>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);
        }
    }
}
