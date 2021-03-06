﻿using UnityEngine;
using System.Collections;

public class CMenu : MonoBehaviour {

	public enum EmenuState
	{
		e_menuState_splash,
		e_menuState_main,
		e_menuState_inGame,
	}

	public GameObject goMenuPrincipal;
	public GameObject goCredits;
	public GameObject goScene;
	public Texture m_Texture_Splash;

	EmenuState m_EState;
	float m_fTempsSplash;
	const float m_fTempsSplashInit = 4.0f;
	bool m_bGoofy;

	public bool IsGoofy
	{
		get {return m_bGoofy; }
		set {m_bGoofy = value; }
	}

	void Start()
	{
		m_EState = EmenuState.e_menuState_splash;
		m_fTempsSplash = m_fTempsSplashInit;
		goScene.SetActive(false);
		DontDestroyOnLoad(transform.gameObject);
	}

	void Update()
	{
		if(m_EState == EmenuState.e_menuState_splash && m_fTempsSplash>0.0f)
			m_fTempsSplash -= Time.deltaTime;
	}

	void OnGUI() 
	{
		switch(m_EState)
		{
			case EmenuState.e_menuState_splash:
			{
				if(m_fTempsSplash > 0.0f)
				{
					float fCoeffScale = 1.0f + (m_fTempsSplashInit - m_fTempsSplash)/(10.0f*m_fTempsSplashInit);
					float fWidth = 960 * fCoeffScale;
					float fHeight = 600 * fCoeffScale;
					GUI.DrawTexture(new Rect((960 - fWidth)/2.0f, (600 - fHeight)/2.0f, fWidth, fHeight), m_Texture_Splash);
				}
				else
				{
					m_EState = EmenuState.e_menuState_main;
					OpenMenuPrincipal();
					goScene.SetActive(true);
				}
				break;
			}
			case EmenuState.e_menuState_main:
			{
				break;
			}
			case EmenuState.e_menuState_inGame:
			{
				break;
			}
		}
	}

	public void ClicGoofy()
	{
		m_bGoofy = true;
		LoadNextLevel ();
	}

	public void ClicRegular()
	{
		m_bGoofy = false;
		LoadNextLevel ();
	}

	public void LoadNextLevel ()
	{
		//Debug.Log("Lancer scene");
		m_EState = EmenuState.e_menuState_inGame;
		if(Application.loadedLevel < Application.levelCount)
		{
			Application.LoadLevel(Application.loadedLevel+1);
		}
	}

	public static void LoadMenuLevel()
	{
		Application.LoadLevel(0);
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
