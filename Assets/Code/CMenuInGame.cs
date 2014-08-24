using UnityEngine;
using System.Collections;

public class CMenuInGame : MonoBehaviour {

	/*public GameObject goInfo_Background;
	public GameObject goInfo_BleuT;
	public GameObject goInfo_JauneS;
	public GameObject goInfo_OrangeA;
	public GameObject goInfo_RoseY;
	public GameObject goInfo_RougeV;
	public GameObject goInfo_TurquoiseC;
	public GameObject goInfo_VertK;
	public GameObject goInfo_VioletX;*/
	public GameObject prefabUIGroot;
	GameObject m_UIGroot;
	// Use this for initialization
	void Start () {
		m_UIGroot = ((GameObject) GameObject.Instantiate(prefabUIGroot));
		m_UIGroot.name = "_UIGroot";
		PlaneteInfoDesactivation();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------
	public void SelectThePlanet(CPlanete planete)
	{
		PlaneteInfoDesactivation();
		m_UIGroot.transform.FindChild("PlaneteInfo").FindChild("PlaneteInfo_Background").gameObject.SetActive(true);

		planete.SelectForDrawInfo();
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


	}


}
