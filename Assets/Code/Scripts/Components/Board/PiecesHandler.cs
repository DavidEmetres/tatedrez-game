using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PiecesHandler
{
	private class BoardPieceInfo
	{
		public PieceViewModel PieceVM;
		public int Row;
		public int Column;
		public Team Team;

		public BoardPieceInfo(PieceViewModel pieceVM, int row, int column, Team team)
		{
			PieceVM = pieceVM;
			Row = row;
			Column = column;
			Team = team;
		}
	}
	
	private IBoardModifier _boardModifier;
	private IBoardObserver _boardObserver;
	private BoxCollider2D _boardBounds;
	private PieceFactory _pieceFactory;
	private IDictionary<Team, IList<BoardPieceInfo>> _piecesInBoard;
	private BoardPieceInfo _selectedPiece;

	public PiecesHandler(IBoardModifier boardModifier, IBoardObserver boardObserver, BoxCollider2D boardBounds, PieceFactory pieceFactory)
	{
		_boardModifier = boardModifier;
		_boardObserver = boardObserver;
		_boardBounds = boardBounds;
		_pieceFactory = pieceFactory;
		_piecesInBoard = new Dictionary<Team, IList<BoardPieceInfo>>();
	}

	public void InstantiatePiece(PieceType type, Team team, int row, int column)
	{
		Team cellOwnership = _boardObserver.GetCellOwnership(row, column);
		if (cellOwnership != Team.None) return;
		
		PieceViewModel pieceVM = _pieceFactory.CreatePieceGameObjectOfType(type, team);
		pieceVM.transform.position = BoardUtils.CellToWorldPosition(row, column, _boardBounds.size, _boardBounds.bounds.min);

		BoardPieceInfo boardPiece = new BoardPieceInfo(pieceVM, row, column, team);
		if (_piecesInBoard.TryGetValue(team, out IList<BoardPieceInfo> pieces))
		{
			pieces.Add(boardPiece);
		}
		else
		{
			_piecesInBoard.Add(team, new List<BoardPieceInfo>() { boardPiece });
		}

		_boardModifier.SetCellOwnership(row, column, team);
	}

	public void CellSelected(int row, int column, Team team)
	{
		Team cellOwnership = _boardObserver.GetCellOwnership(row, column);
		if (cellOwnership == team)
		{
			SelectPiece(row, column, team);
		}
		else if (cellOwnership == Team.None && _selectedPiece != null)
		{
			PlaceSelectedPiece(row, column);
		}
	}

	public int GetBoardPiecesCount(Team team)
	{
		if (_piecesInBoard.TryGetValue(team, out IList<BoardPieceInfo> pieces))
		{
			return pieces.Count;
		}

		return 0;
	}

	private void PlaceSelectedPiece(int row, int column)
	{
		Assert.IsNotNull(_selectedPiece, "No piece selected!");

		_boardModifier.SetCellOwnership(_selectedPiece.Row, _selectedPiece.Column, Team.None);

		_selectedPiece.Row = row;
		_selectedPiece.Column = column;
		_selectedPiece.PieceVM.transform.position = BoardUtils.CellToWorldPosition(row, column, _boardBounds.size, _boardBounds.bounds.min);

		_boardModifier.SetCellOwnership(row, column, _selectedPiece.Team);

		_selectedPiece = null;
	}

	public void SelectPiece(int row, int column, Team team)
	{
		// TODO: Improve piece search;
		IList<BoardPieceInfo> pieces = _piecesInBoard[team];
		for (int i = 0; i < pieces.Count; i++)
		{
			BoardPieceInfo piece = pieces[i];
			if (piece.Row == row && piece.Column == column)
			{
				_selectedPiece = piece;
				return;
			}
		}
	}
}