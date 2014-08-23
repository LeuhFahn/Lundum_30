using UnityEngine;
using System.Collections;

public class CGameCreator : MonoBehaviour {

	static int m_instanceCount = 0;
	public GameObject m_prefabGame;
	public GameObject m_prefabCamera;
	public GameObject m_prefabPlanet;

	public GameObject [] Planetes; //liste des 8 planetes
	public  Vector3[] positions;//=new Vector3[8];

	public int testrandom;
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
			print("Creating positions: "+i);
			positions[i]=new Vector3(Random.Range(-200,200)/10, Random.Range(-200,200)/10, 0);
			//test si colision avec précédent
			bool colision= false;
			for(int j=0; j<i;j++)
			{

				colision=colision || (Choc(positions[j],positions[i]));
				print("test "+i+j+" "+colision+ "  "+Choc(positions[j],positions[i])+" "+positions[i]+" " +positions[j]);
			

			}

			if (!colision){i++;}

		}
		print ("test choc" +Choc(new Vector3(0,0,0),new Vector3(1,1,0)));

		GameObject planet1 = ((GameObject) GameObject.Instantiate (m_prefabPlanet));
		planet1.name = "P1";
		planet1.transform.position = positions[0];

		GameObject planet2 = ((GameObject) GameObject.Instantiate (m_prefabPlanet));
		planet2.name = "P2";
		planet2.transform.position = positions[1];

		
		GameObject planet3 = ((GameObject) GameObject.Instantiate (m_prefabPlanet));
		planet3.name = "P3";
		planet3.transform.position = positions[2];

		
		GameObject planet4 = ((GameObject) GameObject.Instantiate (m_prefabPlanet));
		planet4.name = "P4";
		planet4.transform.position = positions[3];

		GameObject planet5 = ((GameObject) GameObject.Instantiate (m_prefabPlanet));
		planet5.name = "P5";
		planet5.transform.position = positions[4];
		
		GameObject planet6 = ((GameObject) GameObject.Instantiate (m_prefabPlanet));
		planet6.name = "P6";
		planet6.transform.position = positions[5];
		
		
		GameObject planet7 = ((GameObject) GameObject.Instantiate (m_prefabPlanet));
		planet7.name = "P7";
		planet7.transform.position = positions[6];
		
		
		GameObject planet8 = ((GameObject) GameObject.Instantiate (m_prefabPlanet));
		planet8.name = "P8";
		planet8.transform.position = positions[7];


	}

	bool Choc(Vector3 pos1,Vector3 pos2)
	{
		float deltax = pos1.x - pos2.x;
		float deltay = pos1.y - pos2.y;
		print (Mathf.Sqrt(deltax*deltax+deltay*deltay));
		return (Mathf.Sqrt(deltax*deltax+deltay*deltay)<8);
		}
	void Update () 
	{
	
	}
	
	void OnDestroy() {
		m_instanceCount--;
	}
}
