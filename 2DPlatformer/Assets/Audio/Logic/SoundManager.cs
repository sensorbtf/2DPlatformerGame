using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	public AudioSource MusicSource;
	public AudioSource EnemyEffectsSource;

	private List<AudioSource> audioSources;
	private Dictionary<GameObject, AudioSource> walkingAudioSources;

	public static SoundManager Instance { get; private set; }

    private void Awake()
	{
		if (Instance == null)
			Instance = this;
		else if (Instance != this)
			Destroy(gameObject);

		audioSources = new List<AudioSource>();
		walkingAudioSources = new Dictionary<GameObject, AudioSource>();
		DontDestroyOnLoad(gameObject);
	}

    public void PlayMusic(AudioClip clip)
	{
		MusicSource.clip = clip;
		MusicSource.Play();
	}
    
	public void PlayEffects(AudioClip clip)
	{
		foreach (var source in audioSources)
		{
			if (!source.isPlaying)
			{
				source.clip = clip;
				source.Play();
				return;
			}
		}
		
		var newAudioComponent = gameObject.AddComponent<AudioSource>();
		audioSources.Add(newAudioComponent);
		newAudioComponent.clip = clip;
		newAudioComponent.Play();
	}
	
	public void PlayWalkingEffects(GameObject gO, AudioClip clip) 
	{
		foreach (var source in walkingAudioSources.ToList())
		{
			if (source.Key == null)
			{
				walkingAudioSources.Remove(source.Key);
				continue;
			}

			if (source.Key == gO && !source.Value.isPlaying)
			{
				source.Value.clip = clip;
				source.Value.Play();
				return;
			}
		}

		if (walkingAudioSources.Any(x => x.Key == gO))
			return;
		
		var newAudioComponent = gameObject.AddComponent<AudioSource>();
		walkingAudioSources.Add(gO, newAudioComponent);
		newAudioComponent.clip = clip;
		newAudioComponent.Play();
	}

	public void MuteEverythingDespiteMusic()
	{
		MusicSource.loop = false;
		EnemyEffectsSource.mute = true;

		for (int i = 0; i < audioSources.Count; i++)
		{
			audioSources[i].mute = true;
		}

		foreach (var walkingAudioSource in walkingAudioSources)
		{
			walkingAudioSource.Value.mute = true;
		}
	}
}