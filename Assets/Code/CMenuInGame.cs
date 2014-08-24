using UnityEngine;
using System.Collections;

public class CMenuInGame : MonoBehaviour {

	public GameObject prefabUIGroot;
	GameObject m_UIGroot;


	void Start () {
		m_UIGroot = ((GameObject) GameObject.Instantiate(prefabUIGroot));
		m_UIGroot.name = "_UIGroot";
		PlaneteInfoDesactivation();
	}
	

	void Update () {
	
	}

	public int TrouveAnimosite(CPlanete planete, int posVoulue)
	{
		int[] hainesPlanete = new int[8];
		for(int i=0 ; i<8 ; i++)
		{
			hainesPlanete[i] = CConstantes.Game.ppnHainePlanete[planete.nID, i];
		}

		int posDu1Max=0;
		int posDu2Max=0;
		int posDu3Max=0;
		int posReturn=-1;

		for (int i=0; i<8; i++)
		{
			if (hainesPlanete[i] > hainesPlanete[posDu1Max]){posDu1Max = i;}
		}
		for (int i=0; i<8; i++)
		{
			if ( i != posDu1Max )
			{
				if (hainesPlanete[i] > hainesPlanete[posDu2Max]){posDu2Max = i;}
			}
		}
		for (int i=0; i<8; i++)
		{
			if (( i != posDu1Max ) & ( i != posDu2Max ))
			{
				if (hainesPlanete[i] > hainesPlanete[posDu2Max]){posDu3Max = i;}
			}
		}

		if (posVoulue == 1){ posReturn = posDu1Max; }
		if (posVoulue == 2){ posReturn = posDu2Max; }
		if (posVoulue == 3){ posReturn = posDu3Max; }

		return posReturn;
	}

	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------
	public void SelectThePlanet(CPlanete planete)
	{


		planete.SelectForDrawInfo(); //Dessin du halo autour de la planete cliquee

		//Partie affichage du bloc d'info de planete en bas à droite
		PlaneteInfoDesactivation();
		m_UIGroot.transform.FindChild("PlaneteInfo").FindChild("PlaneteInfo_Background").gameObject.SetActive(true);

		//Satisfaction (haineMax = 30 dans CGame)
		m_UIGroot.transform.FindChild("PlaneteInfo").FindChild("PlaneteInfo_Satisfaction").gameObject.GetComponent<UISlider>().value = CConstantes.Game.ppnHainePlanete[planete.nID, planete.nID]/30.0f;
		m_UIGroot.transform.FindChild("PlaneteInfo").FindChild("PlaneteInfo_Satisfaction").gameObject.SetActive(true);

		//Affichage des 3 animosites de la planete

		Debug.Log(TrouveAnimosite(planete, 1));


		switch(planete.m_eNamePlanet)
		{
		case CPlanete.ENamePlanet.e_A:
		{
			m_UIGroot.transform.FindChild("PlaneteInfo").FindChild("PlaneteInfo_OrangeA").gameObject.SetActive(true);
			break;
		}
		case CPlanete.ENamePlanet.e_C:
		{
			m_UIGroot.transform.FindChild("PlaneteInfo").FindChild("PlaneteInfo_TurquoiseC").gameObject.SetActive(true);
			break;
		}
		case CPlanete.ENamePlanet.e_K:
		{
			m_UIGroot.transform.FindChild("PlaneteInfo").FindChild("PlaneteInfo_VertK").gameObject.SetActive(true);
			break;
		}
		case CPlanete.ENamePlanet.e_Sentry:
		{
			m_UIGroot.transform.FindChild("PlaneteInfo").FindChild("PlaneteInfo_JauneS").gameObject.SetActive(true);
			break;
		}
		case CPlanete.ENamePlanet.e_Terra:
		{
			m_UIGroot.transform.FindChild("PlaneteInfo").FindChild("PlaneteInfo_BleuT").gameObject.SetActive(true);
			break;
		}
		case CPlanete.ENamePlanet.e_V:
		{
			m_UIGroot.transform.FindChild("PlaneteInfo").FindChild("PlaneteInfo_RougeV").gameObject.SetActive(true);
			break;
		}
		case CPlanete.ENamePlanet.e_X:
		{
			m_UIGroot.transform.FindChild("PlaneteInfo").FindChild("PlaneteInfo_VioletX").gameObject.SetActive(true);
			break;
		}
		case CPlanete.ENamePlanet.e_Yoranus:
		{
			m_UIGroot.transform.FindChild("PlaneteInfo").FindChild("PlaneteInfo_RoseY").gameObject.SetActive(true);
			break;
		}
		}
	}

	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------
	public void PlaneteInfoDesactivation()
	{
		m_UIGroot.transform.FindChild("PlaneteInfo").FindChild("PlaneteInfo_Background").gameObject.SetActive(false);
		m_UIGroot.transform.FindChild("PlaneteInfo").FindChild("PlaneteInfo_BleuT").gameObject.SetActive(false);
		m_UIGroot.transform.FindChild("PlaneteInfo").FindChild("PlaneteInfo_JauneS").gameObject.SetActive(false);
		m_UIGroot.transform.FindChild("PlaneteInfo").FindChild("PlaneteInfo_OrangeA").gameObject.SetActive(false);
		m_UIGroot.transform.FindChild("PlaneteInfo").FindChild("PlaneteInfo_RoseY").gameObject.SetActive(false);
		m_UIGroot.transform.FindChild("PlaneteInfo").FindChild("PlaneteInfo_RougeV").gameObject.SetActive(false);
		m_UIGroot.transform.FindChild("PlaneteInfo").FindChild("PlaneteInfo_TurquoiseC").gameObject.SetActive(false);
		m_UIGroot.transform.FindChild("PlaneteInfo").FindChild("PlaneteInfo_VertK").gameObject.SetActive(false);
		m_UIGroot.transform.FindChild("PlaneteInfo").FindChild("PlaneteInfo_VioletX").gameObject.SetActive(false);
		m_UIGroot.transform.FindChild("PlaneteInfo").FindChild("PlaneteInfo_Satisfaction").gameObject.SetActive(false);


	}


}
