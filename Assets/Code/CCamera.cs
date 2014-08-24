using UnityEngine;
using System.Collections;

public class CCamera : MonoBehaviour {

	Vector3 m_vInitPosMous;
	int nMaxZoom;
	int nMinZoom;
	// Use this for initialization
	void Start () 
	{
		nMaxZoom = 35;
		nMinZoom = 3;
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

			if(gameObject.transform.position.x < -CConstantes.nMapSize)
				SetPosX(-CConstantes.nMapSize);
			if(gameObject.transform.position.x > CConstantes.nMapSize)
				SetPosX(CConstantes.nMapSize);
			if(gameObject.transform.position.y < -CConstantes.nMapSize)
				SetPosY(-CConstantes.nMapSize);
			if(gameObject.transform.position.y > CConstantes.nMapSize)
				SetPosY(CConstantes.nMapSize);
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

		if(GetComponent<Camera>().orthographicSize > nMaxZoom)
			GetComponent<Camera>().orthographicSize = nMaxZoom;
		if(GetComponent<Camera>().orthographicSize < nMinZoom)
			GetComponent<Camera>().orthographicSize = nMinZoom;
	}

	void SetPosX(float fPosX)
	{
		Vector3 pos = gameObject.transform.position;
		pos.x = fPosX;
		gameObject.transform.position = pos;
	}

	void SetPosY(float fPosY)
	{
		Vector3 pos = gameObject.transform.position;
		pos.y = fPosY;
		gameObject.transform.position = pos;
	}
}
