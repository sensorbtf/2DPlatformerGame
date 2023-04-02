using UnityEngine;

[CreateAssetMenu(fileName = "Projectile_Config_SO", menuName = "Configs/Enemy/")]
public class ProjectileConfig : ScriptableObject
{
    [Header("Projectile data")]
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    [SerializeField] private float lifeTime;
}