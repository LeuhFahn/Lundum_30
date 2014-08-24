using UnityEngine;
using System.Collections;

public class CMenu : MonoBehaviour {

	public GameObject goMenuPrincipal;
	public GameObject goCredits;

	void Start()
	{
		OpenMenuPrincipal();
	 }

	public void LoadNextLevel ()
	{
		Debug.Log("Lancer scene");
//		if(Application.loadedLevel < Application.levelCount)
//		{
//			Application.LoadLevel(Application.loadedLevel+1);
//		}
	}
	
	public void OpenMenuPrincipal()
	{
		goMenuPrincipal.SetActive(true);
		goCredits.SetActive(false);
	}

	public void OpenCredits()
	{
		goMenuPrincipal.SetActive(false);
		goCredits.SetActive(true);
	}

}
