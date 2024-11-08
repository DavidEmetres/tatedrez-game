public interface IMatchStateObserver
{
	MatchState State { get; }
	Team PlayingTeam { get; }
}