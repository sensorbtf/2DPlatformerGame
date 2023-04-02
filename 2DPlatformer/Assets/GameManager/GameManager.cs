using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameManager
{
        public class GameManager : MonoBehaviour
        {
        public static GameManager instance;
        public TextMeshProUGUI enemiesLeftText;
        public TextMeshProUGUI leftMusicNodesText;
        
        public int totalEnemies; 
        public int totalNotes; 
        private int enemiesDefeated;
        private int notesCollected;
        
        void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        void Update()
        {
            totalEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
            // totalNotes = GameObject.FindGameObjectsWithTag("MusicNotes").Length;

            enemiesLeftText.text = totalEnemies.ToString();
            //leftMusicNodesText.text = totalNotes.ToString();

            EnemyDefeated();
        }

        public void EnemyDefeated()
        {            
            if (totalEnemies == 0) //&& notesCollected == totalNotes)
            {
                LoadNextLevel();
            }
        }
        
        void LoadNextLevel()
        {
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            
            if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(nextSceneIndex);
            }
            else
            {
                SceneManager.LoadScene(0);
            }
        }
    }
}
