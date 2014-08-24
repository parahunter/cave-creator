using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]
public class MeshFromBezierGenerator : MonoBehaviour 
{
	public int horziontalIntersections = 5;
	public int verticalIntersections = 5;
							
	Vector3[] vertices;						
	
	public void Generate(UBezier[] contour, UBezier[] ceiling, UBezier[] floor)
	{
		Mesh mesh = new Mesh();
		
		vertices = new Vector3[horziontalIntersections * verticalIntersections * contour.Length];
		
		int offset = horziontalIntersections * verticalIntersections;
		
		for(int c = 0 ; c < contour.Length ; c++)
		{
			UBezier contourBezier = contour[c];
			UBezier ceilingBezierStart = ceiling[c];
			UBezier ceilingBezierEnd = ceiling[(c+1) % ceiling.Length];
						
						
			for(int i = 0 ; i < horziontalIntersections ; i++)
			{
				
				for(int k = 0 ; k < verticalIntersections ; k++)
				{
					float fi = (float)i / (horziontalIntersections -1);
					float fk = (float)k / (verticalIntersections - 1);
					
					Vector3 ceilStart = ceilingBezierStart.Evaluate(fk);
					Vector3 ceilEnd = ceilingBezierEnd.Evaluate(fk);
					
					float distanceCeilStart = ceilStart.magnitude / ceilingBezierStart.p0.magnitude;
					float distanceCeilEnd = ceilEnd.magnitude / ceilingBezierEnd.p0.magnitude;
																				
					float normalizedDistanceToCenter = 1 - Mathf.Lerp( distanceCeilStart, distanceCeilEnd, fi);																														
																																																																																															
					Vector3 heightPosition = Vector3.Lerp(ceilStart, ceilEnd, fi);
					
					//interpolate distance to center
					Vector3 contourPosition = contourBezier.Evaluate(fi);
										
					contourPosition = Vector3.Lerp(contourPosition, Vector3.zero, normalizedDistanceToCenter);
					contourPosition.y = heightPosition.y;	
										
					vertices[c * offset + i * verticalIntersections + k] = contourPosition;
					
				}								
			}									
		}				
	}
	
	void OnDrawGizmos()
	{
		if(Application.isPlaying == false || vertices == null)
			return;
			
		foreach(Vector3 vertex in vertices)
		{
			Gizmos.DrawCube(vertex, Vector3.one * 0.1f);
		}
	}
	
}
