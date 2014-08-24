﻿using UnityEngine;
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
	GameObject m_score;
	int Score=1000;
	int deltascore;
	float m_fTimeOfScore;
	float m_fTime;
	float m_fdeltatime;

	//truc de tournois
	int[] Quart;
	int[] Demi;
	int[] Finale;
	int vainqueur;

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
		m_EpaisseurRectangle = 8;
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


						if (x1!=x2){
							float D=Mathf.Sqrt((x2-x1)*(x2-x1)+(y2-y1)*(y2-y1));
							float D1=Mathf.Sqrt((xA-x1)*(xA-x1)+(yA-y1)*(yA-y1));
							float D2=Mathf.Sqrt((xA-x2)*(xA-x2)+(yA-y2)*(yA-y2));

							float a=(y1-y2)/(x1-x2);
							float b=y1-x1*a;
							float d=Mathf.Abs(a*xA-yA+b)/Mathf.Sqrt(1+a*a);
							float H1=Mathf.Sqrt (D1*D1-d*d);
							float H2=Mathf.Sqrt (D2*D2-d*d);
							//print("entre deux "+i+","+j+","+k+","+H1+" H2 "+H2+" D"+""+D +"+d"+d);
							if(H1<D & H2<D)//on est entre les deux mais d peut etre grand
							{
								if (d<m_EpaisseurRectangle)
								{
									possible=false;
								//	print ("chemin pas possible entre"+i+"et"+j+" a cause de "+k );
								}
							}
						}
						else
						{
							//x1=x2
							float d=Mathf.Abs(xA-x1);
						
							if (Mathf.Abs(y1-y2)==Mathf.Abs(yA-y2)+Mathf.Abs(yA-y1))
							{
								if (d<m_EpaisseurRectangle)
								{
									possible=false;
								//	print ("chemin pas possible entre"+i+"et"+j+" a cause de "+k );
								}
							}
						}
					}
				}
				routePossible[i,j]=possible;
				routePossible[j,i]=possible;

			}	
		}


		//tournois
		Quart = new int[8];
		Demi = new int[4];
		Finale = new int[2];
		for (int i=0; i<8; i++) {
				Quart [i] = -1;
				Demi[i/2]=-1;
				Finale[i/4]=-1;
				}

		 vainqueur = -1;
		tirageAuSort ();

		//score

		deltascore = 0;
		m_fdeltatime = 1.0f;
		m_fTimeOfScore = 300.0f;
		m_fTime = 0.0f;
		//tets
	//	m_score = ((GameObject) GameObject.Instantiate(CConstantes.Game.m_prefab3DText));
	//	m_score.transform.position = new Vector3 (0, 0, 0);
	//	m_score.GetComponent<TextMesh> ().text = Score.ToString();
	}
	
	//-------------------------------------------------------------------------------
	/// Unity
	//-------------------------------------------------------------------------------
	void Update () 
	{
		if (m_fTime >= m_fTimeOfScore) {
			m_fTime=0.0f;
			//score
			Score -= deltascore;
			print ("score " + Score);
		//	m_score.GetComponent<TextMesh> ().text = Score.ToString();
			
			} 
		else 
			{
			m_fTime+=m_fdeltatime;
			}

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
		if (m_PlaneteOrigin != null) {
			if (m_PlaneteDestination != null) {
				//verifier qu'il y a pas déjà une route
				int id1=m_PlaneteOrigin.GetComponent<CPlanete> ().nID;
				int id2=m_PlaneteDestination.GetComponent<CPlanete> ().nID;

				if (graphePlanete [id1, id2] != 0 || graphePlanete [id2, id1] != 0) 
				{
					print ("route déjà existante"+id1+id2);
				} 
				else 
				{

					if (routePossible [id1, id2]) 
					{
						graphePlanete [id2, id1] = -1;
						CreateNewRoad (m_PlaneteOrigin, m_PlaneteDestination);
					}
					else
					{
						print ("route pas possible"+id1+id2);
					}

				}

				if (m_PlaneteOverlap != null) {
					m_PlaneteOverlap.StopSelection ();
					m_PlaneteOverlap = null;
				}
				m_PlaneteDestination.StopSelection ();
				m_PlaneteDestination = null;
			}
			m_PlaneteOrigin.StopSelection ();
			m_PlaneteOrigin = null;
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
		Debug.Log("Route Finie"+id1+id2);
		//route entre planete id 1 et id 2
		graphePlanete[id1,id2]=1;
		graphePlanete[id2,id1]=1;
		updateScore ();
	}

	void updateScore()
	{
		deltascore = 0;

		for(int i=0;i<CConstantes.nNbPlanetes;i++)
		{
			for(int j=0;j<i;j++)
			{
				if (isConnected (i,j))
				{print ("connected"+i+j);
					deltascore+=hainePlanete[i,j];
				}
			}
		}
	}

	bool isConnected (int i, int j)
		{
		if (graphePlanete [i, j] == 1) //TODO BETTER
			{
				return true;		
			}

		return false;
		}

	void tirageAuSort()
	{
		for (int i=0; i<8; i++) //WARNING 8
		{
			int alea=Random.Range (0,7);
			bool placer=false;
			int j=0;
			while(!placer)
			{
				if(Quart[(alea+j)%8]==-1)
				{
					Quart[(alea+j)%8]=i;
					placer=true;
				}
				j++;
			}
		}
	}


}


