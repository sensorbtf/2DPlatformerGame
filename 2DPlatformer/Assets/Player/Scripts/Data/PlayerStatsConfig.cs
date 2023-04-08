using UnityEngine;

[CreateAssetMenu(fileName = "Player_Config_SO", menuName = "Configs/Player")]
public class PlayerStatsConfig : ScriptableObject
{
    [Header("Player movement")]
    [SerializeField] private float speed = 5;
    [SerializeField] private float jumpForce = 30;
    [SerializeField] private float wallSlidingSpeed = 3;
    [SerializeField] private float xWallForce = 5;
    [SerializeField] private float yWallForce = 20;
    [SerializeField] private float wallJumpTime = 0.1f;
    [SerializeField] private float jumpingOffThreshold = 0.176f;
    [SerializeField] private float dashingPower = 100;
    [SerializeField] private float dashingTime = 0.2f;
    [SerializeField] private float dashingCooldown = 1f;
    
    [Header("Player combat stats")]
    [SerializeField] private int health = 3;
    [SerializeField] private int damage = 1;
    [SerializeField] private float timeBetweenAttacks = 0.4f; 
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private float dyingDuration = 0.5f;
    [SerializeField] private float godModeDuration = 1.5f;
    
    // Sound
    [Header("Sounds")]
    [SerializeField] public AudioClip RunningSound;
    [SerializeField] public AudioClip GetDamagedSound;
    [SerializeField] public AudioClip JumpSound;
    [SerializeField] public AudioClip JumpedDownSound;
    [SerializeField] public AudioClip AttackSound;
    [SerializeField] public AudioClip DeathSound;
    
    public float Speed => speed;
    public float JumpForce => jumpForce;
    public float WallSlidingSpeed => wallSlidingSpeed;
    public float XWallForce => xWallForce;
    public float YWallForce => yWallForce;
    public float WallJumpTime => wallJumpTime;
    public float JumpingOffThreshold => jumpingOffThreshold;
    public float DashingPower => dashingPower;
    public float DashingTime => dashingTime;
    public float DashingCooldown => dashingCooldown;
    
    public int Health => health;
    public int Damage => damage;
    public float TimeBetweenAttacks => timeBetweenAttacks; 
    public float AttackRange => attackRange;
    public float DyingDuration => dyingDuration;
    public float GodModeDuration => godModeDuration;
}