using UnityEngine;

public class DamageController : MonoBehaviour
{
    [SerializeField] int damageValue;

    public int GetDamageValue()
    {
        return damageValue;
    }

    public void Hit()
    {
        Destroy(gameObject);
    }
}
