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
                firstRoundStartDate = firstRoundStartDate.AddDays(1);

            DateTime secondRoundStartDate = new DateTime(YearOfStart.Year, 8, 1);
            while (secondRoundStartDate.DayOfWeek != DayOfWeek.Sunday)
                secondRoundStartDate = secondRoundStartDate.AddDays(1);

            //generate the schedule
            this.Schedule = new List<Matches>();
            for (int round = 0; round < teamsQueue.Count; round++)
            {
                //recreate team's list
                LinkedList<Teams> teamsLinkedList = new LinkedList<Teams>(teamsQueue);
                teamsLinkedList.AddFirst(fixedTeam);

                do
                {
                    Teams t1 = teamsLinkedList.First.Value;
                    Teams t2 = teamsLinkedList.Last.Value;

                    Matches m1 = new Matches();
                    m1.HomeTeamId = t1.Id;
                    m1.AwayTeamId = t2.Id;
                    
                    Matches m2 = new Matches();
                    m2.HomeTeamId = t2.Id;
                    m2.AwayTeamId = t1.Id;
                    
                    //every even round switch every home & away team
                    if(round % 2 != 0)
                    {
                        Matches mTemp = m1;
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
            //check if teams are not null
            if (teams == null)
                return false;
            //check if there are at least two teams and the team's number is even
            if (teams.Count < 2 || teams.Count % 2 != 0)
                return false;

            //check if each team have distinctive id
            //prepare a list for comparison
            List<int> checkList = new List<int>();

            foreach(Teams t in teams)
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
