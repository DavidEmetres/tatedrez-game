using System.Collections.Generic;
using Extensions;
using UnityEngine;

[CreateAssetMenu(fileName = "PieceConfig", menuName = "Tatedrez/PieceConfig", order = 0)]
public class PieceConfig : ScriptableObject
{
	public HashSet<Vector2Int> MovementPattern { get; private set; }
	public bool CanJumpOtherPieces => _canJumpOtherPieces;
	
	[SerializeField]
	private bool _canJumpOtherPieces;
	[SerializeField, Header("Board representation:\n\n[  ][  ][  ]\n[  ][  ][  ]\n[x][  ][  ]\n\nwhere x => piece position.\n")]
	private TArray<bool> _possibleMovements = new TArray<bool>(BoardConfig.Size, BoardConfig.Size);

	private void OnValidate()
	{
		if (_possibleMovements.Size.x != BoardConfig.Size || _possibleMovements.Size.y != BoardConfig.Size)
		{
			_possibleMovements.Resize(BoardConfig.Size, BoardConfig.Size);
		}

		MovementPattern = GetMovementPattern();
	}

	private HashSet<Vector2Int> GetMovementPattern()
	{
		int lastRowIndex = _possibleMovements.Size.y - 1;
		HashSet<Vector2Int> movements = new HashSet<Vector2Int>();
		Vector2Int movement = Vector2Int.one;
		for (int row = 0; row < _possibleMovements.Size.y; row++)
		{
			for (int column = 0; column < _possibleMovements.Size.x; column++)
			{
				if (_possibleMovements.Get(column, row))
				{
					// TArray order is inverse to what we configure in editor -> invert row;
					int invertedRow = lastRowIndex - row;

					movement.Set(invertedRow, column);
					movements.Add(movement);

					// mirror x
					movement.Set(invertedRow, -column);
					movements.Add(movement);

					// mirror y
					movement.Set(-invertedRow, column);
					movements.Add(movement);

					// mirror xy
					movement.Set(-invertedRow, -column);
					movements.Add(movement);
				}
			}
		}

		return movements;
	}
}