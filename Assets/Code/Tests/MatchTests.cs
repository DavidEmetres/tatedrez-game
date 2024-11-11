using NUnit.Framework;

public class Match3Tests
{
    [Test]
    public void Match3_HorizontalMatch3Detection()
    {
		Team team = Team.White;
		BoardModel boardModel = new BoardModel();

		// [ ][ ][ ]
		// [ ][ ][ ]
		// [x][x][x]
		boardModel.SetCellOwnership(0, 0, team);
		boardModel.SetCellOwnership(0, 1, team);
		boardModel.SetCellOwnership(0, 2, team);
		Assert.IsTrue(boardModel.GetWinnerTeam() == team, "Horizontal match not detected!");

		boardModel = new BoardModel();

		// [ ][ ][ ]
		// [x][x][x]
		// [ ][ ][ ]
		boardModel.SetCellOwnership(1, 0, team);
		boardModel.SetCellOwnership(1, 1, team);
		boardModel.SetCellOwnership(1, 2, team);
		Assert.IsTrue(boardModel.GetWinnerTeam() == team, "Horizontal match not detected!");

		boardModel = new BoardModel();

		// [x][x][x]
		// [ ][ ][ ]
		// [ ][ ][ ]
		boardModel.SetCellOwnership(2, 0, team);
		boardModel.SetCellOwnership(2, 1, team);
		boardModel.SetCellOwnership(2, 2, team);
		Assert.IsTrue(boardModel.GetWinnerTeam() == team, "Horizontal match not detected!");
    }

	[Test]
    public void Match3_VerticalMatch3Detection()
    {
		Team team = Team.White;
		BoardModel boardModel = new BoardModel();

		// [x][ ][ ]
		// [x][ ][ ]
		// [x][ ][ ]
		boardModel.SetCellOwnership(0, 0, team);
		boardModel.SetCellOwnership(1, 0, team);
		boardModel.SetCellOwnership(2, 0, team);
		Assert.IsTrue(boardModel.GetWinnerTeam() == team, "Vertical match not detected!");

		boardModel = new BoardModel();

		// [ ][x][ ]
		// [ ][x][ ]
		// [ ][x][ ]
		boardModel.SetCellOwnership(0, 1, team);
		boardModel.SetCellOwnership(1, 1, team);
		boardModel.SetCellOwnership(2, 1, team);
		Assert.IsTrue(boardModel.GetWinnerTeam() == team, "Vertical match not detected!");

		boardModel = new BoardModel();

		// [ ][ ][x]
		// [ ][ ][x]
		// [ ][ ][x]
		boardModel.SetCellOwnership(0, 2, team);
		boardModel.SetCellOwnership(1, 2, team);
		boardModel.SetCellOwnership(2, 2, team);
		Assert.IsTrue(boardModel.GetWinnerTeam() == team, "Vertical match not detected!");
    }

	[Test]
    public void Match3_DiagonalMatch3Detection()
    {
		Team team = Team.White;
		BoardModel boardModel = new BoardModel();

		// [ ][ ][x]
		// [ ][x][ ]
		// [x][ ][ ]
		boardModel.SetCellOwnership(0, 0, team);
		boardModel.SetCellOwnership(1, 1, team);
		boardModel.SetCellOwnership(2, 2, team);
		Assert.IsTrue(boardModel.GetWinnerTeam() == team, "Diagonal match not detected!");

		boardModel = new BoardModel();

		// [x][ ][ ]
		// [ ][x][ ]
		// [ ][ ][x]
		boardModel.SetCellOwnership(2, 0, team);
		boardModel.SetCellOwnership(1, 1, team);
		boardModel.SetCellOwnership(0, 2, team);
		Assert.IsTrue(boardModel.GetWinnerTeam() == team, "Diagonal match not detected!");
    }
}
