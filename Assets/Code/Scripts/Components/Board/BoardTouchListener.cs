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
		Vector2Int coordinates = BoardUtils.WorldPositionToCell(touchPosition, _boardBounds.size, _boardBounds.bounds.min);

		CellTouched.Invoke(coordinates.x, coordinates.y);
	}
}