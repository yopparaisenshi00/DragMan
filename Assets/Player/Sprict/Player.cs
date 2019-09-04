﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController chara_cont;
    public Transform chara_ray;
    private Vector3 velocity;
    public float run_spd = 15.0f;           //通常の速さ
    private Vector3 axis;
    private Vector3 input;                  //入力値
    public float jump_power = 15.0f;        //ジャンプ力
    public float gravity = 20.0f;           //重力
    private bool is_grabbed = false;        //掴む判定
    private float stop_fric = 0.1f;         //慣性(停止)
    private float grab_fric = 0;            //慣性(掴む)
    private float grab_fric_power = 0.8f;   //慣性(掴む)
    private float jump_fric = 0;            //慣性(ジャンプ)
    private float jump_fric_power = 0.7f;   //慣性(ジャンプ)
	private Enemy enemy;
	private Vector3 serve_input_normalized; //掴んでいる時に方向保存
	private bool is_freeze = false;         //硬直判定
	private float freeze_timer = 0;			//
	public float freeze_time = 10;			//硬直時間


	//--------------------------------------------------//
	// 出現させるエフェクト
	public GameObject effect;

    private int timer;          // エフェクトが出るまでの待機時間
    private bool on_effect;     // エフェクトを出す判定
    private bool apper_effect;  // エフェクトを出すときに実際にエフェクトが出る判定
    public float apper_time;    // エフェクトが出るまでの時間
    public int num;             // いっきに出すエフェクトの数

    // どこからどこまでの範囲内に出すか
    public float range_max_x;
    public float range_min_x;
    public float range_max_y;
    public float range_min_y;
    public float range_max_z;
    public float range_min_z;
    //-------------------------------------------------//



    // Start is called before the first frame update
    void Start()
    {
        chara_cont = GetComponent<CharacterController>();
        velocity = Vector3.zero;
        serve_input_normalized = Vector3.zero;
		

	}


	//離す
	public void Grab_false() {
		is_grabbed = false;
		is_freeze = true;	//硬直
	}

	//硬直
	void Freeze_Timer() {
		if (is_freeze) {
			freeze_timer += Time.deltaTime;
			if (freeze_timer >= freeze_time) {
				freeze_timer = 0;
				is_freeze = false;
			}
		}
	}



	// Update is called once per frame
	void Update()
    {
		//移動関連
		Move();
		//硬直
		Freeze_Timer();
		Debug.DrawLine(chara_ray.position, chara_ray.position - transform.up * 0.1f, Color.red);

        // エフェクト
        if (on_effect) Effect();
    }

	//移動関連
    void Move()
    {
		//硬直なら全て飛ばす
		if (is_freeze) return;

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
        if (!is_grabbed)
        {
            //今向いている方向を保存
            serve_input_normalized.x = input.x;
            serve_input_normalized.z = input.z;

            //操作している時
            if (input.magnitude > 0f)
            {
                // エフェクトを出す
                on_effect = true;
                //慣性を考慮した速さ
                velocity.x = input.x * (run_spd * jump_fric * grab_fric);
                velocity.z = input.z * (run_spd * jump_fric * grab_fric);
            }
            //操作してない時
            else
            {
                // エフェクトを出さない
                on_effect = false;  
                //停止時慣性
                velocity.x -= velocity.x * stop_fric;
                velocity.z -= velocity.z * stop_fric;
            }

            //向き変更
            transform.LookAt(transform.position + input);
        }
        //掴んでいる時
        else
        {
            // エフェクト出す
            on_effect = true;

            //自動で走る
            velocity = transform.forward * (run_spd * jump_fric * grab_fric);

            //操作してる時
            if (input.magnitude > 0f)
            {                
                //向き変更
                transform.eulerAngles += new Vector3(0, axis.x*2.0f, 0);
            }
        }
        // --------------------------------------------------------

        //ジャンプ
        if (chara_cont.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Jump"))
            {
                velocity.y = jump_power;
            }
        }
        //重力
        else
        {
            on_effect = false;  // エフェクトを出さない
            velocity.y -= gravity * Time.deltaTime;
        }
        //Debug.Log(velocity);
    }// Move()

    // エフェクト
    void Effect()
    {
        if (apper_effect && on_effect)
        {
            // いっきにnum個のeffectを出す
            for (int i = 0; i < num; ++i)
            {
                // 生成する物体、生成場所、回転軸の設定
                Instantiate(effect,
                    new Vector3(
                        transform.position.x + Random.Range(range_min_x, range_max_x),
                        transform.position.y + Random.Range(range_min_y, range_max_y),
                        transform.position.z + Random.Range(range_min_z, range_max_z)) +
                        (transform.forward * -1),
                    effect.transform.rotation);
            }
            apper_effect = false;
        }

        // エフェクトを出すまでの待機時間
        if (timer++ > apper_time && !apper_effect)
        {
            apper_effect = true;
            timer = 0;
        }
    }// Effect()


    void FixedUpdate()
    {
		//硬直なら全て飛ばす
		if (is_freeze) return;
		//キャラクターを移動させる処理
		chara_cont.Move(velocity * Time.deltaTime);
    }






    //何かに当たった時
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
		Grab_Atack(hit);    //敵に当たった時、掴んでいたら敵消去
		Grab(hit);			//掴む
        Landing();			//着地判定

	}

	//敵に当たった時掴んでいたら敵消去
	void Grab_Atack(ControllerColliderHit hit) {
		//敵に当たった時
		if (hit.gameObject.tag == "Enemy") {
			//掴んでいたら
			if (is_grabbed) {
				Destroy(hit.gameObject);
				//is_grabbed = false;
				FindObjectOfType<GameManagerScore>().Score_Add_Grab_Attack(); //スコア加算(Findは使わない方がいいかも)
			}
		}
	}

	//掴む
	void Grab(ControllerColliderHit hit)
    {
        //敵に当たっているとき
        if (hit.gameObject.tag == "Enemy")
        {
            //掴んでいなくて、キーを押したら
            if (Input.GetKeyDown(KeyCode.Z) || Input.GetButtonDown("Grab") && !is_grabbed)
            {
				//掴む
				enemy = hit.gameObject.GetComponent<Enemy>();
				enemy.Grab_Switch(); //掴まれた判定切り替え
				is_grabbed = true;
            }
        }
    }

	//着地判定
	void Landing()
    {
        //真下が当たっていたら
        if (Physics.Raycast(chara_ray.position, chara_ray.position - transform.up * 0.1f))
        {
            velocity.y = 0;
        }
    }


}