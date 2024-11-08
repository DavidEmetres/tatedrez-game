using UnityEngine;

public abstract class Binding : MonoBehaviour
{
	protected virtual void Start()
	{
		BindProperties();
	}

	protected abstract void BindProperties();
}