using Microsoft.Extensions.DependencyInjection;

namespace MintPlayer.SeasonCheck
{
    public static class SeasonCheckerExtensions
    {
        public static IServiceCollection AddSeasonChecker(this IServiceCollection services)
        {
            return services.AddScoped<ISeasonChecker, SeasonChecker>();
        }
    }
}
