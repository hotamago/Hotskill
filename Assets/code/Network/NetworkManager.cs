using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NetworkManager : MonoBehaviour {
	const string VERSION = "v0.1.5";
	public string roomName = "FPS2DHotamago";
	public int MaxPlayers = 50;
	public string playerPrefabName = "player";
	public Transform[] spawnPoint;

	public GameObject MenuCamera;
	public GameObject Loading;
	public Text TextData;

	public Text[] HienThiHoiChieus = new Text[5];
	public Button[] HienThiButtons = new Button[5];

	public Text HTNumberPlayer;
	public int NumberPlayer = 0;

	GameObject SpawnPlayer1;
	GameObject MyCamerat;

	public GameObject ChatInRoom;
	public GameObject AginGame;

	private float TimeLoad=5f;

	public Image KillMark;

	void Start () {
		if (PlayerPrefs.GetInt ("WHero")==0) {
			playerPrefabName = "player";
		} else if (PlayerPrefs.GetInt ("WHero")==1) {
			playerPrefabName = "Hero-Cung";
		}
		PhotonNetwork.ConnectUsingSettings (VERSION);
		if (PhotonNetwork.connected)
		{
			RoomOptions roomOptions = new RoomOptions (){isVisible = false, maxPlayers = (byte)MaxPlayers};
			PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, TypedLobby.Default);
		}else{
			PhotonNetwork.autoJoinLobby = true;
		}
		PhotonNetwork.automaticallySyncScene = true;
		NumberPlayer = 0;
		foreach(var player in PhotonNetwork.playerList){
			NumberPlayer++; 
		}
		HTNumberPlayer.text=NumberPlayer.ToString()+"/"+ MaxPlayers.ToString() +" player now";
	}
	void Update () {
		TimeLoad -= Time.deltaTime;
		if(TimeLoad<=0f){
			NumberPlayer = 0;
			foreach(var player in PhotonNetwork.playerList){
				NumberPlayer++; 
			}
			HTNumberPlayer.text=NumberPlayer.ToString()+"/"+ MaxPlayers.ToString() +" player now";
			TimeLoad = 5f;
		}
	}
	void OnGUI(){
		TextData.text = PhotonNetwork.connectionStateDetailed.ToString ();	
	}
	void OnJoinedLobby () {
		RoomOptions roomOptions = new RoomOptions (){isVisible = false, maxPlayers = (byte)MaxPlayers};
		PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, TypedLobby.Default);
	}
	void OnJoinedRoom(){
		MenuCamera.SetActive (false);
		Loading.SetActive (false);
		int RandomNuber = Random.Range (0, spawnPoint.Length);
		SpawnPlayer1 = (GameObject)PhotonNetwork.Instantiate (playerPrefabName, spawnPoint[RandomNuber].position, spawnPoint[RandomNuber].rotation, 0);
		MyCamerat = (GameObject)PhotonNetwork.Instantiate ("MainCamera", SpawnPlayer1.transform.position, SpawnPlayer1.transform.rotation, 0);
		SpawnPlayer1.GetComponent<MauPlayer>().NetworkMager = gameObject;
		SpawnPlayer1.GetComponent<NetworkPlayer>().MyCamera = MyCamerat;
		player XemXet = SpawnPlayer1.GetComponent<player> ();
		if (XemXet != null) {
			SpawnPlayer1.GetComponent<player>().HienThiHoiChieus = HienThiHoiChieus;
			SpawnPlayer1.GetComponent<player>().HienThiButtons = HienThiButtons;
		} else {
			SpawnPlayer1.GetComponent<ControlCung>().HienThiHoiChieus = HienThiHoiChieus;
			SpawnPlayer1.GetComponent<ControlCung>().HienThiButtons = HienThiButtons;
		}
		SpawnPlayer1.GetComponent<NetworkPlayer>().ChatBox = ChatInRoom;
		SpawnPlayer1.GetComponent<NetworkPlayer>().KillMark = KillMark;
	}
	public void RandomSpamwn(){
		AginGame.SetActive (false);
		MenuCamera.SetActive (false);
		int RandomNuber = Random.Range (0, spawnPoint.Length);
		SpawnPlayer1 = (GameObject)PhotonNetwork.Instantiate (playerPrefabName, spawnPoint[RandomNuber].position, spawnPoint[RandomNuber].rotation, 0);
		MyCamerat = (GameObject)PhotonNetwork.Instantiate ("MainCamera", SpawnPlayer1.transform.position, SpawnPlayer1.transform.rotation, 0);
		SpawnPlayer1.GetComponent<MauPlayer>().NetworkMager = gameObject;
		SpawnPlayer1.GetComponent<NetworkPlayer>().MyCamera = MyCamerat;
		player XemXet = SpawnPlayer1.GetComponent<player> ();
		if (XemXet != null) {
			SpawnPlayer1.GetComponent<player>().HienThiHoiChieus = HienThiHoiChieus;
			SpawnPlayer1.GetComponent<player>().HienThiButtons = HienThiButtons;
		} else {
			SpawnPlayer1.GetComponent<ControlCung>().HienThiHoiChieus = HienThiHoiChieus;
			SpawnPlayer1.GetComponent<ControlCung>().HienThiButtons = HienThiButtons;
		}
		SpawnPlayer1.GetComponent<NetworkPlayer>().ChatBox = ChatInRoom;
		SpawnPlayer1.GetComponent<NetworkPlayer>().KillMark = KillMark;
	}
}
