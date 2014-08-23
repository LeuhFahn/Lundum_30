using UnityEngine;
using System.Collections;

public class CRoad : MonoBehaviour {

	GameObject m_PlanetOrigin;
	GameObject m_PlanetDestination;

	public void Init()
	{
		gameObject.transform.position = m_PlanetOrigin.transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		Debug.DrawLine(m_PlanetOrigin.transform.position, m_PlanetDestination.transform.position);
	}

	public void SetPlanets(CPlanete origin, CPlanete destination)
	{
		m_PlanetOrigin = origin.GetGameObject();
		m_PlanetDestination = destination.GetGameObject();
	}
}
