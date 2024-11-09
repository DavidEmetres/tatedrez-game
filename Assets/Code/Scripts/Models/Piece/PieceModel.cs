using System.Collections.Generic;
using UnityEngine;

public class PieceModel
{
	public Team Team { get; private set; }
	public PieceType Type { get; private set; }
	public int Row { get; private set; }
	public int Column { get; private set; }

	private PieceConfig _pieceConfig;
	
	public PieceModel(Team team, PieceType type, PieceConfig pieceConfig, int row, int column)
	{
		Team = team;
		Type = type;
		_pieceConfig = pieceConfig;
		Row = row;
		Column = column;
	}

	public void Place(int row, int column)
	{
		Row = row;
		Column = column;
	}

	public bool IsMovementAllowed(int row, int column)
	{
		for (int i = 0; i < _pieceConfig.MovementPattern.Count; i++)
		{
			Vector2Int movement = _pieceConfig.MovementPattern[i];
			Vector2Int actualMovement = new Vector2Int(Row + movement.x, Column + movement.y);
			if (actualMovement.x == row && actualMovement.y == column)
			{
				// movement is part of movement pattern;
				return true;
			}
		}

		return false;
	}

	public IEnumerable<Vector2Int> GetPotentialMovements(int row, int column)
	{
		Vector2Int[] possibleMovements = new Vector2Int[_pieceConfig.MovementPattern.Count];
		for (int i = 0; i < _pieceConfig.MovementPattern.Count; i++)
		{
			Vector2Int movement = _pieceConfig.MovementPattern[i];
			possibleMovements[i] = new Vector2Int(row + movement.x, column + movement.y);
		}

		return possibleMovements;
	}
}