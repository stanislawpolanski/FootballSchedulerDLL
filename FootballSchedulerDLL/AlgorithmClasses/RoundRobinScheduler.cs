using System;
using System.Collections.Generic;
using FootballSchedulerWPF;

namespace FootballSchedulerDLL
{
    public class RoundRobinScheduler : IScheduler
    {
        private List<Match> Schedule;
        private List<Team> LoadedTeams;
        private League LoadedLeague;

        /// <summary>
        /// Year of league's start. Only year is considered.
        /// </summary>
        public DateTime YearOfStart { get; set; }

        /// <summary>
        /// Calculates the schedule using inputs set before.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">Thrown when no teams where loaded or no league loaded.</exception>
        public void GenerateSchedule()
        {
            //inputs check
            if (this.LoadedTeams == null)
                throw new InvalidOperationException("No teams loaded");

            if (this.LoadedLeague == null)
                throw new InvalidOperationException("No league assigned");

            //prepare queue list and get the fixed team
            Queue<Team> teamsQueue = new Queue<Team>(this.LoadedTeams);
            Team fixedTeam = teamsQueue.Dequeue();

            //prepare datetime - matches played on sundays
            DateTime firstRoundStartDate = new DateTime(YearOfStart.Year, 8, 1, 15, 0, 0);
            while (firstRoundStartDate.DayOfWeek != DayOfWeek.Sunday)
                firstRoundStartDate = firstRoundStartDate.AddDays(1);

            DateTime secondRoundStartDate = new DateTime(YearOfStart.Year + 1, 2, 1, 15, 0, 0);
            while (secondRoundStartDate.DayOfWeek != DayOfWeek.Sunday)
                secondRoundStartDate = secondRoundStartDate.AddDays(1);

            //prepare the schedule
            this.Schedule = new List<Match>();

            //generate the schedule
            for (int round = 0; round < teamsQueue.Count; round++)
            {
                //recreate team's list - reattach fixed team in the beginning
                //linked list will change every round, but the fixed team must stay in the beginning all the time
                LinkedList<Team> teamsLinkedList = new LinkedList<Team>(teamsQueue);
                teamsLinkedList.AddFirst(fixedTeam);

                do
                {
                    Team t1 = teamsLinkedList.First.Value;
                    Team t2 = teamsLinkedList.Last.Value;

                    //each team must play both home and away
                    Match m1 = new Match();
                    m1.HomeTeamId = t1.Id;
                    m1.AwayTeamId = t2.Id;
                    m1.LeagueId   = this.LoadedLeague.Id;
                    
                    Match m2 = new Match();
                    m2.HomeTeamId = t2.Id;
                    m2.AwayTeamId = t1.Id;
                    m2.LeagueId   = this.LoadedLeague.Id;
                    
                    //every even round switch every home & away team
                    if(round % 2 != 0)
                    {
                        Match mTemp = m1;
                        m1 = m2;
                        m2 = mTemp;
                    }

                    //set dates
                    m1.TimeOfPlay = firstRoundStartDate.AddDays(7 * round);
                    m2.TimeOfPlay = secondRoundStartDate.AddDays(7 * round);

                    //add matches to the schedule
                    this.Schedule.Add(m1);
                    this.Schedule.Add(m2);

                    //and then remove both teams from the current round
                    teamsLinkedList.RemoveFirst();
                    teamsLinkedList.RemoveLast();
                }
                while (teamsLinkedList.Count > 1);

                //take last team into the beginning of the queue
                teamsQueue.Enqueue(teamsQueue.Dequeue());
            }
        }

        /// <summary>
        /// Returns the generated schedule
        /// </summary>
        /// <returns>Matches to be played</returns>
        /// <exception cref="System.NullReferenceException">Thrown when schedule has not been generated yet.</exception>
        public List<Match> GetSchedule()
        {
            if (this.Schedule == null)
                throw new NullReferenceException();
            return this.Schedule;
        }

        /// <summary>
        /// Just loads a league for now.
        /// </summary>
        /// <param name="league">League with its name.</param>
        public void LoadLeague(League league)
        {
            this.LoadedLeague = league;
        }

        /// <summary>
        /// Checks if the team's number is even and positive. Also checks if each team has distinctive id. If so
        /// loads teams into and returns true. Otherwise does nothing and returns false.
        /// </summary>
        /// <param name="teams">List of teams to be checked and eventually loaded into.</param>
        /// <returns></returns>
        public bool LoadTeams(List<Team> teams)
        {
            //check if teams are not null
            if (teams == null)
                return false;

            //check if there are at least two teams and the team's number is even
            if (teams.Count < 2 || teams.Count % 2 != 0)
                return false;

            //check if each team have distinctive id
            //prepare a list for comparison
            List<int> checkList = new List<int>();

            foreach(Team t in teams)
            {
                if (checkList.Exists(id => id == t.Id))
                    return false;
                checkList.Add(t.Id);
            }

            //if the all the checks above are ok then assign the property and return true
            this.LoadedTeams = teams;
            return true;
        }
    }
}
