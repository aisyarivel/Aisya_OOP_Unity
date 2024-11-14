using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Configuration")]
    [SerializeField] private float firingInterval = 3f;

    [Header("Projectile Settings")]
    public Bullet bulletPrefab;
    [SerializeField] private Transform spawnPoint;

    [Header("Object Pool Settings")]
    private IObjectPool<Bullet> bulletPool;

    private readonly bool enableCollectionCheck = false;
    private readonly int initialPoolSize = 30;
    private readonly int maxPoolSize = 100;
    private float cooldownTimer;
    public Transform parentTransform;  

    private void Awake()
    {
        bulletPool = new ObjectPool<Bullet>(CreateBulletInstance, ActivateBullet, DeactivateBullet, DestroyBulletInstance, enableCollectionCheck, initialPoolSize, maxPoolSize);
    }

    private void FixedUpdate()
    {
        cooldownTimer += Time.fixedDeltaTime;
        if (cooldownTimer >= firingInterval)
        {
            cooldownTimer = 0f;
            Fire();
        }
    }

    public void Fire()
    {
        Bullet bulletInstance = bulletPool.Get();
        bulletInstance.transform.position = spawnPoint.position;
        bulletInstance.transform.rotation = spawnPoint.rotation;
        bulletInstance.Initialize();
    }

    private Bullet CreateBulletInstance()
    {
        Bullet bulletInstance = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation, parentTransform);
        bulletInstance.SetPool(bulletPool);
        return bulletInstance;
    }

    private void ActivateBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(true);
    }

    private void DeactivateBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void DestroyBulletInstance(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }
}
