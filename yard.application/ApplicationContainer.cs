using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using yard.application.Services.Implementation;
using yard.application.Services.Interface;
using yard.domain.Models;

namespace yard.application
{
	public static class ApplicationContainer
	{
		public static void InjectApplicationServices(this IServiceCollection services)
		{
			services.AddTransient<IHotelService, HotelService>();
			services.AddTransient<IRatingService, RatingService>();
            services.AddScoped<UserManager<AppUser>>();
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<IIdentityUserService, IdentityUserService>();
            services.AddScoped<ITransactionService, TransactionService>();

            //services.AddTransient<IPaymentService, PaymentService>();   
        }
    }
}