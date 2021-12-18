using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocBanXa : MonoBehaviour {
	public float TocDo;
	public int SatThuong;
	public float TimeTuHuy;
	public GameObject PlayerBan;

	void Update () {
		transform.Translate (Vector3.up * TocDo * Time.deltaTime);
		TimeTuHuy -= Time.deltaTime;
		if (TimeTuHuy <= 0) {
			if(GetComponent<PhotonView>().instantiationId == 0){
				Destroy (gameObject);
			}else if(GetComponent<PhotonView>().isMine){
				PhotonNetwork.Destroy (this.gameObject);
			}
		}
	}
	void OnCollisionEnter2D(Collision2D coll) {
		MauPlayer MP = coll.gameObject.GetComponent<MauPlayer>();
		if (coll.gameObject != gameObject && coll.gameObject != PlayerBan) {
			if (MP != null) {
				if (GetComponent<PhotonView> ().instantiationId == 0) {
				} else if (GetComponent<PhotonView> ().isMine) {
					coll.gameObject.GetComponent<PhotonView> ().RPC ("GetDataPlayerkill", PhotonTargets.All, PlayerBan.GetComponent<PhotonView>().viewID);
					coll.gameObject.GetComponent<PhotonView> ().RPC ("SatThuongMau", PhotonTargets.All, SatThuong);
				}
			} else {
				if (coll.gameObject.tag == "TuongGio") {
					if (GetComponent<PhotonView> ().instantiationId == 0) {
						Destroy (gameObject);
					} else if (GetComponent<PhotonView> ().isMine) {
						PhotonNetwork.Destroy (this.gameObject);
					}
				}
			}
		}
	}
}
