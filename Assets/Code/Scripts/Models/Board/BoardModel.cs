using UnityEngine.Assertions;

using CellOwnershipChange = IBoardObserver.CellOwnershipChange;

public class BoardModel : IBoardModifier, IBoardObserver
{
	public event CellOwnershipChange CellOwnershipChanged;

	private Team[][] _cellsOwnership;

	public BoardModel()
	{
		_cellsOwnership = new Team[BoardConfig.Size][];
		for (int row = 0; row < BoardConfig.Size; row++)
		{
			_cellsOwnership[row] = new Team[BoardConfig.Size];
			for (int column = 0; column < BoardConfig.Size; column++)
			{
				_cellsOwnership[row][column] = Team.None;
			}
		}
	}

	public void SetCellOwnership(int row, int column, Team newTeam)
	{
		Team cellTeam = GetCellTeam(row, column);
		if (cellTeam != newTeam)
		{
			_cellsOwnership[row][column] = newTeam;
			CellOwnershipChanged.Invoke(row, column, newTeam);
		}
	}

	public Team GetCellOwnership(int row, int column)
	{
		Team cellTeam = GetCellTeam(row, column);
		return cellTeam;
	}

	public Team GetWinnerTeam()
	{
		Team diagonalUpTeam = _cellsOwnership[0][0];
		Team diagonalDownTeam = _cellsOwnership[BoardConfig.Size - 1][0];
		
		for (int i = 0; i < BoardConfig.Size; i++)
		{
			if (diagonalUpTeam != Team.None && diagonalUpTeam != _cellsOwnership[i][i])
			{
				diagonalUpTeam = Team.None;
			}

			if (diagonalDownTeam != Team.None && diagonalDownTeam != _cellsOwnership[BoardConfig.Size - 1 -i][i])
			{
				diagonalDownTeam = Team.None;
			}
			
			Team rowTeam = _cellsOwnership[i][0];
			Team columnTeam = _cellsOwnership[0][i];

			for (int j = 1; j < BoardConfig.Size; j++)
			{
				if (rowTeam != Team.None && rowTeam != _cellsOwnership[i][j])
				{
					rowTeam = Team.None;
				}

				if (columnTeam != Team.None && columnTeam != _cellsOwnership[j][i])
				{
					columnTeam = Team.None;
				}
			}

			if (rowTeam != Team.None) return rowTeam;
			if (columnTeam != Team.None) return columnTeam;
		}

		if (diagonalUpTeam != Team.None) return diagonalUpTeam;
		if (diagonalDownTeam != Team.None) return diagonalDownTeam;

		return Team.None;
	}

	private Team GetCellTeam(int row, int column)
	{
		Assert.IsTrue(row >= 0 && row < _cellsOwnership.Length, $"Row {row} is out of bounds! Rows count: {_cellsOwnership.Length}");
		Assert.IsTrue(column >= 0 && column < _cellsOwnership[0].Length, $"Column {column} is out of bounds! Columns count: {_cellsOwnership[0].Length}");

		return _cellsOwnership[row][column];
	}
}