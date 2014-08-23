using UnityEngine;
using System.Collections;

public class CGameCreator : MonoBehaviour {

	static int m_instanceCount = 0;
	public GameObject m_prefabGame;
	public GameObject m_prefabCamera;
	
	void Awake () 
	{
		if(m_instanceCount++ == 0){
			GameObject game = ((GameObject) GameObject.Instantiate(m_prefabGame));
			game.name = "_Game";
			GameObject camera = ((GameObject) GameObject.Instantiate(m_prefabCamera));
			camera.name = "_Camera";
			game.GetComponent<CGame>().m_Camera = camera;
		}
	}

	void Update () 
	{
	
	}
	
	void OnDestroy() {
		m_instanceCount--;
	}
}
