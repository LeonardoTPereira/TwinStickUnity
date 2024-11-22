using UnityEngine;

namespace Enemy
{
	public class FleeMovement : IMovement
	{
		public Vector2 GetMovementVector(Vector2 targetPosition, Vector2 unitPosition)
		{
			return unitPosition - targetPosition;	
		}
	}
}