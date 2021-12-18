using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class MauPlayer : MonoBehaviour {
	public int maxMau = 100;
	public int MauHT;
	public Slider HienThiMau;
	public bool ChoPhepXoaDT;
	public GameObject NetworkMager;
	private NetworkManager Cf;

	//Enemy
	public GameObject DiemHoiSinh;
	public float ThoiGianSinh = 30f;


	public int PlayerIfKill;

	void Start(){
		if (gameObject.tag == "Player") {
			Cf = GameObject.FindObjectOfType<NetworkManager> ();
		}
		MauHT = maxMau;
	}
	[PunRPC]
	void SatThuongMau(int SatThuong){
		MauHT -= SatThuong;
		HienThiMau.value = MauHT;
		if (MauHT <= 0) {
			if (ChoPhepXoaDT) {
				if (GetComponent<PhotonView> ().instantiationId == 0) {
					gameObject.SetActive (false);
				} else {
					if (GetComponent<PhotonView> ().isMine) {
						DiemHoiSinh.gameObject.GetComponent<PhotonView> ().RPC ("HoiSinhGo", PhotonTargets.All, ThoiGianSinh, gameObject.GetComponent<PhotonView>().viewID);
						gameObject.GetComponent<PhotonView> ().RPC ("HoiMau4", PhotonTargets.All, maxMau);
						PhotonView.Find(PlayerIfKill).GetComponent<PhotonView> ().RPC ("UpdateKillMark", PhotonTargets.All, null);
						gameObject.GetComponent<PhotonView> ().RPC ("UpdateKillMark", PhotonTargets.All, null);
						gameObject.GetComponent<PhotonView> ().RPC ("HideORShow", PhotonTargets.All, false);
					}
				}
			} else{
				RpcRespawn ();
		}
		}
				gameObject.GetComponent<PhotonView> ().RPC ("OnChamgeHealth", PhotonTargets.All, MauHT);
	}
	[PunRPC]
	void OnChamgeHealth(int health){
		HienThiMau.value = health;
	}
	[PunRPC]
	void GetDataPlayerkill(int PlayerKill){
		PlayerIfKill = PlayerKill;
	}
	[PunRPC]
	void HideORShow(bool Type){
		gameObject.SetActive (Type);
	}
	void RpcRespawn(){
		if (GetComponent<PhotonView> ().instantiationId == 0) {
			Destroy (gameObject);
			Destroy (gameObject.GetComponent<NetworkPlayer>().MyCamera);
		} else {
			if(GetComponent<PhotonView>().isMine){
				if(gameObject.tag == "Player"){
				Cf.MenuCamera.SetActive (true);
				Cf.AginGame.SetActive (true);
			}
			PhotonNetwork.Destroy (this.gameObject);
			PhotonNetwork.Destroy (this.gameObject.GetComponent<NetworkPlayer>().MyCamera);
				if(PlayerIfKill!=0){
			PhotonView.Find(PlayerIfKill).GetComponent<PhotonView> ().RPC ("UpdateKillMark", PhotonTargets.All, null);
				}
			}
		}
	}
	[PunRPC]
	public void HoiMau4(int SoMauHoi){
		MauHT += SoMauHoi;
		if(MauHT>maxMau){
			MauHT = maxMau;
		}
		HienThiMau.value = MauHT;
		gameObject.GetComponent<PhotonView> ().RPC ("OnChamgeHealth", PhotonTargets.All, MauHT);
	}
}
