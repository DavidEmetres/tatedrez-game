using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(BoardTouchListener))]
public class BoardViewModel : ViewModel
{
	[SerializeField]
	private BoxCollider2D _boardBounds;
	
	private IMatchAdvancer _matchAdvancer;
	private IMatchStateObserver _matchObserver;
	private MatchConfig _matchConfig;
	private BoardTouchListener _touchListener;
	private PiecesHandler _piecesHandler;
	
	protected override void Awake()
	{
		base.Awake();
		
		Assert.IsNotNull(_boardBounds, "Missing Board bounds reference!");
		
		_touchListener = GetComponent<BoardTouchListener>();
	}

	private void OnEnable()
	{
		_touchListener.CellTouched += OnCellTouched;
		if (_matchObserver != null)
		{
			_matchObserver.TurnStarted += OnTurnStarted;
		}
	}

	private void OnDisable()
	{
		_touchListener.CellTouched -= OnCellTouched;
		if (_matchObserver != null)
		{
			_matchObserver.TurnStarted -= OnTurnStarted;
		}
	}

	public void Initialize(IBoardModifier boardModifier, IBoardObserver boardObserver, IMatchAdvancer matchAdvancer, IMatchStateObserver matchObserver, MatchConfig matchConfig, CellHighlightPool cellHightlightPool)
	{
		_matchAdvancer = matchAdvancer;
		_matchObserver = matchObserver;
		_matchConfig = matchConfig;
		_matchObserver.TurnStarted += OnTurnStarted;

		_piecesHandler = new PiecesHandler(boardModifier, boardObserver, _boardBounds, _matchConfig.PieceFactory, cellHightlightPool);
	}

	private void OnCellTouched(int row, int column)
	{
		if (_matchObserver.State == MatchState.Beginning)
		{
			int piecesPlaced = _piecesHandler.GetBoardPiecesCount(_matchObserver.PlayingTeam);
			PieceType type = _matchConfig.InitialPieces[piecesPlaced];
			_piecesHandler.InstantiatePiece(type, _matchObserver.PlayingTeam, row, column);
		}
		else if (_matchObserver.State == MatchState.InProgress)
		{
			_piecesHandler.CellSelected(row, column, _matchObserver.PlayingTeam);
		}
	}

	private void OnTurnStarted(Team team)
	{
		if (_matchObserver.State != MatchState.InProgress) return;
		
		bool movementsAvailable = _piecesHandler.AnyMovementAvailable(team);
		if (!movementsAvailable)
		{
			_matchAdvancer.NextTurn();
		}
	}
}