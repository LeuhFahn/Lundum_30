using UnityEngine;
using System.Collections;

public class CEvent : MonoBehaviour {
	
	public GameObject m_prefabChampAsteroides;

	public enum EeventType{
		e_Asteroides,
		e_Pirates,
		e_TempeteMagnetique
	}

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	static public void LaunchEventOnARoad()
	{
		Debug.Log ("on va tous crever!!");
		//CConstantes.Game.GetComponent<CEvent>().LaunchAsteroides(CConstantes.ListRoad[0]);
	}

	void LaunchAsteroides(GameObject road)
	{
		GameObject asteroide = ((GameObject) GameObject.Instantiate(m_prefabChampAsteroides));
		asteroide.transform.position = road.transform.position;
	}
}
