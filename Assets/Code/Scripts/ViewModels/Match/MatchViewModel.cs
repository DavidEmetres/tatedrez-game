using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class MatchViewModel : MonoBehaviour
{
	[SerializeField]
	private MatchConfig _matchConfig;
	[SerializeField]
	private BoardConfig _boardConfig;
	[SerializeField]
	private PieceFactory _pieceFactory;
	
	private IMatchAdvancer _matchAdvancer;
	private IBoardObserver _boardObserver;
	private GameObject[] _whitePieces;
	private GameObject[] _blackPieces;
	
	private void Awake()
	{
		Assert.IsNotNull(_matchConfig, "Match config not referenced!");
		Assert.IsNotNull(_boardConfig, "Board config not referenced!");
		Assert.IsNotNull(_pieceFactory, "Piece factory not references!");

		_matchAdvancer = new MatchModel();

		// observer pattern;
		_boardObserver = new BoardModel();
		_boardObserver.CellOwnershipChanged += OnCellOwnershipChanged;
	}

	private void Start()
	{		
		InstantiateBoard();
		InstantiatePieces();
	}

	private void InstantiateBoard()
	{
		GameObject boardGO = GameObject.Instantiate(_boardConfig.Prefab);
		Assert.IsNotNull(boardGO, "Error instantiating Board prefab!");

		BoardViewModel boardVM = boardGO.GetComponent<BoardViewModel>();
		Assert.IsNotNull(boardVM, $"BoardViewModel component not found on {boardGO.name} GameObject!");
	}

	private void InstantiatePieces()
	{
		_whitePieces = new GameObject[_matchConfig.PiecesToUseCount];
		_blackPieces = new GameObject[_matchConfig.PiecesToUseCount];
		using (IEnumerator<PieceType> piecesEnumerator = _matchConfig.PiecesToUse.GetEnumerator())
		{
			int index = 0;
			while (piecesEnumerator.MoveNext())
			{
				PieceType pieceType = piecesEnumerator.Current;
				GameObject whitePieceGO = _pieceFactory.CreatePieceGameObjectOfType(pieceType, Team.White);
				_whitePieces[index] = whitePieceGO;
				GameObject blackPieceGO = _pieceFactory.CreatePieceGameObjectOfType(pieceType, Team.Black);
				_blackPieces[index] = blackPieceGO;
				index++;
			}
		}
	}

	private void OnCellOwnershipChanged(int row, int column, Team newTeam)
	{
		// verify win condition & finish game;
	}
}