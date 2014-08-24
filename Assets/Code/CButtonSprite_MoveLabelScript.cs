using UnityEngine;
using System.Collections;

public class CButtonSprite_MoveLabelScript : MonoBehaviour
{
	public void MoveYourLabelDown4 ()
	{
		Vector3 pos = transform.localPosition;
		pos.y += -4;
		transform.localPosition = pos;
	}
	public void MoveYourLabelUp4 ()
	{
		Vector3 pos = transform.localPosition;
		pos.y += 4;
		transform.localPosition = pos;
	}
}
