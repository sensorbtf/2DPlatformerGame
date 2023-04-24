using System;
using System.Collections;
using UnityEngine;

public class ShowWhenPlayerCollides : MonoBehaviour
{
    public static ShowWhenPlayerCollides Instance;
    
    public GameObject[] sprites;
    public float waitTime = 0.2f;
    public bool hasBeenInvoked;
    
    public event Action OnLevelCompletion;

    private void Start()
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            sprites[i].SetActive(false);
        }
        Instance = this;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(ShowNotes());
            
            if (!hasBeenInvoked)
            {
                hasBeenInvoked = true;            
                OnLevelCompletion?.Invoke();
            }
        }
    }

    IEnumerator ShowNotes()
    {
        sprites[0].SetActive(true); 
        yield return new WaitForSeconds(waitTime);
        sprites[1].SetActive(true); 
        yield return new WaitForSeconds(waitTime);
        sprites[2].SetActive(true); 
        yield return new WaitForSeconds(waitTime);
    }
}
