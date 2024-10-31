using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [field: SerializeField] public float Speed { get; private set; }
    public Vector2 Movement { get; private set; }
    public Rigidbody2D Body { get; private set; }

    private void Awake()
    {
        Movement = Vector2.zero;
    }

    // Start is called before the first frame update
    private void Start()
    {
        Body = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Body.velocity = Movement*Speed;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if(context.canceled)
        {
            Movement = Vector2.zero;
            return;
        }
        if (!context.performed) return;
        var movement = context.ReadValue<Vector2>();
        movement.Normalize();
        Movement = new Vector2(movement.x * Speed, movement.y * Speed);
        UpdateRotation(movement);
    }
    
    private void UpdateRotation(Vector2 movement)
    {
        var z_angle = 0;
        if(movement.x > 0)
        {
            z_angle = 90;
        }
        else if(movement.x < 0)
        {
            z_angle = -90;
        }
        if (Mathf.Abs(movement.x) - Mathf.Abs(movement.y) > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, z_angle);
            return;
        }
        if (movement.y > 0)
        {
            z_angle = 180;
        }
        else if (movement.y < 0)
        {
            z_angle = 0;
        }
        transform.eulerAngles = new Vector3(0, 0, z_angle);

    }
}