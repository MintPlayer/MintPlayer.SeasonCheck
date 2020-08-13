using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MintPlayer.SeasonChecker.Abstractions
{
    public interface ISeasonChecker
    {
        Task<TSeason> FindSeasonAsync<TSeason>(IEnumerable<TSeason> seasons, DateTime date) where TSeason : class, ISeason;
    }
}
