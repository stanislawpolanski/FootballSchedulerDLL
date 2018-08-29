using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootballSchedulerDLL.AuxiliaryClasses;

namespace FootballSchedulerDLL
{
    interface IScheduler
    {
        DateTime YearOfStart { get; set; }
        void LoadLeague(ILeague league);
        bool LoadTeams(List<ITeam> teams);
        void GenerateSchedule();
        List<IMatch> GetSchedule();
    }
}
