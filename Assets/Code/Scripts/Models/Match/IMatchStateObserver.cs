using System;

public interface IMatchStateObserver
{
	event Action<Team> TurnStarted;
	
	MatchState State { get; }
	Team PlayingTeam { get; }
	int TurnsPlayed { get; }
}