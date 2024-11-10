using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

public class MatchViewModel : ViewModel
{
	public BindableProperty<Team> PlayingTeam;
	public BindableProperty<MatchState> State;
	public BindableProperty<PieceType> PlacingPieceType;
	
	[SerializeField]
	private MatchConfig _matchConfig;
	[SerializeField]
	private CellHighlightPool _cellHightlightPool;
	
	private MatchModel _matchModel;
	private BoardModel _boardModel;
	
	protected override void Awake()
	{
		base.Awake();
		
		Assert.IsNotNull(_matchConfig, "Match config not referenced!");
		Assert.IsNotNull(_matchConfig.BoardConfig, "Board config not referenced on MatchConfig!");
		Assert.IsNotNull(_matchConfig.PieceFactory, "Piece factory not referenced on MatchConfig!");
		Assert.IsNotNull(_cellHightlightPool, "Cell highlight pool not referenced on MatchConfig!");

		_matchModel = new MatchModel();
		_matchModel.StateChanged += OnStateChanged;
		
		_boardModel = new BoardModel();
		_boardModel.CellOwnershipChanged += OnCellOwnershipChanged;
	}

	protected override void CreateBindableProperties()
	{
		PlayingTeam = CreateBindableProperty<Team>(nameof(PlayingTeam), Team.None);
		State = CreateBindableProperty<MatchState>(nameof(State), MatchState.None);
		PlacingPieceType = CreateBindableProperty<PieceType>(nameof(PlacingPieceType), PieceType.Knight);
	}

	private void Start()
	{		
		InstantiateBoard();
		_matchModel.StartPlacement();
		NextTurn();
	}

	private void InstantiateBoard()
	{
		GameObject boardGO = GameObject.Instantiate(_matchConfig.BoardConfig.Prefab);
		Assert.IsNotNull(boardGO, "Error instantiating Board prefab!");

		BoardViewModel boardVM = boardGO.GetComponent<BoardViewModel>();
		Assert.IsNotNull(boardVM, $"BoardViewModel component not found on {boardGO.name} GameObject!");

		boardVM.Initialize(_boardModel, _boardModel, _matchModel, _matchModel, _matchConfig, _cellHightlightPool);
	}

	public void ResetMatch()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	private void OnCellOwnershipChanged(int row, int column, Team newTeam)
	{
		if (newTeam == Team.None) return;

		Team winnerTeam = _boardModel.GetWinnerTeam();
		if (winnerTeam != Team.None)
		{
			_matchModel.EndMatch();
			return;
		}
		
		if (_matchModel.State == MatchState.Beginning)
		{
			NextTurn();

			int totalPiecesToBeginGame = _matchConfig.InitialPieces.Count * 2;
			if (_matchModel.TurnsPlayed == totalPiecesToBeginGame)
			{
				_matchModel.BeginMatch();
			}
			else
			{
				int pieceIndex = Mathf.FloorToInt(_matchModel.TurnsPlayed / 2.0f);
				PlacingPieceType.Value = _matchConfig.InitialPieces[pieceIndex];
			}
		}
		else if (_matchModel.State == MatchState.InProgress)
		{
			NextTurn();
		}
	}

	private void NextTurn()
	{
		_matchModel.NextTurn();
		PlayingTeam.Value = _matchModel.PlayingTeam;
	}

	private void OnStateChanged(MatchState state)
	{
		State.Value = state;
	}
}