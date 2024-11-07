public interface IBoardObserver
{
	delegate void CellOwnershipChange(int row, int column, Team newTeam);
	event CellOwnershipChange CellOwnershipChanged;

	Team GetCellOwnership(int row, int column);
}