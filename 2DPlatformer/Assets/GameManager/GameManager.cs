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
    
    void Awake()
    {
        victoryPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        
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
        PlayerPrefs.SetInt("CurrentLevel", SceneManager.GetActiveScene().buildIndex);
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
        Time.timeScale = 0f;
    }
    
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void LoadNextLevel()
    {
        int nextSceneIndex = PlayerPrefs.GetInt("CurrentLevel") + 1;
        
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }
    
    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene(0);
    }
}