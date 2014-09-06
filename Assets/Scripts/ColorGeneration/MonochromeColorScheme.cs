using UnityEngine;
using System.Collections;

public class MonochromeColorScheme : ColorScheme 
{
	public float minSatuation = 0.0f;
	public float maxSatuation = 1.0f;

	public float minValue = 0.0f;
	public float maxValue = 0.0f;
	
	
	
	#region implemented abstract members of ColorScheme
	public override Color[] GetColors (float baseValue)
	{
		Color[] colors = new Color[4];
		
		float h = Mathf.Repeat(baseValue, 360f);
		
		//fog color
		colors[0] = UColor.HSVToRPG(h, Random.Range(minSatuation, maxSatuation), Random.Range(minValue, maxValue));
		colors[1] = UColor.HSVToRPG(h, Random.Range(minSatuation, maxSatuation), Random.Range(minValue, maxValue));
		colors[2] = UColor.HSVToRPG(h, Random.Range(minSatuation, maxSatuation), Random.Range(minValue, maxValue));
		colors[3] = UColor.HSVToRPG(h, Random.Range(minSatuation, maxSatuation), Random.Range(minValue, maxValue));
		                            
		return colors;
	}
	#endregion
	
}
