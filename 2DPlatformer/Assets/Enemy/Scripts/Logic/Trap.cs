using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private int damage;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        PlayerController.Instance.TakeDamage(damage);
    }
}
