﻿using UnityEngine;
using System.Collections;

public class WorldGenerator : MonoBehaviour 
{
	public CaveGenerator caveGenerator;
	public int debugSeed = 1337;
			
	// Use this for initialization
	void Awake () 
	{
		if(debugSeed != 0)
			GenerateWorld(debugSeed);			
		
	}
	
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.G))
			GenerateWorld(Random.Range(0, 1000000));
	}
	
	public void GenerateWorld(int seed)
	{
		Random.seed = seed;
		
		caveGenerator.GenerateRepresentation();
		
		
		
		caveGenerator.GenerateMesh();
	}
	
	public void OnDrawGizmos()
	{
		if(Application.isPlaying == false)
			return;
	
		caveGenerator.VisualiseRepresentation();
	}
}
