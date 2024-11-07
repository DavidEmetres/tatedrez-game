using UnityEngine;

public class MatchModel : IMatchAdvancer
{
	private MatchState _state = MatchState.Beginning;
	private int _totalTurns = 0;
	private Team _currentPlayingTeam = Team.None;

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
		if (_currentPlayingTeam == Team.None)
		{
			_currentPlayingTeam = (Team)Random.Range(1, 2);
			return;
		}

		_currentPlayingTeam = _currentPlayingTeam == Team.Black ? Team.White : Team.Black;
		_totalTurns++;
	}
}