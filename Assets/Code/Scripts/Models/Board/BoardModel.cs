using UnityEngine.Assertions;

using CellOwnershipChange = IBoardObserver.CellOwnershipChange;

public class BoardModel : IBoardModifier, IBoardObserver
{
	public event CellOwnershipChange CellOwnershipChanged;

	private CellModel[][] _cells;

	public BoardModel()
	{
		_cells = new CellModel[BoardConfig.Size.x][];
		for (int row = 0; row < BoardConfig.Size.x; row++)
		{
			_cells[row] = new CellModel[BoardConfig.Size.x];
			for (int column = 0; column < BoardConfig.Size.y; column++)
			{
				_cells[row][column] = new CellModel();
			}
		}
	}

	public void SetCellOwnership(int row, int column, Team newTeam)
	{
		CellModel cell = GetCell(row, column);
		if (cell.OwningTeam != newTeam)
		{
			cell.OwningTeam = newTeam;
			CellOwnershipChanged.Invoke(row, column, newTeam);
		}
	}

	public Team GetCellOwnership(int row, int column)
	{
		CellModel cell = GetCell(row, column);
		return cell.OwningTeam;
	}

	private CellModel GetCell(int row, int column)
	{
		Assert.IsTrue(row >= 0 && row < _cells.Length, $"Row {row} is out of bounds! Rows count: {_cells.Length}");
		Assert.IsTrue(column >= 0 && column < _cells[0].Length, $"Column {column} is out of bounds! Columns count: {_cells[0].Length}");

		CellModel cell = _cells[row][column];
		Assert.IsNotNull(cell, $"Null {nameof(CellModel)} at row {row} and column {column}!");

		return cell;
	}
}