using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextMeshProBinding : Binding
{
	[SerializeField]
	private BindingProperty<string> _text;
	[SerializeField]
	private BindingProperty<Color> _color;
	[SerializeField]
	private BindingProperty<bool> _isVisible;
	
	private TextMeshProUGUI TextMeshPro
	{
		get
		{
			if (_textMeshPro == null)
			{
				_textMeshPro = GetComponent<TextMeshProUGUI>();
			}

			return _textMeshPro;
		}
	}
	private TextMeshProUGUI _textMeshPro;
	
	protected override void BindProperties()
	{
		_text.Bind((string text) => TextMeshPro.text = text);
		_color.Bind((Color color) => TextMeshPro.color = color);
		_isVisible.Bind((bool isVisible) => TextMeshPro.enabled = isVisible);
	}
}