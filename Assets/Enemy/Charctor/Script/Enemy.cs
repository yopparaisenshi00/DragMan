﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	public bool is_grab = false;
	public GameObject target;
	private Player player;
	//Collider istri;
	public static int enemy_type;
	Collider ObjColl;


	// Start is called before the first frame update
	void Start() {
		player = target.GetComponent<Player>();
		ObjColl = GetComponent<Collider>();
	}

	// Update is called once per frame
	void Update() {
		//掴まれていたら
		if (is_grab) {
			this.transform.position = player.transform.position - 2 * player.transform.forward;
			this.gameObject.tag = "DragObj";
			ObjColl.isTrigger = true;
		}
		else if (!is_grab) {
			ObjColl.isTrigger = false;
		}
	}


	void Grab(ControllerColliderHit hit) {
		if (is_grab) {
			//敵に当たっているとき
			if (hit.gameObject.tag == "Enemy") {
				Destroy(hit.gameObject);
			}
		}
	}

}