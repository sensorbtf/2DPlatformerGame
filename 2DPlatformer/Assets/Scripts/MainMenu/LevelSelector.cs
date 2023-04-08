using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public int level;
    public void PlayLevel()
    {
        SceneManager.LoadScene("Level" + level.ToString());
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
