using UnityEngine;

namespace Enemy
{
	public class NoMovement: IMovement
	{		
		public Vector2 GetMovementVector(Vector2 targetPosition, Vector2 unitPosition)
		{
			return Vector2.zero;	
		}
	}
}