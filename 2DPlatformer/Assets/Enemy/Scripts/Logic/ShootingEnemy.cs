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
        health = baseConfig.Health;
    }
    private void Update()
    {
        if (health <= 0 || !(Time.time > nextShotTime))
            return;

        anim.SetTrigger("Attacking");
        nextShotTime = Time.time + timeBetweenShots;
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) 
            return;
        
        PlayerController.Instance.TakeDamage(baseConfig.Damage);
        PushBack(PushBackForce);
    }
    
    protected void FireProjectile()
    {
        Instantiate(projectile, shotPoint.position, shotPoint.rotation);
    }
}
