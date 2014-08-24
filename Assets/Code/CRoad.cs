using UnityEngine;
using System.Collections;

public class CRoad : MonoBehaviour {

	public GameObject m_MeshGhost;
	public GameObject m_MeshRoad;
	public AudioSource m_RoadSound;

	GameObject m_PlanetOrigin;
	GameObject m_PlanetDestination;

	float m_fTimeOfConstruction;
	float m_fTime;
	float m_fDistanceBetweenConnectedWorlds;
	bool m_bConstructionIsOver;

	void Awake()
	{
		m_fTime = 0.0f;
		SetSizeOfMesh(m_MeshRoad, 0.0f);
		m_bConstructionIsOver = false;
	}

	public void Init()
	{
		m_fTimeOfConstruction = 5.0f;
		//float fSizeOrigin = m_PlanetOrigin.GetComponent<CPlanete>().fSize;
		//float fSizeDestination = m_PlanetDestination.GetComponent<CPlanete>().fSize;
		Vector3 posOrigin = m_PlanetOrigin.transform.position ;//+ new Vector3(fSizeOrigin, 0, 0);
		Vector3 posDestination = m_PlanetDestination.transform.position ;//- new Vector3(fSizeDestination, 0, 0);
		gameObject.transform.position = posOrigin  + (posDestination - posOrigin)/2.0f;

		m_fDistanceBetweenConnectedWorlds = (m_PlanetDestination.transform.position - m_PlanetOrigin.transform.position).magnitude;
		m_fDistanceBetweenConnectedWorlds /= 2.0f;

		Vector3 direction = m_PlanetDestination.transform.position - m_PlanetOrigin.transform.position;
		gameObject.transform.right = direction;
		//gameObject.transform.RotateAround(gameObject.transform.position, new Vector3(0,0,1), fAngle);
		SetSizeOfMesh(m_MeshGhost, m_fDistanceBetweenConnectedWorlds);

		m_RoadSound.audio.Play ();
		m_RoadSound.audio.loop= true ;

	}
	
	// Update is called once per frame
	void Update () 
	{
		if(m_fTime <= m_fTimeOfConstruction)
		{
			float fProgression = CApoilMath.InterpolationLinear(m_fTime, 0.0f, m_fTimeOfConstruction, 0, m_fDistanceBetweenConnectedWorlds);
			SetSizeOfMesh(m_MeshRoad, fProgression);
			m_fTime += Time.deltaTime;

			m_RoadSound.audio.pitch = (m_fTimeOfConstruction/m_fDistanceBetweenConnectedWorlds)/(5/m_fDistanceBetweenConnectedWorlds);
		}
		if(!m_bConstructionIsOver && m_fTime > m_fTimeOfConstruction)
		{
			EndOfConstruction();
		}
		//Debug.DrawLine(m_PlanetOrigin.transform.position, m_PlanetDestination.transform.position);

	}

	void SetSizeOfMesh(GameObject mesh, float fSize)
	{
		Vector3 scale = mesh.transform.localScale;
		scale.y = fSize;
		mesh.transform.localScale = scale;
	}

	public void SetPlanets(CPlanete origin, CPlanete destination)
	{
		m_PlanetOrigin = origin.GetGameObject();
		m_PlanetDestination = destination.GetGameObject();
	}

	void EndOfConstruction()
	{
		m_bConstructionIsOver = true;
		m_MeshRoad.renderer.material.color = Color.blue;
		CConstantes.Game.MaJ(m_PlanetOrigin.GetComponent<CPlanete>().nID,m_PlanetDestination.GetComponent<CPlanete>().nID);
		m_RoadSound.Stop ();
	}
}
