using UnityEngine;
using System.Collections;

public class CGameCreator : MonoBehaviour {

	static int m_instanceCount = 0;
	int m_nMapSize = 50;
	public GameObject m_prefabGame;
	public GameObject m_prefabCamera;
	public GameObject m_prefabFond;
	public GameObject [] m_prefabPlanets;

	Vector3[] positions;//=new Vector3[8];


	void Awake () 
	{
		if (m_instanceCount++ == 0) 
		{
			GameObject game = ((GameObject)GameObject.Instantiate (m_prefabGame));
			game.name = "_Game";
			GameObject camera = ((GameObject)GameObject.Instantiate (m_prefabCamera));
			camera.name = "_Camera";
			game.GetComponent<CGame> ().m_Camera = camera;

			CConstantes.nNbPlanetes = 8;
			CConstantes.Planetes = new GameObject[CConstantes.nNbPlanetes];

			GameObject fond = ((GameObject)GameObject.Instantiate (m_prefabFond));
			fond.name = "_Fond";
			CConstantes.nMapSize = m_nMapSize;
			float fMapSize = m_nMapSize / 3.0f;
			fond.transform.localScale = new Vector3(fMapSize, fMapSize, fMapSize);
			

			// genere 8 position qui s'entrechoque pas
			positions = new Vector3[CConstantes.nNbPlanetes];
			int i = 0;	
			while (i < CConstantes.nNbPlanetes) {
				//print("Creating positions: "+i);
				positions [i] = new Vector3 (Random.Range (-m_nMapSize, m_nMapSize), Random.Range (-m_nMapSize, m_nMapSize), 0);
				//test si colision avec précédent
				bool colision = false;
				for (int j=0; j<i; j++) {
					colision = colision || (Choc (positions [j], positions [i]));
					//print("test "+i+j+" "+colision+ "  "+Choc(positions[j],positions[i])+" "+positions[i]+" " +positions[j]);
				}

				if (!colision) {
					i++;
				}

			}
			//print ("test choc" +Choc(new Vector3(0,0,0),new Vector3(1,1,0)));

			GameObject planet;
			for (int id = 0; id< CConstantes.nNbPlanetes; ++id) {
				planet = ((GameObject)GameObject.Instantiate (m_prefabPlanets [id]));
				planet.name = m_prefabPlanets [id].name;
				planet.GetComponent<CPlanete> ().nID = id;
				planet.transform.position = positions [id];
				planet.GetComponent<CPlanete> ().nNbWorkers = Random.Range(1,4);
				planet.GetComponent<CPlanete> ().Init ();
				CConstantes.Planetes [id] = planet;
			}
			//tableau bool de trajet entre deux planete 8x8
		}
	}

	


	bool Choc(Vector3 pos1,Vector3 pos2)
	{
		float deltax = pos1.x - pos2.x;
		float deltay = pos1.y - pos2.y;
		//print (Mathf.Sqrt(deltax*deltax+deltay*deltay));
		return (Mathf.Sqrt(deltax*deltax+deltay*deltay) < 8);
	}

	void Update () 
	{
	
	}
	
	void OnDestroy() {
		m_instanceCount--;
	}
}
