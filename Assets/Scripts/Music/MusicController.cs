using UnityEngine;
using System.Collections;

public class MusicController : MonoBehaviour 
{
	public AudioClip[] clips;
	
	AudioClip clipBeingPlayed;
	
	AudioSource[] sources;
	int activeClip = 0;
				
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
		StartCoroutine(Crossfade());
	}
		
	IEnumerator Crossfade()
	{
		
		while(true)
		{
			activeClip++;
			AudioClip clip = clips[ Random.Range(0, clips.Length -1)];
			
			
		}
	}
}
