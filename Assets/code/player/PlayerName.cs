using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerName : Photon.MonoBehaviour {
	
	public Text NickName;

	void Awake () {
		if(photonView.isMine){
		gameObject.GetComponent<PhotonView>().RPC("UpNickName", PhotonTargets.AllBuffered, PhotonNetwork.playerName);
		}
	}
	[PunRPC]
	public void UpNickName(string NickNameNo){
		NickName.text = NickNameNo;
	}
}
