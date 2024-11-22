using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
	public interface IMovement
	{
		Vector2 GetMovementVector(Vector2 targetPosition, Vector2 unitPosition);
	}
}