using UnityEngine;
using System.Collections;

public class CEvent : MonoBehaviour {
	
	public GameObject m_prefabChampAsteroides;
	public GameObject m_prefabPirates;

	public enum EeventType{
		e_Asteroides,
		e_Pirates,
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
		if(nSizeListRoad > 0)
		{
			int nIdRoad = Random.Range(0, nSizeListRoad);
			if(!CConstantes.ListRoad[nIdRoad].GetComponent<CRoad>().IsUnderAttack())
			{
				EeventType eChoiceEvent = CApoilMath.GetRandomEnum<EeventType>();

				CConstantes.Game.GetComponent<CEvent>().LaunchEvent(eChoiceEvent, CConstantes.ListRoad[nIdRoad]);
				CConstantes.ListRoad[nIdRoad].GetComponent<CRoad>().Attack();
			}
		}
	}

	void LaunchEvent(EeventType eChoiceEvent, GameObject road)
	{
		Debug.Log (eChoiceEvent);
		switch(eChoiceEvent)
		{
			case EeventType.e_Asteroides:
			{
				LaunchAsteroides(road);
				break;
			}
			case EeventType.e_Pirates:
			{
				LaunchPirates(road);
				break;
			}
		}
	}

	void LaunchAsteroides(GameObject road)
	{
		GameObject asteroide = ((GameObject) GameObject.Instantiate(m_prefabChampAsteroides));
		asteroide.transform.position = road.transform.position;
		asteroide.transform.right = road.transform.right;
		//asteroide.transform.Translate (10*asteroide.transform.up);
	}

	void LaunchPirates(GameObject road)
	{
		GameObject pirates = ((GameObject) GameObject.Instantiate(m_prefabPirates));
		pirates.transform.position = road.transform.position;
		//pirates.transform.right = -road.transform.right;
		//pirates.transform.forward = road.transform.forward;
	}
}
