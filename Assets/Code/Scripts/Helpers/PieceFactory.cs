using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.Assertions;

[Serializable]
public class PieceEntry
{
	[SerializeField]
	public PieceType Type;
	[SerializeField]
	public PieceConfig Config;
}

[CreateAssetMenu(fileName = "PieceFactory", menuName = "Tatedrez/PieceFactory", order = 0)]
public class PieceFactory : ScriptableObject
{
	[SerializeField]
	private GameObject _piecePrefab;
	[SerializeField]
	private PieceEntry[] _pieceEntries;

	private IDictionary<PieceType, PieceEntry> _pieceEntriesMap = new Dictionary<PieceType, PieceEntry>();
	
	public PieceViewModel CreatePieceGameObjectOfType(PieceType type, Team team)
	{
		PieceEntry entry = _pieceEntriesMap[type];
		Assert.IsNotNull(entry, $"Factory does not contain an entry for type {type}!");

		GameObject pieceGO = GameObject.Instantiate(_piecePrefab);
		Assert.IsNotNull(pieceGO, "Error instantiating Piece prefab!");

		PieceViewModel pieceVM = pieceGO.GetComponent<PieceViewModel>();
		Assert.IsNotNull(pieceVM, "Piece prefab does not have a view model attached!");

		PieceModel pieceModel = new PieceModel(team, type);
		pieceVM.Initialize(pieceModel);

		return pieceVM;
	}
	
	private void OnValidate()
	{
		_pieceEntriesMap.Clear();
		for (int i = 0; i < _pieceEntries.Length; i++)
		{
			PieceEntry entry = _pieceEntries[i];
			_pieceEntriesMap[entry.Type] = entry;
		}
	}
}