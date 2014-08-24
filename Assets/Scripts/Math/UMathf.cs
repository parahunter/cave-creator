using UnityEngine;
using System.Collections;

public class UMathf : MonoBehaviour 
{
	public static float MapToRange(float val, float oldMin, float oldMax, float newMin, float newMax)
	{
		float normalizedVal = (val - oldMin) / (oldMax - oldMin);
		
		return newMin + normalizedVal * (newMax - newMin);	
	}
	
	public static float LinePointDistance(Vector3 lineOrigin, Vector3 lineDirection, Vector3 point)
	{
		Vector3 lineOriginMinusPoint = lineOrigin - point;
		Vector3 projection = lineOriginMinusPoint - Vector3.Dot(lineOriginMinusPoint, lineDirection) * lineDirection;
		
		return projection.magnitude;	
	}
	
	public static float LinePointDistanceSquared(Vector3 lineOrigin, Vector3 lineDirection, Vector3 point)
	{
		Vector3 lineOriginMinusPoint = lineOrigin - point;
		Vector3 projection = lineOriginMinusPoint - Vector3.Dot(lineOriginMinusPoint, lineDirection) * lineDirection;
		
		return projection.sqrMagnitude;	
	}
	
	public static float LineSegmentPointDistanceSquared(Vector3 lineStart, Vector3 lineEnd, Vector3 point)
	{
		Vector3 line = lineEnd - lineStart;
		float squaredLength = line.sqrMagnitude;
		
		if(Mathf.Approximately(squaredLength, 0f))
			return (lineStart - point).sqrMagnitude;
		
		float t = Vector3.Dot (point - lineStart, line) / squaredLength;
		
		if(t < 0)
			return (lineStart - point).sqrMagnitude;
		else if(t > 1)
			return (lineEnd - point).sqrMagnitude;
		else
			return ((lineStart + line * t) - point).sqrMagnitude;
	}
		
	public static float LinePointDistanceSquared(Ray line, Vector3 point)
	{
		Vector3 lineOriginMinusPoint = line.origin - point;
		Vector3 projection = lineOriginMinusPoint - Vector3.Dot(lineOriginMinusPoint, line.direction) * line.direction;
		
		return projection.sqrMagnitude;	
	}
	
	public static bool LineLineIntersection2D(Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3, out Vector2 intersection)
	{	
		intersection = Vector3.zero;	
	    float s1_x, s1_y, s2_x, s2_y;
				
	    s1_x =  p1.x - p0.x;     s1_y = p1.y - p0.y;
	    s2_x = p3.x - p2.x;     s2_y = p3.y - p2.y;
	
	    float s, t;
	    s = (-s1_y * (p0.x - p2.x) + s1_x * (p0.y - p2.y)) / (-s2_x * s1_y + s1_x * s2_y);
	    t = ( s2_x * (p0.y - p2.y) - s2_y * (p0.x - p2.x)) / (-s2_x * s1_y + s1_x * s2_y);
	
	    if (s >= 0 && s <= 1 && t >= 0 && t <= 1)
	    {
	        intersection.x = p0.x + (t * s1_x);
	        intersection.y = p0.y + (t * s1_y);
					
	        return true;
	    }
		
		
	    return false; // No collision
	}
		
	public static bool LineLineIntersection2D(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, out Vector3 intersection)
	{	
		intersection = Vector3.zero;	
	    float s1_x, s1_y, s2_x, s2_y;
				
	    s1_x =  p1.x - p0.x;     s1_y = p1.y - p0.y;
	    s2_x = p3.x - p2.x;     s2_y = p3.y - p2.y;
	
	    float s, t;
	    s = (-s1_y * (p0.x - p2.x) + s1_x * (p0.y - p2.y)) / (-s2_x * s1_y + s1_x * s2_y);
	    t = ( s2_x * (p0.y - p2.y) - s2_y * (p0.x - p2.x)) / (-s2_x * s1_y + s1_x * s2_y);
	
	    if (s >= 0 && s <= 1 && t >= 0 && t <= 1)
	    {
	        intersection.x = p0.x + (t * s1_x);
	        intersection.y = p0.y + (t * s1_y);
					
	        return true;
	    }
		
		
	    return false; // No collision
	}		
	
}
