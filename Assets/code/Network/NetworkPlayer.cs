using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NetworkPlayer : Photon.MonoBehaviour {
	public GameObject MyCamera;
	private Vector3 pos;
	private bool HienChatBox=false;
	public GameObject ChatBox;
	private GameObject ShowText;
	Vector3 VitriHienText;
	float TimeTuHuy;
	string TextChat;

	public Image KillMark;

	public Sprite[] KillMarkS;
	int SoKill=0;
	float TimeQuayLai;
//	Animator anim;
//	Vector3 realPosition = Vector3.zero;
//	Quaternion realRotation = Quaternion.identity;
//	float SpeeTimeLast = 4f;

	void Start () {
//		anim = GetComponent<Animator>();
		if(photonView.isMine){
			MyCamera.SetActive(true);
			player XemXet = gameObject.GetComponent<player> ();
			if (XemXet != null) {
				GetComponent<player> ().enabled = true;
				GetComponent<player> ().playermau.GetComponent<Renderer> ().material.color = Color.blue;
			} else {
				GetComponent<ControlCung>().enabled = true;
				GetComponent<ControlCung>().playermau.GetComponent<Renderer>().material.color = Color.blue;
			}
		}
	}
	void Update () {
		if(TimeTuHuy>0){
		TimeTuHuy -= Time.deltaTime;
		} 
		if(TimeTuHuy<=0 && ShowText!=null){
			PhotonNetwork.Destroy (ShowText);
		}
		if(TimeQuayLai>0){
			TimeQuayLai -= Time.deltaTime;
		}
		if(TimeQuayLai<=0 && SoKill>0){
			SoKill = 0;
			KillMark.gameObject.SetActive (false);
		}
		if (Input.GetKeyDown (KeyCode.Return)) {
			if (HienChatBox) {
				HienChatBox = false;
				gameObject.GetComponent<player> ().IsChatBox = HienChatBox;
				TextChat = ChatBox.GetComponent<InputField> ().text;
				ChatBox.GetComponent<InputField> ().text="";
				if(TextChat.Length>30){TextChat = "Long text :( (max 30)";}
				ChatBox.SetActive(false);
				VitriHienText = transform.position;
				VitriHienText.y += 0.9f;
				VitriHienText.z = -1f;
				if(ShowText!=null){PhotonNetwork.Destroy (ShowText);}
				TimeTuHuy = 5f;
				ShowText = (GameObject)PhotonNetwork.Instantiate ("TextGame", VitriHienText, Quaternion.Euler(0,0,0), 0);
				ShowText.GetComponent<PhotonView>().RPC ("UpText", PhotonTargets.AllBuffered, TextChat);
			} else {
				HienChatBox = true;
				ChatBox.SetActive(true);
				ChatBox.GetComponent<InputField> ().Select();
				ChatBox.GetComponent<InputField> ().ActivateInputField();
				gameObject.GetComponent<player> ().IsChatBox = HienChatBox;
			}
		}
		if (photonView.isMine) {
			pos = transform.position;
			pos.z = -10f;
			MyCamera.transform.position = pos;
		} 
		else {
//			transform.position = Vector3.Lerp(transform.position ,realPosition, SpeeTimeLast);
//			transform.rotation = Quaternion.Lerp(transform.rotation, realRotation, SpeeTimeLast);
		}
	}
//	public void OnPhotonSerialireView(PhotonStream stream, PhotonMessageInfo info){
//		if(stream.isWriting){
//			stream.SendNext(transform.position);
//			stream.SendNext(transform.rotation);
//			stream.SendNext(anim.GetBool("tancong1"));
//		}
//		else{
//			realPosition = (Vector3)stream.ReceiveNext();
//			realRotation = (Quaternion)stream.ReceiveNext();
//			anim.SetBool("tancong1", (bool)stream.ReceiveNext());
//		}
//	}
	[PunRPC]
	public void UpdateKillMark(){
		if (GetComponent<PhotonView> ().isMine) {
			TimeQuayLai = 30f;
			if (SoKill <= 7) {
				SoKill++;
			}
			KillMark.sprite = KillMarkS [SoKill - 1];
			KillMark.gameObject.SetActive (true);

		}
	}
}
