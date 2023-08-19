using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using RoyexTechApplication.Data.Entity;

namespace RoyexTechApplication.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Employee> Employees { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=server;Initial Catalog=DB;User ID=user;Password=password;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Employee");

            entity.Property(e => e.DteJoiningDate)
                .HasColumnType("date")
                .HasColumnName("dteJoiningDate");
            entity.Property(e => e.IntEmployeeId).HasColumnName("intEmployeeId");
            entity.Property(e => e.IntManagerId).HasColumnName("intManagerId");
            entity.Property(e => e.NumSalary)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("numSalary");
            entity.Property(e => e.StrDesignation).HasColumnName("strDesignation");
            entity.Property(e => e.StrEmployeeName).HasColumnName("strEmployeeName");
            entity.Property(e => e.IsBonusAdded)
                    .IsRequired()
                    .HasColumnName("IsBonusAdded")
                    .HasDefaultValueSql("((0))");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
