﻿using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class Enemy : MonoBehaviour
{
    [Header("Sounds")]
    [SerializeField] protected EnemyConfig baseConfig;
    
    protected float PushBackForce => baseConfig.PushBackForce * 1000;
    protected int health;
    protected Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        health = baseConfig.Health;
    }
    
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !PlayerController.Instance.IsImmune)
        {
            SoundManager.Instance.PlayEnemyEffects(baseConfig.PushBackSound);
            PlayerController.Instance.TakeDamage(baseConfig.Damage);
        }
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        anim.SetTrigger("GettingDamage");
        
        if (health <= 0)
        {
            StartCoroutine(Die());
        }
        else
        {
            SoundManager.Instance.PlayEnemyEffects(baseConfig.GettingDamageSound);
        }     
    }
    protected void PushBack(float pushBackForce)
    {
        SoundManager.Instance.PlayEnemyEffects(baseConfig.PushBackSound);
        Vector2 direction = (PlayerController.Instance.RB2D.transform.position - transform.position).normalized;
        PlayerController.Instance.RB2D.AddForce(direction * pushBackForce);
    }

    private IEnumerator Die()
    {
        SoundManager.Instance.PlayEnemyEffects(baseConfig.DyingSound);
        
        GetComponent<Collider2D>().enabled = false;
        anim.SetTrigger("Dying");
        yield return new WaitForSeconds(baseConfig.TimeToDestroyGO);
        Destroy(gameObject);
    }
}