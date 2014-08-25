using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour 
{
	public int seed;
	
	public string tagToTriggerOn = "Player";
	public Transform playerSpawnPoint;
	
	void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag(tagToTriggerOn))
		{
			Use ();
		}
	}
	
	void Use()
	{
		GameObject messageObject = new GameObject("PortalMessage");
		PortalMessage message = messageObject.AddComponent<PortalMessage>();
		PortalMessage.message = message;
		message.oldSeed = WorldGenerator.currentSeed;
		message.newSeed = seed;
		
		DontDestroyOnLoad(messageObject);
		
		Application.LoadLevel(Application.loadedLevel);
		
	}
	
}
