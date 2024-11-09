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
		IEnumerator<Vector2Int> enumerator = _pieceConfig.MovementPattern.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Vector2Int movement = enumerator.Current;
			Vector2Int actualMovement = new Vector2Int(Row + movement.x, Column + movement.y);
			if (actualMovement.x == row && actualMovement.y == column)
			{
				return true;
			}
		}

		return false;
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