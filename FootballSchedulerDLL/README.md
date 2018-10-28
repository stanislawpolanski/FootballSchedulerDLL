# Purpose of the project
This project is a part of:
https://github.com/stanislawpolanski/FootballSchedulerWPF

This project contains scheduling class for computing schedule of football league.
## Sample use
To compute schedule for an even number of teams you can use it like this:
```
//prepare teams
List<Team> teams = new List<Team>();

Team t1 = new Team();
t1.Name = 'Hutnik Kraków';
teams.Add(t1);

Team t2 = new Team();
...

//prepare league
League league = new League();
league.Name = '3. liga, grupa 4';

//create scheduler
RoundRobinScheduler rrs = new RoundRobinScheduler();

//fill scheduler with data
rrs.LoadLeague(league);
rrs.YearOfStart = new DateTime(2017, 3, 8);
rrs.LoadTeams(teams);

//generate schedule
rrs.GenerateSchedule();

//get matches
List<Match> matches = rrs.GetSchedule();
```
## Algorithm classes
+ IScheduler
+ RoundRobinScheduler
## Auxiliary classes
Contains interfaces and sample classes for scheduler.
## Tests
Tests to be run if any changes made.