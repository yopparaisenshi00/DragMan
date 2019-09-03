﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	public bool is_grab = false;
	public GameObject target;
	private Player player;
    Collider istri;

    // Start is called before the first frame update
    void Start() {
		player = target.GetComponent<Player>();
        istri = GetComponent<Collider>();
    }

	// Update is called once per frame
	void Update() {
		//掴まれていたら
		if (is_grab) {
            istri.isTrigger = false;
            this.transform.position = player.transform.position - player.transform.forward;
		}
	}
}