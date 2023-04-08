using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI enemiesLeftText;
    [SerializeField] private TextMeshProUGUI leftMusicNodesText;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject victoryPanel;
    
    private int totalEnemies; 
    private int totalNotes; 
    
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
    }
    
    void Update()
    {
        totalEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        totalNotes = GameObject.FindGameObjectsWithTag("Note").Length;

        enemiesLeftText.text = totalEnemies.ToString();
        leftMusicNodesText.text = totalNotes.ToString();
        
        IsLevelCompleted();
    }

    private void IsLevelCompleted()
    {            
        if (totalEnemies == 0 && totalNotes == 0)
            ShowLevelCompletedPanel();
    }

    private void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    private void ShowLevelCompletedPanel()
    {
        victoryPanel.SetActive(true);
        PlayerPrefs.SetInt("CurrentLevel", SceneManager.GetActiveScene().buildIndex);
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