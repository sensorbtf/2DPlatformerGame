using System.Collections;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [Header("Enemy base info")]
    [SerializeField] protected int health = 2;
    [SerializeField] protected int damage = 1;
    [SerializeField] protected float pushBackForce = 2.2f * 1000;

    protected float PushBackForce => pushBackForce;

    protected Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController.Instance.TakeDamage(damage);
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
    }
    protected void PushBack(float pushBackForce)
    {
        Vector2 direction = (PlayerController.Instance.RB2D.transform.position - transform.position).normalized;
        PlayerController.Instance.RB2D.AddForce(direction * pushBackForce);
    }

    private IEnumerator Die()
    {
        GetComponent<Collider2D>().enabled = false;
        anim.SetTrigger("Dying");
        yield return new WaitForSeconds(0.80f);
        Destroy(gameObject);
    }
}