using UnityEngine;
using System.Collections;

public class CPlanete : MonoBehaviour {

	public GameObject m_Halo;

	int m_nNbWorkers;
	bool m_bIsOverlapByMouse;
	bool m_bIsOrigin;
	bool m_bIsDrawInfos;
	Color m_HaloColor;
	float m_fSize;
	int m_nId;

	public float fSize
	{
		get {return m_fSize; }
		set {m_fSize = value; }
	}

	public int nNbWorkers
	{
		get {return m_nNbWorkers; }
		set {m_nNbWorkers = value; }
	}

	public int nID
	{
		get {return m_nId; }
		set {m_nId = value; }
	}
	
	// Use this for initialization
	void Start () {
		Reset ();
	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	public void Init()
	{
		m_fSize = 5.0f + 2.5f*m_nNbWorkers;
		gameObject.transform.localScale = new Vector3(m_fSize, m_fSize, m_fSize);
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
	public void OverlapByMouse()
	{
		if(!m_bIsOrigin && !m_bIsOverlapByMouse)
		{
			//Debug.Log (gameObject.name+"OverlapByMouse");

			m_bIsOverlapByMouse = true;
			m_Halo.SetActive(true);
			m_Halo.GetComponent<Light>().color = Color.white;

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
}
