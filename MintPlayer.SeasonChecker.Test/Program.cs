using Microsoft.Extensions.DependencyInjection;
using MintPlayer.SeasonChecker.Abstractions;
using System;

namespace MintPlayer.SeasonChecker.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Create service container

            var serviceProvider = new ServiceCollection()
                .AddSeasonChecker()
                .BuildServiceProvider();

            var seasonChecker = serviceProvider.GetService<ISeasonChecker>();

            #endregion
            #region Test with sourced seasons

            // These seasons may come from a DbContext
            var seasons = new Season[] {
                new Season { Name= "Spring", Start = new DateTime(2000, 3, 21), End = new DateTime(2000, 6, 20) },
                new Season { Name = "Summer", Start = new DateTime(2000, 6, 21), End = new DateTime(2000, 9, 20) },
                new Season { Name = "Automn", Start = new DateTime(2000, 9, 21), End = new DateTime(2000, 12, 20) },
                new Season { Name = "Winter", Start = new DateTime(2000, 12, 21), End = new DateTime(2001, 3, 20) }
            };

            // Run test for each day of the year
            var testStart = new DateTime(2020, 1, 1);
            var testDays = 366;
            for (int i = 0; i < testDays; i++)
            {
                var day = testStart.AddDays(i);
                var season = seasonChecker.FindSeasonAsync(seasons, day).Result;
                Console.WriteLine("{0:dd/MM/yyyy} is in the {1}", day, season.Name);
            }
            Console.ReadKey();

            #endregion
            #region Test with built-in seasons

            // Run test for each day of the year
            for (int i = 0; i < testDays; i++)
            {
                var day = testStart.AddDays(i);
                var season = seasonChecker.FindSeasonAsync<Season>(eHemisphere.Southern, day).Result;
                Console.WriteLine("{0:dd/MM/yyyy} is in the {1}", day, season.Name);
            }
            Console.ReadKey();

            #endregion
        }
    }

    class Season : ISeason
    {
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
