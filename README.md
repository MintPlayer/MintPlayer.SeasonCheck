# MintPlayer.SeasonChecker
[![NuGet Version](https://img.shields.io/nuget/v/MintPlayer.SeasonChecker.svg?style=flat)](https://www.nuget.org/packages/MintPlayer.SeasonChecker)
[![NuGet](https://img.shields.io/nuget/dt/MintPlayer.SeasonChecker.svg?style=flat)](https://www.nuget.org/packages/MintPlayer.SeasonChecker)
[![Build Status](https://travis-ci.org/MintPlayer/MintPlayer.SeasonChecker.svg?branch=master)](https://travis-ci.org/MintPlayer/MintPlayer.SeasonChecker)
![.NET Core](https://github.com/MintPlayer/MintPlayer.SeasonChecker/workflows/.NET%20Core/badge.svg)
[![License](https://img.shields.io/badge/License-Apache%202.0-green.svg)](https://opensource.org/licenses/Apache-2.0)
[![Codacy Badge](https://app.codacy.com/project/badge/Grade/6181114185bf40e7bfccc2aa7a794f8a)](https://www.codacy.com/gh/MintPlayer/MintPlayer.SeasonChecker?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=MintPlayer/MintPlayer.SeasonChecker&amp;utm_campaign=Badge_Grade)

Helper library find out the season for a specific date.
## Installation
### NuGet package manager
Open the NuGet package manager and install MintPlayer.SeasonChecker in your project
### Package manager console
Install-Package MintPlayer.SeasonChecker
## Usage
### Adding SeasonChecker services
The SeasonChecker becomes available in the Service Container with the following command (Startup@ConfigureServices).

    services.AddSeasonChecker();

### Code samples
#### Use built-in seasons

    using System;
    using Microsoft.Extensions.DependencyInjection;
    using MintPlayer.SeasonChecker;
    using MintPlayer.SeasonChecker.Abstractions;

    public class Program
    {
        public static void Main()
        {
            var serviceProvider = new ServiceCollection()
                .AddSeasonChecker()
                .BuildServiceProvider();

            var seasonChecker = serviceProvider.GetService<ISeasonChecker>();
            var season = seasonChecker.FindSeasonAsync<Season>(eHemisphere.Northern, DateTime.Today).Result;
            Console.WriteLine(season.Name);
        }
    }

    public class Season : ISeason
    {
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }

[.NET Fiddle](https://dotnetfiddle.net/wClSFp)

#### Use your own seasons

    class Program
    {
        static void Main(string[] args)
        {
            // This data can be sourced from a database
            var seasons = new Season[] {
                new Season
                {
                    Name = "Spring",
                    Start = new DateTime(2000, 3, 21),
                    End = new DateTime(2000, 6, 20)
                },
                new Season
                {
                    Name = "Summer",
                    Start = new DateTime(2000, 6, 21),
                    End = new DateTime(2000, 9, 20)
                },
                new Season
                {
                    Name = "Automn",
                    Start = new DateTime(2000, 9, 21),
                    End = new DateTime(2000, 12, 20)
                },
                new Season
                {
                    Name = "Winter",
                    Start = new DateTime(2000, 12, 21),
                    End = new DateTime(2001, 3, 20)
                }
            };

            // Create service provider
            var serviceProvider = new ServiceCollection()
                .AddSeasonChecker()
                .BuildServiceProvider();

            // Get the season checker from the service-container
            var seasonChecker = serviceProvider.GetService<ISeasonChecker>();

            // Run the test for all days of the year
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

## Equivalent approach in SQL

[CheckSeason.sql](https://github.com/MintPlayer/MintPlayer.SeasonChecker/blob/master/MintPlayer.SeasonChecker/CheckSeason.sql)

## Example with EntityFrameworkCore

[EfCoreSeasonCheck](https://github.com/PieterjanDeClippel/EfCoreSeasonCheck)
