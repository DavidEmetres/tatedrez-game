using UnityEngine;

[CreateAssetMenu(fileName = "BoardConfig", menuName = "Tatedrez/BoardConfig", order = 0)]
public class BoardConfig : ScriptableObject
{
	public int RowCount => _rowCount;
	public int ColumnCount => _columnCount;

	// row & column count cannot be adjusted due to test scope;
	// game logic restricted to a 3x3 board;
	[SerializeField, Range(3, 3)]
	private int _rowCount = 3;
	[SerializeField, Range(3, 3)]
	private int _columnCount = 3;
}