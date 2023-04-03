using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private ProjectileConfig config;

    private void Start()
    {
        Destroy(gameObject, config.LifeTime);
    }

    private void FixedUpdate()
    {
        DirectionOfAttack();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            PlayerController.Instance.TakeDamage(config.Damage);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }
    private void DirectionOfAttack()
    {
        transform.Translate(config.Speed * Time.deltaTime * Vector2.right);
    }
}
