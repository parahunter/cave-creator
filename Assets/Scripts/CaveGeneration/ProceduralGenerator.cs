using UnityEngine;
using System.Collections;

public abstract class ProceduralGenerator : MonoBehaviour 
{
	public abstract void GenerateRepresentation();
	public abstract void VisualiseRepresentation();
	public abstract void GenerateMesh();
}
