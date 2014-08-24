using UnityEngine;
using System.Collections;

public static class UVector3
{
	public static Vector3 VectorFromDirection (float direction, float distance, bool inRadians = false)
	{
		if(!inRadians)
			direction *= Mathf.PI * 2;
		
		return new Vector3(Mathf.Cos(direction), Mathf.Sin(direction), 0) * distance;
	}
	
	public static void OrtoBasisFromDirection(float direction, out Vector3 up, out Vector3 right, bool inRadians = false)
	{
		if(!inRadians)
			direction *= Mathf.PI * 2;
		
		up = new Vector3(Mathf.Cos(direction), Mathf.Sin(direction), 0);
		right = new Vector3(Mathf.Sin (direction), -Mathf.Cos(direction), 0);
	}
}
