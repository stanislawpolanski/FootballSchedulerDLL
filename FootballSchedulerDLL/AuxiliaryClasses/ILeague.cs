﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballSchedulerDLL.AuxiliaryClasses
{
    public interface ILeague
    {
        int Id { get; set; }
        string Name { get; set; }
    }
}
