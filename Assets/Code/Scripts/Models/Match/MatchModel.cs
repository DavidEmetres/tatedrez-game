using UnityEngine;

public class MatchModel : IMatchAdvancer
{
	int _totalTurns = 0;
	Team _currentPlayingTeam = Team.None;

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