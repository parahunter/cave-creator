using UnityEngine;
using System.Collections;

public class WorldGenerator : MonoBehaviour 
{
	public CaveGenerator caveGenerator;
	public CavePlacementGenerator placementGenerator;
	public int debugSeed = 1337;
			
	public static int currentSeed
	{
		get;
		private set;
	}		
			
	// Use this for initialization
	void Awake () 
	{
		if(PortalMessage.message != null)
			GenerateWorld(PortalMessage.message.newSeed);	
		else		
			GenerateWorld(debugSeed);			
		
	}
	
	public void GenerateWorld(int seed)
	{
		print( "generating world with seed " + seed);
		currentSeed = seed;
		Random.seed = seed;
		
		caveGenerator.GenerateRepresentation();
		
		caveGenerator.GenerateMesh();
		
		placementGenerator.GeneratePlacementPoints();
				
	}
	
	public void OnDrawGizmos()
	{
		if(Application.isPlaying == false)
			return;
	
		caveGenerator.VisualiseRepresentation();
	}
}
