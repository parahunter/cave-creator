using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPlacer : MonoBehaviour 
{
	public Transform objectToPlace;
	
	public CavePlacementGenerator placementGenerator;
	
	public int category = 1;
	
	public AnimationCurve sizeDistribution;
	
	// Use this for initialization
	void Awake () 
	{
		placementGenerator.onPointsGenerated += OnPoints;
	}
	
	void OnPoints(CavePlacementGenerator generator)
	{
		List<CavePlacementPoint> points = generator.placementPoints[category];
		
		foreach(CavePlacementPoint point in points)
		{
			Transform instantiated = (Transform)Instantiate(objectToPlace, point.position, Quaternion.identity);
		
			instantiated.parent = transform;
			instantiated.up = point.normal;
			
			instantiated.localScale *= sizeDistribution.Evaluate(Random.Range(0f, 1f));
		}
	}
}
