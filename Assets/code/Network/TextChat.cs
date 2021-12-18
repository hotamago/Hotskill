using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextChat : MonoBehaviour {

	[PunRPC]
	public void UpText(string textchat){
		gameObject.GetComponent<TextMesh>().text = textchat;
	}
}
