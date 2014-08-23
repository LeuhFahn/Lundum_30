using UnityEngine;
using System.Collections;

public class CApoilMath
{
	const float m_fPi = 3.14159f;
	
	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------
	public static Vector2 ConvertCartesianToPolar(Vector2 coordCartesian) // sur [0, 2pi[
	{
		float fX = coordCartesian.x;
		float fY = coordCartesian.y;
		float fR = Mathf.Sqrt(fX * fX + fY * fY);
		float fTheta = 0;

		if(coordCartesian.x > 0)
		{
			if(coordCartesian.y >= 0)
				fTheta = Mathf.Atan(fY / fX);
			else
				fTheta = Mathf.Atan(fY / fX) + 2 * m_fPi;
		}
		else if(coordCartesian.x != 0)
			fTheta = Mathf.Atan(fY / fX) + m_fPi;
		else
		{
			if(coordCartesian.y > 0)
				fTheta = m_fPi / 2.0f;
			else
				fTheta = 3 * fTheta / 2.0f;
		}
		
		return new Vector2(fR, fTheta);
	}
	
	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------
	public static Vector2 ConvertPolarToCartesian(Vector2 coordPolar) 
	{
		return new Vector2(coordPolar.x * Mathf.Cos(coordPolar.y) , coordPolar.x * Mathf.Sin(coordPolar.y));
	}
	
	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------
	public static float ConvertDegreeToRadian(float fDeg) 
	{
		return fDeg * Mathf.Deg2Rad;
	}
	
	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------
	public static float ConvertRadianToDegree(float fRad) 
	{
		return fRad * Mathf.Rad2Deg;
	}
	
	public static float InterpolationLinear(float fTimeCurrent, float fTimeStart, float fTimeEnd, float fStart, float fEnd)
	{
		return fStart + fTimeCurrent * (fEnd - fStart)/(fTimeEnd - fTimeStart);	
	}
}
