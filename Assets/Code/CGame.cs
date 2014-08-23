using UnityEngine;
using System.Collections;

public class CGame : MonoBehaviour {

	public GameObject m_Camera;
	public LayerMask m_Mask;
	public GameObject m_prefabRoad;

	CPlanete m_PlaneteOrigin;
	CPlanete m_PlaneteOverlap;
	CPlanete m_PlaneteDestination;

	//-------------------------------------------------------------------------------
	/// Unity
	//-------------------------------------------------------------------------------
	void Start () 
	{
		CApoilInput.Init();
	}
	
	//-------------------------------------------------------------------------------
	/// Unity
	//-------------------------------------------------------------------------------
	void Update () 
	{
		CApoilInput.Process(Time.deltaTime);
		//Quit on Escape
		if(CApoilInput.QuitGame)
			QuitGame();


		if(Input.GetMouseButtonDown(0))
		{
			Vector3 directionCamera = m_Camera.GetComponent<Camera>().transform.forward;
			RaycastHit hit;
			//Debug.DrawRay(m_Camera.transform.position, 100*directionCamera);
			Debug.DrawRay(m_Camera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition).origin, 100*directionCamera);
			if(Physics.Raycast (m_Camera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition),out hit, 1000, m_Mask))
			{
				if(hit.collider.CompareTag("Planete"))
				{
					CPlanete planete = hit.collider.GetComponent<CPlanete>();
					planete.SelectPlaneteAsOrigin();
					m_PlaneteOrigin = planete;
				}
			}
		}

	
		if (Input.GetMouseButton(0))
		{
			Vector3 directionCamera = m_Camera.GetComponent<Camera>().transform.forward;
			RaycastHit hit;
			//Debug.DrawRay(m_Camera.transform.position, 100*directionCamera);
			Debug.DrawRay(m_Camera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition).origin, 100*directionCamera);
			if(Physics.Raycast (m_Camera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition),out hit, 1000, m_Mask))
			{
				if(hit.collider.CompareTag("Planete"))
				{
					CPlanete planete = hit.collider.GetComponent<CPlanete>();
					if(planete != m_PlaneteOrigin)
					{
						planete.OverlapByMouse();
						m_PlaneteOverlap = planete;
					}
				}
			}
			else if(m_PlaneteOverlap != null)
			{
				m_PlaneteOverlap.StopSelection();
				m_PlaneteOverlap = null;
			}
		}

		if(Input.GetMouseButtonUp(0))
		{
			Vector3 directionCamera = m_Camera.GetComponent<Camera>().transform.forward;
			RaycastHit hit;
			//Debug.DrawRay(m_Camera.transform.position, 100*directionCamera);
			Debug.DrawRay(m_Camera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition).origin, 100*directionCamera);
			if(Physics.Raycast (m_Camera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition),out hit, 1000, m_Mask))
			{
				if(hit.collider.CompareTag("Planete"))
				{
					CPlanete planete = hit.collider.GetComponent<CPlanete>();
					m_PlaneteDestination = planete;
				}
			}
			RoadConstruction();
		}
	}

	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------
	void RoadConstruction()
	{
		if(m_PlaneteOrigin != null)
		{
		   if(m_PlaneteDestination !=null)
			{
				CreateNewRoad(m_PlaneteOrigin, m_PlaneteDestination);

				m_PlaneteDestination.StopSelection();
				m_PlaneteDestination = null;
			}
			m_PlaneteOrigin.StopSelection();
			m_PlaneteOrigin = null;

		}
		if(m_PlaneteOverlap != null)
		{
			m_PlaneteOverlap.StopSelection();
			m_PlaneteOverlap = null;
		}
	}

	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------
	void CreateNewRoad(CPlanete PlaneteOrigin, CPlanete  PlaneteDestination)
	{
		GameObject newRoad = ((GameObject) GameObject.Instantiate(m_prefabRoad));
		newRoad.name = "Road"+PlaneteOrigin.name+PlaneteDestination.name;
		newRoad.GetComponent<CRoad>().SetPlanets(PlaneteOrigin, PlaneteDestination);
		newRoad.GetComponent<CRoad>().Init();
	}

	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------
	void QuitGame()
	{
		Application.Quit();	
	}
}
