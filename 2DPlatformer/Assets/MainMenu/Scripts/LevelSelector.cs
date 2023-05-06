using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public int level;
    public void PlayLevel()
    {
        if (level.Equals(0))
        {
            SceneManager.LoadScene("TutorialLevel");
        }
        else
        {
            SceneManager.LoadScene("Level" + level.ToString());
        }
      
    }

    private void Start()
    {
        Button button = gameObject.GetComponentInChildren<Button>();
        if (PlayerPrefs.GetInt("CurrentLevel") >= level)
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }
    }
}
