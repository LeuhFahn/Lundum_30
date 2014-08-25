using UnityEngine;
using System.Collections;

public class CEventInGame : MonoBehaviour {

	public CEvent.EeventType m_eType;
	float m_fTimerDesactivation;

	// Use this for initialization
	void Awake () 
	{
		m_fTimerDesactivation = CConstantes.fTimerBeforeRoadDestruction + 5.0f;
	}

	// Update is called once per frame
	void Update () 
	{
		if(m_fTimerDesactivation > 0)
		{
			m_fTimerDesactivation -= Time.deltaTime;
		}
		else
		{
			//leave
			Destroy(this.gameObject);
		}


		switch(m_eType)
		{
			case CEvent.EeventType.e_Asteroides:
				break;
			case CEvent.EeventType.e_Pirates:
				gameObject.transform.RotateAround(transform.position, transform.up, 2.0f);
				break;
		}
	}

	public void destroyByShield(Vector3 position)
	{
		//instanciate explosion
		GameObject explosion = GameObject.Instantiate(CConstantes.Game.m_prefabExplosion) as GameObject;
		explosion.transform.position = position;
		Destroy(this.gameObject);
	}
}
