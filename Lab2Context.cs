using Microsoft.EntityFrameworkCore;

namespace Persistance.Models;

public partial class Lab2Context : DbContext
{
    public Lab2Context()
    {
    }

    public Lab2Context(DbContextOptions<Lab2Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<Medicine> Medicines { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<Pharmacy> Pharmacies { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("User ID=postgres;Password=pass12345;Host=localhost;Port=5433;Database=Lab2;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.CompanyId).HasName("companies_pkey");

            entity.ToTable("companies");

            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.CompanyName)
                .HasColumnType("character varying")
                .HasColumnName("company_name");
            entity.Property(e => e.Country)
                .HasColumnType("character varying")
                .HasColumnName("country");
            entity.Property(e => e.Owner)
                .HasColumnType("character varying")
                .HasColumnName("owner");
        });

        modelBuilder.Entity<Medicine>(entity =>
        {
            entity.HasKey(e => e.MedicineId).HasName("medicines_pkey");

            entity.ToTable("medicines");

            entity.Property(e => e.MedicineId).HasColumnName("medicine_id");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.MedicineName)
                .HasColumnType("character varying")
                .HasColumnName("medicine_name");

            entity.HasOne(d => d.Company).WithMany(p => p.Medicines)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("medicines_company_id_fkey");

            entity.HasMany(d => d.Pharmacies).WithMany(p => p.Medicines)
                .UsingEntity<Dictionary<string, object>>(
                    "MedicinePharmacy",
                    r => r.HasOne<Pharmacy>().WithMany()
                        .HasForeignKey("PharmacyId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("medicine_pharmacies_pharmacy_id_fkey"),
                    l => l.HasOne<Medicine>().WithMany()
                        .HasForeignKey("MedicineId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("medicine_pharmacies_medicine_id_fkey"),
                    j =>
                    {
                        j.HasKey("MedicineId", "PharmacyId").HasName("medicine_pharmacies_pkey");
                        j.ToTable("medicine_pharmacies");
                        j.IndexerProperty<long>("MedicineId").HasColumnName("medicine_id");
                        j.IndexerProperty<long>("PharmacyId").HasColumnName("pharmacy_id");
                    });
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.PatientId).HasName("patients_pkey");

            entity.ToTable("patients");

            entity.Property(e => e.PatientId).HasColumnName("patient_id");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.Fullname)
                .HasColumnType("character varying")
                .HasColumnName("fullname");
        });

        modelBuilder.Entity<Pharmacy>(entity =>
        {
            entity.HasKey(e => e.PharmacyId).HasName("pharmacies_pkey");

            entity.ToTable("pharmacies");

            entity.Property(e => e.PharmacyId).HasColumnName("pharmacy_id");
            entity.Property(e => e.City)
                .HasColumnType("character varying")
                .HasColumnName("city");
            entity.Property(e => e.PharmacyName)
                .HasColumnType("character varying")
                .HasColumnName("pharmacy_name");

            entity.HasMany(d => d.Patients).WithMany(p => p.Pharmacies)
                .UsingEntity<Dictionary<string, object>>(
                    "PharmacyPatient",
                    r => r.HasOne<Patient>().WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("pharmacy_patients_patient_id_fkey"),
                    l => l.HasOne<Pharmacy>().WithMany()
                        .HasForeignKey("PharmacyId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("pharmacy_patients_pharmacy_id_fkey"),
                    j =>
                    {
                        j.HasKey("PharmacyId", "PatientId").HasName("pharmacy_patients_pkey");
                        j.ToTable("pharmacy_patients");
                        j.IndexerProperty<long>("PharmacyId").HasColumnName("pharmacy_id");
                        j.IndexerProperty<long>("PatientId").HasColumnName("patient_id");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
