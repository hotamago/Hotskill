using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class ComeMenu : Photon.MonoBehaviour {
	public bool Menu2;
	public InputField Username;
	public GameObject MiniMap;
	private bool IsOpenMap = false;
	public GameObject ChonMayChu;
	public GameObject SeverLuaChon;
	public GameObject BangChonTuong;

	public string[] AppIDSever = new string[3];
	
	void Start () {
		if(Menu2==false){
		Username.text=PlayerPrefs.GetString("Username");
		}
	}
	void Update () {
		if (Input.GetKeyDown (KeyCode.Q)) {
			if (IsOpenMap) {
				IsOpenMap = false;
				Close ();
			} else {
				IsOpenMap = true;
				OpenMap ();
			}
		}
	}
	public void Playgame(){
		PlayerPrefs.SetString("Username", Username.text);
		PlayerPrefs.Save();
		PhotonNetwork.playerName = PlayerPrefs.GetString("Username");
		BangChonTuong.SetActive (true);
	}
	public void BackMenu(){
		PhotonNetwork.LeaveRoom ();
		SceneManager.LoadScene ("MenuGame");
	}
	public void OpenMap(){
		if (Menu2) {
			MiniMap.SetActive (true);
		}
	}
	public void Close(){
		if (Menu2) {
			MiniMap.SetActive (false);
		}
	}
	public void ChonTuong(int loai){
			PlayerPrefs.SetInt("WHero", loai);
			PlayerPrefs.Save();
		BangChonTuong.SetActive (false);
		ChonMayChu.SetActive (true);
	}
	public void JoinGame(){
		PhotonNetwork.Disconnect();
		ChooseSomeThing(0,SeverLuaChon.GetComponent<Dropdown> ().value);
		SceneManager.LoadScene ("demo");
	}
	public void ChooseSomeThing(int type, int value){
		if(type==0){
			PhotonNetwork.PhotonServerSettings.AppID = AppIDSever[value];
		}
	}
}
