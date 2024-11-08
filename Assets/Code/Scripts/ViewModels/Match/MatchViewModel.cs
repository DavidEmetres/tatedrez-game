using UnityEngine;
using UnityEngine.Assertions;

public class MatchViewModel : ViewModel
{
	[SerializeField]
	private MatchConfig _matchConfig;
	[SerializeField]
	
	private MatchModel _matchModel;
	private BoardModel _boardModel;
	
	protected override void Awake()
	{
		base.Awake();
		
		Assert.IsNotNull(_matchConfig, "Match config not referenced!");
		Assert.IsNotNull(_matchConfig.BoardConfig, "Board config not referenced on MatchConfig!");
		Assert.IsNotNull(_matchConfig.PieceFactory, "Piece factory not referenced on MatchConfig!");

		_matchModel = new MatchModel();
		_boardModel = new BoardModel();
		_boardModel.CellOwnershipChanged += OnCellOwnershipChanged;
	}

	private void Start()
	{		
		InstantiateBoard();

		_matchModel.NextTurn();
	}

	private void InstantiateBoard()
	{
		GameObject boardGO = GameObject.Instantiate(_matchConfig.BoardConfig.Prefab);
		Assert.IsNotNull(boardGO, "Error instantiating Board prefab!");

		BoardViewModel boardVM = boardGO.GetComponent<BoardViewModel>();
		Assert.IsNotNull(boardVM, $"BoardViewModel component not found on {boardGO.name} GameObject!");

		boardVM.Initialize(_boardModel, _boardModel, _matchModel, _matchConfig);
	}

	private void OnCellOwnershipChanged(int row, int column, Team newTeam)
	{
		if (newTeam == Team.None) return;
		
		if (_matchModel.State == MatchState.Beginning)
		{
			_matchModel.NextTurn();

			int totalPiecesToBeginGame = _matchConfig.InitialPieces.Count * 2;
			if (_matchModel.TurnsPlayed == totalPiecesToBeginGame)
			{
				_matchModel.BeginMatch();
			}
		}
		else if (_matchModel.State == MatchState.InProgress)
		{
			//TODO: verify win condition & finish game;
			_matchModel.NextTurn();
		}
	}
}