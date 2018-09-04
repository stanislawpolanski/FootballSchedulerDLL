using FootballSchedulerDLL.AuxiliaryClasses;

namespace FootballSchedulerDLL.AuxiliaryClasses
{

    public class Match : AuxiliaryItem, IMatch
    {
        public int LeagueId { get; set; }
        public System.DateTime TimeOfPlay { get; set; }
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
    }
}
