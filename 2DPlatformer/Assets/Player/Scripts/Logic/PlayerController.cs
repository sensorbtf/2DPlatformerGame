using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerMonoConfig PlayerMonoConfig;
    [SerializeField] private PlayerStatsConfig PlayerStats;

    // Hero state checkers
    private bool isGrounded;
    private bool isTouchingWall;
    private bool isFacingRight = true;
    private bool isDashing = false;
    private bool isSlidingWall;
    private bool isJumpingFromWall;

    // Hero movement invocations
    private bool doJump = false;
    private bool doJumpDown = false;
    private bool doJumpFromWall = false;
    private bool doDash = false;
    private bool canDash = true;

    private float input;
    private float nextAttackCooldown;

    void Update()
    {
        if (isDashing)
            return;
        
        input = Input.GetAxisRaw("Horizontal");
        PlayerPositionChecker();
        HeroStateAnimations();
        PlayerInput();
    }

    private void FixedUpdate()
    {
        PlayerMonoConfig.PlayerRigidBody.velocity = new Vector2(input * PlayerStats.Speed, PlayerMonoConfig.PlayerRigidBody.velocity.y);

        PlayerMovementActivator();
    }

    private void PlayerMovementActivator()
    {
        if (doJump)
            Jump();

        if (doJumpDown)
            StartCoroutine(JumpOff());
        
        if (isSlidingWall)
            SlideOnWall();
        
        if (doJumpFromWall)
            JumpFromWall();
        
        if (doDash)
            Dash();
    }
    
    private void PlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
            doJump = true;
        
        if (Input.GetKeyDown(KeyCode.DownArrow) && isGrounded)
            doJumpDown = true;
        
        if (Input.GetKeyDown(KeyCode.UpArrow) && isSlidingWall)
        {
            isJumpingFromWall = true;
            Invoke(nameof(SetWallJumpingToFalse), PlayerStats.WallJumpTime);
        }
        
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash && !isGrounded)
            doDash = true;
        
        if (isTouchingWall && !isGrounded && input != 0)
            isSlidingWall = true;
        else
            isSlidingWall = false;
        
        if (isJumpingFromWall)
            doJumpFromWall = true;
        
        if (Time.time > PlayerStats.TimeBetweenAttacks)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !isSlidingWall)
            {
                nextAttackCooldown = Time.time + PlayerStats.TimeBetweenAttacks;
                PlayerMonoConfig.Animator.SetTrigger("Attacking");
            }
        }
    }
    
    private void HeroStateAnimations()
    {
        if (input != 0 && isGrounded )
        {
            PlayerMonoConfig.Animator.SetBool("isRunning", true);
        }
        else
            PlayerMonoConfig.Animator.SetBool("isRunning", false);
        
        if (isGrounded)
        {
            PlayerMonoConfig.Animator.SetBool("isGrounded", true);
            PlayerMonoConfig.Animator.SetBool("isJumping", false);
            PlayerMonoConfig.Animator.SetBool("isFalling", false);
        }
        else
        {
            PlayerMonoConfig.Animator.SetBool("isGrounded", false);
            PlayerMonoConfig.Animator.SetBool("isJumping", true);
            if (PlayerMonoConfig.PlayerRigidBody.velocity.y < -0.1)
            {
                PlayerMonoConfig.Animator.SetBool("isJumping", false);
                PlayerMonoConfig.Animator.SetBool("isFalling", true);
            }
            else
                PlayerMonoConfig.Animator.SetBool("isFalling", false);
        }
    }

    private void Dash()
    {
        StartCoroutine(PlayerMonoConfig.WallTouchingValidator.position.x >
                       PlayerMonoConfig.PlatformTouchingValidator.position.x
            ? Dash(1)
            : Dash(-1));
    }
    
    private IEnumerator Dash(float Direction)
    {
        isDashing = true;
        canDash = false;
        doDash = false;
        PlayerMonoConfig.Animator.SetTrigger("Dashing");
        
        var gravityScale = PlayerMonoConfig.PlayerRigidBody.gravityScale;

        PlayerMonoConfig.PlayerRigidBody.velocity = new Vector2(PlayerMonoConfig.PlayerRigidBody.velocity.x, 0f);
        PlayerMonoConfig.PlayerRigidBody.AddForce(new Vector2(PlayerStats.DashingPower * Direction, 0f), ForceMode2D.Impulse);

        float originalGravity = gravityScale;
        yield return new WaitForSeconds(PlayerStats.DashingTime);
        gravityScale = originalGravity;
        PlayerMonoConfig.PlayerRigidBody.gravityScale = gravityScale;
        isDashing = false;

        yield return new WaitForSeconds(PlayerStats.DashingCooldown);
        canDash = true;
    }
    
    private void SetWallJumpingToFalse()
    {
        isJumpingFromWall = false;
    }
    
    private void JumpFromWall()
    {
        PlayerMonoConfig.PlayerRigidBody.velocity = new Vector2(PlayerStats.XWallForce * -input, PlayerStats.YWallForce);
        doJumpFromWall = false;
    }
    
    private void SlideOnWall()
    {
        var velocity = PlayerMonoConfig.PlayerRigidBody.velocity;
        PlayerMonoConfig.PlayerRigidBody.velocity = new Vector2(velocity.x, 
            Mathf.Clamp(velocity.y, -PlayerStats.WallSlidingSpeed, float.MaxValue));
    }
    
    private void Jump()
    {
        PlayerMonoConfig.PlayerRigidBody.velocity = Vector2.up * PlayerStats.JumpForce;
        doJump = false;
    }
    
    private IEnumerator JumpOff()
    {
        Physics2D.IgnoreCollision(PlayerMonoConfig.PlayerCollider, PlayerMonoConfig.PlatformCollider, true);
        yield return new WaitForSeconds(PlayerStats.JumpingOffThreshold); // enough time to jump off the one-slice platform
        Physics2D.IgnoreCollision(PlayerMonoConfig.PlayerCollider, PlayerMonoConfig.PlatformCollider, false);
        doJumpDown = false;
    }
    
    private void FlipHeroSprite()
    {
        var transform1 = transform;
        var localScale = transform1.localScale;
        
        localScale = new Vector2(-localScale.x, localScale.y);
        transform1.localScale = localScale;
        
        isFacingRight =! isFacingRight;
    }

    private void PlayerPositionChecker()
    {
        if (Physics2D.OverlapCircle(PlayerMonoConfig.PlatformTouchingValidator.position, 
                PlayerMonoConfig.RadiusChecker, PlayerMonoConfig.WhatIsPlatform) || 
            Physics2D.OverlapCircle(PlayerMonoConfig.PlatformTouchingValidator.position, 
                PlayerMonoConfig.RadiusChecker, PlayerMonoConfig.WhatIsGround))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        isTouchingWall = Physics2D.OverlapCircle(PlayerMonoConfig.WallTouchingValidator.position, 
            PlayerMonoConfig.RadiusChecker, PlayerMonoConfig.WhatAreWallsAndCeiling);
        
        if (input > 0 && !isFacingRight)
            FlipHeroSprite();
        else if (input < 0 && isFacingRight)
            FlipHeroSprite();
    }
    
    private void Attack() // will be used in animation
    {
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(PlayerMonoConfig.AttackValidator.position, 
            PlayerStats.AttackRange, PlayerMonoConfig.WhatAreEnemies);
    
        // foreach (Collider2D enemies in enemiesToDamage)
        //     enemies.GetComponent<Enemy>().TakeDamage(PlayerStats.Damage);
    }
}