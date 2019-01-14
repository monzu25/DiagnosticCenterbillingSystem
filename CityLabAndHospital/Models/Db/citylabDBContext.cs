using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using CityLabAndHospital.Models.Db;

namespace CityLabAndHospital.Models.Db
{
    public partial class citylabDBContext : DbContext
    {
        public virtual DbSet<CashReceiveDetails> CashReceiveDetails { get; set; }
        public virtual DbSet<CashReceiveMaster> CashReceiveMaster { get; set; }
        public virtual DbSet<Doctors> Doctors { get; set; }
        public virtual DbSet<InvestigationDetails> InvestigationDetails { get; set; }
        public virtual DbSet<InvestigationMaster> InvestigationMaster { get; set; }
        public virtual DbSet<Investigations> Investigations { get; set; }
     

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=citylabDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Doctors>(entity =>
            {
                entity.HasKey(e => e.DoctorId);

                entity.Property(e => e.DoctorId).HasColumnName("DoctorID");

                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DoctorName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Mobile)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Qualification)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<InvestigationDetails>(entity =>
            {
                entity.HasKey(e => new { e.VoucherNo, e.SlNo });

                entity.Property(e => e.VoucherNo)
                    .HasColumnName("Voucher_No")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Amount).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.Commision).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Fee).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.ReportGroup)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SlNo).HasColumnName("SL_No");

                entity.Property(e => e.TestGroup)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TestName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.TestNameNavigation)
                    .WithMany(p => p.InvestigationDetails)
                    .HasForeignKey(d => d.TestName)
                    .HasConstraintName("FK__Investiga__TestN__25869641");
            });

            modelBuilder.Entity<InvestigationMaster>(entity =>
            {
                entity.HasKey(e => e.VoucherNo);
                entity.Property(e => e.VoucherNo)
                    .HasColumnName("Voucher_No")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.Discount).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.DoctorId).HasColumnName("DoctorID");

                entity.Property(e => e.DueAmt).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.Gender)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Guardian)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Mobile)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NetAmt).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.PaidAmt).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.PatientName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SlNo).HasColumnName("SL_No");

                entity.Property(e => e.Total).HasColumnType("decimal(12, 2)");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.InvestigationMaster)
                    .HasForeignKey(d => d.DoctorId)
                    .HasConstraintName("FK__Investiga__Docto__2A4B4B5E");
            });

            modelBuilder.Entity<Investigations>(entity =>
            {
                entity.HasKey(e => e.TestName);

                entity.Property(e => e.TestName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Fee).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.ReportGroup)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SlNo)
                    .HasColumnName("SL_No")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.TestGroup)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });


            //New Table//

            modelBuilder.Entity<CashReceiveDetails>(entity =>
            {
                entity.HasKey(e => e.VoucherNo);

                entity.Property(e => e.VoucherNo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Amount).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.InvestigationId)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.PatientName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CashReceiveMaster>(entity =>
            {
                entity.HasKey(e => e.VoucherNo);

                entity.Property(e => e.VoucherNo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.InvestigationId)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.PatientName)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.TotalAmount).HasColumnType("decimal(12, 2)");
            });
        }
    }
}
