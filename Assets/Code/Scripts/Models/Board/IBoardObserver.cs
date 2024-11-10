public interface IBoardObserver
{
	delegate void CellOwnershipChange(int row, int column, Team newTeam);
	event CellOwnershipChange CellOwnershipChanged;

	Team GetWinnerTeam();
	bool IsMovementAllowed(int row, int column, PieceModel piece);
	Team GetCellOwnership(int row, int column);
}