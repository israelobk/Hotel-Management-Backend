using Microsoft.Extensions.DependencyInjection;
using yard.infrastructure.Repositories.Implementation;
using yard.infrastructure.Repositories.Interface;

namespace yard.infrastructure
{
	public static class InfrastructureContainer
	{
		public static void InjectInfraServices(this IServiceCollection services)
		{
			services.AddTransient<IHotelRepository, HotelRepository>();
            services.AddScoped<IRatingRepository, RatingRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddTransient<IRatingRepository, RatingRepository>();
			services.AddScoped<ITransactionRepository, TransactionRepository>();
		}
	}
}