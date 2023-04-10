using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Enemy_Config_SO", menuName = "Configs/Enemy Base Data")]
public class EnemyConfig : ScriptableObject
{
    [Header("Enemy parameters")]
    [SerializeField] private int health = 2;
    [SerializeField] private int damage = 1;
    [SerializeField] private float pushBackForce = 2;
    [SerializeField] private float timeToDestroyGO = 0.3f;

    [Header("Sounds")]
    [SerializeField] private AudioClip gettingDamageSound;
    [SerializeField] private AudioClip pushBackSound;
    [SerializeField] private AudioClip runningSound;
    [SerializeField] private AudioClip dyingSound;

    public int Health => health;
    public int Damage => damage;
    public float PushBackForce => pushBackForce;
    public float TimeToDestroyGO => timeToDestroyGO;
    
    public AudioClip GettingDamageSound => gettingDamageSound;
    public AudioClip PushBackSound => pushBackSound;
    public AudioClip RunningSound => runningSound;
    public AudioClip DyingSound => dyingSound;
 }