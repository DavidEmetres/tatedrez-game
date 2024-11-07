using UnityEngine;

[CreateAssetMenu(fileName = "BoardConfig", menuName = "Tatedrez/BoardConfig", order = 0)]
public class BoardConfig : ScriptableObject
{
	public static Vector2Int Size = new Vector2Int(3, 3);

	public GameObject Prefab => _prefab;

	[SerializeField]
	private GameObject _prefab;
}