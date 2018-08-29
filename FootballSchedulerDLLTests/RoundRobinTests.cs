using System;
using System.Collections.Generic;
using FootballSchedulerDLL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FootballSchedulerDLL.AuxiliaryClasses;


namespace FootballSchedulerDLLTests
{
    [TestClass]
    public class RoundRobinTests
    {
        [TestMethod]
        public void LoadTeams_WhenTeamsQuantityLessThanTwo_ReturnsFalse()
        {
            //try to load one team into the scheduler
            //ARRANGE
            List<ITeam> lt = this.CreateBlankTeams(1);
            RoundRobinScheduler rrs = new RoundRobinScheduler();

            //ACT
            bool loaded = rrs.LoadTeams(lt);

            //ASSERT
            Assert.IsFalse(loaded);
        }

        [TestMethod]
        public void LoadTeams_WhenTeamsCountNotEven_ReturnsFalse()
        {
            //try to load three (odd number) teams into the scheduler
            //ARRANGE
            List<ITeam> teams = this.CreateBlankTeams(3);

            RoundRobinScheduler rrs = new RoundRobinScheduler();

            //ACT
            bool loaded = rrs.LoadTeams(teams);

            //ASSERT
            Assert.IsFalse(loaded);
        }

        [TestMethod]
        public void LoadTeams_WhenTeamsNull_ReturnsFalse()
        {
            //try to load null as teams into the scheduler;
            //ARRANGE
            List<ITeam> teams = null;

            RoundRobinScheduler rrs = new RoundRobinScheduler();

            //ACT
            bool loaded = rrs.LoadTeams(teams);

            //ASSERT
            Assert.IsFalse(loaded);
        }

        [TestMethod]
        public void LoadTeams_WhenTeamsHaveNoDistinctiveIds_ReturnsFalse()
        {
            //try to load collection of teams. In the collection two teams have the same id.
            //ARRANGE
            //get teams
            List<ITeam> teams = this.CreateBlankTeams(17);

            //add one with non distinctive id
            Team nonDistinctiveIdTeam = new Team
            {
                Id = teams.Count - 1
            };
            teams.Add(nonDistinctiveIdTeam);

            RoundRobinScheduler rrs = new RoundRobinScheduler();

            //ACT
            bool loaded = rrs.LoadTeams(teams);

            //ASSERT
            Assert.IsFalse(loaded);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GenerateSchedule_WhenTeamsOrLeagueNull_ThrowsInvalidOperationException()
        {
            //ARRANGE
            RoundRobinScheduler rrs = new RoundRobinScheduler();

            //ACT
            rrs.GenerateSchedule();

            //ASSERT handled by ExpectedException
        }

        /// <summary>
        /// Create collection with teams. Each team has distinctive id but no name.
        /// </summary>
        /// <param name="teamsNumber">Number of teams.</param>
        /// <returns></returns>
        private List<ITeam> CreateBlankTeams(int teamsNumber)
        {
            List<ITeam> teams = new List<ITeam>();

            for (int i = 1; i < teamsNumber + 1; i++)
            {
                Team t = new Team
                {
                    Id = i
                };

                teams.Add(t);
            }

            return teams;
        }

        /// <summary>
        /// Invokes scheduler at year 2017.
        /// </summary>
        /// <param name="teams">Teams to be put into the scheduler.</param>
        /// <returns></returns>
        private RoundRobinScheduler InvokeScheduler(List<ITeam> teams)
        {
            RoundRobinScheduler rrs = new RoundRobinScheduler();

            League league = new League();
            rrs.LoadLeague(league);

            rrs.YearOfStart = new DateTime(2017, 3, 8);

            rrs.LoadTeams(teams);

            return rrs;
        }

        [TestMethod]
        public void GenerateSchedule_ScheduleMustCoverAllPairs()
        {
            //ARRANGE
            int teamsNumber = 16;

            //prepare checklist
            //false - not played
            //already played matches will be marked as true
            bool[][] checkList = new bool[teamsNumber][];
            for(int i = 0; i < teamsNumber; i++)
            {
                checkList[i] = new bool[teamsNumber];
            }

            //create teams
            List<ITeam> teams = this.CreateBlankTeams(teamsNumber);

            //invoke scheduler
            RoundRobinScheduler rrs = this.InvokeScheduler(teams);

            //ACT
            rrs.GenerateSchedule();
            List<IMatch> matches = rrs.GetSchedule();

            //ASSERT
            //loop through each match and mark him as played (true)
            matches.ForEach(x => checkList[(int)x.HomeTeamId - 1][(int)x.AwayTeamId - 1] = true);

            //check if all of the matches has been played
            //however any team can not play itself
            for(int i = 0; i < teamsNumber; i++)
            {
                for(int j = 0; j < teamsNumber; j++)
                {
                    bool diagonal    = (i == j);
                    bool matchPlayed = checkList[i][j];

                    //team cannot play itself
                    if (diagonal && matchPlayed)
                        Assert.Fail();

                    //all of the matches must be played, if not, then fail
                    if (!diagonal && !matchPlayed)
                        Assert.Fail();
                }
            }
            //if not failed, then ok
        }
    }
}
