using FootballSchedulerDLL.AuxiliaryClasses;

namespace FootballSchedulerDLL.AuxiliaryClasses
{
    using System;
    using System.Collections.Generic;
    
    public class Team : AuxiliaryUnit, ITeam
    {
        public string Name { get; set; }
        public int DistrictId { get; set; }
    }
}
