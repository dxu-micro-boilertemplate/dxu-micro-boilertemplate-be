using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Models;

public partial class DxudevContext : DbContext
{
    public DxudevContext()
    {
    }

    public DxudevContext(DbContextOptions<DxudevContext> options)
        : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("Server=133.167.95.70;Port=3306;Database=dxudev;User=dxudev;Password=dxudev");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("user");

            entity.HasIndex(e => e.Id, "User_pk2").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(5000);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
