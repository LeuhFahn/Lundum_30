using UnityEngine;
using System.Collections;

public class CGameCreator : MonoBehaviour {

	static int m_instanceCount = 0;
	int m_nMapSize = 50;
	public GameObject m_prefabGame;
	public GameObject m_prefabCamera;
	public GameObject m_prefabFond;
	public GameObject m_prefabPlanet;
	
	public Material m_MaterialPlanetBleue;
	public Material m_MaterialPlanetOrange;
	public Material m_MaterialPlanetRose;
	public Material m_MaterialPlanetRouge;
	public Material m_MaterialPlanetSentry;
	public Material m_MaterialPlanetTerra;
	public Material m_MaterialPlanetVert;
	public Material m_MaterialPlanetViolet;

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
			for (int id = 0; id< CConstantes.nNbPlanetes; ++id) 
			{
				planet = ((GameObject)GameObject.Instantiate (m_prefabPlanet));
				planet.GetComponent<CPlanete> ().m_eNamePlanet = GetPlanetName(id);
				planet.name = planet.GetComponent<CPlanete> ().m_eNamePlanet.ToString();
				planet.GetComponent<CPlanete> ().nID = id;
				planet.GetComponent<CPlanete> ().m_Mesh.renderer.material = GetPlanetMaterial(planet.GetComponent<CPlanete>());
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
		return (Mathf.Sqrt(deltax*deltax+deltay*deltay) < 30);
	}

	void Update () 
	{
	
	}
	
	void OnDestroy() {
		m_instanceCount--;
	}

	CPlanete.ENamePlanet GetPlanetName(int id)
	{
		CPlanete.ENamePlanet ePlanet = CPlanete.ENamePlanet.e_A;
		switch(id)
		{
			case 0:
				ePlanet = CPlanete.ENamePlanet.e_A;
				break;
			case 1:
				ePlanet = CPlanete.ENamePlanet.e_C;
				break;
			case 2:
				ePlanet = CPlanete.ENamePlanet.e_K;
				break;
			case 3:
				ePlanet = CPlanete.ENamePlanet.e_Sentry;
				break;
			case 4:
				ePlanet = CPlanete.ENamePlanet.e_Terra;
				break;
			case 5:
				ePlanet = CPlanete.ENamePlanet.e_V;
				break;
			case 6:
				ePlanet = CPlanete.ENamePlanet.e_X;
				break;
			case 7:
				ePlanet = CPlanete.ENamePlanet.e_Yoranus;
				break;
		}
		return ePlanet;
	}

	Material GetPlanetMaterial(CPlanete planete)
	{
		Material mat = m_MaterialPlanetOrange;
		switch(planete.m_eNamePlanet)
		{
			case CPlanete.ENamePlanet.e_A:
			{
				mat = m_MaterialPlanetOrange;
				break;
			}
			case CPlanete.ENamePlanet.e_C:
			{
				mat = m_MaterialPlanetBleue;
				break;
			}
			case CPlanete.ENamePlanet.e_K:
			{
				mat = m_MaterialPlanetVert;
				break;
			}
			case CPlanete.ENamePlanet.e_Sentry:
			{
				mat = m_MaterialPlanetSentry;
				break;
			}
			case CPlanete.ENamePlanet.e_Terra:
			{
				mat = m_MaterialPlanetTerra;
				break;
			}
			case CPlanete.ENamePlanet.e_V:
			{
				mat = m_MaterialPlanetRouge;
				break;
			}
			case CPlanete.ENamePlanet.e_X:
			{
				mat = m_MaterialPlanetViolet;
				break;
			}
			case CPlanete.ENamePlanet.e_Yoranus:
			{
				mat = m_MaterialPlanetRose;
				break;
			}
		}
		return mat;
	}
}
