using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public abstract class TouchListener : MonoBehaviour
{
	[SerializeField]
	private BoxCollider2D _collider;

	private PlayerInput _playerInput;
	private InputAction _positionAction;
	private InputAction _isPressedAction;

	private static string _positionActionName = "Position";
	private static string _isPressedActionName = "IsPressed";

	protected virtual void Awake()
	{
		Assert.IsNotNull(_collider, "Missing reference to collider for input detection!");

		_playerInput = GetComponent<PlayerInput>();

		_positionAction = _playerInput.actions[_positionActionName];
		Assert.IsNotNull(_positionAction, $"Action with name {_positionActionName} not found!");

		_isPressedAction = _playerInput.actions[_isPressedActionName];
		Assert.IsNotNull(_isPressedAction, $"Action with name {_isPressedActionName} not found!");
	}

	private void OnEnable()
	{
		_isPressedAction.performed += OnTouchPressed;
	}

	private void OnDisable()
	{
		_isPressedAction.performed -= OnTouchPressed;
	}

	protected abstract void OnTouch(Vector3 touchPosition);

	private void OnTouchPressed(InputAction.CallbackContext context)
	{
		Vector2 screenPosition = _positionAction.ReadValue<Vector2>();
		Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
		worldPosition.z = 0.0f;

		if (_collider.bounds.Contains(worldPosition))
		{
			OnTouch(worldPosition);
		}
	}
}