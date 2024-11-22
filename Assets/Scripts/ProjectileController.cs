using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{	
	[field: SerializeField]
	public float Speed { get; private set; }
	[field: SerializeField]
	public int Damage { get; private set; }
	
	private Vector2 Position { get; set; }
	private Vector2 Movement { get; set; }
	private Rigidbody2D Body { get; set; }

	private void Start()
	{
		Body = GetComponent<Rigidbody2D>();
	}

	public void Shoot(Vector2 direction, Vector2 playerMovement)
	{
		StartCoroutine(ShootAfterStart(direction * Speed + playerMovement));
	}

	private IEnumerator ShootAfterStart(Vector2 direction)
	{
		yield return new WaitForEndOfFrame();
        Body.AddForce(direction, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
		if(CompareTag("PlayerProjectile"))
		{
            if (collision.gameObject.CompareTag("Enemy"))
            {
                collision.gameObject.GetComponent<HealthController>().Hit(Damage);
                Destroy(gameObject);
            }
        }
        if (CompareTag("EnemyProjectile"))
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<HealthController>().Hit(Damage);
                Destroy(gameObject);
            }
        }
		if (collision.gameObject.CompareTag("Block"))
		{
			Destroy(gameObject);
		}
    }
}
