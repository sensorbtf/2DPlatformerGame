using UnityEngine;

public class ShootingEnemy : Enemy
{
    [Header("Projectile parameters")]
    [SerializeField] private GameObject projectile;
    [SerializeField] private float timeBetweenShots;
    [SerializeField] private Transform shotPoint;

    private float nextShotTime;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (health <= 0)
            return;

        if (!(Time.time > nextShotTime)) return;
        
        anim.SetTrigger("Attacking");
        nextShotTime = Time.time + timeBetweenShots;
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        
        PlayerController.Instance.TakeDamage(damage);
        PushBack(PushBackForce);
    }
    
    protected void FireProjectile()
    {
        Instantiate(projectile, shotPoint.position, shotPoint.rotation);
    }
}
