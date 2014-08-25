using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CavePlacementGenerator : MonoBehaviour 
{
	[System.Serializable]
	public class PlacementCategory
	{
		public AnimationCurve pointsToSizeCorelation;
		public AnimationCurve heightBias;
		public float minDistanceToOtherObjects;
		public float minDistanceToSameObjects;
		public bool disregardOtherPlacedObjects = false;
		
	}

	public List<PlacementCategory> categories = new List<PlacementCategory>();

	public CaveGenerator caveGenerator;
	
	public event System.Action<CavePlacementGenerator>  onPointsGenerated;
	
	public List<List<CavePlacementPoint>> placementPoints
	{
		get;
		private set;
	}
	
	public void GeneratePlacementPoints()
	{
		placementPoints = new List<List<CavePlacementPoint>>( categories.Count );
		
		for(int i = 0 ; i < categories.Count ; i++)
		{
			PlacementCategory category = categories[i];
			
			int amount = (int)category.pointsToSizeCorelation.Evaluate(caveGenerator.size);
			
			int counter = 0;
			
			List<CavePlacementPoint> points = new List<CavePlacementPoint>(amount);
			placementPoints.Add(points);
			while(counter < amount)
			{
				CavePlacementPoint point = caveGenerator.GetPointOnSurface(category.heightBias);
				
				int failsafeCounter = 0;
               	if(!category.disregardOtherPlacedObjects)
				{
					bool canPlace = true;
					for(int k = i ; k >= 0 ; k--)
					{
						PlacementCategory categoryToCheckAgainst = categories[k];

						float measure;
						if(k == i)
							measure = categoryToCheckAgainst.minDistanceToSameObjects;
                        else
							measure = categoryToCheckAgainst.minDistanceToOtherObjects;
                                
						float distanceSquared = Mathf.Pow( measure, 2);
						List<CavePlacementPoint> pointsToCheckAgainst = placementPoints[k];
						
						foreach(CavePlacementPoint pointToCheckAgainst in pointsToCheckAgainst)
						{
							float distance = (point.position - pointToCheckAgainst.position).sqrMagnitude;
							if(distanceSquared > distance)
								canPlace = false;
						}
						    
					}
					
					if(canPlace == false && failsafeCounter < 100)
					{
						failsafeCounter++;
						continue;
					}
				}
				
				points.Add(point);
				
				counter++;
			}
			
			print ("points added " + points.Count);			
		}
		
	}
	
	void OnDrawGizmosSelected()
	{
		if(!Application.isPlaying)
			return;
	
		Gizmos.color = Color.blue;
		foreach(List<CavePlacementPoint> points in placementPoints)
		{
			foreach(CavePlacementPoint point in points)
			{
				Gizmos.DrawLine(point.position, point.position + point.normal * 10);
			}
		}
	}
	

	void Start()
	{
		if(onPointsGenerated != null)
			onPointsGenerated(this);
	}

}
