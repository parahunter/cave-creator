using UnityEngine;
using System.Collections;

public class AdjacentColorScheme : ColorScheme 
{
	public float minDiffHue = 5f;
	public float maxDifHue = 170f;
	
	public float minSaturation = 0;
	public float maxSaturation = 1;
	public float minValue = 0;
	public float maxValue = 1;
	

	#region implemented abstract members of ColorScheme
	public override Color[] GetColors (float baseValue)
	{	
		float basehue = Mathf.Repeat(baseValue, 360f);
		
		float oppositeHue = basehue + 180f;
		
		float hueDif = Random.Range(minDiffHue, maxDifHue);
		
		float secondaryHue = Mathf.Repeat(oppositeHue - hueDif, 360f);
		float thirdHue = Mathf.Repeat(oppositeHue + hueDif, 360f);
										
		Color[] colors = new Color[4];
		
		//fog color
		colors[0] = UColor.HSVToRPG(basehue, Random.Range(minSaturation, maxSaturation), Random.Range(minValue, maxValue));
		colors[1] = UColor.HSVToRPG(secondaryHue, Random.Range(minSaturation, maxSaturation), Random.Range(minValue, maxValue));
		colors[2] = UColor.HSVToRPG(basehue, Random.Range(minSaturation, maxSaturation), Random.Range(minValue, maxValue));
		colors[3] = UColor.HSVToRPG(thirdHue, Random.Range(minSaturation, maxSaturation), Random.Range(minValue, maxValue));
		
		return colors;
	}
	#endregion
	
}
