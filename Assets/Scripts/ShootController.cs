using System.Collections;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    [Header("Player Variables")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifeTime = 5f;
    [SerializeField] float baseFireRate = 0.2f;

    [Header("AI Variables")]
    [SerializeField] float minFireRate = 0.2f;
    [SerializeField] float fireRateVariance = 0f;
    [SerializeField] bool useAI;

    [HideInInspector] public bool isShooting;
    Coroutine shootCoroutine;

    void Start()
    {
        if (useAI)
        {
            isShooting = true;
        }   
    }

    void Update()
    {
        Shoot(); 
    }

    void Shoot()
    {
        if (isShooting && shootCoroutine == null)
        {
            shootCoroutine = StartCoroutine(ShootContinuously());
        }
        else if (!isShooting && shootCoroutine != null)
        {
            StopCoroutine(shootCoroutine);
            shootCoroutine = null;
        }
    }

    IEnumerator ShootContinuously()
    {
        while (true)
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectile.transform.rotation = transform.rotation;

            Rigidbody2D projectileRB = projectile.GetComponent<Rigidbody2D>();
            projectileRB.linearVelocity = transform.up * projectileSpeed;

            Destroy(projectile, projectileLifeTime);

            float waitTime = Random.Range(baseFireRate - fireRateVariance, baseFireRate + fireRateVariance);
            waitTime = Mathf.Clamp(waitTime, minFireRate, float.MaxValue);

            yield return new WaitForSeconds(waitTime);
        }
    }
}
