using UnityEngine;

public class FloatAnimator : Binding
{
	[SerializeField]
	private BindingProperty<bool> _isAnimated;
	[SerializeField]
	private Vector2 _minMaxScale;
	[SerializeField]
	private float _floatSpeed;
	
	private float _floatDelta;
	private int _floatDirection = 1;

	private void Update()
	{
		_floatDelta += Time.deltaTime * _floatDirection * _floatSpeed;
		if (_floatDelta > 1.0f)
		{
			_floatDelta = 1.0f;
			_floatDirection = -1;
		}
		else if (_floatDelta < 0.0f)
		{
			_floatDelta = 0.0f;
			_floatDirection = 1;
		}
		
		float scale = Mathf.Lerp(_minMaxScale.x, _minMaxScale.y, _floatDelta);
		gameObject.transform.localScale = _isAnimated.GetValue() ? Vector3.one * scale : Vector3.one;
	}

	protected override void BindProperties()
	{
		_isAnimated.Bind();
	}
}