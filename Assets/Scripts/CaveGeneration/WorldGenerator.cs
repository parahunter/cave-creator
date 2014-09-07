using UnityEngine;
using System.Collections;

public class WorldGenerator : MonoBehaviour 
{
	public CaveGenerator caveGenerator;
	public CavePlacementGenerator placementGenerator;
	public ColorManager colorManager;
	public int debugSeed = 1337;
			
	public static int currentSeed
	{
		get;
		private set;
	}		
						
	// Use this for initialization
	void Start () 
	{	
		if(PortalMessage.message != null)
		{
			GenerateWorld(PortalMessage.message.newSeed);	
		}
		else
		{
#if UNITY_WEBPLAYER	&& !UNITY_EDITOR
			Application.ExternalCall("GetSeed", "");
#else
			GenerateWorld(debugSeed);
#endif
		}
	}
	
	void SetSeedFromWebpage(string seed)
	{
		GenerateWorld(int.Parse(seed));
	}
		
	public void GenerateWorld(int seed)
	{
		print( "generating world with seed " + seed);
		currentSeed = seed;
		Random.seed = seed;
		
		colorManager.AssignColors();		
								
		caveGenerator.GenerateRepresentation();
		
		caveGenerator.GenerateMesh();
		
		placementGenerator.GeneratePlacementPoints();
				
		placementGenerator.PlaceStuff();		
				
		#if UNITY_WEBPLAYER		
		Application.ExternalCall("SetSeed", currentSeed);
		#endif
	}
	
	public void OnDrawGizmos()
	{
		if(Application.isPlaying == false)
			return;
	
		caveGenerator.VisualiseRepresentation();
	}
}
