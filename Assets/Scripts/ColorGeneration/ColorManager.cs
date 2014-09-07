using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ColorManager : MonoBehaviour 
{
	public Material caveMaterial;
	public Material crystalMaterial;
	public Material budMaterial;
	
	public List<ColorScheme> colorSchemes = new List<ColorScheme>();
	
	public ColorSet currentSet;
	
	public float minFog = 0.001f;
	public float maxFog = 0.1f;
	
	
	
	[System.Serializable]
	public class ColorSet
	{
		public Color fogColor = Color.white;
		public FogMode fogMode;
		public float fogDensity = 0.005f;
		public Color primary = Color.white;
		public Color secondary = Color.white;
		public Color third = Color.white;	
	}
		
	// Use this for initialization
	public void AssignColors () 
	{
		currentSet = GenerateSet();
		
		RenderSettings.fogMode = currentSet.fogMode;
		RenderSettings.fogColor = currentSet.fogColor;
		RenderSettings.fogDensity = currentSet.fogDensity;
		
		caveMaterial.color = currentSet.primary;
		budMaterial.color = currentSet.secondary;
		crystalMaterial.color = currentSet.third;
	}
	
	ColorSet GenerateSet()
	{
		ColorSet set = new ColorSet();
		Color[] colors = colorSchemes[Random.Range(0, colorSchemes.Count)].GetColors(Random.Range(0, 99999f));
		
		set.primary = colors[0];
		set.secondary = colors[1];
		set.third = colors[2];
		set.fogColor = colors[3];
		
		set.fogMode = FogMode.Exponential;
		set.fogDensity = Random.Range(minFog, maxFog);
				
		return set;
	}
}
