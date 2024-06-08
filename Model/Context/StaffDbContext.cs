using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Model.Entities;

namespace Model.Context;

public partial class StaffDbContext : DbContext
{
    public StaffDbContext()
    {
    }

    public StaffDbContext(DbContextOptions<StaffDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Plan> Plans { get; set; }

    public virtual DbSet<Record> Records { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    public virtual DbSet<Worker> Workers { get; set; }

    public virtual DbSet<Workshop> Workshops { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Staff;Username=postgres;Password=zv1488");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Plan>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Plans_pkey");

            entity.Property(e => e.Month).HasColumnType("character varying");
            entity.Property(e => e.Year).HasColumnType("character varying");
        });

        modelBuilder.Entity<Record>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Records_pkey");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.WorkerId).HasColumnName("Worker_id");

            entity.HasOne(d => d.Worker).WithMany()
                .HasForeignKey(d => d.WorkerId)
                .HasConstraintName("Records_Worker_id_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("User_pkey");

            entity.ToTable("User");

            entity.Property(e => e.Login).HasColumnType("character varying");
            entity.Property(e => e.Password).HasColumnType("character varying");

            entity.HasOne(d => d.RoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.Role)
                .HasConstraintName("User_Role_fkey");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("UserRole_pkey");

            entity.ToTable("UserRole");

            entity.Property(e => e.Role).HasColumnType("character varying");
        });

        modelBuilder.Entity<Worker>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Workers_pkey");

            entity.Property(e => e.HireDate).HasColumnName("Hire_date");
            entity.Property(e => e.LastName)
                .HasColumnType("character varying")
                .HasColumnName("Last_name");
            entity.Property(e => e.Name).HasColumnType("character varying");
            entity.Property(e => e.Surname).HasColumnType("character varying");
            entity.Property(e => e.WorkshopId).HasColumnName("Workshop_id");

            entity.HasOne(d => d.Workshop).WithMany(p => p.Workers)
                .HasForeignKey(d => d.WorkshopId)
                .HasConstraintName("Workers_Workshop_id_fkey");
        });

        modelBuilder.Entity<Workshop>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Workshop_pkey");

            entity.ToTable("Workshop");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
