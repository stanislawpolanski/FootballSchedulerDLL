# FootballScheduler library
The library contains classes to generate fixtures.
## Checklist
Classes:
+ [ ] IScheduler - interface for communication
+ [ ] Team - basic team class with its identifier
+ [ ] League - list of teams, league's id and starting year
+ [ ] Match - match contains team's id, datetime and league's id
+ [ ] RoundRobinScheduler - generating league fixtures
+ [ ] KnockOutScheduler - generating cup
## Details
### IScheduler
+ bool LoadLeague(League) - check number of teams
+ void GenerateSchedule()
+ List\<Matches\> GetSchedule - returns generated schedule
### Team
int Id
### League
+ List\<Team\> - list of teams
+ int Id
+ Year (datetime?) StartDate
### Match
+ Team HomeTeam, AwayTime
+ DateTime TimeOfPlay
+ int LeagueId
### RoundRobinScheduler
aa.
### KnockOutScheduler
aa.



