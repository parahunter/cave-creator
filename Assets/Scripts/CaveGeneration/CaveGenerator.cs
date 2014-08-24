using UnityEngine;
using System.Collections;

public class CaveGenerator : ProceduralGenerator 
{
	public float minLength = 30f;
	public float maxLength = 50f;
	public float minWidth = 30f;
	public float maxWidth = 50f;
	
	Vector3[] contourPoints = new Vector3[8];
	UBezier[] contour = new UBezier[8];
	
	#region implemented abstract members of ProceduralGenerator
	public override void GenerateRepresentation ()
	{
		GenerateContour();
	}
	
	void GenerateContour()
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
	
	public override void VisualiseRepresentation ()
	{
		Gizmos.color = Color.green;
		for(int i = 0 ; i < contourPoints.Length; i++)
			Gizmos.DrawLine(contourPoints[i], contourPoints[ (i+1) % contourPoints.Length]);
			
		
	}
	
	public override void GenerateMesh ()
	{
		throw new System.NotImplementedException ();
	}
	
	#endregion
	
}
