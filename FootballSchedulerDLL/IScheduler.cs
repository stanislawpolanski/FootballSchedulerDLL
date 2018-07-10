using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootballSchedulerWPF;

namespace FootballSchedulerDLL
{
    interface IScheduler
    {
        DateTime YearOfStart { get; set; }
        void LoadLeague(Leagues league);
        bool LoadTeams(List<Teams> teams);
        void GenerateSchedule();
        List<Matches> GetSchedule();
    }
}
