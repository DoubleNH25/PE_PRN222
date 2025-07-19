using System;
using System.Collections.Generic;
using LionPetManagement_NguyenHangNhatHuy.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LionPetManagement_NguyenHangNhatHuy.DAL;

public partial class Su25lionDbContext : DbContext
{
    public Su25lionDbContext()
    {
    }

    public Su25lionDbContext(DbContextOptions<Su25lionDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<LionAccount> LionAccounts { get; set; }

    public virtual DbSet<LionProfile> LionProfiles { get; set; }

    public virtual DbSet<LionType> LionTypes { get; set; }

	private string GetConnectionString()
	{
		IConfiguration config = new ConfigurationBuilder()
			 .SetBasePath(Directory.GetCurrentDirectory())
					.AddJsonFile("appsettings.json", true, true)
					.Build();
		var strCont = config["ConnectionStrings:DefaultConnectionStringDB"];

		return strCont;
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	=> optionsBuilder.UseSqlServer(GetConnectionString());

	protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LionAccount>(entity =>
        {
            entity.HasKey(e => e.AccountId);

            entity.ToTable("LionAccount");

            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(50);
            entity.Property(e => e.UserName).HasMaxLength(50);
        });

        modelBuilder.Entity<LionProfile>(entity =>
        {
            entity.ToTable("LionProfile");

            entity.Property(e => e.Characteristics).HasMaxLength(2000);
            entity.Property(e => e.LionName).HasMaxLength(150);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Warning).HasMaxLength(1500);

            entity.HasOne(d => d.LionType).WithMany(p => p.LionProfiles)
                .HasForeignKey(d => d.LionTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LionProfile_LionType");
        });

        modelBuilder.Entity<LionType>(entity =>
        {
            entity.ToTable("LionType");

            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.LionTypeName).HasMaxLength(250);
            entity.Property(e => e.Origin).HasMaxLength(250);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
