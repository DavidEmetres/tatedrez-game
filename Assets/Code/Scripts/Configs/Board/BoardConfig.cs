using UnityEngine;

[CreateAssetMenu(fileName = "BoardConfig", menuName = "Tatedrez/BoardConfig", order = 0)]
public class BoardConfig : ScriptableObject
{
	public static int Size = 3;

	public GameObject Prefab => _prefab;

	[SerializeField]
	private GameObject _prefab;
}