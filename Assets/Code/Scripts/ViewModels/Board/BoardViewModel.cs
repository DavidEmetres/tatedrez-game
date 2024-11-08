using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(BoardTouchListener))]
public class BoardViewModel : ViewModel
{
	[SerializeField]
	private BoxCollider2D _boardBounds;
	
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
	}

	private void OnDisable()
	{
		_touchListener.CellTouched -= OnCellTouched;
	}

	public void Initialize(IBoardModifier boardModifier, IBoardObserver boardObserver, IMatchStateObserver matchObserver, MatchConfig matchConfig)
	{
		_matchObserver = matchObserver;
		_matchConfig = matchConfig;

		_piecesHandler = new PiecesHandler(boardModifier, boardObserver, _boardBounds, _matchConfig.PieceFactory);
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
}