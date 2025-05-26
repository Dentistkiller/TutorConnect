using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TutorConnect.Models;

public partial class TutorConnectDbContext : DbContext
{
    public TutorConnectDbContext()
    {
    }

    public TutorConnectDbContext(DbContextOptions<TutorConnectDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Session> Sessions { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Tutor> Tutors { get; set; }

    public virtual DbSet<TutorSession> TutorSessions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=VCKNDNFTLLP19\\SQLEXPRESS;Database=TutorConnectDb;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Session>(entity =>
        {
            entity.HasKey(e => e.SessionId).HasName("PK__Sessions__C9F492908369EFCF");

            entity.Property(e => e.SessionDate).HasColumnType("datetime");

            entity.HasOne(d => d.Student).WithMany(p => p.Sessions)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Sessions__Studen__3D5E1FD2");

            entity.HasOne(d => d.Tutor).WithMany(p => p.Sessions)
                .HasForeignKey(d => d.TutorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Sessions__TutorI__3E52440B");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__Students__32C52B999F57160C");

            entity.HasIndex(e => e.Email, "UQ__Students__A9D1053432A7EA95").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber).HasMaxLength(15);
        });

        modelBuilder.Entity<Tutor>(entity =>
        {
            entity.HasKey(e => e.TutorId).HasName("PK__Tutors__77C70FE20701D9A9");

            entity.HasIndex(e => e.Email, "UQ__Tutors__A9D105349946656D").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Subject).HasMaxLength(50);
        });

        modelBuilder.Entity<TutorSession>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("TutorSession");

            entity.Property(e => e.SessionDate).HasColumnType("datetime");
            entity.Property(e => e.StudentEmail).HasMaxLength(100);
            entity.Property(e => e.StudentName).HasMaxLength(100);
            entity.Property(e => e.StudentPhone).HasMaxLength(15);
            entity.Property(e => e.TutorEmail).HasMaxLength(100);
            entity.Property(e => e.TutorName).HasMaxLength(100);
            entity.Property(e => e.TutorSubject).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
