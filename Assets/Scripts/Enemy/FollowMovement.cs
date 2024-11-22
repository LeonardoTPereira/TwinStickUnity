using UnityEngine;

namespace Enemy
{
	public class FollowMovement : IMovement
	{
		public Vector2 GetMovementVector(Vector2 targetPosition, Vector2 unitPosition)
		{
			return targetPosition - unitPosition;
		}
	}
}
