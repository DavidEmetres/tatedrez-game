using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MatchConfig", menuName = "Tatedrez/MatchConfig", order = 0)]
public class MatchConfig : ScriptableObject
{
	public IList<PieceType> InitialPieces => _initialPieces;
	public BoardConfig BoardConfig => _boardConfig;
	public PieceFactory PieceFactory => _pieceFactory;
	
	[SerializeField]
	private BoardConfig _boardConfig;
	[SerializeField]
	private PieceFactory _pieceFactory;
	[SerializeField]
	private List<PieceType> _initialPieces;
}