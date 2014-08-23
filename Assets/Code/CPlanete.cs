using UnityEngine;
using System.Collections;

public class CPlanete : MonoBehaviour {

	public GameObject m_Halo;

	public int m_nNbWorkers = 0;
	bool m_bIsOverlapByMouse;
	bool m_bIsOrigin;
	Color m_HaloColor;
	float m_fSize;

	public float fSize
	{
		get {return m_fSize; }
		set {m_fSize = value; }
	}
	
	// Use this for initialization
	void Start () {
		Reset ();
		m_fSize = 5.0f + 2.5f*m_nNbWorkers;
		gameObject.transform.localScale = new Vector3(m_fSize, m_fSize, m_fSize);
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
		//Debug.Log (gameObject.name+"SelectPlaneteAsOrigin");
		m_Halo.SetActive(true);
		m_Halo.GetComponent<Light>().color = Color.green;
		m_bIsOrigin = true;
	}

	public void OverlapByMouse()
	{
		if(!m_bIsOrigin && !m_bIsOverlapByMouse)
		{
			//Debug.Log (gameObject.name+"OverlapByMouse");

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
