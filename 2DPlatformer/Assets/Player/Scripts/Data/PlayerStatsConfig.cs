using UnityEngine;

[CreateAssetMenu(fileName = "Player_Config_SO", menuName = "Configs/Player")]
public class PlayerStatsConfig : ScriptableObject
{
    [Header("Player parameters")]
    public float Speed = 5;
    public float JumpForce = 30;
    public float WallSlidingSpeed = 3;
    public float XWallForce = 5;
    public float YWallForce = 20;
    public float WallJumpTime = 0.1f;
}