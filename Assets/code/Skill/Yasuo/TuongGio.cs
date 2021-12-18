using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuongGio : MonoBehaviour {
	public float TocDo;
	public float TimeTuHuy;
	public GameObject PlayerBan;
	public float DoXa = 2f;

//	private Vector3 pos;
//	private float pos1;

//	void Start(){
//		pos1 = transform.position.magnitude + DoXa;
//	}
	void Update () {
		TimeTuHuy -= Time.deltaTime;
		if (TimeTuHuy <= 0) {
			if (GetComponent<PhotonView> ().instantiationId == 0) {
				Destroy (gameObject);
			} else if (GetComponent<PhotonView> ().isMine) {
				PhotonNetwork.Destroy (this.gameObject);
			}
		}
		DoXa -= Time.deltaTime;
		if (DoXa>0) {
			transform.Translate (Vector3.up * TocDo * Time.deltaTime);
		}
	}
}

