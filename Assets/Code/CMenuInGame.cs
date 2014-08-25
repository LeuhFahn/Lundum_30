using UnityEngine;
using System.Collections;

public class CMenuInGame : MonoBehaviour {

	public GameObject prefabUIGroot;
	GameObject m_UIGroot;
	public bool infoPlaneteEnDur = true;

	void Awake(){
		m_UIGroot = ((GameObject) GameObject.Instantiate(prefabUIGroot));
		m_UIGroot.name = "_UIGroot";
		PlaneteInfoDesactivation();
		PlaneteInfoDurDesactivation();
		CalendrierDesactivation();
		}
	void Start () {


		//Test appel du calendrier prochain match
		//CalendrierProchainMatch( CConstantes.Planetes[0].GetComponent<CPlanete>(), CConstantes.Planetes[2].GetComponent<CPlanete>(), CConstantes.Planetes[6].GetComponent<CPlanete>() );
	}
	

	void Update () {
		m_UIGroot.transform.FindChild("ScoreUI").FindChild("Label").gameObject.GetComponent<UILabel>().text = CConstantes.Game.pScore.ToString();
		m_UIGroot.transform.FindChild ("Calendrier").FindChild ("Chrono").gameObject.GetComponent<UISlider> ().value = (35 - (float) CConstantes.Game.pjourAvantProchainMatch) / 35f;;

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
		PlaneteInfoDurDesactivation();
		m_UIGroot.transform.FindChild("PlaneteInfo").FindChild("PlaneteInfo_Background").gameObject.SetActive(true);

		//Satisfaction (haineMax = 30 dans CGame)
		m_UIGroot.transform.FindChild("PlaneteInfo").FindChild("PlaneteInfo_Satisfaction").gameObject.GetComponent<UISlider>().value = CConstantes.Game.ppnHainePlanete[planete.nID, planete.nID]/30.0f;
		m_UIGroot.transform.FindChild("PlaneteInfo").FindChild("PlaneteInfo_Satisfaction").gameObject.SetActive(true);

		//Affichage des 3 animosites de la planete
		//Animosite i    TrouveAnimosite(planete, i))  avec i entre 1 et 3
	
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
	public void CalendrierProchainMatch(CPlanete planet1, CPlanete planet2, CPlanete lieu)
	{
	//	print ("azeaze "+lieu.m_eNamePlanet);
	//	m_UIGroot.transform.FindChild ("Calendrier").FindChild ("Planete").FindChild ("OrangeA").gameObject.SetActive (true);
		CalendrierDesactivation();

		//Active affichage lieu
		switch(lieu.m_eNamePlanet)
		{
		case CPlanete.ENamePlanet.e_A: { m_UIGroot.transform.FindChild("Calendrier").FindChild("Planete").FindChild("OrangeA").gameObject.SetActive(true); break; }
		case CPlanete.ENamePlanet.e_C: { m_UIGroot.transform.FindChild("Calendrier").FindChild("Planete").FindChild("TurquoiseC").gameObject.SetActive(true); break; }
		case CPlanete.ENamePlanet.e_K: { m_UIGroot.transform.FindChild("Calendrier").FindChild("Planete").FindChild("VertK").gameObject.SetActive(true); break; }
		case CPlanete.ENamePlanet.e_Sentry: { m_UIGroot.transform.FindChild("Calendrier").FindChild("Planete").FindChild("JauneS").gameObject.SetActive(true); break; }
		case CPlanete.ENamePlanet.e_Terra: { m_UIGroot.transform.FindChild("Calendrier").FindChild("Planete").FindChild("BleuT").gameObject.SetActive(true); break; }
		case CPlanete.ENamePlanet.e_V: { m_UIGroot.transform.FindChild("Calendrier").FindChild("Planete").FindChild("RougeV").gameObject.SetActive(true); break; }
		case CPlanete.ENamePlanet.e_X: { m_UIGroot.transform.FindChild("Calendrier").FindChild("Planete").FindChild("VioletX").gameObject.SetActive(true); break; }
		case CPlanete.ENamePlanet.e_Yoranus: { m_UIGroot.transform.FindChild("Calendrier").FindChild("Planete").FindChild("RoseY").gameObject.SetActive(true); break; }
		}

		//Active affichage equipe1
		switch(planet1.m_eNamePlanet)
		{
		case CPlanete.ENamePlanet.e_A: { m_UIGroot.transform.FindChild("Calendrier").FindChild("Equipe1").FindChild("OrangeA").gameObject.SetActive(true); break; }
		case CPlanete.ENamePlanet.e_C: { m_UIGroot.transform.FindChild("Calendrier").FindChild("Equipe1").FindChild("TurquoiseC").gameObject.SetActive(true); break; }
		case CPlanete.ENamePlanet.e_K: { m_UIGroot.transform.FindChild("Calendrier").FindChild("Equipe1").FindChild("VertK").gameObject.SetActive(true); break; }
		case CPlanete.ENamePlanet.e_Sentry: { m_UIGroot.transform.FindChild("Calendrier").FindChild("Equipe1").FindChild("JauneS").gameObject.SetActive(true); break; }
		case CPlanete.ENamePlanet.e_Terra: { m_UIGroot.transform.FindChild("Calendrier").FindChild("Equipe1").FindChild("BleuT").gameObject.SetActive(true); break; }
		case CPlanete.ENamePlanet.e_V: { m_UIGroot.transform.FindChild("Calendrier").FindChild("Equipe1").FindChild("RougeV").gameObject.SetActive(true); break; }
		case CPlanete.ENamePlanet.e_X: { m_UIGroot.transform.FindChild("Calendrier").FindChild("Equipe1").FindChild("VioletX").gameObject.SetActive(true); break; }
		case CPlanete.ENamePlanet.e_Yoranus: { m_UIGroot.transform.FindChild("Calendrier").FindChild("Equipe1").FindChild("RoseY").gameObject.SetActive(true); break; }
		}

		//Active affichage equipe2
		switch(planet2.m_eNamePlanet)
		{
		case CPlanete.ENamePlanet.e_A: { m_UIGroot.transform.FindChild("Calendrier").FindChild("Equipe2").FindChild("OrangeA").gameObject.SetActive(true); break; }
		case CPlanete.ENamePlanet.e_C: { m_UIGroot.transform.FindChild("Calendrier").FindChild("Equipe2").FindChild("TurquoiseC").gameObject.SetActive(true); break; }
		case CPlanete.ENamePlanet.e_K: { m_UIGroot.transform.FindChild("Calendrier").FindChild("Equipe2").FindChild("VertK").gameObject.SetActive(true); break; }
		case CPlanete.ENamePlanet.e_Sentry: { m_UIGroot.transform.FindChild("Calendrier").FindChild("Equipe2").FindChild("JauneS").gameObject.SetActive(true); break; }
		case CPlanete.ENamePlanet.e_Terra: { m_UIGroot.transform.FindChild("Calendrier").FindChild("Equipe2").FindChild("BleuT").gameObject.SetActive(true); break; }
		case CPlanete.ENamePlanet.e_V: { m_UIGroot.transform.FindChild("Calendrier").FindChild("Equipe2").FindChild("RougeV").gameObject.SetActive(true); break; }
		case CPlanete.ENamePlanet.e_X: { m_UIGroot.transform.FindChild("Calendrier").FindChild("Equipe2").FindChild("VioletX").gameObject.SetActive(true); break; }
		case CPlanete.ENamePlanet.e_Yoranus: { m_UIGroot.transform.FindChild("Calendrier").FindChild("Equipe2").FindChild("RoseY").gameObject.SetActive(true); break; }
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

	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------
	public void PlaneteInfoDurDesactivation()
	{
		m_UIGroot.transform.FindChild("PlaneteInfoDur").FindChild("PlaneteInfo_BleuT").gameObject.SetActive(false);
		m_UIGroot.transform.FindChild("PlaneteInfoDur").FindChild("PlaneteInfo_JauneS").gameObject.SetActive(false);
		m_UIGroot.transform.FindChild("PlaneteInfoDur").FindChild("PlaneteInfo_OrangeA").gameObject.SetActive(false);
		m_UIGroot.transform.FindChild("PlaneteInfoDur").FindChild("PlaneteInfo_RoseY").gameObject.SetActive(false);
		m_UIGroot.transform.FindChild("PlaneteInfoDur").FindChild("PlaneteInfo_RougeV").gameObject.SetActive(false);
		m_UIGroot.transform.FindChild("PlaneteInfoDur").FindChild("PlaneteInfo_TurquoiseC").gameObject.SetActive(false);
		m_UIGroot.transform.FindChild("PlaneteInfoDur").FindChild("PlaneteInfo_VertK").gameObject.SetActive(false);
		m_UIGroot.transform.FindChild("PlaneteInfoDur").FindChild("PlaneteInfo_VioletX").gameObject.SetActive(false);
	}

	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------
	public void CalendrierDesactivation()
	{
		m_UIGroot.transform.FindChild("Calendrier").FindChild("Planete").FindChild("BleuT").gameObject.SetActive(false);
		m_UIGroot.transform.FindChild("Calendrier").FindChild("Planete").FindChild("JauneS").gameObject.SetActive(false);
		m_UIGroot.transform.FindChild("Calendrier").FindChild("Planete").FindChild("OrangeA").gameObject.SetActive(false);
		m_UIGroot.transform.FindChild("Calendrier").FindChild("Planete").FindChild("RoseY").gameObject.SetActive(false);
		m_UIGroot.transform.FindChild("Calendrier").FindChild("Planete").FindChild("RougeV").gameObject.SetActive(false);
		m_UIGroot.transform.FindChild("Calendrier").FindChild("Planete").FindChild("TurquoiseC").gameObject.SetActive(false);
		m_UIGroot.transform.FindChild("Calendrier").FindChild("Planete").FindChild("VertK").gameObject.SetActive(false);
		m_UIGroot.transform.FindChild("Calendrier").FindChild("Planete").FindChild("VioletX").gameObject.SetActive(false);

		m_UIGroot.transform.FindChild("Calendrier").FindChild("Equipe1").FindChild("BleuT").gameObject.SetActive(false);
		m_UIGroot.transform.FindChild("Calendrier").FindChild("Equipe1").FindChild("JauneS").gameObject.SetActive(false);
		m_UIGroot.transform.FindChild("Calendrier").FindChild("Equipe1").FindChild("OrangeA").gameObject.SetActive(false);
		m_UIGroot.transform.FindChild("Calendrier").FindChild("Equipe1").FindChild("RoseY").gameObject.SetActive(false);
		m_UIGroot.transform.FindChild("Calendrier").FindChild("Equipe1").FindChild("RougeV").gameObject.SetActive(false);
		m_UIGroot.transform.FindChild("Calendrier").FindChild("Equipe1").FindChild("TurquoiseC").gameObject.SetActive(false);
		m_UIGroot.transform.FindChild("Calendrier").FindChild("Equipe1").FindChild("VertK").gameObject.SetActive(false);
		m_UIGroot.transform.FindChild("Calendrier").FindChild("Equipe1").FindChild("VioletX").gameObject.SetActive(false);
		
		m_UIGroot.transform.FindChild("Calendrier").FindChild("Equipe2").FindChild("BleuT").gameObject.SetActive(false);
		m_UIGroot.transform.FindChild("Calendrier").FindChild("Equipe2").FindChild("JauneS").gameObject.SetActive(false);
		m_UIGroot.transform.FindChild("Calendrier").FindChild("Equipe2").FindChild("OrangeA").gameObject.SetActive(false);
		m_UIGroot.transform.FindChild("Calendrier").FindChild("Equipe2").FindChild("RoseY").gameObject.SetActive(false);
		m_UIGroot.transform.FindChild("Calendrier").FindChild("Equipe2").FindChild("RougeV").gameObject.SetActive(false);
		m_UIGroot.transform.FindChild("Calendrier").FindChild("Equipe2").FindChild("TurquoiseC").gameObject.SetActive(false);
		m_UIGroot.transform.FindChild("Calendrier").FindChild("Equipe2").FindChild("VertK").gameObject.SetActive(false);
		m_UIGroot.transform.FindChild("Calendrier").FindChild("Equipe2").FindChild("VioletX").gameObject.SetActive(false);
	}
}
