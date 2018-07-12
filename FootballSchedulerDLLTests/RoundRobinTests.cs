using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace FootballSchedulerDLLTests
{
    [TestClass]
    public class RoundRobinTests
    {
        [TestMethod]
        public void LoadTeams_WhenTeamsCountLessThanTwo_ReturnsFalse()
        {
            throw new AssertInconclusiveException();
            //arrange
            //act
            //assert
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
        [ExpectedException(typeof(InvalidOperationException))]
        public void GenerateSchedule_WhenTeamsOrLeagueIsNull_ThrowsInvalidOperationException()
        {
            throw new AssertInconclusiveException();
            //arrange
            //act
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
