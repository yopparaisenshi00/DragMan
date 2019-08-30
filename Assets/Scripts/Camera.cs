using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
	public GameObject player;
	public float followSpd = 3.0f;  //カメラ追従の速さ
	public float rotateSpd = 4.0f;  //カメラ回転の速さ

	// Use this for initialization
	void Start() {
		this.transform.position = player.transform.position;
	}

	// Update is called once per frame
	void Update() {

	}

	void FixedUpdate() {
		//カメラ追従
		//this.transform.position = player.transform.position;
		this.transform.position = Vector3.Lerp(this.transform.position, player.transform.position, followSpd * Time.deltaTime);

		//マウスでカメラの向き変更
		//rotateCameraAngle();

	}

	//マウスでカメラの向き変更
	void rotateCameraAngle() {
		//マウスの右ボタンが押されている時
		if (Input.GetMouseButton(1)) {
			Vector3 angle = new Vector3(
				Input.GetAxis("Mouse X") * rotateSpd,
				0,
				0
			);
			transform.eulerAngles += new Vector3(angle.y, angle.x);
		}
	}
}
