using UnityEngine;

[RequireComponent(typeof(BoardTouchListener))]
public class BoardViewModel : MonoBehaviour
{
	private IBoardModifier _boardModifier;
	private IBoardObserver _boardObserver;
	private IMatchStateObserver _matchObserver;
	private BoardTouchListener _touchListener;
	
	private void Awake()
	{
		_touchListener = GetComponent<BoardTouchListener>();
	}

	private void OnEnable()
	{
		_touchListener.CellTouched += OnCellTouched;
	}

	private void OnDisable()
	{
		_touchListener.CellTouched -= OnCellTouched;
	}

	public void Initialize(IBoardModifier boardModifier, IBoardObserver boardObserver, IMatchStateObserver matchObserver)
	{
		_boardModifier = boardModifier;
		_boardObserver = boardObserver;
		_matchObserver = matchObserver;
	}

	private void OnCellTouched(int row, int column)
	{
		if (_matchObserver.State != MatchState.InProgress)
		{
			return;
		}
	}
}