using UnityEngine;

public class MovementController : MonoBehaviour
{
	[field: SerializeField]
	public float Speed { get; protected set; }
	public Vector2 Movement { get; protected set; }
	public Rigidbody2D Body { get; protected set; }
	protected bool IsAlive;

	protected virtual void Awake()
	{
		Movement = Vector2.zero;
	}

	protected virtual void Start()
	{
		Body = GetComponent<Rigidbody2D>();
	}

	protected void FixedUpdate()
	{
		Body.velocity = Movement * Speed;
	}

	protected void UpdateRotation(Vector2 movement)
	{
		var zAngle = 0;
		if (movement.x > 0)
		{
			zAngle = 90;
		}
		else if (movement.x < 0)
		{
			zAngle = -90;
		}
		if (Mathf.Abs(movement.x) - Mathf.Abs(movement.y) > 0)
		{
			transform.eulerAngles = new Vector3(0, 0, zAngle);
			return;
		}
		if (movement.y > 0)
		{
			zAngle = 180;
		}
		else if (movement.y < 0)
		{
			zAngle = 0;
		}
		transform.eulerAngles = new Vector3(0, 0, zAngle);
	}
	
	public void Kill()
	{
		IsAlive = false;
	}
}