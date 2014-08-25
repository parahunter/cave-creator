using UnityEngine;
using System.Collections;

public class MusicController : MonoBehaviour 
{
	public AudioClip[] clips;
	
	AudioClip clipBeingPlayed;
	
	AudioSource[] sources;
	int activeSource = 0;
				
	public float fadeTime = 2f;			
	public float volume = 0.5f;			
	public AnimationCurve fadeCurve;
				
	public static MusicController instance
	{
		get;
		private set;
	}
				
	// Use this for initialization
	void Awake () 
	{
		if(instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
			sources = new AudioSource[2];
			sources[0] = gameObject.AddComponent<AudioSource>();
			sources[1] = gameObject.AddComponent<AudioSource>();
						
			Play ();
		}
		else
			Destroy(gameObject);
		
	}
		
	void Play()
	{
		StopAllCoroutines();
		StartCoroutine(Crossfade());
	}
	
	void OnLevelWasLoaded()
	{
		Play ();
	}
	
	IEnumerator Crossfade()
	{
		activeSource++;
		AudioClip clip = clips[ Random.Range(0, clips.Length -1)];
		
		float accTime = 0;
		
		if(clipBeingPlayed != clip)
		{
			float f = 0;
			
			clipBeingPlayed = clip;
			//sources[activeSource % sources.Length].time = Random.Range(0, clip.length);
			sources[activeSource % sources.Length].clip = clip;
			
			sources[activeSource % sources.Length].Play();
			
			while(f <= 1f)
			{
				float fadeValue = fadeCurve.Evaluate(f); 
				sources[(activeSource + 1) % sources.Length].volume = volume * fadeValue;
				sources[(activeSource) % sources.Length].volume = volume * (1 - fadeValue);
			
				f += Time.deltaTime / fadeTime;
				accTime += Time.deltaTime;
				yield return 0;
			}
		}

		print (accTime);

		sources[activeSource % sources.Length].volume = volume;
		
		yield return 0;
	}
}
