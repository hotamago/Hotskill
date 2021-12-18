using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CungThuong : MonoBehaviour {
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
		if (coll.gameObject != gameObject && coll.gameObject != PlayerBan) {
			MauPlayer MP = coll.gameObject.GetComponent<MauPlayer> ();
			PhotonView MP1 = coll.gameObject.GetComponent<PhotonView> ();
			if (MP != null && MP1!=null) {
				if (GetComponent<PhotonView> ().instantiationId == 0) {
					Destroy (gameObject);
				} else if (GetComponent<PhotonView> ().isMine) {
					PhotonNetwork.Destroy (this.gameObject);
					coll.gameObject.GetComponent<PhotonView> ().RPC ("GetDataPlayerkill", PhotonTargets.All, PlayerBan.GetComponent<PhotonView> ().viewID);
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
