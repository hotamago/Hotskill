using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CanChien : MonoBehaviour {
	public GameObject vitriplayer;
	private Vector3 newpos;
	public float TocDo = 2f;
	private Vector3 pos;
	private Vector3 pos1;
	Collider2D m_Collider;

	public float ThoiGianBamDuoi = 15f;
	private float timeWaitPassed;
	private int ChoPhepTime = 0;
	public int ChoPhepBamDuoi = 0;

	//----- Khai bao tan cong cua zombie ------

	private float ThoiGianTT;
	public float ThoiGianTCMD;
	private int TrangThaiTC = 0;
	public int ChiSoTC;

    public float TimeDiLucTung = 8f;
	public float TimeDiLucTung2 = 4f;

	private NavMeshAgent theagent;

    void Start() {
		timeWaitPassed = ThoiGianBamDuoi;
		m_Collider = GetComponent<Collider2D>();
		theagent = GetComponent<NavMeshAgent> ();
	}
    void Update() {
        if (ChoPhepTime == 1)
        {
            timeWaitPassed -= Time.deltaTime;
            if (timeWaitPassed <= 0f)
            {
                timeWaitPassed = ThoiGianBamDuoi;
                ChoPhepTime = 0;
                ChoPhepBamDuoi = 0;
            }
        }
		else if (vitriplayer==null) {
            TimeDiLucTung -= Time.deltaTime;
			if(TimeDiLucTung<=0f){
				Quaternion rotation = transform.rotation;
				TimeDiLucTung2 = 4f;
				TimeDiLucTung = 8f;
				transform.rotation = rotation;
				transform.Rotate(Vector3.forward, Random.Range(0, 360));
			}
        }
		if (TimeDiLucTung2 > 0f && vitriplayer==null) {
			TimeDiLucTung2 -= Time.deltaTime;
			transform.Translate (Vector3.up * TocDo * Time.deltaTime);
		}
        //-------------------
        if (vitriplayer!=null){
			pos = transform.position;
			pos1 = vitriplayer.transform.position;
			Vector3 dir = pos1 - pos;
			float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.AngleAxis(angle -90, Vector3.forward);
			transform.Translate (Vector3.up * TocDo * Time.deltaTime);
		}
		if(ChoPhepBamDuoi==1){
			Vector3 dir = pos1 - pos;
			float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.AngleAxis(angle -90, Vector3.forward);
			transform.Translate (Vector3.up * TocDo * Time.deltaTime);
			ChoPhepTime = 1;
		}

		if(TrangThaiTC == 1){
			ThoiGianTT -= Time.deltaTime;   //increase time
			if (ThoiGianTT <= 0f)
			{
				ThoiGianTT = ThoiGianTCMD;
				vitriplayer.GetComponent<PhotonView>().RPC("SatThuongMau", PhotonTargets.All, ChiSoTC);
				vitriplayer.GetComponent<PhotonView> ().RPC ("GetDataPlayerkill", PhotonTargets.All, 0);
			}
		}
	}
	void OnCollisionEnter2D(Collision2D col1) {
		if(col1.gameObject.tag == "Player"){
			vitriplayer = col1.gameObject;
			TrangThaiTC = 1;
				ThoiGianTT = 0;		
		}
		if(col1.gameObject.tag == "LocBanXa"){
	
		}
	}
	void OnCollisionExit2D(Collision2D col1) 
	{
		if(col1.gameObject.tag == "Player"){
			TrangThaiTC = 0;
		}
	}
	void OnTriggerEnter2D(Collider2D col2) {
		if(col2.gameObject.tag == "Player"){
			vitriplayer = col2.gameObject;
		}
	}

	void OnTriggerExit2D(Collider2D col2) {
		if(col2.gameObject.tag == "Player"){
			vitriplayer = null;
		}
	}
	[PunRPC]
	public void UpdateKillMark(){
		m_Collider.enabled = false;
		m_Collider.enabled = true;
	}
}
