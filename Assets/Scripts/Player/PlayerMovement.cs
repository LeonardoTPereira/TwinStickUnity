using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
	public class PlayerMovement : MovementController
	{
		public void OnMove(InputAction.CallbackContext context)
		{
			if (context.canceled)
			{
				Movement = Vector2.zero;
				return;
			}
			if (!context.performed) return;
			var movement = context.ReadValue<Vector2>();
			movement.Normalize();
			Movement = movement * Speed;
			UpdateRotation(movement);
		}
	}
}
