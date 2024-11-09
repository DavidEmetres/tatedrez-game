using System.Collections.Generic;
using UnityEngine;

public class CellHighlightPool : MonoBehaviour
{
	[SerializeField]
	private GameObject _highlightCellPrefab;
	[SerializeField]
	private int _initialPoolCount;

	private IList<GameObject> _pool = new List<GameObject>();

	private void Awake()
	{
		for (int i = 0; i < _initialPoolCount; i++)
		{
			CreateHighlight();
		}
	}

	public GameObject GetHighlight()
	{
		if (_pool.Count == 0)
		{
			CreateHighlight();
		}

		GameObject highlightGO = _pool[0];
		_pool.RemoveAt(0);
		highlightGO.SetActive(true);

		return highlightGO;
	}

	public void ReturnHightlight(GameObject highlightGO)
	{
		_pool.Add(highlightGO);
		highlightGO.SetActive(false);
	}

	private void CreateHighlight()
	{
		GameObject hightlightGO = GameObject.Instantiate(_highlightCellPrefab, transform);
		hightlightGO.SetActive(false);
		_pool.Add(hightlightGO);
	}
}