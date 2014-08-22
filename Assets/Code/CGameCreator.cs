using UnityEngine;
using System.Collections;

public class CGameCreator : MonoBehaviour {

	static int m_instanceCount = 0;
	public GameObject m_prefabGame;
	
	void Awake () 
	{
		if(m_instanceCount++ == 0){
			GameObject game = ((GameObject) GameObject.Instantiate(m_prefabGame));
			game.name = "_Game";
		}
	}

	void Update () 
	{
	
	}
	
	void OnDestroy() {
		m_instanceCount--;
	}
}
