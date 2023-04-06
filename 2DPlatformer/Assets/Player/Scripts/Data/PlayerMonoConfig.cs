using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMonoConfig : MonoBehaviour
{
    [Header("Checkers")]
    [SerializeField] private Transform platformTouchingValidator;
    [SerializeField] private Transform wallTouchingValidator;
    [SerializeField] private Transform attackValidator;
    [SerializeField] private Transform ceilingChecker;
    [SerializeField] private float radiusChecker = 0.3f;
    [SerializeField] private LayerMask whatIsPlatform;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatAreWallsAndCeiling;
    [SerializeField] private LayerMask whatAreEnemies;
    [SerializeField] private Collider2D platformCollider;
    [SerializeField] private Animator animator;

    [Header("Player Components")]
    [SerializeField] private Rigidbody2D playerRigidBody;
    [SerializeField] private Collider2D playerCollider;
    
    public Transform PlatformTouchingValidator => platformTouchingValidator;
    public Transform WallTouchingValidator => wallTouchingValidator;
    public Transform CeilingChecker => ceilingChecker;
    public Transform AttackValidator => attackValidator;
    public float RadiusChecker => radiusChecker;
    public LayerMask WhatIsPlatform => whatIsPlatform;
    public LayerMask WhatIsGround => whatIsGround;
    public LayerMask WhatAreWallsAndCeiling => whatAreWallsAndCeiling;
    public LayerMask WhatAreEnemies => whatAreEnemies;
    public Animator Animator => animator;
    public Collider2D PlatformCollider => platformCollider;
    
    public Rigidbody2D PlayerRigidBody => playerRigidBody;
    public Collider2D PlayerCollider => playerCollider;
}
