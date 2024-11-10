using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class EnumToConverter<T1,T2> : ViewModel where T1 : struct, IComparable, IFormattable, IConvertible
{
	[Serializable]
	private struct EnumOutputPair
	{
		[SerializeField]
		public T1 EnumValue;
		[SerializeField]
		public T2 OutputValue;
	}
	
	public BindableProperty<T2> Output;
	
	[SerializeField]
	private BindingProperty<T1> _enum;
	[SerializeField]
	private EnumOutputPair[] _enumOutputMap;

	private IDictionary<T1, T2> _map = new Dictionary<T1,T2>();
	
	public EnumToConverter()
	{
		Assert.IsTrue(typeof(T1).IsEnum);
	}

	protected override void Awake()
	{
		base.Awake();

		for (int i = 0; i < _enumOutputMap.Length; i++)
		{
			EnumOutputPair pair = _enumOutputMap[i];
			_map.Add(pair.EnumValue, pair.OutputValue);
		}
	}

	private void Start()
	{
		BindProperties();
	}

	protected override void CreateBindableProperties()
	{
		Output = CreateBindableProperty<T2>(nameof(Output), default);
	}

	private void BindProperties()
	{
		_enum.Bind((T1 EnumValue) => Output.Value = GetOutputForEnum(EnumValue));
	}

	private T2 GetOutputForEnum(T1 enumValue)
	{
		if (_map.TryGetValue(enumValue, out T2 output))
		{
			return output;
		}
		
		return default;
	}
}