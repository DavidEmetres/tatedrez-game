using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MatchConfig", menuName = "Tatedrez/MatchConfig", order = 0)]
public class MatchConfig : ScriptableObject
{
	public IEnumerable<PieceType> PiecesToUse => _piecesToUse;
	public int PiecesToUseCount => _piecesToUse.Length;
	
	[SerializeField]
	private PieceType[] _piecesToUse;
}