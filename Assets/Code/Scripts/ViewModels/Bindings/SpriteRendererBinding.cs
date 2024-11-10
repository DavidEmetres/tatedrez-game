using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteRendererBinding : Binding
{
	[SerializeField]
	private BindingProperty<Color> Color;
	[SerializeField]
	private BindingProperty<Sprite> Sprite;

	private SpriteRenderer SpriteRenderer
	{
		get
		{
			if (_spriteRenderer == null)
			{
				_spriteRenderer = GetComponent<SpriteRenderer>();
			}

			return _spriteRenderer;
		}
	}
	private SpriteRenderer _spriteRenderer;

	protected override void BindProperties()
	{
		Color.Bind((Color color) => SpriteRenderer.color = color);
		Sprite.Bind((Sprite sprite) => SpriteRenderer.sprite = sprite);
	}
}