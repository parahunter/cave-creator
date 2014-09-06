using UnityEngine;
using System.Collections;

public abstract class ColorScheme : MonoBehaviour 
{
	public abstract Color[] GetColors(float baseValue);
	
	void Awake()
	{
		GetComponent<ColorManager>().colorSchemes.Add(this);
	}
}
