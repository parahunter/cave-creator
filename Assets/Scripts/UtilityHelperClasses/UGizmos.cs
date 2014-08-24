using UnityEngine;
using System.Collections;

/// <summary>
/// Utility class to draw stuff the normal Gizmos class can't
/// </summary>
public class UGizmos
{
	private const float sphericalStepSize = 0.05f;
	
	public static void DrawWireCapsule(Vector3 start, Vector3 end, float radius)
	{
		
		Vector3 startToEnd = start - end;
		
		Vector3 left = Vector3.Cross(startToEnd, Vector3.up).normalized * radius;
		if(left.magnitude < 0.5f * radius)
			left = Vector3.Cross(startToEnd, Vector3.forward).normalized * radius;
		
		Vector3 right = -left;
		Vector3 forward = Vector3.Cross(startToEnd, left).normalized * radius;
		Vector3 back = -forward;
		
		Vector3 top = (end - start).normalized * radius;
		
		GL.Color(Gizmos.color);
		
		GL.Begin(GL.LINES);			
			GL.Vertex(end + left);
			GL.Vertex(start + left);
			GL.Vertex(end + right);
			GL.Vertex(start + right);
			GL.Vertex(end + forward);
			GL.Vertex(start + forward);
			GL.Vertex(end + back);
			GL.Vertex(start + back);		
		GL.End();
		
		DrawSlerp(left, forward, start);
		DrawSlerp(forward, right, start);
		DrawSlerp(right, back, start);
		DrawSlerp(back, left, start);
		
		DrawSlerp(left, forward, end);
		DrawSlerp(forward, right, end);
		DrawSlerp(right, back, end);
		DrawSlerp(back, left, end);
		
		DrawSlerp(right, top, end);
		DrawSlerp(left, top, end);
		DrawSlerp(forward, top, end);
		DrawSlerp(back, top, end);

		DrawSlerp(right, -top, start);
		DrawSlerp(left, -top, start);
		DrawSlerp(forward, -top, start);
		DrawSlerp(back, -top, start);		
	}
	
	public static void DrawSweptWireCapsule(Vector3 point1, Vector3 point2, float radius, Vector3 direction, float distance)
	{
		Vector3 startToEnd = point1 - point2;
		
		Vector3 left = Vector3.Cross(startToEnd, direction).normalized * radius;
			
		Vector3 right = -left;
		Vector3 back = Vector3.Cross(startToEnd, left).normalized * radius;
		Vector3 forward = -back;
		
		Vector3 top = Vector3.Cross(left,direction).normalized * radius;
		
		Vector3 point1End = point1 + direction.normalized * distance;
		Vector3 point2End = point2 + direction.normalized * distance;
		
		GL.Color(Gizmos.color);
		
		GL.Begin(GL.LINES);
			//lines for start capsule
			GL.Vertex(point1 + left);
			GL.Vertex(point2 + left);
			GL.Vertex(point1 + right);
			GL.Vertex(point2 + right);
			GL.Vertex(point1 + back);
			GL.Vertex(point2 + back);
			//lines for end capsule
			GL.Vertex(point1End + left);
			GL.Vertex(point2End + left);
			GL.Vertex(point1End + right);
			GL.Vertex(point2End + right);
			GL.Vertex(point1End + forward);
			GL.Vertex(point2End + forward);
			//connecting lines
			GL.Vertex(point1 + left);
			GL.Vertex(point1End + left);
			GL.Vertex(point1 + right);
			GL.Vertex(point1End + right);
			GL.Vertex(point2 + left);
			GL.Vertex(point2End + left);
			GL.Vertex(point2 + right);
			GL.Vertex(point2End + right);
			GL.Vertex(point1 - top);
			GL.Vertex(point1End - top);
			GL.Vertex(point2 + top);
			GL.Vertex(point2End + top);		
			
		GL.End();
		
		
		DrawSlerp(right, back,  point1);
		DrawSlerp(back, left,  point1);
		DrawSlerp(left, forward, point1End);
		DrawSlerp(forward, right,  point1End);
		
		DrawSlerp(left, forward, point2End);
		DrawSlerp(forward, right, point2End);
		DrawSlerp(right, back, point2);
		DrawSlerp(back, left, point2);
		
		DrawSlerp(right, top, point2);
		DrawSlerp(left, top, point2);
		DrawSlerp(right, -top,  point1);
		DrawSlerp(left, -top,  point1);
		DrawSlerp(right, top, point2End);
		DrawSlerp(left, top, point2End);
		DrawSlerp(right, -top,  point1End);
		DrawSlerp(left, -top,  point1End);
		
		DrawSlerp(forward, top, point2End);
		DrawSlerp(back, top, point2);
		DrawSlerp(forward, -top,  point1End);
		DrawSlerp(back, -top,  point1);
	}
	
	
	
	public static void DrawSlerp(Vector3 start, Vector3 end, Vector3 center)
	{	
		GL.Begin(GL.LINES);
			for(float t = sphericalStepSize ; t < 1 + sphericalStepSize; t += sphericalStepSize)
			{
				GL.Vertex(center + Vector3.Slerp(start, end, t - sphericalStepSize));
				GL.Vertex(center + Vector3.Slerp(start, end, t));
			}
		GL.End();
	}
}
