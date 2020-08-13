using Microsoft.Extensions.DependencyInjection;
using MintPlayer.SeasonChecker.Abstractions;

namespace MintPlayer.SeasonChecker
{
    public static class SeasonCheckerExtensions
    {
        public static IServiceCollection AddSeasonChecker(this IServiceCollection services)
        {
            return services.AddScoped<ISeasonChecker, SeasonChecker>();
        }
    }
}
