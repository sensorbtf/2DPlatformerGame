using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class SoundManager : MonoBehaviour
{
	private List<AudioSource> audioSources;
	private Dictionary<GameObject, AudioSource> walkingAudioSources;
	
	public AudioSource MusicSource;
	
	public AudioSource EnemyEffectsSource;

	public static SoundManager Instance { get; private set; }

    private void Awake()
	{
		if (Instance == null)
			Instance = this;
		else if (Instance != this)
			Destroy(gameObject);
		

		audioSources = new List<AudioSource>();
		walkingAudioSources = new Dictionary<GameObject, AudioSource>();
		UnMuteEverythingDespiteMusic();
	}

    public void PlayMusic(AudioClip clip, bool shouldStop = false)
	{
		if (shouldStop)
		{
			MusicSource.Stop();
			return;
		}
		
		MusicSource.Stop();
		MusicSource.loop = false;
		MusicSource.clip = clip;
		MusicSource.Play();
	}
    
    public void StopMusic()
    {
		MusicSource.Stop();
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
	
	public void UnMuteEverythingDespiteMusic()
	{
		MusicSource.loop = true;
		EnemyEffectsSource.mute = false;

		for (int i = 0; i < audioSources.Count; i++)
		{
			audioSources[i].mute = false;
		}

		foreach (var walkingAudioSource in walkingAudioSources)
		{
			walkingAudioSource.Value.mute = false;
		}
	}
}