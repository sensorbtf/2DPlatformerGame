using UnityEngine;

public class ShootingEnemy : Enemy
{
    [Header("Projectile parameters")]
    [SerializeField] private GameObject projectile;
    [SerializeField] private float timeBetweenShots;
    [SerializeField] private Transform shotPoint;
    [SerializeField] private AudioClip attackClip;

    private float nextShotTime;
    private float randomTimeBetweenShots;

    private void Start()
    {
        anim = GetComponent<Animator>();
        health = baseConfig.Health;
        enabled = false;
    }
    
    private void Update()
    {
        if (health <= 0 || !(Time.time > nextShotTime))
            return;

        anim.SetTrigger("Attacking");
        randomTimeBetweenShots = Random.Range(timeBetweenShots - timeBetweenShots * Random.Range(0.1f, 0.9f), timeBetweenShots);
        nextShotTime = Time.time + randomTimeBetweenShots;
    }
    
    private void OnBecameVisible()
    {
        enabled = true;
    }

    private void OnBecameInvisible()
    {
        enabled = false;
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
        SoundManager.Instance.PlayEffects(attackClip);
        Instantiate(projectile, shotPoint.position, shotPoint.rotation);
    }
}
