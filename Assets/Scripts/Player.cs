using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	private CharacterController chara_cont;
	private Vector3		velocity;
	private float		run_spd			 = 10.0f;   //通常の速さ
	private Vector3		axis;
	private Vector3		input;						//入力値
	private float		jump_power		 = 15.0f;	//ジャンプ力
	private float		gravity			 = 20.0f;	//重力
	private bool		is_grabbed		 = false;   //掴む判定
	private float		fric			 = 0;		//慣性
	private float		fric_power		 = 0.7f;	//慣性
	private float		jump_fric		 = 0;		//ジャンプ時慣性
	private float		jump_fric_power	 = 0.7f;    //ジャンプ時慣性
	public  GameObject	target;
	private	Enemy		enemy;

	// Start is called before the first frame update
	void Start()
    {
		chara_cont = GetComponent<CharacterController>();
		velocity = Vector3.zero;
		enemy = target.GetComponent<Enemy>();
	}




	// Update is called once per frame
	void Update(){
		Move();
	}


	void Move() {
		//if (chara_cont.isGrounded) {
		//	velocity = Vector3.zero;
		//}

		//移動
		axis.x = Input.GetAxis("Horizontal");
		axis.z = Input.GetAxis("Vertical");
		input = new Vector3(axis.x, 0f, axis.z);

		transform.LookAt(transform.position + input);

		if (input.magnitude > 0f) {
			//地面に着いている時
			if (chara_cont.isGrounded)	jump_fric = 1;
			else						jump_fric = jump_fric_power;
			//掴んでいない時
			if (!is_grabbed)	fric = 1;
			else				fric = fric_power;

			//慣性を考慮した速さ
			velocity.x = input.normalized.x * (run_spd * jump_fric * fric);
			velocity.z = input.normalized.z * (run_spd * jump_fric * fric);
		}
		else {
			velocity.x -= velocity.x * fric;
			velocity.z -= velocity.z * fric;
		}

		//ジャンプ
		if (chara_cont.isGrounded) {
			if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Jump")) {
				velocity.y += jump_power;
			}
		}
		//重力
		velocity.y -= gravity * Time.deltaTime;

		Debug.Log(velocity);
	}


	void FixedUpdate() {
		//キャラクターを移動させる処理
		chara_cont.Move(velocity * Time.deltaTime);
	}







	//何かに当たった時
	void OnControllerColliderHit(ControllerColliderHit hit) {
		Grab(hit);	//掴む
		Landing();	//着地判定
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
		if (Physics.Raycast(transform.position, transform.position - transform.up * 1.5f)) {
			velocity.y = 0;
		}
	}


}
