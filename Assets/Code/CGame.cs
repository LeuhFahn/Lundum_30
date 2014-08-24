using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CGame : MonoBehaviour {

	public GameObject m_Camera;
	public LayerMask m_Mask;
	public GameObject m_prefabRoad;
	//public GameObject m_prefab3DText;

	public int m_EpaisseurRectangle;

	CPlanete m_PlaneteOrigin;
	CPlanete m_PlaneteOverlap;
	CPlanete m_PlaneteDestination;

	string[]  couleurPlanete;

	//Truc de score
	int[,] graphePlanete;
	int[,] hainePlanete;
	bool[,] routePossible;
	GameObject m_score;
	int Score=1000;
	int deltascore;
	int[] multiplicateurScore;

	//truc de temps
	float m_fTime;
	int m_currentWeek;
	int m_currentDay;
	float timeOfStartup;
	float[] timeOfMatch ;
	float timeMultiplicator;
	float m_fTimeOfScore;
	float m_fdeltatime;
	int currentMatch;
	bool matchEnCours;
	bool gameEnded;


	bool notAlreadyLaunchedThisWeek;
	//truc de tournois
	int[] Quart;


	public int[,] ppnHainePlanete
	{
		get {return hainePlanete; }
		set {hainePlanete = value; }
	}

	public int pScore
	{
		get {return Score; }
		set {Score = value; }
	}

	//-------------------------------------------------------------------------------
	/// Unity
	//-------------------------------------------------------------------------------
	void Start () 
	{

		couleurPlanete  = new string[] {"Orange","bleue" ,"verte" ,"Beige" ,"Terre","Rouge","Violette","Rose"};
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
		CConstantes.ListRoad = new List<GameObject>();
		CConstantes.fTimerBeforeRoadDestruction = 10.0f;

		//initialise randomly haine planete symetrique
		//Haine max totale 400, max totale un couple 30
		 hainePlanete = new int[8, 8];
		int haineMax = 400;
		for (int i=0; i<8; i++)
		{
			hainePlanete[i,i]=4+i*2; //Haine de la planete envers elle-meme (= satisfaction)
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
		matchEnCours = false;
		Quart = new int[15];

		for (int i=0; i < 15; i++) {
				Quart [i] = -1;
				}
		
		tirageAuSort ();
		currentMatch = 0;
		print ("prochain match" +couleurPlanete [Quart [2 * currentMatch]] + " VS " + couleurPlanete [Quart [2 * currentMatch + 1]]);
		timeOfStartup = 0.0f;
		timeMultiplicator = 5f;
		currentMatch = 0;
		
		timeOfMatch  = new float[] {5f, 10f, 15f, 20f,25f,30f,35f};
		//score
		multiplicateurScore = new int[]{1,1,1,1,2,2,4};
		m_currentWeek = 0;
		 m_currentDay=0;
		deltascore = 0;
		m_fTime = 0.0f;
		//events
		bool notAlreadyLaunchedThisWeek = true;

		/*
		m_score = ((GameObject) GameObject.Instantiate(CConstantes.Game.m_prefab3DText));
		m_score.transform.position = new Vector3 (0, 0, 0);
		m_score.GetComponent<TextMesh> ().text = Score.ToString();*/

	}
	
	//-------------------------------------------------------------------------------
	/// Unity
	//-------------------------------------------------------------------------------
	void Update () 
	{
		if (!gameEnded) {
						m_fTime += Time.deltaTime;
						//GESTION DES MATCHS
						int m_currentWeek2 = Mathf.FloorToInt ((m_fTime - timeOfStartup) / (timeMultiplicator));

						if (m_currentWeek2 != m_currentWeek) {
								notAlreadyLaunchedThisWeek = true;
								m_currentWeek = m_currentWeek2;
								print ("semaine actuelle " + m_currentWeek);
						

						}

						if (currentMatch < 7) {
				
								if (m_fTime - timeOfStartup > (timeOfMatch [currentMatch] * timeMultiplicator - 4f) & matchEnCours == false) {
										startMatch (currentMatch);
										matchEnCours = true;
								}
								if (m_fTime - timeOfStartup > timeOfMatch [currentMatch] * timeMultiplicator) {
										endMatch (currentMatch);
								}
						}
						//GESTION Du SCORES
	
						//GESTION DES EVENTS
						//Un event peut commencer aléatoirement une fois par semaine
						int m_currentDay2 = Mathf.FloorToInt ((7 * m_fTime - timeOfStartup) / (timeMultiplicator));
						if (m_currentDay2 != m_currentDay) {
								updateScore ();
								Score -= deltascore;
								//print ("Score:"+Score);
								m_currentDay = m_currentDay2;
								//print (m_currentDay2);
								if (notAlreadyLaunchedThisWeek & Random.Range (0, 6) == 0) {
										print ("launch");
										CEvent.LaunchEventOnARoad (); 
										notAlreadyLaunchedThisWeek = false;
								}
				
						}

						CApoilInput.Process (Time.deltaTime);
						//Quit on Escape
						if (CApoilInput.QuitGame)
								QuitGame ();

						ClickOnPlanetes ();
				}

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
					CPlanete planete = hit.collider.transform.parent.GetComponent<CPlanete>();
					planete.SelectPlaneteAsOrigin();
					m_PlaneteOrigin = planete;
				}
			}
			else
			{
				gameObject.GetComponent<CMenuInGame>().PlaneteInfoDesactivation();
				
				for(int i = 0 ; i < CConstantes.nNbPlanetes ; ++i)
				{
					CConstantes.Planetes[i].GetComponent<CPlanete>().StopDrawInfo();
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
					CPlanete planete = hit.collider.transform.parent.GetComponent<CPlanete>();
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
					CPlanete planete = hit.collider.transform.parent.GetComponent<CPlanete>();
					if(planete != m_PlaneteOrigin)
					{
						m_PlaneteDestination = planete;
					}
					else //it's a simple click on a planet
					{
						gameObject.GetComponent<CMenuInGame>().SelectThePlanet(planete);
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
						//Verifier que la main d'Oeuvre est dispo
						if(m_PlaneteOrigin.nNbWorkers > 0 && m_PlaneteDestination.nNbWorkers > 0)
						{
							graphePlanete [id2, id1] = -1;
							CreateNewRoad (m_PlaneteOrigin, m_PlaneteDestination);
						}
						else
						{
							print ("pas assez de main d'oeuvre dude!");
						}
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

		PlaneteOrigin.nNbWorkers = PlaneteOrigin.nNbWorkers - 1;
		PlaneteDestination.nNbWorkers = PlaneteDestination.nNbWorkers - 1;

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
	public void MaJ( int id1, int id2)
	{
		Debug.Log("Route Finie"+id1+id2);
		//route entre planete id 1 et id 2
		graphePlanete[id1,id2]=1;
		graphePlanete[id2,id1]=1;
		updateScore ();
	}
	public void removeRoad(int id1,int id2){
		print ("route enlever");
		graphePlanete[id1,id2]=0;
		graphePlanete[id2,id1]=0;
		updateScore ();
		}
	void updateScore()
	{
		deltascore = 0;

		for(int i=0;i<CConstantes.nNbPlanetes;i++)
		{
			for(int j=0;j<i;j++)
			{
				int distance=distanceConnected(i,j);
				if (distance<10)
				{
					//print ("distance"+i+j+ " :"+distance);
					deltascore+=(hainePlanete[i,j]);
				}
			}
		}

	}

	bool isConnected (int id1, int id2){
		int distance=distanceConnected ( id1, id2);
		if(distance<10){return true;}else{return false;}
		}
	int distanceConnected (int id1, int id2)
	{
	

	/*	if (graphePlanete [id1, id2] == 1) //TODO BETTER
			{
				return true;		
			}

		return false;*/
		int distance = 10;
		int[] visite = {0,0,0,0,0,0,0,0};
		visite [id1] = 1;
		for (int i=0; i<8; i++) {
			for(int j=0;j<8;j++){
				if(visite[j]==1){
					for(int k=0;k<8;k++){
						if(graphePlanete[j,k]==1){
							visite[k]=1;
							if (k==id2){distance=Mathf.Min(i+1,distance);}
						}
					}
				}
			}
		}
		return distance;
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

	void startMatch(int match)
	{
		print ("début du match " + (match+1));
		}
	void endMatch(int match){
		print ("MATCH "+(currentMatch+1)+" " + CConstantes.Planetes [Quart [2 * currentMatch]].name + " VS " + CConstantes.Planetes [Quart [2 * currentMatch + 1]].name + " Fini");
	//	string adv1=CConstantes.Planetes[Quart[2*match]].name;
//		string adv2=CConstantes.Planetes[Quart[2*match+1]].name;

		//Aléa qui gagne
		int gagnant=Random.Range(0,1);
		Quart[match+8]=Quart[2*match+gagnant];
		//Si les deux planetes sonr relié ont augmente le score
		int changeScore=hainePlanete[Quart [2 * currentMatch], Quart [2 * currentMatch + 1]]*500*multiplicateurScore[match];
		print ("changeScore:" +changeScore);
		if (isConnected (Quart [2 * currentMatch], Quart [2 * currentMatch + 1])) {

			Score+=changeScore;
				} else {
			Score-=changeScore;
				}
		//SI  C est la finale on vient de calculer le vainqueur ,on passe a endgame


		if (currentMatch >= 6) {
						endGame (true);
				} else {
						MatchIsOver (CConstantes.Planetes [Quart [2 * match + gagnant]]);
					/*	if (gagnant == 0) {
								print (adv1 + " a gagné");
						} else {
								print (adv2 + " a gagné");
						}
						*/
						print ("prochain match" +couleurPlanete [Quart [2 * currentMatch]] + " VS " + couleurPlanete [Quart [2 * currentMatch + 1]]);
				}
	}

	void MatchIsOver(GameObject winPlanet)

	{
		matchEnCours=false;
		currentMatch++;
		}

	void endGame(bool win)
	{
		matchEnCours=false;
		if (win) {
			gameEnded=true;
			print ("le GRAND VAINQUEUR EST "+ CConstantes.Planetes [Quart [14]].name);
			print ("you win");
				} 
		else {
			print ("you lose");
				}
	}



}


