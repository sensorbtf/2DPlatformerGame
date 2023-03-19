using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Player_Config_SO", menuName = "Configs")]
public class PlayerConfig : ScriptableObject
{
    public int numberOfHearts = 3;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite brokenHeart;

    [Header("Player parameters")]
    public int health = 3;
    public int damage = 1;
    public float speed = 5;
    public float jumpForce = 30;
    public float wallSlidingSpeed = 3;
    public float timeBetweenAttacks = 0.4f;
    public float attackRange = 0.5f;
    public float xWallForce = 5;
    public float yWallForce = 20;
    public float wallJumpTime = 0.1f;

    [Header("Checkers")]
    public Transform platformTouchingValidator;
    public Transform wallTouchingValidator;
    public Transform attackValidator;
    public float radiousChecker;
    public LayerMask whatIsPlatform;
    public LayerMask whatAreWallsAndCeiling;
    public LayerMask whatAreEnemies;
    public GameObject Blood;
    public Collider2D mapCollider;

    [Header("Sounds")]
    public AudioClip jumpSound;
    public AudioClip runningSound;
    public AudioClip getDamagedSound;
    public AudioClip jumpedDownSound;
    public AudioClip attackSound;
    public AudioClip deathSound;
}