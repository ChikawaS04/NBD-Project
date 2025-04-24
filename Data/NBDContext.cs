using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using NBD3.Models;
using NBD3.ViewModels;

namespace NBD3.Data
{
    public class NBDContext : DbContext
    {
        public NBDContext(DbContextOptions<NBDContext> options) : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Status> Statuss { get; set; }
        public DbSet<Models.Project> Projects { get; set; }
        public DbSet<Models.Location> Locations { get; set; }
        public DbSet<Inventory> Inventorys { get; set; }
        public DbSet<MaterialCategory> MaterialCategorys { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Labour> Labours { get; set; }
        public DbSet<Bid> Bids { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<StaffDetail> StaffDetails { get; set; }
        public DbSet<LabourDetail> LabourDetails { get; set; }
        public DbSet<MaterialDetail> MaterialDetails { get; set; }
        public DbSet<BidSummaryVM> BidMSummaries { get; set; }
	public DbSet<ProjectPhoto> ProjectPhotos { get; set; } 
        public DbSet<ProjectThumbnail> ProjectThumbnails { get; set; } 
        public DbSet<StaffPhoto> StaffPhotos { get; set; } 
        public DbSet<StaffThumbnail> StaffThumbnails { get; set; } 


		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //For the BidSummary ViewModel
            //Note: The Database View name is BidMSummaries
            modelBuilder
                .Entity<BidSummaryVM>()
                .ToView(nameof(BidMSummaries))
                .HasKey(a => a.ID);

            //Many to Many Intersection Staff
            modelBuilder.Entity<StaffDetail>()
            .HasKey(t => new { t.StaffID, t.BidID });

            //Many to Many Intersection Labour
            modelBuilder.Entity<LabourDetail>()
            .HasKey(t => new { t.LabourID, t.BidID });

            //Many to Many Intersection Material/Inventory
            modelBuilder.Entity<MaterialDetail>()
            .HasKey(t => new { t.InventoryID, t.BidID });

            //Add this so you don't get Cascade Delete
            modelBuilder.Entity<Inventory>()
                .HasMany<MaterialDetail>(p => p.MaterialDetails)
                .WithOne(c => c.Inventory)
                .HasForeignKey(c => c.InventoryID)
                .OnDelete(DeleteBehavior.Restrict);

            //Prevent Cascade Delete from Client to Project
            modelBuilder.Entity<Client>()
                .HasMany(c => c.Projects)
                .WithOne(p => p.Client)
                .HasForeignKey(p => p.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Client>()
		 .HasMany<Models.Project>(p => p.Projects)
		 .WithOne(c => c.Client)
		 .HasForeignKey(c => c.ClientId)
		 .OnDelete(DeleteBehavior.Restrict);


			//Client Unique name
			modelBuilder.Entity<Client>()
                .HasIndex(c => c.ClientCommpanyName)
                .IsUnique();

            //Project Unique name
            modelBuilder.Entity<Models.Project>()
                .HasIndex(p => p.ProjectName)
                .IsUnique();

            //Location Unique name
            modelBuilder.Entity<Models.Location>()
                .HasIndex(l => l.LocationName)
                .IsUnique();

            //Labor Type Unique name
            modelBuilder.Entity<Labour>()
                .HasIndex(p => p.LabourType)
                .IsUnique();

        }
    }
}
