using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : Enemy
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        PlayerController.Instance.TakeDamage(damage);
        PushBack(PushBackForce);
    }
    public void TakeDamage(int damageAmount)
    {
        health = 1;
    }
}
