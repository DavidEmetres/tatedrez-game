public class PieceViewModel : ViewModel
{
	public BindableProperty<Team> PieceTeam;
	public BindableProperty<PieceType> Type;
	public BindableProperty<bool> IsSelected;

	public void Initialize(PieceModel pieceModel)
	{
		PieceTeam.Value = pieceModel.Team;
		Type.Value = pieceModel.Type;
		
		pieceModel.SelectionChanged += OnSelectionChanged;
	}
	
	protected override void CreateBindableProperties()
	{
		PieceTeam = CreateBindableProperty<Team>(nameof(PieceTeam), Team.None);
		Type = CreateBindableProperty<PieceType>(nameof(Type), PieceType.Knight);
		IsSelected = CreateBindableProperty<bool>(nameof(IsSelected), false);
	}

	private void OnSelectionChanged(bool isSelected)
	{
		IsSelected.Value = isSelected;
	}
}