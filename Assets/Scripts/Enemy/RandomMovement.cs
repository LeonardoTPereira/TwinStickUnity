using UnityEngine;

namespace Enemy
{
	public class RandomMovement : IMovement
	{
		public Vector2 GetMovementVector(Vector2 targetPosition, Vector2 unitPosition)
		{
			return new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
		}
	}
}