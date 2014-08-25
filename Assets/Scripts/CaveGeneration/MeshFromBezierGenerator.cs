using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]
public class MeshFromBezierGenerator : MonoBehaviour 
{
	public int horziontalIntersections = 5;
	public int verticalIntersections = 5;
							
	Vector3[] vertices;				
	int[] triangles;
	Vector3[] normals;
	
	public bool invert = false;
	int offset;
	int contourCount;
	
    public void Generate(UBezier[] contour, UBezier[] ceiling, UBezier[] floor)
	{
		Mesh mesh = new Mesh();
		
		vertices = new Vector3[horziontalIntersections * verticalIntersections * contour.Length];
		triangles = new int[(horziontalIntersections ) * (verticalIntersections ) * contour.Length * 6]; 
		
		offset = horziontalIntersections * verticalIntersections;
		contourCount = contour.Length;
		
		int quadsPerSegment = (horziontalIntersections) * (verticalIntersections);
        		
		for(int c = 0 ; c < contour.Length ; c++)
		{
			UBezier contourBezier = contour[c];
						
			for(int h = 0 ; h < horziontalIntersections ; h++)
			{	
				for(int v = 0 ; v < verticalIntersections ; v++)
				{
					float fh = (float)h / horziontalIntersections;
					float fv = (float)v / (verticalIntersections - 1);
					
					UBezier ceilingBezierStart = ceiling[c];
					UBezier ceilingBezierEnd = ceiling[(c+1) % floor.Length];
					
					Vector3 startEndPoint = ceilingBezierStart.p3;
					Vector3 endEndPoint = ceilingBezierEnd.p3;
					
					if(floor != null)
					{
						if(v > verticalIntersections / 2)
						{
							ceilingBezierStart = floor[c];
							ceilingBezierEnd = floor[(c+1) % floor.Length];
//							startEndPoint = ceilingBezierStart.p0;
//							endEndPoint = ceilingBezierEnd.p0;
							fv = (fv - 0.5f) * 2;
						}
						else
							fv *= 2;
					}						
																
					Vector3 contourPosition = GeneratePosition(contour[c], ceilingBezierStart, ceilingBezierEnd, fh, fv, startEndPoint, endEndPoint);
										
					vertices[c * offset + h * verticalIntersections + v] = contourPosition;
				}								
			}									
		}
		
		int quad;
		for(int c = 0 ; c < contour.Length ; c++)
		{
			for(int i = 0 ; i < horziontalIntersections ; i++)
			{	
				quad = quadsPerSegment * c + i * verticalIntersections;
                
				for(int k = 0 ; k < verticalIntersections - 1 ; k++)
				{
					if(invert)
					{
						triangles[quad * 6] = VertexIndex( quad );
						triangles[quad * 6 + 1] = VertexIndex( quad + verticalIntersections );
						triangles[quad * 6 + 2] = VertexIndex( quad + 1 ); 
						triangles[quad * 6 + 3] = VertexIndex( quad + verticalIntersections );
						triangles[quad * 6 + 4] = VertexIndex( quad + verticalIntersections + 1 );
						triangles[quad * 6 + 5] = VertexIndex( quad + 1 );
					}
					else
					{
						triangles[quad * 6] = VertexIndex( quad );
						triangles[quad * 6 + 1] = VertexIndex( quad + 1 );
						triangles[quad * 6 + 2] = VertexIndex( quad + verticalIntersections );
						triangles[quad * 6 + 3] = VertexIndex( quad + verticalIntersections );
						triangles[quad * 6 + 4] = VertexIndex( quad + 1 );
						triangles[quad * 6 + 5] = VertexIndex( quad + verticalIntersections + 1 );
					}
					quad++;
                }
            }	
        }	
        
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
		normals = mesh.normals;
            
		GetComponent<MeshFilter>().mesh = mesh;
		
		GetComponent<MeshCollider>().sharedMesh = mesh;
        
    }
    
   	public Vector3 GeneratePosition(UBezier contour, UBezier heightStart, UBezier heightEnd, float c, float h, Vector3 startP3, Vector3 endP3)
    {
		Vector3 ceilStart = heightStart.Evaluate(h);
		Vector3 ceilEnd = heightEnd.Evaluate(h);
		
		Vector3 heightPosition = Vector3.Lerp(ceilStart, ceilEnd, c);
		ceilEnd.y = 0;
		ceilStart.y = 0;
		
		float distanceCeilStart = ceilStart.magnitude / startP3.magnitude;
		float distanceCeilEnd = ceilEnd.magnitude / endP3.magnitude;
		
		float normalizedDistanceToCenter = 1 - Mathf.Lerp( distanceCeilStart, distanceCeilEnd, c);																														
		
		//interpolate distance to center
		Vector3 contourPosition = contour.Evaluate(c);
		
		contourPosition = Vector3.Lerp(contourPosition, Vector3.zero, normalizedDistanceToCenter);
		contourPosition.y = heightPosition.y;
		
		return contourPosition;
    }
    
	public Vector3 GenerateNormal( int c, float h, float v)
	{
		int hIndex = (int)(h * (horziontalIntersections - 1));
		int vIndex = (int)(v * (verticalIntersections - 1));
		int index = offset * c + hIndex * verticalIntersections + vIndex;
				
		return normals[index];
	}
    
	            
	int VertexIndex(int absolouteIndex)
	{
		return absolouteIndex % vertices.Length;	
	}           
	            
	void OnDrawGizmos()
	{
        if(Application.isPlaying == false || vertices == null)
			return;
			
//		for(int i = 0; i < vertices.Length ; i++)
//		{
//			Gizmos.DrawLine(vertices[i], vertices[i] + normals[i]);
//		}	
//			
//		foreach(Vector3 vertex in vertices)
//		{
//			Gizmos.DrawCube(vertex, Vector3.one * 1f);
//		}
				
	}
	
}
