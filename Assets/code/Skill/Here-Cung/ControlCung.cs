using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class ControlCung : MonoBehaviour {
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
	// Danh thuong
	private int LanDanhThu=0;
	public float timeskill3 = 0f;
	// Hoi chieu
	private double[] HoiChieus = new double[5];
	public Text[] HienThiHoiChieus = new Text[5];
	public Button[] HienThiButtons = new Button[5];
	// LocBanXa
	public Transform BanCung;
	//
	public GameObject HieuUngHoiMau;
	float TimeTamThoi = 1f;

	public bool IsChatBox;

	void Start(){
		loaiskill = 0;
		chophepskill = false;
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
						HoiChieus [0] = 1f;
						TimeMacDinh = 0.7f;
					SatThuongskill1 = 10;
					loaiskill = 0;
					Vector3 rotationVector = BanCung.transform.rotation.eulerAngles;
					rotationVector.z += -45;
					GameObject SpawnCungTen = (GameObject)PhotonNetwork.Instantiate ("CungThuong", BanCung.position, BanCung.rotation, 0);
					SpawnCungTen.GetComponent<CungThuong>().PlayerBan = this.gameObject;
					HienThiButtons [loaiskill].GetComponent<Button>().image.color = Color.green;
					TimeChay = TimeMacDinh;
				}
			}
			if(Input.GetKey(KeyCode.Mouse0) && chophepskill==true && skilling==false){
				switch(loaiskill){
				case 1:
					skilling = true;
					HoiChieus[1]=3f;
					chophepskill = false;
					TimeChay = TimeMacDinh;
					GameObject SpawnCungTen = (GameObject)PhotonNetwork.Instantiate ("CungNo", BanCung.position, BanCung.rotation, 0);
					SpawnCungTen.GetComponent<CungNo>().PlayerBan = gameObject;
					break;
				case 3:
					skilling = true;
					HoiChieus[3]=50f;
					chophepskill = false;
					TimeChay = TimeMacDinh;
					GameObject SpawnCungTen1 = (GameObject)PhotonNetwork.Instantiate ("CungXuyenMap", BanCung.position, BanCung.rotation, 0);
					SpawnCungTen1.GetComponent<CungThuong>().PlayerBan = gameObject;
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
					HienThiButtons [loaiskill].GetComponent<Button>().image.color = Color.green;
				}
			}
			if (Input.GetKey(KeyCode.Alpha2) && TimeChay<=0  && skilling==false)
			{
				if(HoiChieus[2]<=0){
					TimeTamThoi = 0.2f;
					HienThiButtons [loaiskill].GetComponent<Button>().image.color = Color.white;
					loaiskill = 2;
					HienThiButtons [loaiskill].GetComponent<Button>().image.color = Color.green;
					HoiChieus[2]=15f;
					TimeMacDinh = 25f;
					TimeChay = TimeMacDinh;
					skilling = true;
				}
			}
			if (Input.GetKey(KeyCode.Alpha3) && TimeChay<=0  && skilling==false)
			{
				if(HoiChieus[3]<=0){
					HienThiButtons [loaiskill].GetComponent<Button>().image.color = Color.white;
					TimeMacDinh = 0.6f;
					loaiskill = 3;
					chophepskill = true;
					SatThuongskill1 = 50;
					HienThiButtons [loaiskill].GetComponent<Button>().image.color = Color.green;
				}
			}
			if (Input.GetKey(KeyCode.F) && TimeChay<=0  && skilling==false)
			{
				if(HoiChieus[4]<=0){
					TimeTamThoi = 1f;
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
		if (timeskill3 > 0) {
			timeskill3 -= Time.deltaTime;
			if (timeskill3 <= 0) {
				HienThiButtons [2].GetComponent<Button>().image.color = Color.white;
			}
		}
		// tinh time
		if(TimeChay>0){
			switch(loaiskill){
			case 0:
				TimeChay -= Time.deltaTime;
				if (TimeChay <= 0) {
					HienThiButtons [loaiskill].GetComponent<Button>().image.color = Color.white;
				}
				break;
			case 1:
				TimeChay -= Time.deltaTime;
				if (TimeChay <= 0) {
					skilling = false;
					HienThiButtons [loaiskill].GetComponent<Button>().image.color = Color.white;
				}
				break;
			case 2:
				TimeTamThoi -= Time.deltaTime;
				if(TimeTamThoi<=0f){
					TimeChay--;
					SatThuongskill1 = 10;
					Vector3 rotationVector = BanCung.transform.rotation.eulerAngles;
					rotationVector.z += -45;
					GameObject SpawnCungTen = (GameObject)PhotonNetwork.Instantiate ("CungThuong", BanCung.position, BanCung.rotation, 0);
					SpawnCungTen.GetComponent<CungThuong>().PlayerBan = this.gameObject;
					TimeTamThoi = 0.2f;
				}
				if (TimeChay <= 0) {
					skilling = false;
					HienThiButtons [2].GetComponent<Button>().image.color = Color.white;
				}
				break;
			case 3:
				TimeChay -= Time.deltaTime;
				if (TimeChay <= 0) {
					skilling = false;
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