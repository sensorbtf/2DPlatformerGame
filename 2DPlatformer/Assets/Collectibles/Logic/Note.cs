using UnityEngine;

public class Note : MonoBehaviour
{
    [SerializeField] private AudioClip collectingCoinAudio;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) 
            return;
        
        SoundManager.Instance.PlayEffects(collectingCoinAudio);
        Destroy(gameObject);
    }
}
