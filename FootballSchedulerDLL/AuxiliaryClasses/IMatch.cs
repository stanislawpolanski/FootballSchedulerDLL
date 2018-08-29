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
        int HomeTeamId { get; set; }
        int AwayTeamId { get; set; }
    }
}
