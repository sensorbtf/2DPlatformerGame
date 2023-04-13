using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class LocalColorGrading : MonoBehaviour
{
    private PostProcessVolume volume;
    public PostProcessProfile initialProfile;
    public PostProcessProfile overrideProfile;


    private void Start()
    {
        volume = Camera.main.GetComponent<PostProcessVolume>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(OverrideColorGrading(overrideProfile));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(OverrideColorGrading(initialProfile));
        }
    }

    private IEnumerator OverrideColorGrading(PostProcessProfile overrideProfile)
    {
        volume.profile = overrideProfile;
        yield return null;
    }

}

