using System;
using System.Collections.Generic;
using FootballSchedulerWPF;

namespace FootballSchedulerDLL
{
    public class RoundRobinScheduler : IScheduler
    {
        private List<Matches> Schedule;
        private List<Teams> LoadedTeams;
        private Leagues LoadedLeague;

        public DateTime YearOfStart { get; set; }

        public RoundRobinScheduler()
        {
        }

        public void GenerateSchedule()
        {
            //inputs check
            if (this.LoadedTeams == null)
                throw new InvalidOperationException("No teams loaded");

            if (this.LoadedLeague == null)
                throw new InvalidOperationException("No league assigned");

            //prepare collections and get the fixed team
            Queue<Teams> teamsQueue = new Queue<Teams>(this.LoadedTeams);
            Teams fixedTeam = teamsQueue.Dequeue();

            //prepare datetime - matches played on sundays
            DateTime firstRoundStartDate = new DateTime(YearOfStart.Year, 2, 1);
            while (firstRoundStartDate.DayOfWeek != DayOfWeek.Sunday)
                firstRoundStartDate.AddDays(1);

            DateTime secondRoundStartDate = new DateTime(YearOfStart.Year, 8, 1);
            while (secondRoundStartDate.DayOfWeek != DayOfWeek.Sunday)
                secondRoundStartDate.AddDays(1);

            //generate the schedule
            this.Schedule = new List<Matches>();
            for (int round = 1; round < teamsQueue.Count; round++)
            {
                LinkedList<Teams> teamsLinkedList = new LinkedList<Teams>(teamsQueue);
                teamsLinkedList.AddFirst(fixedTeam);

                do
                {
                    Teams t1 = teamsLinkedList.First.Value;
                    Teams t2 = teamsLinkedList.Last.Value;

                    Matches m1 = new Matches();
                    m1.HomeTeamId = t1.Id;
                    m1.AwayTeamId = t2.Id;
                    m1.TimeOfPlay = firstRoundStartDate.AddDays(7 * (round - 1));

                    Matches m2 = new Matches();
                    m2.HomeTeamId = t2.Id;
                    m2.AwayTeamId = t1.Id;
                    m2.TimeOfPlay = secondRoundStartDate.AddDays(7 * (round - 1));


                    teamsLinkedList.RemoveFirst();
                    teamsLinkedList.RemoveLast();

                    //every even round switch every home & away team
                    if(round % 2 != 0)
                    {
                        Matches mTemp = m1;
                        m1 = m2;
                        m1 = mTemp;
                    }
                }
                while (teamsLinkedList.Count > 1);
            }
        }

        public List<Matches> GetSchedule()
        {
            if (this.Schedule == null)
                throw new NullReferenceException();
            return this.Schedule;
        }

        public void LoadLeague(Leagues league)
        {
            this.LoadedLeague = league;
        }

        public bool LoadTeams(List<Teams> teams)
        {
            //check if there are at least two teams and the team's number is even
            if (teams.Count < 2 || teams.Count % 2 != 0 || teams == null)
                return false;

            //if the check above is ok then assign the property and return true
            this.LoadedTeams = teams;
            return true;
        }
    }
}
