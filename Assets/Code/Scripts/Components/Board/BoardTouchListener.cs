using System;
using UnityEngine;
using UnityEngine.Assertions;

public class BoardTouchListener : TouchListener
{
	public delegate void CellTouch(int row, int column);
	public event CellTouch CellTouched;
	
	[SerializeField]
	private BoxCollider2D _boardBounds;
	
	protected override void Awake()
	{
		base.Awake();
		
		Assert.IsNotNull(_boardBounds, "Missing Board bounds reference!");
	}
	
	protected override void OnTouch(Vector3 touchPosition)
	{
		Vector2 cellSize = new Vector2(_boardBounds.size.x/BoardConfig.Size.x, _boardBounds.size.y/BoardConfig.Size.y);
		float deltaX = touchPosition.x - _boardBounds.bounds.min.x;
		float deltaY = touchPosition.y - _boardBounds.bounds.min.y;

		int row = Mathf.FloorToInt(deltaY/cellSize.y);
		int column = Mathf.FloorToInt(deltaX/cellSize.x);

		Assert.IsTrue(row >= 0 && row < BoardConfig.Size.x, $"Row {row} is out of bounds! Rows count: {BoardConfig.Size.x}");
		Assert.IsTrue(column >= 0 && column < BoardConfig.Size.y, $"Column {column} is out of bounds! Columns count: {BoardConfig.Size.y}");

		CellTouched.Invoke(row, column);
	}
}