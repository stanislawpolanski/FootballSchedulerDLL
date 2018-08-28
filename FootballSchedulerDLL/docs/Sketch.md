# FootballScheduler library
The library contains classes to generate fixtures. Cooperates with Football Scheduler WPF.
## Checklist
Classes:
+ [ ] Football Entity - contains id, basic for auxiliary classes
+ [ ] IScheduler - interface for communication
+ [ ] RoundRobinScheduler - generating league fixtures
+ [ ] KnockOutScheduler - generating cup
## Details
### IScheduler
+ void LoadLeague(League) - 
+ bool LoadTeams(Teams) - check number of teams (> 1, even)
+ void GenerateSchedule()
+ List\<Matches\> GetSchedule - returns generated schedule
### RoundRobinScheduler
aa.
### KnockOutScheduler
aa. - will not be implemented right now



