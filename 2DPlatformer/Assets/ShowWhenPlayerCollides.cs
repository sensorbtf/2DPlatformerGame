using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowWhenPlayerCollides : MonoBehaviour
{
    public SpriteRenderer sprite;
    public float waitTime = 0.01f;
    private void Start()
    {
        sprite.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(waiter());

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            sprite.enabled = false;
        }
    }

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(waitTime);
        sprite.enabled = true;
    }
}
