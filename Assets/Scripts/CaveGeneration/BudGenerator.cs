using UnityEngine;
using System.Collections;

public class BudGenerator : MonoBehaviour 
{
	public int minBasePoints = 3;
	public int maxBasePoints = 6;
	public AnimationCurve radiusProbability;
	public AnimationCurve heightProbability;
	
	UBezier[] contour;
	UBezier[] topCurves;
	
	public MeshFromBezierGenerator meshGenerator;
	// Use this for initialization
	void Start () 
	{
		int amount = Random.Range(minBasePoints, maxBasePoints);
		float height = heightProbability.Evaluate(Random.Range(0, 1f));
		float radius = radiusProbability.Evaluate(Random.Range(0, 1f));
		contour = new UBezier[amount];
		topCurves = new UBezier[amount];		
		
		Vector3[] basePoints = new Vector3[amount];
						
		for(int i = 0 ; i < amount ; i++)
		{
			float fi = i /(float)amount;
			Vector3 direction = new Vector3(Mathf.Cos(fi * Mathf.PI * 2), 0, Mathf.Sin(fi * Mathf.PI * 2));
			
			basePoints[i] = direction * radius + Vector3.down;		
		}		
				
				
		float extrusion = 2 * Random.Range(-1, 1f);
		float blah = 1f * Random.Range(-1f, 1f);
		
				
		for(int i = 0 ; i < amount ; i++)
		{
			Vector3 p0 = basePoints[i];
			Vector3 p3 = basePoints[(i+1) % amount];
			Vector3 p1 = p0 * extrusion;
			Vector3 p2 = p3 * extrusion;
			
			contour[i] = new UBezier(p0, p1, p2, p3);
			p3 = Vector3.up * height;
			p2 += Vector3.up * blah;
						
			topCurves[i] = new UBezier(p0, p1, p2, p3);
			
		}
		
		meshGenerator.Generate( contour, topCurves, null);								
	}
	
	void OnDrawGizmosSelected()
	{
		if(!Application.isPlaying)
			return;
	
		foreach(UBezier topCurve in topCurves)
			topCurve.Visualise();
			
		foreach(UBezier baseCurve in contour)
			baseCurve.Visualise();
	}
}
