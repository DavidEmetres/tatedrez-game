using System;

[Serializable]
public class BindableProperty<T>
{
	public event Action<T> OnValueChanged;
	
	public T Value
	{
		get => _value;
		set
		{
			_value = value;
			OnValueChanged?.Invoke(_value);
		}
	}

	private T _value;
}