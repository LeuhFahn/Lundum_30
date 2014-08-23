using UnityEngine;
using System.Collections;

public class CGame : MonoBehaviour {

	public GameObject m_Camera;
	public LayerMask m_Mask;
	public GameObject m_prefabRoad;
	public GameObject m_prefab3DText;

	public int m_EpaisseurRectangle;

	CPlanete m_PlaneteOrigin;
	CPlanete m_PlaneteOverlap;
	CPlanete m_PlaneteDestination;

	//Truc de score
	int[,] graphePlanete;
	int[,] hainePlanete;
	bool[,] routePossible;
	int Score=100;
	int deltascore;

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
		//calcul les routes qui peuvent exister ou pas
		m_EpaisseurRectangle = 4;
		routePossible = new bool[CConstantes.nNbPlanetes, CConstantes.nNbPlanetes];
		for (int i=0; i<CConstantes.nNbPlanetes; i++) 
		{
			for(int j=0; j<i; j++){
				bool possible=true;
				for(int k=0; k<CConstantes.nNbPlanetes; k++)
				{
				if(k!= i & k!=j)//on teste la route i j avec les autres
					{

						float x1=CConstantes.Planetes[i].GetComponent<CPlanete>().transform.position.x;
						float y1=CConstantes.Planetes[i].GetComponent<CPlanete>().transform.position.y;
						
						float x2=CConstantes.Planetes[j].GetComponent<CPlanete>().transform.position.x;
						float y2=CConstantes.Planetes[j].GetComponent<CPlanete>().transform.position.y;
						
						float xA=CConstantes.Planetes[k].GetComponent<CPlanete>().transform.position.x;
						float yA=CConstantes.Planetes[k].GetComponent<CPlanete>().transform.position.y;
						print(i+","+j+","+k+","+x1);

						if (x1!=x2){
							float D=Mathf.Sqrt((x2-x1)*(x2-x1)+(y2-y1)*(y2-y1));
							float D1=Mathf.Sqrt((xA-x1)*(xA-x1)+(yA-y1)*(yA-y1));
							float D2=Mathf.Sqrt((xA-x2)*(xA-x2)+(yA-y2)*(yA-y2));

							float a=(y1-y2)/(x1-x2);
							float b=y1-x1*a;
							float d=Mathf.Abs(a*xA-yA+b)/Mathf.Sqrt(1+a*a);
							float H1=Mathf.Sqrt (D1*D1-d*d);
							float H2=Mathf.Sqrt (D2*D2-d*d);
							if(H1<D & H2<D)//on est entre les deux mais d peut etre grand
							{
								if (d<m_EpaisseurRectangle)
								{
									possible=false;
								}
							}
						}
						else
						{
							//x1=x2
							float d=Mathf.Abs(xA-x1);
						
							if (Mathf.Abs(y1-y2)==Mathf.Abs(yA-y2)+Mathf.Abs(yA-y1))
							{
								if (d<4)
								{
									possible=false;
									print ("chemin pas possible entre"+i+"et"+j+" a cause de "+k );
								}
							}
						}
					}
				}
				routePossible[i,j]=possible;
				routePossible[j,i]=possible;

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

		ClickOnPlanetes();

	}

	//-------------------------------------------------------------------------------
	/// 
	//-------------------------------------------------------------------------------
	void ClickOnPlanetes()
	{
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
						SelectThePlanet(planete);
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
	void SelectThePlanet(CPlanete planete)
	{
		planete.SelectForDrawInfo();
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
	//updateDeltaScore();

}


