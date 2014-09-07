using UnityEngine;
using System.Collections;

public abstract class ColorScheme : MonoBehaviour 
{
	public abstract Color[] GetColors(float baseValue);
	
	public bool activated = true;
	
	void Awake()
	{
		if(activated)
			GetComponent<ColorManager>().colorSchemes.Add(this);
	}
}
