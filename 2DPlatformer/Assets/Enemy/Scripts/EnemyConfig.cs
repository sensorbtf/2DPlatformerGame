using UnityEngine;
using UnityEngine.UI;

//[CreateAssetMenu(fileName = "Enemy_Config_SO", menuName = "Configs/")]
public class EnemyConfig : ScriptableObject
{
    [Header("Enemy parameters")]
    [SerializeField] private int health = 2;
    [SerializeField] private int damage = 1;
    [SerializeField] private float pushBackForce = 2.2f;

    [Header("Coins reward")]
    [SerializeField] private int minimumCount = 1;
    [SerializeField] private int maximumCount = 2;
    [SerializeField] private GameObject coin = null;

    [Header("Effects")]
    [SerializeField] private GameObject Blood;

    [Header("Sounds")]
    [SerializeField] private AudioClip gettingDamageSound;
    [SerializeField] protected AudioClip pushBackSound;
    [SerializeField] protected AudioClip runningSound;
    [SerializeField] protected AudioClip dyingSound;

    [Header("AttackingEnemy")]
    [SerializeField] private int attackDamage = 1;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private float attackColdown = 1.5f;
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] Transform player;
    [SerializeField] Transform attackValidator;

    [Header("PatrolEnemy")]
    [SerializeField] private float speed = 2f;
    [SerializeField] private float startWaitTime = 1.5f;
    [SerializeField] private Transform[] pointsOfPatrol;

    [Header("Projectile parameters")]
    [SerializeField] private GameObject projectile;
    [SerializeField] private float timeBetweenShots;
    [SerializeField] private Transform shotPoint;


}