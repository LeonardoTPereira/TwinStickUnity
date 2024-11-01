using System;
using System.Collections;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public static event EventHandler EnemyHitEventHandler;
    public static event EventHandler PlayerHitEventHandler;
    private Vector2 pos, moveDir;
    private float MoveSpeed { get; set; }
    [SerializeField]
    private int damage;

    private Rigidbody2D bulletRigidBody;

    private void Start()
    {
        bulletRigidBody = GetComponent<Rigidbody2D>();
    }


    public void DestroyBullet()
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        Destroy(gameObject);
    }

    private void OnEnemyHit()
    {
        EnemyHitEventHandler?.Invoke(null, EventArgs.Empty);
    }

    private void OnPlayerHit()
    {
        PlayerHitEventHandler?.Invoke(null, EventArgs.Empty);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var collisionDirection = Vector3.Normalize(collision.gameObject.transform.position - gameObject.transform.position);
        if (CompareTag("EnemyBullet"))
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                OnPlayerHit();
                //collision.gameObject.GetComponent<HealthController>().ApplyDamage(damage, collisionDirection);
                DestroyBullet();
            }
        }
        else if (CompareTag("Bullet"))
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                OnEnemyHit();
                //collision.gameObject.GetComponent<EnemyController>().ApplyDamageEffects(collisionDirection);
                //collision.gameObject.GetComponent<HealthController>().ApplyDamage(damage, collisionDirection);
                DestroyBullet();
            }
            if (collision.gameObject.CompareTag("Shield"))
            {
                DestroyBullet();
            }
        }
        if (collision.gameObject.CompareTag("Block"))
        {
            DestroyBullet();
        }
    }

    public void Shoot(Vector2 facingDirection)
    {
        StartCoroutine(ShootAfterStart(facingDirection));
    }

    public IEnumerator ShootAfterStart(Vector2 facingDirection)
    {
        yield return new WaitForEndOfFrame();
        bulletRigidBody.AddForce(facingDirection, ForceMode2D.Impulse);
    }
}