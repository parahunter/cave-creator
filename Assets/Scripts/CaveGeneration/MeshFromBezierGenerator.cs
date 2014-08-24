using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]
public class MeshFromBezierGenerator : MonoBehaviour 
{
	public int horziontalIntersections = 5;
	public int verticalIntersections = 5;
							
	Vector3[] vertices;
	Vector3[] normals;					
	int[] triangles;
	
	public void Generate(UBezier[] contour, UBezier[] ceiling, UBezier[] floor)
	{
		Mesh mesh = new Mesh();
		
		vertices = new Vector3[horziontalIntersections * verticalIntersections * contour.Length];
		normals = new Vector3[vertices.Length];
		triangles = new int[(horziontalIntersections ) * (verticalIntersections ) * contour.Length * 6]; 
		
		int offset = horziontalIntersections * verticalIntersections;
		
		int quadsPerSegment = (horziontalIntersections) * (verticalIntersections);
        		
		for(int c = 0 ; c < contour.Length ; c++)
		{
			UBezier contourBezier = contour[c];
			UBezier ceilingBezierStart = ceiling[c];
			UBezier ceilingBezierEnd = ceiling[(c+1) % ceiling.Length];
						
			for(int i = 0 ; i < horziontalIntersections ; i++)
			{	
				for(int k = 0 ; k < verticalIntersections ; k++)
				{
					float fi = (float)i / horziontalIntersections;
					float fk = (float)k / (verticalIntersections - 1);
					
					Vector3 ceilStart = ceilingBezierStart.Evaluate(fk);
					Vector3 ceilEnd = ceilingBezierEnd.Evaluate(fk);
					
					Vector3 heightPosition = Vector3.Lerp(ceilStart, ceilEnd, fi);
					ceilEnd.y = 0;
					ceilStart.y = 0;
                    
                    float distanceCeilStart = ceilStart.magnitude / ceilingBezierStart.p0.magnitude;
					float distanceCeilEnd = ceilEnd.magnitude / ceilingBezierEnd.p0.magnitude;
																				
					float normalizedDistanceToCenter = 1 - Mathf.Lerp( distanceCeilStart, distanceCeilEnd, fi);																														
					
					//interpolate distance to center
					Vector3 contourPosition = contourBezier.Evaluate(fi);
										
					contourPosition = Vector3.Lerp(contourPosition, Vector3.zero, normalizedDistanceToCenter);
					contourPosition.y = heightPosition.y;	
										
					vertices[c * offset + i * verticalIntersections + k] = contourPosition;
					normals[c * offset + i * verticalIntersections + k] = -contourPosition.normalized;
					
					
					
				}								
			}									
		}
			
		print("tris " + (triangles.Length) );
		
		int quad;
		for(int c = 0 ; c < contour.Length ; c++)
		{
			for(int i = 0 ; i < horziontalIntersections ; i++)
			{	
				quad = quadsPerSegment * c + i * verticalIntersections;
                
				for(int k = 0 ; k < verticalIntersections - 1 ; k++)
				{
					triangles[quad * 6] = VertexIndex( quad );
					triangles[quad * 6 + 1] = VertexIndex( quad + 1 );
					triangles[quad * 6 + 2] = VertexIndex( quad + verticalIntersections );
					triangles[quad * 6 + 3] = VertexIndex( quad + verticalIntersections );
					triangles[quad * 6 + 4] = VertexIndex( quad + 1 );
					triangles[quad * 6 + 5] = VertexIndex( quad + verticalIntersections + 1 );
					
					quad++;
                }
            }	
        }	
        
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        
		GetComponent<MeshFilter>().mesh = mesh;
		
		GetComponent<MeshCollider>().sharedMesh = mesh;
        
    }
	            
	int VertexIndex(int absolouteIndex)
	{
		return absolouteIndex % vertices.Length;	
	}           
	            
	void OnDrawGizmos()
	{
            if(Application.isPlaying == false || vertices == null)
			return;
//			
//		foreach(Vector3 vertex in vertices)
//		{
//			Gizmos.DrawCube(vertex, Vector3.one * 1f);
//		}
				
	}
	
}
