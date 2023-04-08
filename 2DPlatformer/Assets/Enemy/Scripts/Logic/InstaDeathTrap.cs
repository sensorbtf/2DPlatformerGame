using UnityEngine;

public class InstaDeathTrap : MonoBehaviour
{
    [SerializeField] private int damage;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController.Instance.TakeDamage(damage);

            gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.red;
        }
    }
}
