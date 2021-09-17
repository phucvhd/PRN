using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace BookManageLibrary.DataAccess
{
    public partial class BookManagementDBContext : DbContext
    {
        public BookManagementDBContext()
        {
        }

        public BookManagementDBContext(DbContextOptions<BookManagementDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Age> TblAges { get; set; }
        public virtual DbSet<Category> TblCategories { get; set; }
        public virtual DbSet<Product> TblProducts { get; set; }
        public virtual DbSet<Supplier> TblSuppliers { get; set; }
        public virtual DbSet<SuppliersReport> TblSuppliersReports { get; set; }
        public virtual DbSet<User> TblUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server=LAPTOP-5PP1NT2P\\SQLEXPRESS;database=BookManagementDB;uid=phucvhd;pwd=19091997;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Age>(entity =>
            {
                entity.HasKey(e => e.ForAgesId);

                entity.ToTable("tblAges");

                entity.Property(e => e.ForAgesId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("forAgesID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("description");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.CategoryId);

                entity.ToTable("tblCategories");

                entity.Property(e => e.CategoryId)
                    .HasMaxLength(10)
                    .HasColumnName("categoryID");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("categoryName");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.ProductId);

                entity.ToTable("tblProducts");

                entity.Property(e => e.ProductId)
                    .HasMaxLength(10)
                    .HasColumnName("productID")
                    .IsFixedLength(true);

                entity.Property(e => e.Author)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("author");

                entity.Property(e => e.CategoryId)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("categoryID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdDate");

                entity.Property(e => e.Description)
                    .HasMaxLength(3000)
                    .HasColumnName("description");

                entity.Property(e => e.ForAgesId)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("forAgesID");

                entity.Property(e => e.Image)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("image");

                entity.Property(e => e.Isbn10)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("isbn10")
                    .IsFixedLength(true);

                entity.Property(e => e.Isbn13)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("isbn13")
                    .IsFixedLength(true);

                entity.Property(e => e.LastModified)
                    .HasColumnType("datetime")
                    .HasColumnName("lastModified");

                entity.Property(e => e.Price)
                    .HasColumnType("money")
                    .HasColumnName("price");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("productName");

                entity.Property(e => e.Publisher)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("publisher")
                    .IsFixedLength(true);

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.TblProducts)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblProducts_tblCategories");

                entity.HasOne(d => d.ForAges)
                    .WithMany(p => p.TblProducts)
                    .HasForeignKey(d => d.ForAgesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblProducts_tblAges");
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.HasKey(e => e.SupplierId);

                entity.ToTable("tblSuppliers");

                entity.Property(e => e.SupplierId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("supplierID");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("address");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.PhoneNum)
                    .IsRequired()
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("phoneNum");

                entity.Property(e => e.SupplierName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("supplierName");
            });

            modelBuilder.Entity<SuppliersReport>(entity =>
            {
                entity.ToTable("tblSuppliersReport");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.ProductId)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("productID")
                    .IsFixedLength(true);

                entity.Property(e => e.ReceiverEmail)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("receiverEmail");

                entity.Property(e => e.SuppilerId)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("suppilerID");

                entity.Property(e => e.SupplyDate)
                    .HasColumnType("datetime")
                    .HasColumnName("supplyDate");

                entity.Property(e => e.SupplyQuantity).HasColumnName("supplyQuantity");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.TblSuppliersReports)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblSuppliersReport_tblProducts");

                entity.HasOne(d => d.ReceiverEmailNavigation)
                    .WithMany(p => p.TblSuppliersReports)
                    .HasForeignKey(d => d.ReceiverEmail)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblSuppliersReport_tblUsers");

                entity.HasOne(d => d.Suppiler)
                    .WithMany(p => p.TblSuppliersReports)
                    .HasForeignKey(d => d.SuppilerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblSuppliersReport_tblSuppliers");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Email);

                entity.ToTable("tblUsers");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("fullName");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.PhoneNum)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("phoneNum")
                    .IsFixedLength(true);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
