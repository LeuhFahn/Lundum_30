using UnityEngine;
using System.Collections;

public class CGameCreator : MonoBehaviour {

	public GameObject m_prefabGame;
	// Use this for initialization
	void Start () 
	{
		GameObject game = ((GameObject) GameObject.Instantiate(m_prefabGame));
		game.name = "_Game";
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
