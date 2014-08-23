using UnityEngine;
using System.Collections;

public class CCamera : MonoBehaviour {

	Vector3 m_vInitPosMous;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		GestionCamera();
	}

	void GestionCamera()
	{
		if (CApoilInput.RightClickDown)
		{
			m_vInitPosMous = GetMousePositionInScreen();
		}
		
		if (CApoilInput.RightClick)
		{
			Vector3 vDeltaMousePos = GetMousePositionInScreen() - m_vInitPosMous;
			MoveHorizontal(vDeltaMousePos.x);
			MoveVertical(vDeltaMousePos.y);
		}
		
		ZoomInOut(5.0f * Input.GetAxis("Mouse ScrollWheel"));
	}
	
	
	Vector3 GetMousePositionInScreen()
	{
		return new Vector3(Input.mousePosition.x / (float)Screen.width, Input.mousePosition.y / (float)Screen.height, 0.0f);
	}

	public void MoveHorizontal(float fDeltaPosition)
	{
		transform.Translate(new Vector3(fDeltaPosition, 0, 0), Space.Self);
	}
	
	public void MoveVertical(float fDeltaPosition)
	{
		transform.Translate(new Vector3(0, fDeltaPosition, 0), Space.Self);
	}
	
	public void ZoomInOut(float fDeltaPosition)
	{
		GetComponent<Camera>().orthographicSize -= fDeltaPosition;
	}
}
