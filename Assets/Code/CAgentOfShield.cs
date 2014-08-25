using UnityEngine;
using System.Collections;

public class CAgentOfShield : MonoBehaviour {

	float m_fTimeDesactivation;

	// Use this for initialization
	void Awake () 
	{
		m_fTimeDesactivation = 2.0f;
	}
	
	// Update is called once per frame
	void Update () {

		if(m_fTimeDesactivation > 0.0f)
		{
			float fAngularVelocity = 2.0f;
			gameObject.transform.RotateAround(gameObject.transform.position, transform.up, fAngularVelocity);
			m_fTimeDesactivation -= Time.deltaTime;
		}
		else
		{
			gameObject.SetActive(false);
		}
	}

	public void Activate()
	{
		m_fTimeDesactivation = 2.0f;
	}

	public void SetTiling(float fSize)
	{
		float fWidth = gameObject.renderer.material.GetTexture("_MainTex").width;
		gameObject.renderer.material.SetTextureScale("_MainTex", new Vector2(1, fSize/4.0f));	
	}
}
