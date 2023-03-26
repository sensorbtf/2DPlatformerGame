using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Sprite fullHeart; // potential healthregen feature?
    [SerializeField] private Sprite brokenHeart;
    [SerializeField] private GameObject heartPrefab;
    [SerializeField] private RectTransform heartsRect;
    
    private List<Image> fullHearts;
    private int currentHealth = 0;
    
    void Start()
    {
        float neededHearthsRectSize = 0;
        RectTransform heartRectTransform = new RectTransform();

        fullHearts = new List<Image>();
        
        for (int i = 0; i < PlayerController.Instance.Health; i++)
        {
            GameObject tempGO = Instantiate(heartPrefab, GameObject.Find("Hearts").transform, true);
            fullHearts.Add(tempGO.GetComponent<Image>());
            
            heartRectTransform = (RectTransform)tempGO.transform;
            neededHearthsRectSize += heartRectTransform.rect.width;
        }
        
        heartsRect.sizeDelta = new Vector2(neededHearthsRectSize, heartRectTransform.rect.height);

        PlayerController.Instance.OnHealthLose += LoseHeart;
    }

    private void LoseHeart(int p_heartLost)
    {
        for (int i = 0; i < p_heartLost; i++)
        {
            if (fullHearts.Count > currentHealth)
            {
                fullHearts[currentHealth].sprite = brokenHeart;
                currentHealth++;  
            }
        }
    }
}
