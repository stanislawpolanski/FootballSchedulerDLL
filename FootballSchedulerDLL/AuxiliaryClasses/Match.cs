using FootballSchedulerDLL.AuxiliaryClasses;

namespace FootballSchedulerWPF
{

    public class Match : FootballEntity
    {
        public int LeagueId { get; set; }
        public System.DateTime TimeOfPlay { get; set; }
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
    }
}
