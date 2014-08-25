using UnityEngine;
using System.Collections;

public class CPlanete : MonoBehaviour {

	public enum ENamePlanet{
		e_Terra,
		e_Sentry,
		e_Yoranus,
		e_C,
		e_A,
		e_V,
		e_K,
		e_X
	}
	
	public GameObject m_Halo;
	public GameObject m_Mesh;
	public GameObject m_Text;

	//NININ
	public GameObject m_TextHaine;

	//
	public GameObject m_Artifices;

	public ENamePlanet m_eNamePlanet;

	int m_nNbWorkers;
	bool m_bIsOverlapByMouse;
	bool m_bIsOrigin;
	bool m_bIsDrawInfos;
	Color m_HaloColor;
	float m_fSize;
	float m_fAngluarVelocity;
	public int m_nId;/// <summary>
	/// ///////////WARNING
	/// </summary>


	public float fSize
	{
		get {return m_fSize; }
		set {m_fSize = value; }
	}

	public int nNbWorkers
	{
		get {return m_nNbWorkers; }
		set {
			m_nNbWorkers = value; 
			SetText();
		}
	}

	public int nID
	{
		get {return m_nId; }
		set {m_nId = value; }
	}
	
	// Use this for initialization
	void Start () 
	{
		Reset ();
		m_Text.transform.parent = transform;
		m_Text.transform.position = transform.position;
		m_Text.transform.Translate(0,m_fSize - m_fSize/4.0f, 0);
		m_Text.SetActive(true);
		SetText();


		//NININ texte haine au cas ou

		m_TextHaine.transform.parent = transform;
		m_TextHaine.transform.position = transform.position;
		m_TextHaine.transform.Translate(0f,0f, 0);
		m_TextHaine.SetActive(false);
		m_TextHaine.GetComponent<TextMesh>().text = "test";
		//\NININ


		m_Halo.GetComponent<Light>().range = 6.0f + 2.5f * m_nNbWorkers;

		m_fAngluarVelocity = Random.Range(0.1f, 0.6f);
	}
	
	// Update is called once per frame
	void Update () 
	{
		m_Mesh.transform.RotateAround(m_Mesh.transform.position, m_Mesh.transform.up, m_fAngluarVelocity);
	}

	public void Init()
	{
		m_fSize = 2.5f + 2.5f*m_nNbWorkers;
		m_Mesh.transform.localScale = new Vector3(m_fSize, m_fSize, m_fSize);
	}

	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------
	public void Reset()
	{
		m_bIsOverlapByMouse = false;
		m_bIsOrigin = false;
		StopDrawInfo();
	}

	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------
	public void SelectPlaneteAsOrigin()
	{
		Reset();
		//Debug.Log (gameObject.name+"SelectPlaneteAsOrigin");
		m_Halo.SetActive(true);
		m_Halo.GetComponent<Light>().color = Color.green;
		m_bIsOrigin = true;
	}

	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------
	public void OverlapByMouse(CPlanete m_PlaneteOrigin)
	{
		if(!m_bIsOrigin && !m_bIsOverlapByMouse)
		{
			//Debug.Log (gameObject.name+"OverlapByMouse");
			m_bIsOverlapByMouse = true;
			m_Halo.SetActive(true);
			m_TextHaine.GetComponent<TextMesh>().text = CConstantes.Game.ppnHainePlanete[m_PlaneteOrigin.nID,m_nId].ToString();
			m_TextHaine.SetActive(true);
			if(CConstantes.Game.routePossible[m_PlaneteOrigin.nID,m_nId]){
			m_Halo.GetComponent<Light>().color = Color.white;
			}
			else{
				m_Halo.GetComponent<Light>().color = Color.red;
			}

		}
	}

	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------
	public void StopSelection()
	{
		if(!m_bIsDrawInfos)
		{
			m_bIsOverlapByMouse = false;
			m_bIsOrigin = false;
			m_Halo.SetActive(false);
			m_TextHaine.SetActive(false);
		}
	}

	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------
	public void SelectForDrawInfo()
	{
		for(int i = 0 ; i < CConstantes.nNbPlanetes ; ++i)
		{
			CConstantes.Planetes[i].GetComponent<CPlanete>().StopDrawInfo();
		}
		m_bIsDrawInfos = true;
		m_Halo.SetActive(true);
		m_Halo.GetComponent<Light>().color = Color.yellow;

	}

	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------
	public void StopDrawInfo()
	{
		m_bIsDrawInfos = false;
		m_Halo.SetActive(false);
		//m_Halo.GetComponent<Light>().color = Color.yellow;
	}

	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------
	public GameObject GetGameObject()
	{
		return this.gameObject;
	}

	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------
	void SetText()
	{
		m_Text.GetComponent<TextMesh>().text = m_nNbWorkers.ToString();
	}
	public void SetText2(string t){
		m_Text.GetComponent<TextMesh>().text = t;
		}
}
