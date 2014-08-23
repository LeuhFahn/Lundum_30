using UnityEngine;
using System.Collections;

public class CPlanete : MonoBehaviour {

	bool m_bIsOverlapByMouse;
	bool m_bIsOrigin;
	public GameObject m_Halo;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(m_bIsOverlapByMouse && !m_bIsOrigin)
		{
			m_bIsOverlapByMouse = false;
			m_Halo.SetActive(false);
		}
	}

	public void SelectPlaneteAsOrigin()
	{
		m_Halo.SetActive(true);
		m_bIsOrigin = true;
	}

	public void OverlapByMouse()
	{
		m_bIsOverlapByMouse = true;
		m_Halo.SetActive(true);
	}

	public void StopSelection()
	{
		m_Halo.SetActive(false);
	}
}
