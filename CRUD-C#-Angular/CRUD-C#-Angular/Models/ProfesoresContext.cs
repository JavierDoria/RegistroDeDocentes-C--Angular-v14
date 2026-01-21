using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CRUD_C__Angular.Models;

public partial class ProfesoresContext : DbContext
{
    public ProfesoresContext()
    {
    }

    public ProfesoresContext(DbContextOptions<ProfesoresContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Docente> Docentes { get; set; }

    public virtual DbSet<Especialidad> Especialidad { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Docente>(entity =>
        {
            entity.HasKey(e => e.IdDocente).HasName("PK__Docentes__B5A15A242D30B5DB");

            entity.HasIndex(e => e.Dni, "UQ__Docentes__C030857568572D68").IsUnique();

            entity.Property(e => e.IdDocente).HasColumnName("Id_Docente");
            entity.Property(e => e.ApellidoMaterno)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ApellidoPaterno)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Correo)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.Dni)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.IdEspecialidad).HasColumnName("Id_Especialidad");
            entity.Property(e => e.NombrePrimer)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.NombreSegundo)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.IdEspecialidadNavigation).WithMany(p => p.Docentes)
                .HasForeignKey(d => d.IdEspecialidad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Docente_Especialidad");
        });

        modelBuilder.Entity<Especialidad>(entity =>
        {
            entity.HasKey(e => e.IdEspecialidad).HasName("PK__Especial__9B7935BF61396FB9");

            entity.ToTable("Especialidad");

            entity.HasIndex(e => e.Nombre, "UQ__Especial__75E3EFCFDC037A4D").IsUnique();

            entity.Property(e => e.IdEspecialidad).HasColumnName("Id_Especialidad");
            entity.Property(e => e.Nombre)
                .HasMaxLength(70)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
