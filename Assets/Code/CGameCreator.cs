﻿using UnityEngine;
using System.Collections;

public class CGameCreator : MonoBehaviour {

	static int m_instanceCount = 0;
	public GameObject m_prefabGame;
	public GameObject m_prefabCamera;
	public GameObject [] m_prefabPlanets;

	 GameObject [] Planetes; //liste des 8 planetes
	 Vector3[] positions;//=new Vector3[8];


	void Awake () 
	{

		if(m_instanceCount++ == 0){
			GameObject game = ((GameObject) GameObject.Instantiate(m_prefabGame));
			game.name = "_Game";
			GameObject camera = ((GameObject) GameObject.Instantiate(m_prefabCamera));
			camera.name = "_Camera";
			game.GetComponent<CGame>().m_Camera = camera;
		}

		//GameObject planet1 = ((GameObject) GameObject.Instantiate (m_prefabPlanet));
	//	planet1.name = "P1";

	//	planet1.transform.position= new Vector3(Random.Range(-500,500)/10, Random.Range(-500,500)/10, 0);
	//	print (Choc (planet1.transform.position,planet1.transform.position));


		// genere 8 position qui s'entrechoque pas
		positions = new Vector3[8];
		int i = 0;	
		while(i<8)
		{
			//print("Creating positions: "+i);
			positions[i]=new Vector3(Random.Range(-200,200)/10, Random.Range(-200,200)/10, 0);
			//test si colision avec précédent
			bool colision= false;
			for(int j=0; j<i;j++)
			{

				colision=colision || (Choc(positions[j],positions[i]));
				//print("test "+i+j+" "+colision+ "  "+Choc(positions[j],positions[i])+" "+positions[i]+" " +positions[j]);
			

			}

			if (!colision){i++;}

		}
		//print ("test choc" +Choc(new Vector3(0,0,0),new Vector3(1,1,0)));

		GameObject planet;
		for(int id = 0; id< 8 ; ++id)
		{
			planet = ((GameObject) GameObject.Instantiate (m_prefabPlanets[id]));
			planet.name = "P"+(id+1).ToString();
			planet.transform.position = positions[id];
		}



	}

	bool Choc(Vector3 pos1,Vector3 pos2)
	{
		float deltax = pos1.x - pos2.x;
		float deltay = pos1.y - pos2.y;
		//print (Mathf.Sqrt(deltax*deltax+deltay*deltay));
		return (Mathf.Sqrt(deltax*deltax+deltay*deltay)<8);
	}

	void Update () 
	{
	
	}
	
	void OnDestroy() {
		m_instanceCount--;
	}
}
