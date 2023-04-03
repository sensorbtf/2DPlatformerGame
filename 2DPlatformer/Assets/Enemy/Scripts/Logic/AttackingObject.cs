using UnityEngine;

public class AttackingObject : MonoBehaviour
{
    [Header("AttackingEnemy")]
    [SerializeField] private int attackDamage = 1;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private float attackCooldown = 1.5f;
    [Header("Sounds")]
    [SerializeField] private AudioClip attackAudio;

    private float nextAttack = 0.2f;
    private Animator anim;

    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] Transform player;
    [SerializeField] Transform attackValidator;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (player != null && attackValidator != null && attackCooldown != 0 && !PlayerController.Instance.IsImmune)
            AttackPlayer();
    }
    private void AttackPlayer()
    {
        if (Vector2.Distance(player.position, attackValidator.position) <= attackRange && Time.time > nextAttack)
        {
            Collider2D[] playerToDamage = Physics2D.OverlapCircleAll(attackValidator.position, attackRange, whatIsPlayer);
            foreach (Collider2D player in playerToDamage)
            {
                SoundManager.Instance.PlayEnemyEffects(attackAudio);
                
                player.GetComponent<PlayerController>().TakeDamage(attackDamage);
            }
            anim.SetTrigger("Attacking");
            nextAttack = Time.time + attackCooldown;
        }
    }
}