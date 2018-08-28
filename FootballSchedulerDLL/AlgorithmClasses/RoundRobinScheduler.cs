﻿using System;
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
        /// Contains dates of dates when each of the rounds starts (usually spring & autumn).
        /// </summary>
        private Tuple<DateTime, DateTime> DatesOfRoundsStarts;

        /// <summary>
        /// Year of league's start. Only year is considered.
        /// </summary>
        public DateTime YearOfStart { get; set; }

        /// <summary>
        /// Checks if teams and league are loaded.
        /// </summary>
        private void InputsCheck()
        {
            if (this.LoadedTeams == null)
                throw new InvalidOperationException("No teams loaded");

            if (this.LoadedLeague == null)
                throw new InvalidOperationException("No league assigned");
        }

        private Tuple<DateTime, DateTime> CalculateDatesOfRoundsBeginnings()
        {
            DateTime firstRoundStartDate = new DateTime(YearOfStart.Year, 8, 1, 15, 0, 0);
            while (firstRoundStartDate.DayOfWeek != DayOfWeek.Sunday)
                firstRoundStartDate = firstRoundStartDate.AddDays(1);

            DateTime secondRoundStartDate = new DateTime(YearOfStart.Year + 1, 2, 1, 15, 0, 0);
            while (secondRoundStartDate.DayOfWeek != DayOfWeek.Sunday)
                secondRoundStartDate = secondRoundStartDate.AddDays(1);

            return new Tuple<DateTime, DateTime>(firstRoundStartDate, secondRoundStartDate);
        }

        /// <summary>
        /// Calculates the schedule using inputs set before.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">Thrown when no teams where loaded or no league loaded.</exception>
        public void GenerateSchedule()
        {
            //inputs check
            this.InputsCheck();

            //prepare queue list and get the fixed team, prepare the schedule
            Queue<Team> teamsQueue = new Queue<Team>(this.LoadedTeams);
            Team fixedTeam = teamsQueue.Dequeue();
            this.Schedule = new List<Match>();

            //prepare datetime - matches played on sundays
            DatesOfRoundsStarts = this.CalculateDatesOfRoundsBeginnings();

            //generate the schedule
            for (int round = 0; round < teamsQueue.Count; round++)
            {
                //recreate team's list - reattach fixed team in the beginning
                //linked list will change every round, but the fixed team must stay in the beginning all the time
                LinkedList<Team> teamsLinkedList = new LinkedList<Team>(teamsQueue);
                teamsLinkedList.AddFirst(fixedTeam);

                do
                {
                    
                    //TODO Refactor - kick out inner loop into function
                    Team t1 = teamsLinkedList.First.Value;
                    Team t2 = teamsLinkedList.Last.Value;

                    Tuple<Match, Match> pairOfMatches = this.GeneratePairOfMatches(round, t1, t2);

                    //add matches to the schedule
                    this.Schedule.Add(pairOfMatches.Item1);
                    this.Schedule.Add(pairOfMatches.Item2);

                    //and then remove both teams from the current round
                    teamsLinkedList.RemoveFirst();
                    teamsLinkedList.RemoveLast();
                }
                while (teamsLinkedList.Count > 1);

                //take last team into the beginning of the queue
                teamsQueue.Enqueue(teamsQueue.Dequeue());
            }

            this.DatesOfRoundsStarts = null;
        }

        private Tuple<Match, Match> GeneratePairOfMatches(int round, Team team1, Team team2)
        {
            //each team must play both home and away
            Match m1 = new Match()
            {
                HomeTeamId = team1.Id,
                AwayTeamId = team2.Id,
                LeagueId = this.LoadedLeague.Id
            };


            Match m2 = new Match()
            {
                HomeTeamId = team2.Id,
                AwayTeamId = team1.Id,
                LeagueId = this.LoadedLeague.Id
            };

            //every even round switch every home & away team
            if (round % 2 != 0)
            {
                Match mTemp = m1;
                m1 = m2;
                m2 = mTemp;
            }

            //set dates
            m1.TimeOfPlay = this.DatesOfRoundsStarts.Item1.AddDays(7 * round);
            m2.TimeOfPlay = this.DatesOfRoundsStarts.Item2.AddDays(7 * round);

            return new Tuple<Match, Match>(m1, m2);
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
