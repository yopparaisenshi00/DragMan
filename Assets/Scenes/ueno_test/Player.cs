﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	private CharacterController chara_cont;
	public  Transform	 chara_ray;
	private Vector3		 velocity;
	public float		 run_spd		 = 15.0f;   //通常の速さ
	private Vector3		 axis;
	private Vector3		 input;						//入力値
	public float		 jump_power		 = 15.0f;   //ジャンプ力
	public float		 gravity		 = 20.0f;	//重力
	private bool		 is_grabbed		 = false;   //掴む判定
	private float		 stop_fric		 = 0.1f;    //慣性(停止)
	private float		 grab_fric		 = 0;		//慣性(掴む)
	private float		 grab_fric_power = 0.8f;    //慣性(掴む)
	private float		 jump_fric		 = 0;       //慣性(ジャンプ)
	private float		 jump_fric_power = 0.7f;    //慣性(ジャンプ)
	public  GameObject	 target;
	private Enemy		 enemy;
	private Vector3		 serve_input_normalized;    //掴んでいる時に保存

	// Start is called before the first frame update
	void Start() {
		chara_cont = GetComponent<CharacterController>();
		velocity = Vector3.zero;
		enemy = target.GetComponent<Enemy>();
		serve_input_normalized = Vector3.zero;
	}




	// Update is called once per frame
	void Update() {
		Move();
		Debug.DrawLine(chara_ray.position, chara_ray.position - transform.up * 0.1f, Color.red);
	}


	void Move() {
		//地面に着いている時の慣性
		if (chara_cont.isGrounded) jump_fric = 1;
		else jump_fric = jump_fric_power;
		//掴んでいる時の慣性
		if (is_grabbed) grab_fric = grab_fric_power;
		else grab_fric = 1;


		//移動
		axis.x = Input.GetAxis("Horizontal");
		axis.z = Input.GetAxis("Vertical");
		input = new Vector3(axis.x, 0f, axis.z);

		//掴んでいない時
		if (!is_grabbed) {
			//今向いている方向を保存
			serve_input_normalized.x = input.x;
			serve_input_normalized.z = input.z;
			//操作している時
			if (input.magnitude > 0f) {
				//慣性を考慮した速さ
				velocity.x = input.x * (run_spd * jump_fric * grab_fric);
				velocity.z = input.z * (run_spd * jump_fric * grab_fric);
			}
			//操作してない時
			else {
				//停止時慣性
				velocity.x -= velocity.x * stop_fric;
				velocity.z -= velocity.z * stop_fric;
			}
			//向き変更
			transform.LookAt(transform.position + input);
		}
		//掴んでいる時
		else {
			//自動で走る
			velocity = transform.forward * (run_spd * jump_fric * grab_fric);
			//操作してる時
			if (input.magnitude > 0f) {
				//向き変更
				transform.eulerAngles += new Vector3(0, axis.x*2.0f, 0);
			}
		}
		// --------------------------------------------------------

		//ジャンプ
		if (chara_cont.isGrounded) {
			if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Jump")) {
				velocity.y = jump_power;
			}
		}
		//重力
		else velocity.y -= gravity * Time.deltaTime;

		//Debug.Log(velocity);

	}

	void FixedUpdate() {
		//キャラクターを移動させる処理
		chara_cont.Move(velocity * Time.deltaTime);
	}







	//何かに当たった時
	void OnControllerColliderHit(ControllerColliderHit hit) {
		Grab(hit);  //掴む
		Landing();  //着地判定
	}

	//掴む
	void Grab(ControllerColliderHit hit) {
		//敵に当たっているとき
		if (hit.gameObject.tag == "Enemy") {
			//掴んでいなくて、キーを押したら
			if (Input.GetKeyDown(KeyCode.Z) || Input.GetButtonDown("Grab")
				&& !is_grabbed) {
				//掴む
				enemy.is_grab = true;
				is_grabbed = true;
			}
		}
	}

	//着地判定
	void Landing() {
		//真下が当たっていたら
		if (Physics.Raycast(chara_ray.position, chara_ray.position - transform.up * 0.1f)) {
			velocity.y = 0;
		}
	}


}