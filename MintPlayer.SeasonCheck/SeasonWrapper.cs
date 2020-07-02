namespace MintPlayer.SeasonCheck
{
    internal class SeasonWrapper<TSeason> where TSeason : ISeason
    {
        public TSeason OriginalSeason { get; set; }
        public ISeason ProcessableSeason { get; set; }
    }
}
