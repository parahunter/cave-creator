using UnityEngine;
using System.Collections;

public class CaveGenerator : ProceduralGenerator 
{
	public float minLength = 30f;
	public float maxLength = 50f;
	public float minWidth = 30f;
	public float maxWidth = 50f;
	public float minHeight = 10;
	public float maxHeight = 10;
	
	public float curveAngleDitterDegrees = 20f;
	public float curveMidPointsMinDistance = 0.1f;
	public float curveMidPointsMaxDistance = 0.5f;
	public float minHeightDitter = 0.3f;	
	public float maxHeightDitter = 0.8f;
	
	public MeshFromBezierGenerator meshGenerator;
	
	Vector3[] contourPoints = new Vector3[8];
	UBezier[] contourCurve = new UBezier[8];
	UBezier[] ceilingCurves = new UBezier[8];
	UBezier[] floorCurves = new UBezier[8];
	
	
	
	#region implemented abstract members of ProceduralGenerator
	public override void GenerateRepresentation ()
	{
		GenerateContourPoints();
		GenerateContourCurve();
		GenerateCeilingAndFloorCurves();
	}
	
	void GenerateContourPoints()
	{
		//first generate primary control points
		contourPoints[0] = Vector3.left * Random.Range(minWidth, maxWidth); 
		contourPoints[2] = Vector3.forward * Random.Range(minLength, maxLength); 
		contourPoints[4] = -Vector3.left * Random.Range(minWidth, maxWidth); 
		contourPoints[6] = -Vector3.forward * Random.Range(minLength, maxLength); 
		
		//then generate secondary points
		contourPoints[1] =  Vector3.left * Random.Range(minWidth, maxWidth) + 
							Vector3.forward * Random.Range(minLength, maxLength); 
		
		contourPoints[3] = -Vector3.left * Random.Range(minWidth, maxWidth) + 
						    Vector3.forward * Random.Range(minLength, maxLength); 
		
		contourPoints[5] = -Vector3.left * Random.Range(minWidth, maxWidth) + 
							-Vector3.forward * Random.Range(minLength, maxLength); 
		
		contourPoints[7] = Vector3.left * Random.Range(minWidth, maxWidth) + 
						  -Vector3.forward * Random.Range(minLength, maxLength); 
	}
	
	void GenerateContourCurve()
	{
		//how much the secondary control point's position should change
		float[] angleDitters = new float[contourCurve.Length];
		for(int i = 0 ; i < angleDitters.Length ; i++)
			angleDitters[i] = Random.Range(-curveAngleDitterDegrees, curveAngleDitterDegrees);
						
		for(int i = 0 ; i < contourPoints.Length; i++)
		{
			//assign end points
			Vector3 p0 = contourPoints[i];
			Vector3 p3 = contourPoints[(i+1) % contourPoints.Length];
			
			//calculate mid points
			float distance = Vector3.Distance(p0, p3);
			Vector3 direction = new Vector3(-p0.z, 0, p0.x).normalized;
			Vector3 p1 = p0 + GenerateBezierMidPoint(-direction, distance, angleDitters[i]);
			direction = new Vector3(-p3.z, 0, p3.x).normalized * -1;
			
			Vector3 p2 = p3 + GenerateBezierMidPoint(-direction, distance, angleDitters[(i+1) % contourPoints.Length]);;

			contourCurve[i] = new UBezier(p0, p1, p2, p3);
			
		}
	}
	
	void GenerateCeilingAndFloorCurves()
	{
		float ceilingHeight = Random.Range(minHeight, maxHeight);
		
		for(int i = 0 ; i < ceilingCurves.Length ; i++)
		{
			Vector3 p0 = Vector3.up * ceilingHeight;
			
			Vector3 p1 = Vector3.up * Random.Range(minHeightDitter, maxHeightDitter) * ceilingHeight + contourCurve[i].p0;
			Vector3 p2 = p1;			
			Vector3 p3 = contourCurve[i].p0;
			
			ceilingCurves[i] = new UBezier(p0, p1, p2, p3);
			
			p1.y *= -1;
			p2.y *= -1;
			p0.y *= -1;
						
			floorCurves[i] = new UBezier(p3, p2, p1, p0);									
		}
						
		
	}
	
	Vector3 GenerateBezierMidPoint(Vector3 direction, float distance, float angleDitter)
	{
		Quaternion rotation = Quaternion.Euler(new Vector3(0, angleDitter, 0));
		direction = rotation * direction;
		
		return direction * distance * Random.Range(curveMidPointsMinDistance, curveMidPointsMaxDistance);							
	}
	
	public override void VisualiseRepresentation ()
	{
		Gizmos.color = Color.green;
		for(int i = 0 ; i < contourPoints.Length; i++)
			Gizmos.DrawLine(contourPoints[i], contourPoints[ (i+1) % contourPoints.Length]);
		
		Gizmos.color = Color.red;
		for(int i = 0 ; i < contourCurve.Length ; i++)
		{	
			contourCurve[i].Visualise();
		}
		
		for(int i = 0 ; i < ceilingCurves.Length; i++)
		{	
			ceilingCurves[i].Visualise();
		}
		
		for(int i = 0 ; i < floorCurves.Length; i++)
		{	
			floorCurves[i].Visualise();
		}
		
		
	}
	
	public override void GenerateMesh ()
	{
		meshGenerator.Generate(contourCurve, ceilingCurves, floorCurves);
	}
	
	#endregion
	
}
