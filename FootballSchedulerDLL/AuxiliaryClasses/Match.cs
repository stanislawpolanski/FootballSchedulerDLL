using System;
using FootballSchedulerDLL.AuxiliaryClasses;

namespace FootballSchedulerDLL.AuxiliaryClasses
{

    public class Match : AuxiliaryItem, IMatch
    {
        public int LeagueId { get; set; }
        public DateTime TimeOfPlay { get; set; }
        public ITeam HomeTeam { get; set; }
        public ITeam AwayTeam { get; set; }
    }
}
