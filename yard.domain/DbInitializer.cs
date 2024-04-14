using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using yard.domain.Context;
using yard.domain.enums;
using yard.domain.Models;


namespace yard.domain
{
	public static class DbInitializer
	{
		public static async Task SeedData(IApplicationBuilder app)
		{
			using var serviceScope = app.ApplicationServices.CreateScope();
			await Initialize(
				serviceScope.ServiceProvider.GetService<UserManager<AppUser>>(),
				serviceScope.ServiceProvider.GetService<ApplicationContext>(),
				serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>());
		}

		private static async Task Initialize(UserManager<AppUser>? userManager, ApplicationContext? context,
			RoleManager<IdentityRole>? roleManager)
		{
			if ((await context.Database.GetPendingMigrationsAsync()).Any())
			{
				await context.Database.MigrateAsync();
			}

			if (context.Hotels.Any() && context.Users.Any())
			{
				return;
			}

			var address = new Address()
			{
				City = "Lagos",
				Country = "Nigeria",
				PostalCode = "00000",
				State = "La",
				Street = "Don't ask street"
			};

			await context.Addresses.AddAsync(address);

			var controlUser = new AppUser
			{
				FirstName = "John",
				LastName = "Sample",
				UserName = "Doe",
				Email = "testmail@gmail.com",
				PhoneNumber = "08162292349",
				PhoneNumberConfirmed = true,
				Gender = Gender.Male,
				IsActive = true,
				CreatedAt = DateTime.Now,
				UpdatedAt = DateTime.Now,
				EmailConfirmed = true,
				ProfilePictureUrl = "www.avatar.com",
				Address = address,
				AddressId = address.Id
			};
			await userManager.CreateAsync(controlUser, "Password@123");

			var rooms = new RoomType[]
			{
				new RoomType
				{
					Name = "Regular",
					Description = "thoughtfully designed spaces, essential amenities and a warm ambiance",
					Price = 5000M,
					Discount = 1000M,
					Thumbnail = "www.fakethumbnail.com"
				},
				new RoomType
				{
					Name = "Executive",
					Description = "Tastefully appointed decor, modern amenities and personalized services",
					Price = 150000M,
					Discount = 10000M,
					Thumbnail = "www.fakethumbnail.com"
				},
				new RoomType
				{
					Name = "Presidential Suite",
					Description = "Sophisticated decor, panoramic views and exclusive amenities",
					Price = 900000M,
					Discount = 50000M,
					Thumbnail = "www.fakethumbnail.com"
				}
			};


			var hotel = new Hotel[]
			{
				new Hotel
				{
					Name = "Decagon Lounge",
					Description = "The Best hotel in Benin with a serene and congenial " +
					              "atmosphere with a picturesque that's second to none",
					Email = "decagonlounge@gmail.com",
					Phone = "+2348137246538",
					Address = new Address
					{
						City = "Benin", State = "Edo", Country = "Nigeria", PostalCode = "11111", Street = "Lagos road"
					},
					RoomTypes = rooms
				}
			};

			context.Hotels.AddRange(hotel);
			await context.SaveChangesAsync();
		}
	}
}