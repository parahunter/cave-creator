using UnityEngine;
using System.Collections;

public static class Vector3Extensions 
{
	public static Vector4 ToHomogeneousVec4(this Vector3 vector) 
	{
       	return new Vector4(vector.x, vector.y, vector.z, 1);
    }
}
