using UnityEngine;
using System.Collections;

public class CEventInGame : MonoBehaviour {

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
			Destroy(this.gameObject);
		}
	}
}
