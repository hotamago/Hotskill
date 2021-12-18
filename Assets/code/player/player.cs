using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class player : MonoBehaviour {
	public GameObject playermau;
	private Vector3 mpos;
	private Vector3 lpos;
	public float TocDo = 4f;
	//khoa chuot
	private bool lockmouse = false;
	//time hoi skill
	public int SatThuongskill1;
	private float TimeChay = 0f;
	public float TimeMacDinh = 0f;
	//ma skill
	private bool skilling=false;
	private int loaiskill;
	private bool chophepskill;
	Animator myAnim;
	// Danh thuong
	private int LanDanhThu=0;
	// Hoi chieu
	private double[] HoiChieus = new double[5];
	public Text[] HienThiHoiChieus = new Text[5];
	public Button[] HienThiButtons = new Button[5];
	// LocBanXa
	public Transform LocBanXa;
	//
	public GameObject PhanDanhDinhST;
	public GameObject HieuUngHoiMau;
	float TimeTamThoi = 1f;

	public bool IsChatBox;

	void Start(){
		loaiskill = 0;
		chophepskill = false;
		myAnim = GetComponent<Animator>();
		PhanDanhDinhST.GetComponent<DanhThuong>().PlayerBan = gameObject;
	}
	void FixedUpdate () {
		//cotronl player 2D
		if(lockmouse==false){
		mpos=Input.mousePosition;
		lpos=Camera.main.ScreenToWorldPoint (mpos);
		Quaternion rot = Quaternion.LookRotation(transform.position - lpos, Vector3.forward );
		transform.rotation = rot;  
		transform.eulerAngles = new Vector3(0, 0,transform.eulerAngles.z);
		}
		//attck player 2D
		if(IsChatBox==false){
		if(Input.GetKey(KeyCode.Mouse0) && chophepskill==false && skilling==false){
			if (HoiChieus [0] <= 0) {
				HienThiButtons [loaiskill].GetComponent<Button>().image.color = Color.white;
				PhanDanhDinhST.SetActive (true);
				HoiChieus [0] = 0.8f;
				TimeMacDinh = 0.7f;
				SatThuongskill1 = 15;
				PhanDanhDinhST.GetComponent<DanhThuong>().SatThuong = SatThuongskill1;
				loaiskill = 0;
				HienThiButtons [loaiskill].GetComponent<Button>().image.color = Color.green;
				if (LanDanhThu == 0) {
					LanDanhThu = 1;
				} else if (LanDanhThu == 1) {
					LanDanhThu = 2;
				} else if (LanDanhThu == 2) {
					LanDanhThu = 1;
				}
				TimeChay = TimeMacDinh;
				switch (LanDanhThu) {
				case 1:
					myAnim.SetBool ("tancong1", true);
					break;
				case 2:
					myAnim.SetBool ("danhthuong", true);
					break;
				}
			}
		}
		if(Input.GetKey(KeyCode.Mouse0) && chophepskill==true && skilling==false){
			myAnim.SetBool ("tancong1", false);
			myAnim.SetBool ("danhthuong", false);
			switch(loaiskill){
			case 1:
				PhanDanhDinhST.SetActive (true);
				skilling = true;
				HoiChieus[1]=3f;
				chophepskill = false;
				TimeChay = TimeMacDinh;
				myAnim.SetBool ("tancong1", true);
				lockmouse = true;
				break;
			case 2:
				skilling = true;
				HoiChieus[2]=5f;
				chophepskill = false;
				TimeChay = TimeMacDinh;
				myAnim.SetBool ("tancong1", true);
				GameObject SpawnLuoiGio = (GameObject)PhotonNetwork.Instantiate ("LuotChem", LocBanXa.position, LocBanXa.rotation, 0);
				SpawnLuoiGio.GetComponent<LocBanXa>().PlayerBan = gameObject;
				break;
			case 3:
				skilling = true;
				HoiChieus[3]=10f;
				chophepskill = false;
				TimeChay = TimeMacDinh;
				myAnim.SetBool ("tancong1", true);
				PhotonNetwork.Instantiate ("TuongGio", LocBanXa.position, LocBanXa.rotation, 0);
				break;
			}
		}
		if (Input.GetKey(KeyCode.Alpha1) && TimeChay<=0  && skilling==false)
		{
			if(HoiChieus[1]<=0){
			HienThiButtons [loaiskill].GetComponent<Button>().image.color = Color.white;
			TimeMacDinh = 0.5f;
			loaiskill = 1;
			chophepskill = true;
			SatThuongskill1 = 20;
			PhanDanhDinhST.GetComponent<DanhThuong>().SatThuong = SatThuongskill1;
			HienThiButtons [loaiskill].GetComponent<Button>().image.color = Color.green;
			}
		}
		if (Input.GetKey(KeyCode.Alpha2) && TimeChay<=0  && skilling==false)
		{
			if(HoiChieus[2]<=0){
				HienThiButtons [loaiskill].GetComponent<Button>().image.color = Color.white;
				TimeMacDinh = 0.6f;
				loaiskill = 2;
				chophepskill = true;
				SatThuongskill1 = 0;
				HienThiButtons [loaiskill].GetComponent<Button>().image.color = Color.green;
			}
		}
		if (Input.GetKey(KeyCode.Alpha3) && TimeChay<=0  && skilling==false)
		{
			if(HoiChieus[3]<=0){
				HienThiButtons [loaiskill].GetComponent<Button>().image.color = Color.white;
				TimeMacDinh = 0.6f;
				loaiskill = 3;
				chophepskill = true;
				SatThuongskill1 = 0;
				HienThiButtons [loaiskill].GetComponent<Button>().image.color = Color.green;
			}
		}
		if (Input.GetKey(KeyCode.F) && TimeChay<=0  && skilling==false)
		{
			if(HoiChieus[4]<=0){
				HienThiButtons [loaiskill].GetComponent<Button>().image.color = Color.white;
				TimeMacDinh = 5f;
				loaiskill = 4;
				SatThuongskill1 = 0;
				skilling = true;
				HoiChieus [4] = 30f;
				TimeChay = TimeMacDinh;
				HieuUngHoiMau.SetActive (true);
				HienThiButtons [loaiskill].GetComponent<Button>().image.color = Color.green;
			}
		}
		//move player 2D
		if (Input.GetKey(KeyCode.A))
		{
			transform.position += Vector3.left * TocDo * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.D))
		{
			transform.position += Vector3.right * TocDo * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.W))
		{
			transform.position += Vector3.up * TocDo * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.S))
		{
			transform.position += Vector3.down * TocDo * Time.deltaTime;
		}
	}
		// tinh time
		if(TimeChay>0){
			switch(loaiskill){
			case 0:
				TimeChay -= Time.deltaTime;
				if (TimeChay <= 0) {
					PhanDanhDinhST.SetActive (false);
					myAnim.SetBool ("tancong1", false);
					myAnim.SetBool ("danhthuong", false);
					HienThiButtons [loaiskill].GetComponent<Button>().image.color = Color.white;
				}
				break;
			case 1:
				transform.Translate (Vector3.up * 15f * Time.deltaTime);
				TimeChay -= Time.deltaTime;
				if (TimeChay <= 0) {
					PhanDanhDinhST.SetActive (false);
					skilling = false;
					myAnim.SetBool ("tancong1", false);
					lockmouse = false;
					HienThiButtons [loaiskill].GetComponent<Button>().image.color = Color.white;
				}
				break;
			case 2:
				TimeChay -= Time.deltaTime;
				if (TimeChay <= 0) {
					skilling = false;
					myAnim.SetBool ("tancong1", false);
					HienThiButtons [loaiskill].GetComponent<Button>().image.color = Color.white;
				}
				break;
			case 3:
				TimeChay -= Time.deltaTime;
				if (TimeChay <= 0) {
					skilling = false;
					myAnim.SetBool ("tancong1", false);
					HienThiButtons [loaiskill].GetComponent<Button>().image.color = Color.white;
				}
				break;
			case 4:
				TimeTamThoi -= Time.deltaTime;
				if(TimeTamThoi<=0f){
					TimeChay--;
					gameObject.GetComponent<MauPlayer> ().HoiMau4 (8);
					TimeTamThoi = 1f;
				}
				if (TimeChay <= 0) {
					HieuUngHoiMau.SetActive (false);
					skilling = false;
					HienThiButtons [loaiskill].GetComponent<Button>().image.color = Color.white;
				}
				break;
			}
		}
		//NapHoiChieu
		for(int i=0; i<=HoiChieus.Length-1; i++){
			if(HoiChieus[i]>0f){
				HoiChieus[i] -= Time.deltaTime;
				HienThiHoiChieus [i].text = HoiChieus[i].ToString();
			}
			if(HoiChieus[i]<=0f){
				HoiChieus [i] = 0f;
				HienThiHoiChieus [i].text = HoiChieus[i].ToString();
			}
		}
	}

}
