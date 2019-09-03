﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	public bool is_grab = false;
	public GameObject target;
	private Player player;

	// Start is called before the first frame update
	void Start() {
		player = target.GetComponent<Player>();
	}

	// Update is called once per frame
	void Update() {
		//掴まれていたら
		if (is_grab) {
			this.transform.position = player.transform.position - player.transform.forward;
		}
	}
}