using System;
using UnityEngine;

[Serializable]
public class BindingProperty<T>
{	
	public event Action<T> OnValueChanged;
	
	[SerializeField]
	private GameObject _bindedGameObject;
	[SerializeField]
	private string _bindedViewModelType;
	[SerializeField]
	private string _bindedPropertyName;
	
	private BindableProperty<T> _bindedProperty;

	public void Bind(Action<T> onValueChanged = null)
	{
		if (_bindedGameObject == null || string.IsNullOrEmpty(_bindedViewModelType) || string.IsNullOrEmpty(_bindedPropertyName)) return;

		ViewModel[] viewModels = _bindedGameObject.GetComponents<ViewModel>();
		for (int i = 0; i < viewModels.Length; ++i)
		{
			ViewModel viewModel = viewModels[i];
			if (viewModel.GetType().Name != _bindedViewModelType) continue;

			_bindedProperty = viewModel.GetBindableProperty<T>(_bindedPropertyName);
			if (_bindedProperty != null)
			{
				_bindedProperty.OnValueChanged += OnBindedPropertyValueChanged;
			}

			// notify on bind
			if (onValueChanged != null)
			{
				OnValueChanged += onValueChanged;
				OnBindedPropertyValueChanged(GetValue());
			}
		}
	}

	public T GetValue()
	{
		if (_bindedProperty == null) return default;

		return _bindedProperty.Value;
	}

	private void OnBindedPropertyValueChanged(T value)
	{
		OnValueChanged?.Invoke(value);
	}
}