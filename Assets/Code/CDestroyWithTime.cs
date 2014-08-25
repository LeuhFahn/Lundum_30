using UnityEngine;
using System.Collections;

public class CDestroyWithTime : MonoBehaviour {

	public float fTimer = 1.0f;
	float m_fTimerDesactivation;

	void Awake () 
	{
		m_fTimerDesactivation = fTimer;
	}
	
	// Update is called once per frame
	void Update () {
		if(m_fTimerDesactivation > 0.0f)
		{
			m_fTimerDesactivation -= RealTime.deltaTime;
		}
		else
		{
			Destroy(this.gameObject);
		}
	}
}
