using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShot : MonoBehaviour
{
    private struct BulletForceAndRotation
    {
        public Vector2 Force;
        public int Rotation;
    }
    [SerializeField]
    protected float shootSpeed, atkSpeed;
    private bool _canShoot;
    private bool _isHoldingShoot;
    [SerializeField] private GameObject bulletSpawn;
    [SerializeField] private GameObject bulletPrefab;
    private Rigidbody2D _rigidbody2D;
    private const float _minShootMagnitude = 0.01f;
    private const float _maxShootDir = 0.125f;

    private void Awake()
    {
        _canShoot = true;
    }

    private void Start()
    {
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
    }

    /*
    private void OnEnable()
    {
        PlayerController.PlayerDeathEventHandler += StopInput;
        PlayerController.SceneLoaded += StartInput;
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    private void OnDisable()
    {
        PlayerController.PlayerDeathEventHandler -= StopInput;
        PlayerController.SceneLoaded -= StartInput;
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }
    */

    public void Shoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _isHoldingShoot = true;
            //PlayerAnimator.SetBool(IsShooting, true);
        }
        else if (context.canceled)
        {
            _isHoldingShoot = false;
            //PlayerAnimator.SetBool(IsShooting, false);
        }
        var shotDirection = context.ReadValue<Vector2>();
        shotDirection.Normalize();
        if (shotDirection.magnitude > _minShootMagnitude)
            StartCoroutine(ShootBullet(shotDirection));
    }

    private IEnumerator ShootBullet(Vector2 shotDirection)
    {
        while (_isHoldingShoot)
        {
            yield return null;
            if (!_canShoot) continue;
            var bulletForceAndRotation = GetBulletForceAndRotation(shotDirection);
            //UpdateShotAnimation(shotDirection);
            RotateSpawnPoint(bulletForceAndRotation);
            SpawnAndShootBullet(bulletForceAndRotation);
            var coolDownTime = GetCooldown();
            StartCoroutine(CountCooldown(coolDownTime));
        }
    }

    private float GetCooldown()
    {
        return 1.0f / atkSpeed;
    }

    private void SpawnAndShootBullet(BulletForceAndRotation bulletForceAndRotation)
    {
        var bullet = Instantiate(bulletPrefab, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
        var bulletController = bullet.GetComponent<ProjectileController>();
        bulletController.Shoot(bulletForceAndRotation.Force + _rigidbody2D.velocity.normalized);
    }

    private void RotateSpawnPoint(BulletForceAndRotation bulletForceAndRotation)
    {
        bulletSpawn.transform.rotation = Quaternion.Euler(0, 0, bulletForceAndRotation.Rotation);
    }

    private BulletForceAndRotation GetBulletForceAndRotation(Vector2 shotDirection)
    {
        const int rotationOffset = -45;
        BulletForceAndRotation bulletForceAndRotation;
        if (shotDirection.x > _maxShootDir)
        {
            bulletForceAndRotation.Rotation = 0 + rotationOffset;
            bulletForceAndRotation.Force = new Vector2(shootSpeed, 0f);
        }
        else if (shotDirection.x < -_maxShootDir)
        {
            bulletForceAndRotation.Rotation = 180 + rotationOffset;
            bulletForceAndRotation.Force = new Vector2(-shootSpeed, 0f);
        }
        else if (shotDirection.y > _maxShootDir)
        {
            bulletForceAndRotation.Rotation = 90 + rotationOffset;
            bulletForceAndRotation.Force = new Vector2(0f, shootSpeed);
        }
        else
        {
            bulletForceAndRotation.Rotation = 270 + rotationOffset;
            bulletForceAndRotation.Force = new Vector2(0f, -shootSpeed);
        }

        return bulletForceAndRotation;
    }

    private IEnumerator CountCooldown(float bulletCooldown)
    {
        _canShoot = false;
        yield return new WaitForSeconds(bulletCooldown);
        _canShoot = true;
    }

    private void StartInput(object sender, EventArgs eventArgs)
    {
        _canShoot = true;
    }

    private void StopInput(object sender, EventArgs eventArgs)
    {
        _canShoot = false;
    }

}
