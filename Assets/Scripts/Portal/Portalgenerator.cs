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
		
		for(int i = 0 ; i < points.Count ; i++)
		{
			CavePlacementPoint point = points[i];		

			Portal portal = (Portal)Instantiate(portalPrefab, point.position, Quaternion.identity);
			
			portal.transform.parent = transform;
			portal.transform.up = point.normal;
			
			if(i == 0 && PortalMessage.message != null)
			{
				portal.seed = PortalMessage.message.oldSeed;
				
				player.position = portal.playerSpawnPoint.position;
				player.LookAt(Vector3.zero);
				
				Destroy(PortalMessage.message.gameObject);
			}
			else
				portal.seed = Random.Range(0, int.MaxValue);
        }		
								
	}
}
