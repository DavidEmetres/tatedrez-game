public class PieceViewModel : ViewModel
{
	public BindableProperty<Team> PieceTeam;
	public BindableProperty<PieceType> Type;
	
	public void Initialize(PieceModel pieceModel)
	{
		PieceTeam.Value = pieceModel.Team;
		Type.Value = pieceModel.Type;
	}
	
	protected override void CreateBindableProperties()
	{
		PieceTeam = CreateBindableProperty<Team>(nameof(PieceTeam), Team.None);
		Type = CreateBindableProperty<PieceType>(nameof(Type), PieceType.Knight);
	}
}