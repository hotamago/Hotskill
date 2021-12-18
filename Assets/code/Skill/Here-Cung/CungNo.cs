using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CungNo : MonoBehaviour {
	public float TocDo;
	public float TimeTuHuy;
	public GameObject PlayerBan;
	public bool DaNo = false;

	void Update () {
		transform.Translate (Vector3.up * TocDo * Time.deltaTime);
		TimeTuHuy -= Time.deltaTime;
		if (TimeTuHuy <= 0 && DaNo==false) {
			GameObject SpawnCungTen = (GameObject)PhotonNetwork.Instantiate ("BCungNo", gameObject.transform.position, gameObject.transform.rotation, 0);
			SpawnCungTen.GetComponent<DanhThuong>().PlayerBan = PlayerBan;
			if(GetComponent<PhotonView>().instantiationId == 0){
				Destroy (gameObject);
			}else if(GetComponent<PhotonView>().isMine){
				PhotonNetwork.Destroy (gameObject);
			}
			DaNo = true;
		}
	}
	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject != gameObject && coll.gameObject != PlayerBan) {
			MauPlayer MP = coll.gameObject.GetComponent<MauPlayer>();
			if (MP != null) {
				GameObject SpawnCungTen = (GameObject)PhotonNetwork.Instantiate ("BCungNo", gameObject.transform.position, gameObject.transform.rotation, 0);
				SpawnCungTen.GetComponent<DanhThuong> ().PlayerBan = PlayerBan;
				if (GetComponent<PhotonView> ().instantiationId == 0) {
					Destroy (gameObject);
				} else if (GetComponent<PhotonView> ().isMine) {
					PhotonNetwork.Destroy (gameObject);
				}
				DaNo = true;
			} else {
				if (coll.gameObject.tag == "TuongGio") {
					GameObject SpawnCungTen = (GameObject)PhotonNetwork.Instantiate ("BCungNo", gameObject.transform.position, gameObject.transform.rotation, 0);
					SpawnCungTen.GetComponent<DanhThuong> ().PlayerBan = PlayerBan;
					if (GetComponent<PhotonView> ().instantiationId == 0) {
						Destroy (gameObject);
					} else if (GetComponent<PhotonView> ().isMine) {
						PhotonNetwork.Destroy (gameObject);
					}
					DaNo = true;
				}
			}
		}
	}
}
