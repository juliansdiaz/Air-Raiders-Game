using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] int health;
    [SerializeField] int scoreValue;
    [SerializeField] bool isPlayer;

    public int GetHealth()
    {
        return health;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageController damageController = collision.GetComponent<DamageController>();
        if(damageController != null)
        {
            TakeDamage(damageController.GetDamageValue());
            damageController.Hit();
        }
    }

    void TakeDamage(int damageReceived)
    {
        health -= damageReceived;
        if(health <= 0)
        {
            if (isPlayer)
            {
                Debug.Log("Game Over");
            }
            else
            {
                Debug.Log("Plane Killed");
            }
            Destroy(gameObject);
        }
    }
}
