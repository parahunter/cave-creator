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
		public float minDistanceBetweenPoints;
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
			while(counter < amount)
			{
				CavePlacementPoint point = caveGenerator.GetPointOnSurface(category.heightBias);
				
				points.Add(point);
				
				counter++;
			}
						
			placementPoints.Add(points);
		}
						
		
			
		
		
	}

	void Start()
	{
		if(onPointsGenerated != null)
			onPointsGenerated(this);
	}

}
