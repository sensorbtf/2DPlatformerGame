using UnityEngine;

public class PlayerMonoConfig : MonoBehaviour
{
    [Header("Checkers")]
    public Transform platformTouchingValidator;
    public Transform wallTouchingValidator;
    public Transform attackValidator;
    public float radiousChecker = 0.3f;
    public LayerMask whatIsPlatform;
    public LayerMask whatAreWallsAndCeiling;
    public LayerMask whatAreEnemies;
    public Collider2D mapCollider;
}
