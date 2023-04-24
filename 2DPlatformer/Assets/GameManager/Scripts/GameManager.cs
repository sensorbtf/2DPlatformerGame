using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI enemiesLeftText;
    [SerializeField] private TextMeshProUGUI leftMusicNodesText;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject victoryPanel;
    
    [SerializeField] private GameObject Guitar;
    [SerializeField] private GameObject Syntetizer;
    [SerializeField] private GameObject Drums;
    
    private int totalEnemies; 
    private int totalNotes; 
    private Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
    
    void Start()
    {
        victoryPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        
        var levelCompleted = PlayerPrefs.GetInt("CurrentLevel");
        switch (levelCompleted)
        {
            case 1:
                SoundManager.Instance.StopMusic();
                SoundManager.Instance.PlayMusic(SoundManager.Instance.DrumsOnly);
                break;
            case 2:
                SoundManager.Instance.StopMusic();
                SoundManager.Instance.PlayMusic(SoundManager.Instance.DrumsAndSyntetizer);
                break;
            
            case 3:
                SoundManager.Instance.StopMusic();
                SoundManager.Instance.PlayMusic(SoundManager.Instance.EveryInstrument);
                break;
        }
        
        Time.timeScale = 1f;
        PlayerController.Instance.OnDeath += ShowGameOverPanel;
        ShowWhenPlayerCollides.Instance.OnLevelCompletion += EndGame;
    }
    
    void Update()
    {
        totalEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        totalNotes = GameObject.FindGameObjectsWithTag("Note").Length;

        if (enemiesLeftText.text != totalEnemies.ToString() || leftMusicNodesText.text != totalNotes.ToString())
            IsLevelCompleted();
        
        enemiesLeftText.text = totalEnemies.ToString();
        leftMusicNodesText.text = totalNotes.ToString();
    }

    private void IsLevelCompleted()
    {            
        if (totalEnemies == 0 && totalNotes == 0)
            StartCoroutine(ShowLevelCompletedPanel());
    }
    
    private void EndGame()
    {
        PlayerController.Instance.RevokePlayerControl(); 
        StartCoroutine(ActivateEndingScreen(5));
    }

    private void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    private IEnumerator ActivateEndingScreen(int p_seconds)
    {
        yield return new WaitForSeconds(p_seconds);
        StartCoroutine(ShowLevelCompletedPanel());
    }

    private IEnumerator ShowLevelCompletedPanel()
    {
        PlayerPrefs.SetInt("CurrentLevel", SceneManager.GetActiveScene().buildIndex);

        var levelCompleted = PlayerPrefs.GetInt("CurrentLevel");
        GameObject newGo = null;
        
        switch (levelCompleted)
        {
            case 1:
                newGo = Instantiate(Drums, screenCenter, Quaternion.identity);
                SoundManager.Instance.PlayMusic(SoundManager.Instance.DrumsOnly);
                newGo.transform.SetParent(GameObject.Find("InGameUi").transform);
                break;
            case 2:
                newGo = Instantiate(Syntetizer, screenCenter, Quaternion.identity);
                newGo.transform.SetParent(GameObject.Find("InGameUi").transform);
                SoundManager.Instance.StopMusic();
                SoundManager.Instance.PlayMusic(SoundManager.Instance.DrumsAndSyntetizer);
                break;
            case 3:
                newGo = Instantiate(Guitar, screenCenter, Quaternion.identity);
                newGo.transform.SetParent(GameObject.Find("InGameUi").transform);
                SoundManager.Instance.StopMusic();
                SoundManager.Instance.PlayMusic(SoundManager.Instance.EveryInstrument);
                break;
        }

        if (newGo != null)
        {
            newGo.SetActive(true);
            yield return new WaitForSeconds(3f);
            Destroy(newGo);
        }

        victoryPanel.SetActive(true);
        Time.timeScale = 0f;
    }
    
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void LoadNextLevel()
    {
        int nextSceneIndex = PlayerPrefs.GetInt("CurrentLevel") + 1;

        SceneManager.LoadScene(nextSceneIndex < SceneManager.sceneCountInBuildSettings ? nextSceneIndex : 0);
    }
    
    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene(0);
    }
}