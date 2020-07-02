using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MintPlayer.SeasonCheck
{
    public interface ISeasonChecker
    {
        Task<ISeason> FindSeasonAsync(IEnumerable<ISeason> seasons, DateTime date);
    }

    internal class SeasonChecker : ISeasonChecker
    {
        public SeasonChecker()
        {
        }

        public Task<ISeason> FindSeasonAsync(IEnumerable<ISeason> seasons, DateTime date)
        {
            var mSeasons = seasons.ToList();

            // Find season that crosses newyear
            var endyearCrossingSeason = mSeasons.SingleOrDefault(s => s.Start.Year != s.End.Year);

            if (endyearCrossingSeason != null)
            {
                // Split the season
                mSeasons.Remove(endyearCrossingSeason);
                mSeasons.AddRange(new[]
                {
                    new Season
                    {
                        Name = endyearCrossingSeason.Name,
                        Start = new DateTime(2000, endyearCrossingSeason.Start.Month, endyearCrossingSeason.Start.Day),
                        End = new DateTime(2000, 12, 31)
                    },
                    new Season
                    {
                        Name = endyearCrossingSeason.Name,
                        Start = new DateTime(2000, 1, 1),
                        End = new DateTime(2000, endyearCrossingSeason.End.Month, endyearCrossingSeason.End.Day)
                    },
                });
            }

            // Remap the seasons to the year 2000
            var seasons2000 = mSeasons.Select(s => new Season
            {
                Name = s.Name,
                Start = new DateTime(2000, s.Start.Month, s.Start.Day),
                End = new DateTime(2000, s.End.Month, s.End.Day)
            });
            var date2000 = new DateTime(2000, date.Month, date.Day);

            return seasons2000.FirstOrDefault(s => DateTime.Compare(s.Start, date2000) <= 0 && DateTime.Compare(date2000, s.End) <= 0);
        }
    }
}
