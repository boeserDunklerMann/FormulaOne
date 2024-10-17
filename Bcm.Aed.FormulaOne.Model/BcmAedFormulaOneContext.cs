using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Bcm.Aed.FormulaOne.Model;

public partial class BcmAedFormulaOneContext : DbContext
{
    public BcmAedFormulaOneContext()
    {
    }

    public BcmAedFormulaOneContext(DbContextOptions<BcmAedFormulaOneContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Driver> Drivers { get; set; }

    public virtual DbSet<Race> Races { get; set; }

    public virtual DbSet<RaceResult> RaceResults { get; set; }

    public virtual DbSet<RaceResultType> RaceResultTypes { get; set; }

    public virtual DbSet<RaceType> RaceTypes { get; set; }

    public virtual DbSet<Racetrack> Racetracks { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<Vehicle> Vehicles { get; set; }

	// TODO: Check this: https://learn.microsoft.com/en-us/ef/core/miscellaneous/connection-strings
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(local)\\SQLEXPRESS;Initial Catalog=Bcm.Aed.FormulaOne;Persist Security Info=True;User ID=FormulaOne;Password=f1.0815;Encrypt=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Country>(entity =>
        {
            entity.ToTable("Country", "bcm");

            entity.Property(e => e.CountryId).HasColumnName("CountryID");
            entity.Property(e => e.CountryName).HasMaxLength(100);
        });

        modelBuilder.Entity<Driver>(entity =>
        {
            entity.ToTable("Driver", "bcm");

            entity.Property(e => e.DriverId).HasColumnName("DriverID");
            entity.Property(e => e.DriverCountryId).HasColumnName("DriverCountryID");
            entity.Property(e => e.DriverName).HasMaxLength(200);
            entity.Property(e => e.DriverTeamId).HasColumnName("DriverTeamID");
            entity.Property(e => e.DriverVehicleId).HasColumnName("DriverVehicleID");

            entity.HasOne(d => d.DriverCountry).WithMany(p => p.Drivers)
                .HasForeignKey(d => d.DriverCountryId)
                .HasConstraintName("FK_Driver_Country");

            entity.HasOne(d => d.DriverTeam).WithMany(p => p.Drivers)
                .HasForeignKey(d => d.DriverTeamId)
                .HasConstraintName("FK_Driver_Team");

            entity.HasOne(d => d.DriverVehicle).WithMany(p => p.Drivers)
                .HasForeignKey(d => d.DriverVehicleId)
                .HasConstraintName("FK_Driver_Vehicle");
        });

        modelBuilder.Entity<Race>(entity =>
        {
            entity.ToTable("Race", "bcm");

            entity.Property(e => e.RaceId).HasColumnName("RaceID");
            entity.Property(e => e.DriverId).HasColumnName("DriverID");
            entity.Property(e => e.RaceDate).HasColumnType("datetime");
            entity.Property(e => e.RaceTypeId).HasColumnName("RaceTypeID");
            entity.Property(e => e.RacetrackId).HasColumnName("RacetrackID");

            entity.HasOne(d => d.Driver).WithMany(p => p.Races)
                .HasForeignKey(d => d.DriverId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Race_Driver");

            entity.HasOne(d => d.RaceType).WithMany(p => p.Races)
                .HasForeignKey(d => d.RaceTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Race_RaceType");

            entity.HasOne(d => d.Racetrack).WithMany(p => p.Races)
                .HasForeignKey(d => d.RacetrackId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Race_Racetrack");
        });

        modelBuilder.Entity<RaceResult>(entity =>
        {
            entity.HasKey(e => e.RaceId);

            entity.ToTable("RaceResult", "bcm");

            entity.Property(e => e.RaceId)
                .ValueGeneratedNever()
                .HasColumnName("RaceID");
            entity.Property(e => e.DistanceKm)
                .HasColumnType("decimal(4, 2)")
                .HasColumnName("DistanceKM");
            entity.Property(e => e.DurationMs).HasColumnName("DurationMS");
            entity.Property(e => e.RaceResultTypeId).HasColumnName("RaceResultTypeID");

            entity.HasOne(d => d.Race).WithOne(p => p.RaceResult)
                .HasForeignKey<RaceResult>(d => d.RaceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RaceResult_Race");

            entity.HasOne(d => d.RaceResultType).WithMany(p => p.RaceResults)
                .HasForeignKey(d => d.RaceResultTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RaceResult_RaceResultType");
        });

        modelBuilder.Entity<RaceResultType>(entity =>
        {
            entity.ToTable("RaceResultType", "bcm");

            entity.Property(e => e.RaceResultTypeId).HasColumnName("RaceResultTypeID");
            entity.Property(e => e.RaceResultShort)
                .HasMaxLength(5)
                .IsUnicode(false);
        });

        modelBuilder.Entity<RaceType>(entity =>
        {
            entity.ToTable("RaceType", "bcm");

            entity.Property(e => e.RaceTypeId).HasColumnName("RaceTypeID");
            entity.Property(e => e.RaceTypeName).HasMaxLength(50);
            entity.Property(e => e.RaceTypeShort)
                .HasMaxLength(5)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Racetrack>(entity =>
        {
            entity.ToTable("Racetrack", "bcm");

            entity.Property(e => e.RacetrackId).HasColumnName("RacetrackID");
            entity.Property(e => e.RacetrackCountryId).HasColumnName("RacetrackCountryID");
            entity.Property(e => e.RacetrackDistanceKm)
                .HasColumnType("decimal(4, 2)")
                .HasColumnName("RacetrackDistanceKM");
            entity.Property(e => e.RacetrackName).HasMaxLength(150);

            entity.HasOne(d => d.RacetrackCountry).WithMany(p => p.Racetracks)
                .HasForeignKey(d => d.RacetrackCountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Racetrack_Country");
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.ToTable("Team", "bcm");

            entity.Property(e => e.TeamId).HasColumnName("TeamID");
            entity.Property(e => e.TeamCountryId).HasColumnName("TeamCountryID");
            entity.Property(e => e.TeamName).HasMaxLength(150);

            entity.HasOne(d => d.TeamCountry).WithMany(p => p.Teams)
                .HasForeignKey(d => d.TeamCountryId)
                .HasConstraintName("FK_Team_Country");
        });

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.ToTable("Vehicle", "bcm");

            entity.Property(e => e.VehicleId).HasColumnName("VehicleID");
            entity.Property(e => e.VehicleName).HasMaxLength(200);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
