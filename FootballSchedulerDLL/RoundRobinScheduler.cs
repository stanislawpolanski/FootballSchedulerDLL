using System;
using System.Collections.Generic;
using FootballSchedulerWPF;

namespace FootballSchedulerDLL
{
    class RoundRobinScheduler : IScheduler
    {
        private List<Matches> Schedule;
        private List<Teams> LoadedTeams;
        private Leagues LoadedLeague;

        public DateTime YearOfStart { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public RoundRobinScheduler()
        {
            this.Schedule     = null;
            this.LoadedTeams  = null;
            this.LoadedLeague = null;
        }

        public void GenerateSchedule()
        {
            //inputs check
            if (this.LoadedTeams == null)
                throw new InvalidOperationException("No teams loaded");

            if (this.LoadedLeague == null)
                throw new InvalidOperationException("No league assigned");

            //if ok then generate schedule

            Queue<Teams> teamsQueue = new Queue<Teams>(this.LoadedTeams);
            Teams fixedTeam = teamsQueue.Dequeue();

            this.Schedule = new List<Matches>();

            for (int round = 1; round < teamsQueue.Count; round++)
            {
                LinkedList<Teams> teamsLinkedList = new LinkedList<Teams>(teamsQueue);
                teamsLinkedList.AddFirst(fixedTeam);

                do
                {
                    Teams t1 = teamsLinkedList.First.Value;
                    Teams t2 = teamsLinkedList.Last.Value;

                    if(round % 2 == 0)
                    {
                        //TODO: set dates
                        Matches m1 = new Matches();
                        m1.HomeTeamId = t1.Id;
                        m1.AwayTeamId = t2.Id;

                        Matches m2 = new Matches();
                        m2.HomeTeamId = t2.Id;
                        m2.AwayTeamId = t1.Id;
                    }
                    else
                    {
                        //TODO: fill this
                    }


                    teamsLinkedList.RemoveFirst();
                    teamsLinkedList.RemoveLast();
                }
                while (teamsLinkedList.Count > 1);
            }




            //TODO implement
            throw new NotImplementedException();
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
            if (teams.Count < 2 || teams.Count % 2 != 0)
                return false;

            //if the check above is ok then assign the property and return true
            this.LoadedTeams = teams;
            return true;
        }
    }
}
