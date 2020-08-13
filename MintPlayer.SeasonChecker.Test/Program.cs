using Microsoft.Extensions.DependencyInjection;
using System;

namespace MintPlayer.SeasonCheck.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            // Deze gegevens kunnen uit de database komen
            var seasons = new Season[] {
                new Season { Name= "Spring", Start = new DateTime(2000, 3, 21), End = new DateTime(2000, 6, 20) },
                new Season { Name = "Summer", Start = new DateTime(2000, 6, 21), End = new DateTime(2000, 9, 20) },
                new Season { Name = "Automn", Start = new DateTime(2000, 9, 21), End = new DateTime(2000, 12, 20) },
                new Season { Name = "Winter", Start = new DateTime(2000, 12, 21), End = new DateTime(2001, 3, 20) }
            };

            var serviceProvider = new ServiceCollection()
                .AddSeasonChecker()
                .BuildServiceProvider();

            var seasonChecker = serviceProvider.GetService<ISeasonChecker>();

            // Test uitvoeren voor alle dagen van het jaar
            var testStart = new DateTime(2020, 1, 1);
            var testDays = 366;
            for (int i = 0; i < testDays; i++)
            {
                var day = testStart.AddDays(i);
                var season = seasonChecker.FindSeasonAsync(seasons, day).Result;
                Console.WriteLine("{0:dd/MM/yyyy} is in the {1}", day, season.Name);
            }
            Console.ReadKey();
        }
    }

    class Season : ISeason
    {
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
