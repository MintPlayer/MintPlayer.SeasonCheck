using System;

namespace MintPlayer.SeasonCheck
{
    internal class InternalSeason : ISeason
    {
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
