using FootballSchedulerDLL.AuxiliaryClasses;

namespace FootballSchedulerWPF
{
    using System;
    using System.Collections.Generic;
    
    public class Team : FootballEntity
    {
        public string Name { get; set; }
        public int DistrictId { get; set; }
    }
}
