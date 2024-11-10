using System;
using UnityEngine;

public class MatchModel : IMatchAdvancer, IMatchStateObserver
{
	public event Action<Team> TurnStarted;
	
	public MatchState State => _state;
	public Team PlayingTeam => _playingTeam;
	public int TurnsPlayed => _totalTurns;
	
	private MatchState _state = MatchState.Beginning;
	private int _totalTurns = 0;
	private Team _playingTeam = Team.None;

	public void BeginMatch()
	{
		_state = MatchState.InProgress;
	}

	public void EndMatch()
	{
		_state = MatchState.End;
	}

	public void NextTurn()
	{
		if (_playingTeam == Team.None)
		{
			_playingTeam = (Team)UnityEngine.Random.Range(1, 3);
			return;
		}

		_playingTeam = _playingTeam == Team.Black ? Team.White : Team.Black;
		_totalTurns++;

		TurnStarted?.Invoke(_playingTeam);
	}
}