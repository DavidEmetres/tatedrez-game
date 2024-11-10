using UnityEngine;
using UnityEngine.Assertions;

public static class BoardUtils
{
	public static Vector3 CellToWorldPosition(int row, int column, Vector2 boardSize, Vector2 boardOrigin)
	{
		Assert.IsTrue(row >= 0 && row < BoardConfig.Size, $"Row {row} is out of bounds! Rows count: {BoardConfig.Size}");
		Assert.IsTrue(column >= 0 && column < BoardConfig.Size, $"Column {column} is out of bounds! Columns count: {BoardConfig.Size}");
		
		float cellWidth = boardSize.x / BoardConfig.Size;
		float cellHeight = boardSize.y / BoardConfig.Size;
		float deltaX = column * cellWidth + cellWidth * 0.5f;
		float deltaY = row * cellHeight + cellHeight * 0.5f;

		return new Vector3(boardOrigin.x + deltaX, boardOrigin.y + deltaY, 0.0f);
	}

	public static Vector2Int WorldPositionToCell(Vector2 worldPosition, Vector2 boardSize, Vector2 boardOrigin)
	{
		Vector2 cellSize = new Vector2(boardSize.x / BoardConfig.Size, boardSize.y / BoardConfig.Size);
		float deltaX = worldPosition.x - boardOrigin.x;
		float deltaY = worldPosition.y - boardOrigin.y;

		int row = Mathf.FloorToInt(deltaY / cellSize.y);
		int column = Mathf.FloorToInt(deltaX / cellSize.x);

		Assert.IsTrue(row >= 0 && row < BoardConfig.Size, $"Row {row} is out of bounds! Rows count: {BoardConfig.Size}");
		Assert.IsTrue(column >= 0 && column < BoardConfig.Size, $"Column {column} is out of bounds! Columns count: {BoardConfig.Size}");

		return new Vector2Int(row, column);
	}
}