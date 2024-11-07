public class PieceModel
{
	public Team Team { get; private set; }

	public PieceModel(Team team)
	{
		Team = team;
	}
}