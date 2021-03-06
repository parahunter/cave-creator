﻿using UnityEngine;
using System.Collections;

public static class UColor
{
	public static Color HSVToRPG(float h, float s, float v)
	{
		//HSV to RGB conversion
		Color returnCol = new Color();
		returnCol.a = 1.0f;
		
		int i;
		float f, p, q, t;
		
		if( s == 0 ) {
			// achromatic (grey)
			returnCol.r = v;
			returnCol.g = v;
			returnCol.b = v;
			return returnCol;
		}
		h /= 60;			// sector 0 to 5
		i = (int)System.Math.Floor( (double)h );
		f = h - i;			// factorial part of h
		p = v * ( 1 - s );
		q = v * ( 1 - s * f );
		t = v * ( 1 - s * ( 1 - f ) );
		switch( i ) {
		case 0:
			returnCol.r = v;
			returnCol.g = t;
			returnCol.b = p;
			break;
		case 1:
			returnCol.r = q;
			returnCol.g = v;
			returnCol.b = p;
			break;
		case 2:
			returnCol.r = p;
			returnCol.g = v;
			returnCol.b = t;
			break;
		case 3:
			returnCol.r = p;
			returnCol.g = q;
			returnCol.b = v;
			break;
		case 4:
			returnCol.r = t;
			returnCol.g = p;
			returnCol.b = v;
			break;
		default:		// case 5:
			returnCol.r = v;
			returnCol.g = p;
			returnCol.b = q;
			break;
		}
		
		return returnCol;
	}
}
