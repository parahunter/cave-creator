using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UBezier 
{
	public Vector3 p0;
	public Vector3 p1;
	public Vector3 p2;
	public Vector3 p3;

	float flipNormalsFactor = 1f;

	private bool _flipNormals = false;
	public bool flipNormals
	{
		get
		{
			return _flipNormals;
		}
		set
		{
			_flipNormals = value;
			if(value)
				flipNormalsFactor = -1;
			else
				flipNormalsFactor = 1f;
		}
	}


	public UBezier(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
	{
		this.p0 = p0;
		this.p1 = p1;
		this.p2 = p2;
		this.p3 = p3;
	}

	public void InvertX()
	{
		p0.x *= -1;
		p1.x *= -1;
		p2.x *= -1;
		p3.x *= -1;
	}

	public Vector3 Evaluate(float t)
	{
		float oneMinusT = 1 - t;

		Vector3 first = Mathf.Pow(oneMinusT, 3) * p0;
		Vector3 second = 3 * Mathf.Pow(oneMinusT,2) * t * p1;
		Vector3 third = 3 * oneMinusT * Mathf.Pow(t,2) * p2;
		Vector3 fourth = Mathf.Pow(t, 3) * p3;

		return first + second + third + fourth;
	}

	public Vector3 EvaluateTangent(float t)
	{
		float oneMinusT = 1 - t;

		Vector3 first = 3 * Mathf.Pow(oneMinusT, 2) * (p1 - p0);
		Vector3 second = 6 * oneMinusT * t * (p2 - p1);
		Vector3 third = 3 * Mathf.Pow(t, 2) * (p3 - p2);

		return first + second + third;
	}

	public Vector3 EvaluateNormal(float t)
	{
		Vector3 tangent = EvaluateTangent(t);

		return new Vector3(-tangent.y, tangent.x, tangent.z).normalized * flipNormalsFactor;
	}

	const float interpolationStepSize = 0.05f;
	public void Visualise()
	{
		float oldT = 0;
		for(float t = interpolationStepSize ; t < 1 + interpolationStepSize ; t += interpolationStepSize)
		{
			Vector2 oldPoint = Evaluate(oldT);
			Vector2 newPoint = Evaluate(t);

			Gizmos.DrawLine(oldPoint, newPoint);
			oldT = t;
		}

		Gizmos.DrawLine(p0, p1);
		Gizmos.DrawLine(p2, p3);
	}

	public void VisualiseTangents()
	{
		for(float t = interpolationStepSize ; t < 1 ; t += interpolationStepSize)
		{
			Vector2 position = Evaluate(t);
			Vector2 normal = EvaluateTangent(t);
			
			Gizmos.DrawLine(position, position + normal);
		}
	}

	public void VisualiseNormals()
	{
		for(float t = interpolationStepSize ; t < 1 ; t += interpolationStepSize)
		{
			Vector2 position = Evaluate(t);
			Vector2 normal = EvaluateNormal(t);
			
			Gizmos.DrawLine(position, position + normal);
		}
	}


}
	                

