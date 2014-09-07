using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPlacer : MonoBehaviour 
{
	public Transform[] objectsToPlace;

	public bool placeUpright = false;
	
	public CavePlacementGenerator placementGenerator;
	
	public int category = 1;
	
	public AnimationCurve sizeDistribution;
	public AnimationCurve rotationDitter;
	
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
			Transform objectToPlace = objectsToPlace[Random.Range(0, objectsToPlace.Length)];
			
			Transform instantiated = (Transform)Instantiate(objectToPlace, point.position, Quaternion.identity);
		
			instantiated.parent = transform;
			
			if(placeUpright && rotationDitter.Evaluate(Random.Range(0,1f)) > 0.5f)
            {
            	instantiated.Rotate(Vector3.up * Random.Range(0, 360f));
			}
			else
				instantiated.up = point.normal;
			
			instantiated.localScale *= sizeDistribution.Evaluate(Random.Range(0f, 1f));
		}
	}
}
