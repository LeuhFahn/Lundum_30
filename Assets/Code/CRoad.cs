using UnityEngine;
using System.Collections;

public class CRoad : MonoBehaviour {

	public GameObject m_MeshGhost;
	public GameObject m_MeshRoad;
	public GameObject m_Text;
	public AudioSource m_RoadSound;

	GameObject m_PlanetOrigin;
	GameObject m_PlanetDestination;

	float m_fTimeOfConstruction;
	float m_fTime;
	float m_fTimeBeforeDestruction;
	float m_fDistanceBetweenConnectedWorlds;
	bool m_bConstructionIsOver;
	bool m_bIsUnderAttack;

	void Awake()
	{
		m_fTime = 0.0f;
		SetSizeOfMesh(m_MeshRoad, 0.0f);
		m_bConstructionIsOver = false;
		m_Text.SetActive(false);

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

		m_Text.transform.right = new Vector3(1,0,0);

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


		if(m_bIsUnderAttack)
		{
			m_fTimeBeforeDestruction -= Time.deltaTime;
			SetText();
			if(m_fTimeBeforeDestruction <= 0)
			{
				DestroyRoad();
			}
		}
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

		CConstantes.ListRoad.Add(this.gameObject);

		m_PlanetOrigin.GetComponent<CPlanete>().nNbWorkers++; 
		m_PlanetDestination.GetComponent<CPlanete>().nNbWorkers++; 
		m_RoadSound.Stop ();
	}

	public void Attack()
	{
		m_bIsUnderAttack = true;
		m_fTimeBeforeDestruction = CConstantes.fTimerBeforeRoadDestruction;
		m_Text.SetActive(true);
	}

	public void RescueRoad()
	{
		m_bIsUnderAttack = false;
		m_Text.SetActive(false);
	}

	public void DestroyRoad()
	{
		CConstantes.Game.removeRoad (m_PlanetOrigin.GetComponent<CPlanete> ().nID, m_PlanetDestination.GetComponent<CPlanete> ().nID);
		m_RoadSound.Stop ();
		GameObject.Destroy(this.gameObject);

	}

	public bool IsUnderAttack()
	{
		return m_bIsUnderAttack;
	}

	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------
	void SetText()
	{
		m_Text.GetComponent<TextMesh>().text = m_fTimeBeforeDestruction.ToString();
	}
}
