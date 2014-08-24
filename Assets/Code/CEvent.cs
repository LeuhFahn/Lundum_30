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
		if(CApoilInput.DebugF9)
			CEvent.LaunchEventOnARoad();
	}

	static public void LaunchEventOnARoad()
	{
		int nSizeListRoad = CConstantes.ListRoad.Count;
		int nIdRoad = Random.Range(1, nSizeListRoad);
		EeventType eChoiceEvent = CApoilMath.GetRandomEnum<EeventType>();
		CConstantes.Game.GetComponent<CEvent>().LaunchEvent(eChoiceEvent);
	}

	void LaunchEvent(EeventType eChoiceEvent)
	{
		Debug.Log (eChoiceEvent);

	}

	void LaunchAsteroides(GameObject road)
	{
		GameObject asteroide = ((GameObject) GameObject.Instantiate(m_prefabChampAsteroides));
		asteroide.transform.position = road.transform.position;
	}
}
