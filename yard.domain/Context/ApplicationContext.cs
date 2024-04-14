using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using yard.domain.Models;

namespace yard.domain.Context
{
    public class ApplicationContext : IdentityDbContext<AppUser, IdentityRole<int>, int>
	{
		public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
		{
		}

		public DbSet<Amenity> Amenities { get; set; }
		public DbSet<Address> Addresses { get; set; }
		public DbSet<Booking> Bookings { get; set; }
		public DbSet<Gallery> Galleries { get; set; }
		public DbSet<Hotel> Hotels { get; set; }
		public DbSet<Payment> Payments { get; set; }
		public DbSet<Rating> Ratings { get; set; }
		public DbSet<Room> Rooms { get; set; }
		public DbSet<RoomType> RoomTypes { get; set; }
		public DbSet<WishList> WishLists { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			var decimalProps = modelBuilder.Model.GetEntityTypes().SelectMany
					(t => t.GetProperties())
				.Where(p => (Nullable.GetUnderlyingType(p.ClrType) ?? p.ClrType) == typeof(decimal));

			foreach (var property in decimalProps)
			{
				property.SetPrecision(18);
				property.SetScale(2);
			}

			foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
			{
				relationship.DeleteBehavior = DeleteBehavior.NoAction;
			}

			modelBuilder.Entity<Booking>()
				.HasOne(c => c.Room)
				.WithMany(c => c.Bookings)
				.HasForeignKey(c => c.RoomId)
				.HasPrincipalKey(c => c.Id)
				.OnDelete(DeleteBehavior.NoAction);
			//.WillCascadeOnDelete(false);

			modelBuilder.Entity<Booking>()
				.HasOne(c => c.AppUser)
				.WithMany(c => c.Bookings)
				.HasForeignKey(c => c.AppUserId)
				.HasPrincipalKey(c => c.Id)
				.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<Booking>()
				.HasOne(c => c.Hotel)
				.WithMany(c => c.Bookings)
				.HasForeignKey(c => c.HotelId)
				.HasPrincipalKey(c => c.Id)
				.OnDelete(DeleteBehavior.NoAction);

			//modelBuilder.Entity<WishList>()
			//	.HasKey(bc => new { bc.AppUserId, bc.HotelId });

			modelBuilder.Entity<WishList>()
				.HasOne(bc => bc.AppUser)
				.WithMany(b => b.WishLists)
				.HasForeignKey(bc => bc.AppUserId);

			modelBuilder.Entity<WishList>()
				.HasOne(bc => bc.Hotel)
				.WithMany(c => c.WishLists)
				.HasForeignKey(bc => bc.HotelId);

			modelBuilder.Entity<AppUser>()
				.HasMany(x => x.Ratings);

			modelBuilder.Entity<Rating>()
				.HasIndex(x => x.AppUserId);

			modelBuilder.Entity<Rating>()
				.HasIndex(x => x.HotelId);

			modelBuilder.Entity<Hotel>()
				.HasMany(c => c.Ratings);

			base.OnModelCreating(modelBuilder);
		}
	}
}