using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MintPlayer.SeasonCheck
{
    public interface ISeasonChecker
    {
        Task<TSeason> FindSeasonAsync<TSeason>(IEnumerable<TSeason> seasons, DateTime date) where TSeason : class, ISeason;
    }

    internal class SeasonChecker : ISeasonChecker
    {
        public SeasonChecker()
        {
        }

        public Task<TSeason> FindSeasonAsync<TSeason>(IEnumerable<TSeason> seasons, DateTime date) where TSeason : class, ISeason
        {
            var result = seasons
                .Select(s =>
                {
                    // Find season that crosses newyear
                    if (s.Start.Year == s.End.Year)
                    {
                        return new[] {
                            new {
                                OriginalSeason = s,
                                // Remap the season to the year 2000
                                ProcessableSeason = new InternalSeason {
                                    Name = s.Name,
                                    Start = new DateTime(2000, s.Start.Month, s.Start.Day),
                                    End = new DateTime(2000, s.End.Month, s.End.Day)
                                }
                            }
                        };
                    }
                    else
                    {
                        // If the season crosses the newyear, split the season
                        return new[] {
                            new {
                                OriginalSeason = s,
                                // Remap the season to the year 2000
                                ProcessableSeason = new InternalSeason {
                                    Name = s.Name,
                                    Start = new DateTime(2000, s.Start.Month, s.Start.Day),
                                    End = new DateTime(2000, 12, 31)
                                }
                            },
                            new {
                                OriginalSeason = s,
                                // Remap the season to the year 2000
                                ProcessableSeason = new InternalSeason {
                                    Name = s.Name,
                                    Start = new DateTime(2000, 1, 1),
                                    End = new DateTime(2000, s.End.Month, s.End.Day)
                                }
                            }
                        };
                    }
                })
                .SelectMany(s => s)
                .FirstOrDefault(s =>
                    // Now we can easily compare the dates.
                    DateTime.Compare(s.ProcessableSeason.Start, new DateTime(2000, date.Month, date.Day)) <= 0 &&
                    DateTime.Compare(new DateTime(2000, date.Month, date.Day), s.ProcessableSeason.End) <= 0
                )?.OriginalSeason;

            return Task.FromResult(result);
        }
    }
}
