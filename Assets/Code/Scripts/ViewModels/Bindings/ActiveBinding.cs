using UnityEngine;

public class ActiveBinding : Binding
{
	[SerializeField]
	private BindingProperty<bool> _isActive;
	
	protected override void BindProperties()
	{
		_isActive.Bind((bool isActive) => gameObject.SetActive(isActive));
	}
}