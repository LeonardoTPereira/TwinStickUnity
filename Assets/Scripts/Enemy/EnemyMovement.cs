using System.Collections;
using UnityEngine;

namespace Enemy
{
	public class EnemyMovement : MovementController
	{
		[field: SerializeField] public Movements MovementType { get; protected set; }
		[field: SerializeField] public float RestTime { get; protected set; }
		[field: SerializeField] public float ActiveTime { get; protected set; }

		private IMovement _movementStrategy;
		private bool _isFixedMovement;
		private bool _is1DMovement;
		private GameObject _player;

		protected override void Awake()
		{
			base.Awake();
			IsAlive = true;
			_isFixedMovement = false;
			_is1DMovement = false;
		}

		protected override void Start()
		{
			base.Start();
			StartCoroutine(WalkAndWait());
			_movementStrategy = GetMovementStrategy();
			_player = GameObject.Find("Player");
		}

		public enum Movements
		{
			None,
			Random,
			Follow,
			Flee,
			Random1D,
			Follow1D,
			Flee1D,
			Count
		}

		private IMovement GetMovementStrategy()
		{
			switch (MovementType)
			{
				case Movements.None:
					_isFixedMovement = true;
					return new NoMovement();
				case Movements.Random:
					_isFixedMovement = true;
					return new RandomMovement();
				case Movements.Flee:
					return new FleeMovement();
				case Movements.Follow:
					return new FollowMovement();
				case Movements.Follow1D:
					_is1DMovement = true;
					return new FollowMovement();
				case Movements.Random1D:
					_isFixedMovement = true;
					_is1DMovement = true;
					return new RandomMovement();
				case Movements.Flee1D:
					_is1DMovement = true;
					return new FleeMovement();
				default:
					Debug.LogError("No Movement Attached to Enemy");
					return null;
			}
		}

		private IEnumerator WalkAndWait()
		{
			while (IsAlive)
			{
				yield return new WaitForSeconds(RestTime);
				yield return StartCoroutine(Walk());
				Wait();
			}
		}

		private IEnumerator Walk()
		{

			var timeWalked = 0.0f;
			var directionMask = new Vector2(1, 1);
			if (_is1DMovement)
				directionMask = Random.value < 0.5f ? new Vector2(0, 1) : new Vector2(1, 0);
			Movement = GetMovementVector(directionMask);
			if (_isFixedMovement)
			{
				yield return new WaitForSeconds(ActiveTime);
			}
			else
			{
				while (timeWalked < ActiveTime)
				{
					Movement = GetMovementVector(directionMask);
					timeWalked += Time.deltaTime;
					yield return null;
				}
			}
		}

		private Vector2 GetMovementVector(Vector2 directionMask)
		{
			var playerPosition = _player.transform.position;
			var movement = _movementStrategy.GetMovementVector(playerPosition, transform.position);
			movement.Normalize();
			movement *= directionMask;
			return movement;
		}

		private void Wait()
		{
			Movement = Vector2.zero;
		}
	}
}
