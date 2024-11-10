using System;
using UnityEngine;

public class MatchModel : IMatchAdvancer, IMatchStateObserver
{
	public event Action<Team> TurnStarted;
	public event Action<MatchState> StateChanged;
	
	public MatchState State => _state;
	public Team PlayingTeam => _playingTeam;
	public int TurnsPlayed => _totalTurns;
	
	private MatchState _state = MatchState.None;
	private int _totalTurns = 0;
	private Team _playingTeam = Team.None;

	public void StartPlacement()
	{
		SetState(MatchState.Beginning);
	}

	public void BeginMatch()
	{
		SetState(MatchState.InProgress);
	}

	public void EndMatch()
	{
		SetState(MatchState.End);
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

	private void SetState(MatchState state)
	{
		_state = state;
		StateChanged?.Invoke(_state);
	}
}