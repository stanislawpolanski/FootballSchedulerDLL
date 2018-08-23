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
        void LoadLeague(League league);
        bool LoadTeams(List<Team> teams);
        void GenerateSchedule();
        List<Match> GetSchedule();
    }
}
