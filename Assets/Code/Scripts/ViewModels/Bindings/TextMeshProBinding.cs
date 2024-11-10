using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextMeshProBinding : Binding
{
	[SerializeField]
	private BindingProperty<string> Text;
	[SerializeField]
	private BindingProperty<Color> Color;
	
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
		Text.Bind((string text) => TextMeshPro.text = text);
		Color.Bind((Color color) => TextMeshPro.color = color);
	}
}