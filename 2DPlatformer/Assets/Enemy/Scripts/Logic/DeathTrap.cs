using UnityEngine;

public class DeathTrap : MonoBehaviour
{
    [SerializeField] private int damage;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player") || PlayerController.Instance.IsImmune) 
            return;
        
        PlayerController.Instance.TakeDamage(damage);
        gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.red;
    }
}
