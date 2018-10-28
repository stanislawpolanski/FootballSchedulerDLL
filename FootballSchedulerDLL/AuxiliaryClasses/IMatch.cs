using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballSchedulerDLL.AuxiliaryClasses
{
    public interface IMatch
    {
        int Id { get; set; }
        int LeagueId { get; set; }
        System.DateTime TimeOfPlay { get; set; }
        ITeam HomeTeam { get; set; }
        ITeam AwayTeam { get; set; }
    }
}
