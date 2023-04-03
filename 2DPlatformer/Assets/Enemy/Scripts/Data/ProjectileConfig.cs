using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Projectile_Config_SO", menuName = "Configs/Projectile")]
public class ProjectileConfig : ScriptableObject
{
    [Header("Projectile data")]
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    [SerializeField] private float lifeTime;

    [Header("Sounds")]
    [SerializeField] private AudioClip bombExplode;

    public float Speed => speed;
    public int Damage => damage;
    public float LifeTime => lifeTime;
    public AudioClip BombExplode => bombExplode;
}