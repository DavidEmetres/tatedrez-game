using System;
using System.Collections.Generic;
using UnityEngine;

public class PieceModel
{
	public Action<bool> SelectionChanged;
	
	public Team Team { get; private set; }
	public PieceType Type { get; private set; }
	public int Row { get; private set; }
	public int Column { get; private set; }
	public bool IsSelected
	{
		get => _isSelected;
		set
		{
			_isSelected = value;
			SelectionChanged?.Invoke(_isSelected);
		}
	}
	public bool CanJumpOtherPieces => _pieceConfig.CanJumpOtherPieces;

	private PieceConfig _pieceConfig;
	private bool _isSelected;
	
	public PieceModel(Team team, PieceType type, PieceConfig pieceConfig, int row, int column)
	{
		Team = team;
		Type = type;
		_pieceConfig = pieceConfig;
		Row = row;
		Column = column;
		IsSelected = false;
	}

	public void Place(int row, int column)
	{
		Row = row;
		Column = column;
	}

	public bool IsMovementAllowed(int row, int column)
	{
		Vector2Int normalizedCoordinates = new Vector2Int(row - Row, column - Column);
		return _pieceConfig.MovementPattern.Contains(normalizedCoordinates);
	}

	public IEnumerable<Vector2Int> GetPotentialMovements()
	{
		IEnumerator<Vector2Int> enumerator = _pieceConfig.MovementPattern.GetEnumerator();
		HashSet<Vector2Int> potentialMovements = new HashSet<Vector2Int>();
		while (enumerator.MoveNext())
		{
			Vector2Int movement = enumerator.Current;
			potentialMovements.Add(new Vector2Int(Row + movement.x, Column + movement.y));
		}

		return potentialMovements;
	}
}