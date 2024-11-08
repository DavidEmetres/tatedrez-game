using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteRendererBinding : Binding
{
	[SerializeField]
	private BindingProperty<Color> Color;
	[SerializeField]
	private BindingProperty<Sprite> Sprite;

	private SpriteRenderer SpriteRend
	{
		get
		{
			if (spriteRend == null)
			{
				spriteRend = GetComponent<SpriteRenderer>();
			}

			return spriteRend;
		}
	}
	private SpriteRenderer spriteRend;

	protected override void BindProperties()
	{
		Color.Bind((Color color) => SpriteRend.color = color);
		Sprite.Bind((Sprite sprite) => SpriteRend.sprite = sprite);
	}
}