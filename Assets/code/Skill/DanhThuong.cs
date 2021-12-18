using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanhThuong : MonoBehaviour {
	public GameObject PlayerBan;
	public int SatThuong = 15;
	public bool ChoPhepTH = false;
	public float TimeTuHuy;
	void Update () {
		if(ChoPhepTH==true){
		TimeTuHuy -= Time.deltaTime;
		if (TimeTuHuy <= 0) {
			if(GetComponent<PhotonView>().instantiationId == 0){
				Destroy (gameObject);
			}else if(GetComponent<PhotonView>().isMine){
				PhotonNetwork.Destroy (this.gameObject);
			}
		}
		}
	}
	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject != gameObject && coll.gameObject != PlayerBan) {
			MauPlayer MP = coll.gameObject.GetComponent<MauPlayer> ();
			PhotonView MP1 = coll.gameObject.GetComponent<PhotonView> ();
			if (MP != null && MP1!=null) {
				if (ChoPhepTH == true) {
					if (GetComponent<PhotonView> ().instantiationId == 0) {
					} else if (GetComponent<PhotonView> ().isMine) {
						coll.gameObject.GetComponent<PhotonView> ().RPC ("GetDataPlayerkill", PhotonTargets.All, PlayerBan.GetComponent<PhotonView> ().viewID);
						coll.gameObject.GetComponent<PhotonView> ().RPC ("SatThuongMau", PhotonTargets.All, SatThuong);
					}
				} else {
					coll.gameObject.GetComponent<PhotonView> ().RPC ("GetDataPlayerkill", PhotonTargets.All, PlayerBan.GetComponent<PhotonView> ().viewID);
					coll.gameObject.GetComponent<PhotonView> ().RPC ("SatThuongMau", PhotonTargets.All, SatThuong);
				}
			}
		}
	}
}
