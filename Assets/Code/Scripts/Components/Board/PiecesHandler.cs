using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PiecesHandler
{
	private class BoardPieceInfo
	{
		public PieceModel PieceModel;
		public GameObject PieceGO;

		public BoardPieceInfo(PieceModel pieceModel, GameObject pieceGO)
		{
			PieceModel = pieceModel;
			PieceGO = pieceGO;
		}
	}
	
	private IBoardModifier _boardModifier;
	private IBoardObserver _boardObserver;
	private BoxCollider2D _boardBounds;
	private PieceFactory _pieceFactory;
	private IDictionary<Team, IList<BoardPieceInfo>> _piecesInBoard;
	private BoardPieceInfo _selectedPiece;
	private IReadOnlyList<Vector2Int> _pieceMovements;

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
		
		PieceModel pieceModel = new PieceModel(team, type, _pieceFactory.GetPieceConfig(type), row, column);
		
		GameObject pieceGO = _pieceFactory.CreatePieceGameObjectOfType(pieceModel);
		pieceGO.transform.position = BoardUtils.CellToWorldPosition(row, column, _boardBounds.size, _boardBounds.bounds.min);

		BoardPieceInfo boardPiece = new BoardPieceInfo(pieceModel, pieceGO);
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

		if (!_selectedPiece.PieceModel.IsMovementAllowed(row, column)) return;

		_boardModifier.SetCellOwnership(_selectedPiece.PieceModel.Row, _selectedPiece.PieceModel.Column, Team.None);

		_selectedPiece.PieceModel.Place(row, column);
		_selectedPiece.PieceGO.transform.position = BoardUtils.CellToWorldPosition(row, column, _boardBounds.size, _boardBounds.bounds.min);

		_boardModifier.SetCellOwnership(row, column, _selectedPiece.PieceModel.Team);

		_selectedPiece = null;
	}

	private void SelectPiece(int row, int column, Team team)
	{
		// TODO: Improve piece search;
		IList<BoardPieceInfo> pieces = _piecesInBoard[team];
		for (int i = 0; i < pieces.Count; i++)
		{
			BoardPieceInfo piece = pieces[i];
			if (piece.PieceModel.Row == row && piece.PieceModel.Column == column)
			{
				_selectedPiece = piece;
				return;
			}
		}
	}
}