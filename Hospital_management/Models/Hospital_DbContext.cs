using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Hospital_management.Models
{
    public partial class Hospital_DbContext : DbContext
    {
        public Hospital_DbContext()
        {
        }

        public Hospital_DbContext(DbContextOptions<Hospital_DbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Appointment> Appointments { get; set; }
        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-GRHOCPO\\SQLEXPRESS;Database=Hospital_Db;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.ToTable("Appointment");

                entity.Property(e => e.AppointmentDateTime).HasColumnType("datetime");

                entity.Property(e => e.AppointmentState).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.AppointmentDoctor)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.AppointmentDoctorId)
                    .HasConstraintName("FK__Appointme__Appoi__2C3393D0");

                entity.HasOne(d => d.AppointmentUser)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.AppointmentUserId)
                    .HasConstraintName("FK__Appointme__Appoi__2B3F6F97");
            });

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.ToTable("Doctor");

                entity.Property(e => e.DoctorAppointmentCount).HasDefaultValueSql("((0))");

                entity.Property(e => e.DoctorExperience).HasMaxLength(35);

                entity.Property(e => e.DoctorPosition).HasMaxLength(55);

                entity.HasOne(d => d.DoctorUser)
                    .WithMany(p => p.Doctors)
                    .HasForeignKey(d => d.DoctorUserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_doctorUserId");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("Status");

                entity.Property(e => e.StatusName).HasMaxLength(25);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UsersId)
                    .HasName("PK__Users__A349B062C266F4EB");

                entity.Property(e => e.UsersActualName).HasMaxLength(25);

                entity.Property(e => e.UsersName)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.UsersPassword)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.UsersPhone)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.UsersSurname).HasMaxLength(25);

                entity.HasOne(d => d.UsersStatus)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.UsersStatusId)
                    .HasConstraintName("FK__Users__UsersStat__286302EC");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
