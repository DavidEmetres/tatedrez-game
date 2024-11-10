using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ImageBinding : Binding
{
	[SerializeField]
	private BindingProperty<Sprite> _sprite;
	[SerializeField]
	private BindingProperty<Color> _color;
	[SerializeField]
	private BindingProperty<bool> _isVisible;
	
	private Image Image
	{
		get
		{
			if (_image == null)
			{
				_image = GetComponent<Image>();
			}

			return _image;
		}
	}
	private Image _image;
	
	protected override void BindProperties()
	{
		_sprite.Bind((Sprite sprite) => Image.sprite = sprite);
		_color.Bind((Color color) => Image.color = color);
		_isVisible.Bind((bool isVisible) => Image.enabled = isVisible);
	}
}