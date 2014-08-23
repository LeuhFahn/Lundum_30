using UnityEngine;
using System.Collections;

public class CGame : MonoBehaviour {

	public GameObject m_Camera;
	public LayerMask m_Mask;
	public GameObject m_prefabRoad;

	CPlanete m_PlaneteOrigin;
	CPlanete m_PlaneteOverlap;
	CPlanete m_PlaneteDestination;

	//
	int[,] graphePlanete;
	int[,] hainePlanete;

	//-------------------------------------------------------------------------------
	/// Unity
	//-------------------------------------------------------------------------------
	void Start () 
	{

		//initialise graphePlanete a 0
		graphePlanete=new int[8,8];
		for(int i=0;i<8;i++)
		{
			for(int j=0;j<8;j++)
			{
				graphePlanete[i,j]=0;
			}
		}
		print (graphePlanete[0,0]);

		CApoilInput.Init();

		CConstantes.Game = this;

		//initialise randomly haine planete symetrique
		//Haine max totale 400, max totale un couple 30
		 hainePlanete = new int[8, 8];
		int haineMax = 400;
		for (int i=0; i<8; i++)
		{
			for (int j=0;j<i;j++)
			{
				int haine= Random.Range(0,Mathf.Min (30,haineMax));
				haineMax=haineMax-haine;
				hainePlanete[i,j]=haine;
				hainePlanete[j,i]=haine;
				//print ("haine "+i+" "+ "j" +" " +haine);
			}
		}
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


		if(CApoilInput.LeftClickDown)
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

	
		if (CApoilInput.LeftClick)
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

		if(CApoilInput.LeftClickUp)
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
						m_PlaneteDestination = planete;
					}
					else //it's a simple click on a planet
					{
						SelectThePlanet();
					}
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

	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------
	void SelectThePlanet()
	{

	}

	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------
	public void MaJ( int id1, int id2)
	{
		Debug.Log("ninin suce des queues!"+id1+id2);
		//route entre planete id 1 et id 2
		graphePlanete[id1,id2]++;
		graphePlanete[id2,id1]++;
	}


}
