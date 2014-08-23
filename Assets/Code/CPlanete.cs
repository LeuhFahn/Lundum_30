using UnityEngine;
using System.Collections;

public class CPlanete : MonoBehaviour {

	bool m_bIsOverlapByMouse;
	bool m_bIsOrigin;
	public GameObject m_Halo;
	Color m_HaloColor;

	// Use this for initialization
	void Start () {
		Reset ();
	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	public void Reset()
	{
		m_bIsOverlapByMouse = false;
		m_bIsOrigin = false;
		m_Halo.SetActive(false);
	}

	public void SelectPlaneteAsOrigin()
	{
		Debug.Log (gameObject.name+"SelectPlaneteAsOrigin");
		m_Halo.SetActive(true);
		m_Halo.GetComponent<Light>().color = Color.red;
		m_bIsOrigin = true;
	}

	public void OverlapByMouse()
	{
		if(!m_bIsOrigin && !m_bIsOverlapByMouse)
		{
			Debug.Log (gameObject.name+"OverlapByMouse");

			m_bIsOverlapByMouse = true;
			m_Halo.GetComponent<Light>().color = Color.white;
			m_Halo.SetActive(true);
		}
	}

	public void StopSelection()
	{
		Reset ();
	}

	public GameObject GetGameObject()
	{
		return this.gameObject;
	}
}
