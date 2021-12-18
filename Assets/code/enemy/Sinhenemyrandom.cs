using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sinhenemyrandom : MonoBehaviour {
	public string Enemy;
	public float ThoiGianSinh = 5f;
	public float timeWaitPassed;
	public int sozombie = 20;

	public float PosX = 43f;
	public float PosY = 43f;

	public List<int> values = new List<int>();

	// Use this for initialization
	void Start () {
		timeWaitPassed = ThoiGianSinh;
	}
	void Update () {
			if (sozombie != 0) {
				timeWaitPassed -= Time.deltaTime; 
			}
			if (timeWaitPassed <= 0f && sozombie != 0) {
				timeWaitPassed = ThoiGianSinh;
				sozombie--;
			foreach (int value in values)
			{
				PhotonView.Find (value).gameObject.SetActive (true);
				PhotonView.Find (value).gameObject.transform.position = new Vector3 (Random.Range (-43f, PosX), Random.Range (-43f, PosY), 0f);
				PhotonView.Find (value).gameObject.transform.rotation = gameObject.transform.rotation;
				for(int i = 0; i < values.Count; i++){
					if(values[i]==value){
						values.RemoveAt(i);
					}
				}
			}

			}
	}
	[PunRPC]
	void HoiSinhGo(float timeHS, int GameOfit){
		ThoiGianSinh=timeHS;
		if(sozombie==0){
			timeWaitPassed = ThoiGianSinh;
		}
		sozombie++;
		values.Add(GameOfit);
	}
}
