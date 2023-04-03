using UnityEngine;

public class SoundManager : MonoBehaviour
{
	public AudioSource MusicSource;
	public AudioSource PlayerEffectsSource;
	public AudioSource PlayerWalkingSource;
	public AudioSource EnemyEffectsSource;
	public AudioSource EnviromentEffectSource;

	public static SoundManager Instance { get; private set; }

    private void Awake()
	{
		if (Instance == null)
			Instance = this;
		else if (Instance != this)
			Destroy(gameObject);
		
		DontDestroyOnLoad(gameObject);
	}

    public void PlayMusic(AudioClip clip)
	{
		MusicSource.clip = clip;
		MusicSource.Play();
	}
    
	public void PlayWalkingEffect(AudioClip clip)
	{
		PlayerWalkingSource.clip = clip;
		PlayerWalkingSource.Play();
	}
	
	public void PlayPlayerEffects(AudioClip clip)
	{
		PlayerEffectsSource.clip = clip;
		PlayerEffectsSource.Play();
	}
	
	public void PlayEnemyEffects(AudioClip clip)
	{
		EnemyEffectsSource.clip = clip;
		EnemyEffectsSource.Play();
	}
	
	public void PlayEnvironmentEffects(AudioClip clip)
	{
		EnviromentEffectSource.clip = clip;
		EnviromentEffectSource.Play();
	}
	
	public void MuteDespiteMusic()
	{
		MusicSource.loop = false;
		PlayerEffectsSource.mute = true;
		EnemyEffectsSource.mute = true;
		EnviromentEffectSource.mute = true;
		PlayerWalkingSource.mute = true;
	}
}
