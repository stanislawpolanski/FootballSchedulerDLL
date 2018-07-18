using System;
using System.Collections.Generic;
using FootballSchedulerDLL;
using FootballSchedulerWPF;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace FootballSchedulerDLLTests
{
    [TestClass]
    public class RoundRobinTests
    {
        [TestMethod]
        public void LoadTeams_WhenTeamsCountLessThanTwo_ReturnsFalse()
        {
            //arrange
            List<Teams> lt = new List<Teams>();
            lt.Add(new Teams());

            RoundRobinScheduler rrs = new RoundRobinScheduler();

            //act
            bool loaded = rrs.LoadTeams(lt);
            //assert
            Assert.IsFalse(loaded);
        }

        [TestMethod]
        public void LoadTeams_WhenTeamsCountNotEven_ReturnsFalse()
        {
            //arrange
            List<Teams> teams = new List<Teams>();
            teams.Add(new Teams());
            teams.Add(new Teams());
            teams.Add(new Teams());

            RoundRobinScheduler rrs = new RoundRobinScheduler();

            //act
            bool loaded = rrs.LoadTeams(teams);

            //assert
            Assert.IsFalse(loaded);
        }

        [TestMethod]
        public void LoadTeams_WhenTeamsNull_ReturnsFalse()
        {
            //arrange
            List<Teams> teams = null;

            RoundRobinScheduler rrs = new RoundRobinScheduler();

            //act
            bool loaded = rrs.LoadTeams(teams);

            //assert
            Assert.IsFalse(loaded);
        }

        [TestMethod]
        public void LoadTeams_WhenTeamsHaveNoDistinctiveIds_ReturnsFalse()
        {
            //arrange
            List<Teams> teams = new List<Teams>();

            for(int i = 0; i < 17; i++)
            {
                Teams t = new Teams
                {
                    Id = i
                };

                teams.Add(t);
            }

            Teams nonDistinctiveIdTeam = new Teams
            {
                Id = 15
            };
            teams.Add(nonDistinctiveIdTeam);

            RoundRobinScheduler rrs = new RoundRobinScheduler();

            //act
            bool loaded = rrs.LoadTeams(teams);

            //assert
            Assert.IsFalse(loaded);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GenerateSchedule_WhenTeamsOrLeagueNull_ThrowsInvalidOperationException()
        {
            //arrange
            RoundRobinScheduler rrs = new RoundRobinScheduler();
            //act
            rrs.GenerateSchedule();
            //assert handled by ExpectedException
        }

        [TestMethod]
        public void GenerateSchedule_ScheduleMustCoverAllPairs()
        {
            //ARRANGE
            int teamsNumber = 8;

            //prepare checklist
            bool[][] checkList = new bool[teamsNumber][];
            for(int i = 0; i < teamsNumber; i++)
            {
                checkList[i] = new bool[teamsNumber];
            }

            //create teams
            List<Teams> teams = new List<Teams>();

            for(int i = 1; i < teamsNumber + 1; i++)
            {
                Teams t = new Teams
                {
                    Id = i
                };

                teams.Add(t);
            }

            //invoke scheduler
            RoundRobinScheduler rrs = new RoundRobinScheduler();

            Leagues league = new Leagues();
            rrs.LoadLeague(league);

            rrs.YearOfStart = new DateTime(2017, 3, 8);

            rrs.LoadTeams(teams);

            //ACT
            rrs.GenerateSchedule();
            List<Matches> matches = rrs.GetSchedule();

            //ASSERT
            matches.ForEach(x => checkList[(int)x.HomeTeamId - 1][(int)x.AwayTeamId - 1] = true);



            //check if all of the matches has been played
            //however any team can not play itself
            for(int i = 0; i < teamsNumber; i++)
            {
                for(int j = 0; j < teamsNumber; j++)
                {
                    bool iEqualsJ    = (i == j);
                    bool matchPlayed = checkList[i][j];

                    //team cannot play itself!
                    if (iEqualsJ && matchPlayed)
                        Assert.Fail();

                    //all the matches must be played, if not, then fail
                    if (!iEqualsJ && !matchPlayed)
                        Assert.Fail();
                }
            }

            //if not failed, then ok
        }
    }
}
