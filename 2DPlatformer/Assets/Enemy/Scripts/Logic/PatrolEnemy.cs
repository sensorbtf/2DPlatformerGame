using UnityEngine;

public class PatrolEnemy : Enemy
{
    [Header("PatrolEnemy")] 
    [SerializeField] private float speed = 2f;
    [SerializeField] private float startWaitTime = 1.5f;
    [SerializeField] private Transform[] pointsOfPatrol;

    private float patrolWaitTime;
    private int currentPointIndex;
    private Vector2[] patrolPointsPosition;
    private bool isFacingRight;

    private void Start()
    {
        patrolPointsPosition = new Vector2[pointsOfPatrol.Length];
        for (int i = 0; i < pointsOfPatrol.Length; i++)
        {
            patrolPointsPosition[i] = pointsOfPatrol[i].position;
        }
        
        transform.position = pointsOfPatrol[0].position;
        patrolWaitTime = startWaitTime;
        health = baseConfig.Health;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (health <= 0)
            return;

        PatrollingTask();
    }
    
    private void OnBecameVisible()
    {
        enabled = true;
    }

    private void OnBecameInvisible()
    {
        enabled = false;
    }
    
    private void PatrollingTask()
    {
        transform.position = Vector2.MoveTowards(transform.position, patrolPointsPosition[currentPointIndex],
            speed * Time.deltaTime);

        if ((Vector2)transform.position == patrolPointsPosition[currentPointIndex])
        {
            anim.SetBool("isRunning", false);
            if (patrolWaitTime <= 0)
            {
                if (currentPointIndex + 1 < patrolPointsPosition.Length)
                {
                    currentPointIndex++;
                    FlipEnemy();
                }
                else
                {
                    currentPointIndex = 0;
                    FlipEnemy();
                }

                patrolWaitTime = startWaitTime;
            }
            else
            {
                patrolWaitTime -= Time.deltaTime;
            }
        }
        else
        {
            anim.SetBool("isRunning", true);
            WalkingSoundEffect();
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) 
            return;
        
        PlayerController.Instance.TakeDamage(baseConfig.Damage);
        PushBack(PushBackForce);
    }

    private void FlipEnemy()
    {
        var transform1 = transform;
        var localScale = transform1.localScale;

        localScale = new Vector2(-localScale.x, localScale.y);
        transform1.localScale = localScale;
        isFacingRight = !isFacingRight;
    }
    
    private void WalkingSoundEffect()
    {
        if (transform.position != pointsOfPatrol[currentPointIndex].position)
        {
            SoundManager.Instance.PlayWalkingEffects(gameObject, baseConfig.RunningSound);
        }
        else if (transform.position == pointsOfPatrol[currentPointIndex].position)
            SoundManager.Instance.EnemyEffectsSource.Stop();
    }
}