using Extensions;
using UnityEngine;

[CreateAssetMenu(fileName = "PieceConfig", menuName = "Tatedrez/PieceConfig", order = 0)]
public class PieceConfig : ScriptableObject
{
	[SerializeField]
	private PieceType _type;
	[SerializeField, Header("Board representation:\n\n[  ][  ][  ]\n[  ][  ][  ]\n[x][  ][  ]\n\nwhere x => piece position.\n")]
	private TArray<bool> _possibleMovements = new TArray<bool>(BoardConfig.Size);

	private void OnValidate()
	{
		if (_possibleMovements.Size.x != BoardConfig.Size.x || _possibleMovements.Size.y != BoardConfig.Size.y)
		{
			_possibleMovements.Resize(BoardConfig.Size.x, BoardConfig.Size.y);
		}
	}
}