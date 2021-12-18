using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiemHoiSinh : MonoBehaviour {
	public string Enemy;
	public float ThoiGianSinh = 5f;
	public float timeWaitPassed;
	public int sozombie = 100;
	public bool OkIntEnemy = true;
	public bool ChoPhepHoisinh = false;
	private int EnemyJun;

	// Use this for initialization
	void Start () {
		timeWaitPassed = ThoiGianSinh;
	}

	// Update is called once per frame
	void Update () {
		if(sozombie!=0 || OkIntEnemy==false){
			if (ChoPhepHoisinh == true) {
				timeWaitPassed -= Time.deltaTime; 
			}
		}
		if (timeWaitPassed <= 0f && sozombie != 0 && OkIntEnemy==true) {
			timeWaitPassed = ThoiGianSinh;
			sozombie--;
			PhotonView.Find (EnemyJun).gameObject.SetActive (true);
			PhotonView.Find (EnemyJun).gameObject.transform.position = gameObject.transform.position;
			PhotonView.Find (EnemyJun).gameObject.transform.rotation = gameObject.transform.rotation;
			ChoPhepHoisinh = false;
		} else if(timeWaitPassed <= 0f && OkIntEnemy==false){
			ChoPhepHoisinh = false;
			timeWaitPassed = ThoiGianSinh;
			PhotonView.Find (EnemyJun).gameObject.SetActive (true);
			PhotonView.Find (EnemyJun).gameObject.transform.position = gameObject.transform.position;
			PhotonView.Find (EnemyJun).gameObject.transform.rotation = gameObject.transform.rotation;
		}
	}
	[PunRPC]
	void HoiSinhGo(float timeHS, int GameOfit){
		ThoiGianSinh=timeHS;
		timeWaitPassed = ThoiGianSinh;
		ChoPhepHoisinh = true;
		EnemyJun = GameOfit;
	}
}
