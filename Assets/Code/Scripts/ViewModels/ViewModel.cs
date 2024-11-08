using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class ViewModel : MonoBehaviour
{
	private readonly Dictionary<string, object> _bindableProperties = new Dictionary<string, object>();
	
	protected virtual void Awake()
	{
		CreateBindableProperties();
	}
	
	public BindableProperty<T> GetBindableProperty<T>(string name)
	{
		if (_bindableProperties.TryGetValue(name, out object property))
		{
			return (BindableProperty<T>)property;
		}

		return null;
	}
	
	protected BindableProperty<T> CreateBindableProperty<T>(string name, T defaultValue = default)
	{
		Assert.IsFalse(_bindableProperties.ContainsKey(name));

		BindableProperty<T> property = new BindableProperty<T>();
		property.Value = defaultValue;

		_bindableProperties.Add(name, property);

		return property;
	}

	protected virtual void CreateBindableProperties()
	{
	}
}