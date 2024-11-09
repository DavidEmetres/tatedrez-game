using System.Collections.Generic;
using System.Numerics;
using Extensions;
using UnityEngine;

[CreateAssetMenu(fileName = "PieceConfig", menuName = "Tatedrez/PieceConfig", order = 0)]
public class PieceConfig : ScriptableObject
{
	public IReadOnlyList<Vector2Int> MovementPattern { get; private set; }
	
	[SerializeField, Header("Board representation:\n\n[  ][  ][  ]\n[  ][  ][  ]\n[x][  ][  ]\n\nwhere x => piece position.\n")]
	private TArray<bool> _possibleMovements = new TArray<bool>(BoardConfig.Size);

	private void OnValidate()
	{
		if (_possibleMovements.Size.x != BoardConfig.Size.x || _possibleMovements.Size.y != BoardConfig.Size.y)
		{
			_possibleMovements.Resize(BoardConfig.Size.x, BoardConfig.Size.y);
		}

		MovementPattern = GetMovementPattern();
	}

	private IReadOnlyList<Vector2Int> GetMovementPattern()
	{
		int lastRowIndex = _possibleMovements.Size.y - 1;
		List<Vector2Int> movements = new List<Vector2Int>();
		for (int row = 0; row < _possibleMovements.Size.y; row++)
		{
			for (int column = 0; column < _possibleMovements.Size.x; column++)
			{
				if (_possibleMovements.Get(column, row))
				{
					// TArray order is inverse to what we configure in editor -> invert row;
					int invertedRow = lastRowIndex - row;

					movements.Add(new Vector2Int(invertedRow, column));
					// mirror x
					movements.Add(new Vector2Int(invertedRow, -column));
					// mirror y
					movements.Add(new Vector2Int(-invertedRow, column));
					// mirror xy
					movements.Add(new Vector2Int(-invertedRow, -column));
				}
			}
		}

		return movements;
	}
}