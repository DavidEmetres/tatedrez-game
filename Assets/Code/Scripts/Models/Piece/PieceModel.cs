public class PieceModel
{
	public Team Team { get; private set; }
	public PieceType Type { get; private set; }

	public PieceModel(Team team, PieceType type)
	{
		Team = team;
		Type = type;
	}
}