using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class LocalLensDistortion : MonoBehaviour
{
    private PostProcessVolume volume;
    private LensDistortion lensDistortion;
    public float targetValue;
    public float duration;

    private void Start()
    {
        volume = Camera.main.GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings(out lensDistortion);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            volume = Camera.main.GetComponent<PostProcessVolume>();
            volume.profile.TryGetSettings(out lensDistortion);
            ChangeLensDistortion(targetValue, duration);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            volume = Camera.main.GetComponent<PostProcessVolume>();
            volume.profile.TryGetSettings(out lensDistortion);
            ChangeLensDistortion(0, duration);
        }
    }

    public void ChangeLensDistortion(float targetValue, float duration)
    {
        StartCoroutine(LerpLensDistortion(targetValue, duration));
    }

    IEnumerator LerpLensDistortion(float targetValue, float duration)
    {
        float startTime = Time.time;
        float startValue = lensDistortion.intensity.value;

        while (Time.time < startTime + duration)
        {
            float elapsedTime = Time.time - startTime;
            float t = elapsedTime / duration;
            float currentValue = Mathf.Lerp(startValue, targetValue, t);
            lensDistortion.intensity.Override(currentValue);
            yield return null;
        }

        lensDistortion.intensity.Override(targetValue);
    }

}
