using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using UHP.Domain.Models.Drugs;
using UHP.Domain.Models.Public;
using UHP.Domain.Models.Users;

#nullable disable

namespace UHP.Persistence
{
    public partial class UhpContext : DbContext
    {
        public UhpContext()
        {
        }

        public UhpContext(DbContextOptions<UhpContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Drug> Drugs { get; set; }
        public virtual DbSet<DrugByDrugInteraction> DrugByDrugInteractions { get; set; }
        public virtual DbSet<DrugProduct> DrugProducts { get; set; }
        public virtual DbSet<Prescription> Prescriptions { get; set; }
        public virtual DbSet<PrescriptionByDrugProduct> PrescriptionByDrugProducts { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=uhp;Username=postgres;Password=123456");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "en_US.utf8");

            modelBuilder.Entity<Drug>(entity =>
            {
                entity.ToTable("Drug", "drugs");

                entity.HasIndex(e => e.Id, "\"drug\"_\"id\"_uindex")
                    .IsUnique();

                entity.HasIndex(e => e.DrugBankId, "drug_drugbankid_uindex")
                    .IsUnique();

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()");
            });

            modelBuilder.Entity<DrugByDrugInteraction>(entity =>
            {
                entity.ToTable("DrugByDrugInteraction", "drugs");

                entity.HasIndex(e => e.Id, "drugbydruginteraction_\"id\"_uindex")
                    .IsUnique();

                entity.HasIndex(e => e.FirstDrugId, "drugbydruginteraction_firstdrugid_index");

                entity.HasIndex(e => e.SecondDrugId, "drugbydruginteraction_seconddrugid_index");

                entity.HasOne(d => d.FirstDrug)
                    .WithMany(p => p.DrugByDrugInteractionFirstDrugs)
                    .HasForeignKey(d => d.FirstDrugId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("drugbydruginteraction_drug_id_fk");

                entity.HasOne(d => d.SecondDrug)
                    .WithMany(p => p.DrugByDrugInteractionSecondDrugs)
                    .HasForeignKey(d => d.SecondDrugId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("drugbydruginteraction_drug_id_fk_2");
            });

            modelBuilder.Entity<DrugProduct>(entity =>
            {
                entity.ToTable("DrugProduct", "drugs");

                entity.HasIndex(e => e.Id, "drugproduct_\"id\"_uindex")
                    .IsUnique();

                entity.HasIndex(e => e.DrugId, "drugproduct_drugid_index");

                entity.HasOne(d => d.Drug)
                    .WithMany(p => p.DrugProducts)
                    .HasForeignKey(d => d.DrugId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("drugproduct_drug_id_fk");
            });

            modelBuilder.Entity<Prescription>(entity =>
            {
                entity.ToTable("Prescription");

                entity.HasIndex(e => e.Id, "\"prescription\"_\"id\"_uindex")
                    .IsUnique();

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.PrescriptionDoctors)
                    .HasForeignKey(d => d.DoctorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("\"prescription\"_user_id_fk");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.PrescriptionPatients)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("\"prescription\"_user_id_fk_2");
            });

            modelBuilder.Entity<PrescriptionByDrugProduct>(entity =>
            {
                entity.ToTable("PrescriptionByDrugProduct");

                entity.HasIndex(e => e.Id, "\"prescriptionbydrugproduct\"_\"id\"_uindex")
                    .IsUnique();

                entity.HasOne(d => d.DrugProduct)
                    .WithMany(p => p.PrescriptionByDrugProducts)
                    .HasForeignKey(d => d.DrugProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("\"prescriptionbydrugproduct\"_drugproduct_id_fk");

                entity.HasOne(d => d.Prescription)
                    .WithMany(p => p.PrescriptionByDrugProducts)
                    .HasForeignKey(d => d.PrescriptionId)
                    .HasConstraintName("\"prescriptionbydrugproduct\"_prescription_id_fk");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role", "users");

                entity.HasIndex(e => e.Id, "role_\"id\"_uindex")
                    .IsUnique();

                entity.HasIndex(e => e.Name, "role_\"name\"_uindex")
                    .IsUnique();

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User", "users");

                entity.HasIndex(e => e.Email, "user_\"email\"_uindex")
                    .IsUnique();

                entity.HasIndex(e => e.Id, "user_\"id\"_uindex")
                    .IsUnique();

                entity.Property(e => e.Email).IsRequired();

                entity.Property(e => e.Firstname).IsRequired();

                entity.Property(e => e.Lastname).IsRequired();

                entity.Property(e => e.Password).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_role_id_fk");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
