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
	
	public GameObject CreatePieceGameObjectOfType(PieceType type, Team team)
	{
		PieceEntry entry = _pieceEntriesMap[type];
		Assert.IsNotNull(entry, $"Factory does not contain an entry for type {type}!");

		GameObject pieceGO = GameObject.Instantiate(_piecePrefab);
		Assert.IsNotNull(pieceGO, "Error instantiating Piece prefab!");

		PieceModel pieceModel = new PieceModel(team);

		// get viewmodel & inject model;

		return pieceGO;
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