using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject Goblin;
    [SerializeField] private GameObject Sign;
    [SerializeField] private GameObject RunAwayGO;
    [SerializeField] private GameObject Guitar;
    [SerializeField] private GameObject Syntetizer;
    [SerializeField] private GameObject Drums;
    [SerializeField] private GameObject Player;
    
    [SerializeField] private AudioClip DrumsOnly;
    [SerializeField] private AudioClip DrumsAndSyntetizer;
    [SerializeField] private AudioClip EveryInstrument;

    private bool canStart = false;
    private bool isFacingRight = false;
    
    private void Start()
    {
        var levelCompleted = PlayerPrefs.GetInt("CurrentLevel");
        switch (levelCompleted)
        {
            case 0:
                StartCoroutine(MoveToPosition(Goblin.transform, Guitar, 2));
                break;
            case 1:
                Drums.SetActive(true);
                SoundManager.Instance.StopMusic();
                SoundManager.Instance.PlayMusic(DrumsOnly);
                Guitar.SetActive(false);
                Syntetizer.SetActive(false);
                Goblin.SetActive(false);
                canStart = true;
                break;
            
            case 2:
                SoundManager.Instance.StopMusic();
                SoundManager.Instance.PlayMusic(DrumsAndSyntetizer);
                Syntetizer.SetActive(true);
                Drums.SetActive(true);
                Guitar.SetActive(false);
                Goblin.SetActive(false);
                canStart = true;
                break;
            
            case 3:
                SoundManager.Instance.StopMusic();
                SoundManager.Instance.PlayMusic(EveryInstrument);
                Syntetizer.SetActive(true);
                Drums.SetActive(true);
                Guitar.SetActive(true);
                Goblin.SetActive(false);
                canStart = true;
                break;
            
            //TODO Case4 after completion of all levels 
        }
    }
    
    public void PlayGame()
    {
        if (canStart)
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt("CurrentLevel")+1);
        }
    }    
    
    public void DeletePrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
    IEnumerator MoveToPosition(Transform p_transform, GameObject p_gO, float p_timeToMove) {
        Vector3 currentPos = p_transform.position;
        float t = 0f;
        while (t < 1) {
            t += Time.deltaTime / p_timeToMove;
            p_transform.position = Vector3.Lerp(currentPos, p_gO.transform.position, t);
            yield return null;
        }
        p_gO.SetActive(false);
        
        if (Syntetizer.activeSelf)
        {
            StartCoroutine(MoveToPosition(Goblin.transform, Syntetizer, 3));
        }

        if (Drums.activeSelf && !Syntetizer.activeSelf)
        {
            StartCoroutine(MoveToPosition(Goblin.transform, Drums, 2));
        }
        
        if (!canStart && Goblin.activeSelf && !Syntetizer.activeSelf && 
            !Guitar.activeSelf && !Drums.activeSelf && Sign.activeSelf)
        {
            FlipEnemy();
            SoundManager.Instance.StopMusic();
            StartCoroutine(MoveToPosition(Goblin.transform, Sign, 3));
        }
        
        if (!Sign.activeSelf)
        {
            StartCoroutine(MoveToPosition(Goblin.transform, RunAwayGO, 2));
            Sign.SetActive(true);
            canStart = true;
        }

        if (Sign.activeSelf && canStart)
        {
            StartCoroutine(MoveToPosition(Player.transform, RunAwayGO, 3));
        }
    }
    
    private void FlipEnemy()
    {
        var transform1 = Goblin.transform;
        var localScale = transform1.localScale;

        localScale = new Vector2(-localScale.x, localScale.y);
        transform1.localScale = localScale;
        isFacingRight = !isFacingRight;
    }
}
