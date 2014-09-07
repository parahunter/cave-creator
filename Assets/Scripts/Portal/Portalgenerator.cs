using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Portalgenerator : MonoBehaviour 
{
	public Portal portalPrefab;
	public int placementCategory = 0;
	
	public CavePlacementGenerator placementGenerator;
	
	public Transform player;
		
	// Use this for initialization
	void Awake () 
	{
		placementGenerator.onPointsGenerated += OnPlacement;
	}
	
	void OnPlacement(CavePlacementGenerator generator)
	{
		List<CavePlacementPoint> points = generator.placementPoints[placementCategory];
		
		int currentSeed = WorldGenerator.currentSeed;
		
		int currentLower = (int) (currentSeed & 0x0000FFFF);
		int currentHigher = currentSeed >> 16; 
								
		for(int i = 0 ; i < points.Count ; i++)
		{
			CavePlacementPoint point = points[i];		

			Portal portal = (Portal)Instantiate(portalPrefab, point.position, Quaternion.identity);
			
			portal.transform.parent = transform;
			portal.transform.up = point.normal;
			
			int lower = currentLower;
			int higher = currentHigher;			
			
			switch(i)
			{
				case 0:
					lower++;
					break;
				case 1:
					higher++;
					break;
				case 2:
					lower--;
					break;
				case 3:
					higher--;
					break;
			}
			
			portal.seed = (int)( lower + (higher << 16) );
			portal.gameObject.name = "portal " + i + " seed " + portal.seed;
			
			if(PortalMessage.message != null && portal.seed == PortalMessage.message.oldSeed)
			{
				
				player.position = portal.playerSpawnPoint.position;
				player.LookAt(Vector3.zero);
				
				Destroy(PortalMessage.message.gameObject);
			}
        }		
		
				
					
	}
}
