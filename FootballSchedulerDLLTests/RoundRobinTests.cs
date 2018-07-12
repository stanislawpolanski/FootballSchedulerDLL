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
            throw new AssertInconclusiveException();
            //arrange
            //act
            //assert
        }

        [TestMethod]
        public void LoadTeams_WhenTeamsNull_ReturnsFalse()
        {
            throw new AssertInconclusiveException();
            //arrange
            //act
            //assert
        }

        [TestMethod]
        public void LoadTeams_AllTeamsMustHaveDistinctiveId()
        {
            throw new AssertInconclusiveException();
            //arrange
            //act
            //assert
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
            throw new AssertInconclusiveException();
            //arrange
            //act
            //assert
        }
    }
}
