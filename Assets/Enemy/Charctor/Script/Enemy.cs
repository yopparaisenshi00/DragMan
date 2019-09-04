﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	public bool is_grab = false;			//持たれている判定
	public GameObject target;
	private Player player;
	public static int enemy_type;
	private Collider obj_collider;
	public float grab_die_time;            //掴まれた後、消えるまでの時間
	private float timer = 0;


	// Start is called before the first frame update
	void Start() {
		player = target.GetComponent<Player>();
		obj_collider = GetComponent<Collider>();
	}

	// Update is called once per frame
	void Update() {
		//あとで関数分けする!!

		//掴まれていたら
		if (is_grab) {
			this.gameObject.tag = "DragObj";
			obj_collider.isTrigger = true;

			//位置固定
			this.transform.position = player.transform.position + (player.transform.right + player.transform.up);
			//一定時間経ったら消去
			DestoryTimer();
		}
		//掴まれていなかったら
		else if (!is_grab) {
			obj_collider.isTrigger = false;
		}
	}


	//掴まれた判定切り替え
	public void Grab_Switch() {
		is_grab = !is_grab;
	}


	//一定時間経ったら消去
	void DestoryTimer() {
		timer += Time.deltaTime;
		if (timer >= grab_die_time) {
			timer = 0;
			Destroy(this);
			player.Grab_false(); //プレイヤー離す
			FindObjectOfType<Score>().Score_Add_Grab(); //スコア加算(Findは使わない方がいいかも)
		}	
	}

}