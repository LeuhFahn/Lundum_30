using UnityEngine;
using System.Collections;

public class CApoilInput
{
	public static bool QuitGame;

	//Debug
	public static bool DebugF9;
	public static bool DebugF10;
	public static bool DebugF11;
	public static bool DebugF12;

	public static void Init()
	{
	}
	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------
	public static void Process(float fDeltatime) 
	{
		QuitGame = Input.GetKey(KeyCode.Escape);
		
		DebugF9 = Input.GetKeyDown(KeyCode.F9);
		DebugF10 = Input.GetKeyDown(KeyCode.F10);
		DebugF11 = Input.GetKeyDown(KeyCode.F11);
		DebugF12 = Input.GetKeyDown(KeyCode.F12);
	}
}
