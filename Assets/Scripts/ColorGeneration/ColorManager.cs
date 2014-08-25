using UnityEngine;
using System.Collections;

public class ColorManager : MonoBehaviour 
{
	public Material caveMaterial;
	public Material crystalMaterial;
	public Material budMaterial;
	
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
	
	public ColorSet[] sets;
	
	// Use this for initialization
	void Start () 
	{
		ColorSet set = sets[WorldGenerator.currentSeed % sets.Length];
		
		RenderSettings.fogMode = set.fogMode;
		RenderSettings.fogColor = set.fogColor;
		caveMaterial.color = set.primary;
		budMaterial.color = set.secondary;
		crystalMaterial.color = set.third;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
