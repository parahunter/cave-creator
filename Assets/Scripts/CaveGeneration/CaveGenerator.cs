using UnityEngine;
using System.Collections;

public class CaveGenerator : ProceduralGenerator 
{
	public float minLength = 30f;
	public float maxLength = 50f;
	public float minWidth = 30f;
	public float maxWidth = 50f;
	
	public float curveAngleDitterDegrees = 20f;
	public float curveMidPointsMinDistance = 0.1f;
	public float curveMidPointsMaxDistance = 0.5f;
		
	Vector3[] contourPoints = new Vector3[8];
	UBezier[] contourCurve = new UBezier[8];
	
	#region implemented abstract members of ProceduralGenerator
	public override void GenerateRepresentation ()
	{
		GenerateContourPoints();
		GenerateContourCurve();
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
		
		for(int i = 0 ; i < contourCurve.Length ; i++)
		{	
			Gizmos.color = Color.red;
			contourCurve[i].Visualise();
		}
	}
	
	public override void GenerateMesh ()
	{
		throw new System.NotImplementedException ();
	}
	
	#endregion
	
}
