using UnityEngine;
using System.Collections;

public class Portalgenerator : MonoBehaviour 
{
	public Portal portalPrefab;
	public int placementCategory = 0;
	
	public CavePlacementGenerator placementGenerator;
		
	// Use this for initialization
	void Awake () 
	{
		placementGenerator.onPointsGenerated += OnPlacement;
	}
	
	void OnPlacement(CavePlacementGenerator generator)
	{
		
	}
}
