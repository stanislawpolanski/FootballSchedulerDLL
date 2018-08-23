namespace FootballSchedulerWPF
{

    public partial class Match
    {
        public int Id { get; set; }
        public int LeagueId { get; set; }
        public System.DateTime TimeOfPlay { get; set; }
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
    }
}
